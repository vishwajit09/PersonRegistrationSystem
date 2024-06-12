using Newtonsoft.Json;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersonRegistrationSystem.BussinessLogic;
using PersonRegistrationSystem.Database;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Processing;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.JsonPatch;

namespace PersonRegistrationSystem;


[Authorize]
[ApiController]
[Route("api/[controller]")]
public class PersonalInformationController : Controller
{

    private readonly IPersonInformationService _personInformationService;
    private readonly IPlaceOfResidenceService _placeOfResidenceService;

    private readonly IMapper _mapper;
    private readonly ILogger<PersonalInformationController> _logger;

    public PersonalInformationController(IPersonInformationService personInformationService,
    IMapper mapper, ILogger<PersonalInformationController> logger, IPlaceOfResidenceService placeOfResidenceService)
    {
        _personInformationService = personInformationService;
        _mapper = mapper;
        _logger = logger;
        _placeOfResidenceService = placeOfResidenceService;

    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> AddPersonInformation([FromForm] PersonInformationRequest personInformationRequest)
    {
        var userClaims = User.Claims;
        var userIdClaim = userClaims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

        if (userIdClaim != null)
        {

            var userId = userIdClaim.Value;
            try
            {
                var personInformation = _mapper.Map<PersonInformation>(personInformationRequest);



                if (personInformationRequest.ProfilePhoto is not null)
                {
                    var resizedImage = await ProcessImageAsync(personInformationRequest.ProfilePhoto);
                    personInformation.ProfilePhoto = resizedImage;
                }

                if (personInformation == null)
                {
                    _logger.LogInformation(userId, "Personinformation not found for User");
                    return NotFound($"Personinformation not found for User {userId}");
                }
                personInformation.UserId = Convert.ToInt32(userId);
                await _personInformationService.AddPersonAsync(personInformation);



                _logger.LogInformation($"Person with name {personInformation.Name} added.");

                return Ok();
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }
        else
        {
            return NotFound("User claim not found in the token.");
        }

    }

    [HttpGet("AllPersonalInformation")]
    [ProducesResponseType(typeof(PersonInformationResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]

    public async Task<IActionResult> GetAllPersonalInformationAsync()
    {
        var userClaims = User.Claims;
        var userIdClaim = userClaims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);


        if (userIdClaim != null)
        {

            var userId = userIdClaim.Value;
            try
            {
                IEnumerable<PersonInformation>? lisofPersonInformation = await _personInformationService.GetAllPersonInformationAsync(Convert.ToInt32(userId));



                if (lisofPersonInformation == null || lisofPersonInformation?.Any() == false)
                {
                    _logger.LogInformation(userId, "Personinformation not found for User");
                    return NotFound($"Personinformation not found for User {userId}");
                }
                List<PersonInformationResponse> personInformationResponse = _mapper.Map<List<PersonInformationResponse>>(lisofPersonInformation);


                return Ok(personInformationResponse);
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }
        else
        {
            return NotFound("User claim not found in the token.");
        }

    }

    [HttpGet]
    [ProducesResponseType(typeof(PersonInformationResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> GetPersonalInformationbyIdAsync([FromQuery] int personInformationId)
    {
        var userClaims = User.Claims;
        var userIdClaim = userClaims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

        if (userIdClaim != null)
        {

            var userId = userIdClaim.Value;
            try
            {

                var personInformation = await _personInformationService.GetPersonByIdAsync(personInformationId);

                if (personInformation == null)
                {
                    _logger.LogInformation($"Person with Id  {personInformationId} not found.");

                    return NotFound($"Person information with Id{personInformationId} not found .");
                }

                if (userId != personInformation.UserId.ToString())
                {
                    return BadRequest("Updating Data of Another User not allowed ");
                }


                var response = _mapper.Map<PersonInformationResponse>(personInformation);

                return Ok(response);
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }
        else
        {
            return NotFound("User claim not found in the token.");
        }


    }

    [HttpPut("personInformationId")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> PutPersonalInformationbyIdAsync(int personInformationId, [FromBody] PersonInformationUpdate personInformationRequest)
    {

        var userClaims = User.Claims;
        var userIdClaim = userClaims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

        if (userIdClaim != null)
        {

            var userId = userIdClaim.Value;
            try
            {
                if (personInformationRequest == null)
                {
                    return BadRequest();
                }
                var person = await _personInformationService.GetPersonByIdAsync(personInformationId); ;
                if (person == null)
                {
                    _logger.LogInformation($"Person with ID {personInformationId} not found.");
                    return NotFound();
                }

                if (!string.IsNullOrWhiteSpace(personInformationRequest.Name)) person.Name = personInformationRequest.Name;
                if (!string.IsNullOrWhiteSpace(personInformationRequest.LastName)) person.LastName = personInformationRequest.LastName;
                if (!string.IsNullOrWhiteSpace(personInformationRequest.PersonalCode)) person.PersonalCode = personInformationRequest.PersonalCode;
                if (!string.IsNullOrWhiteSpace(personInformationRequest.TelephoneNumber)) person.TelephoneNumber = personInformationRequest.TelephoneNumber;
                if (!string.IsNullOrWhiteSpace(personInformationRequest.Email)) person.Email = personInformationRequest.Email;
                if (!string.IsNullOrWhiteSpace(personInformationRequest.Gender)) person.Gender = personInformationRequest.Gender;
                if (personInformationRequest.Birthday != null) person.Birthday = personInformationRequest.Birthday;

                var placeOfResidence = await _placeOfResidenceService.GetPlaceOfResidenceByIdAsync(person.PlaceOfResidence.Id);
                if (placeOfResidence == null)
                {
                    _logger.LogInformation($"Place of residence with ID {person.PlaceOfResidence.Id} not found.");
                    return NotFound();
                }

                if (!string.IsNullOrWhiteSpace(personInformationRequest.PlaceOfResidence.City)) placeOfResidence.City = personInformationRequest.PlaceOfResidence.City;
                if (!string.IsNullOrWhiteSpace(personInformationRequest.PlaceOfResidence.Street)) placeOfResidence.Street = personInformationRequest.PlaceOfResidence.Street;
                if (!string.IsNullOrWhiteSpace(personInformationRequest.PlaceOfResidence.HouseNumber)) placeOfResidence.HouseNumber = personInformationRequest.PlaceOfResidence.HouseNumber;
                if (!string.IsNullOrWhiteSpace(personInformationRequest.PlaceOfResidence.ApartmentNumber)) placeOfResidence.ApartmentNumber = personInformationRequest.PlaceOfResidence.ApartmentNumber;

                await _personInformationService.UpdatePersonAsync(person.UserId, person);


                var placeofResidenceDto = _mapper.Map<PlaceOfResidenceDto>(placeOfResidence);
                await _placeOfResidenceService.UpdatePlaceOfResidenceAsync(person.PlaceOfResidence.Id, placeofResidenceDto);

                _logger.LogInformation($"Person and place of residence for ID {person.PlaceOfResidence.Id} updated.");

                return Ok();

            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }
        else
        {
            return NotFound("User claim not found in the token.");
        }

    }

    [HttpGet("GetPhotobyPersonId")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]

    public async Task<IActionResult> GetUserPhotoByUserIDAsync([FromQuery] int personInformationId)
    {
        var userClaims = User.Claims;
        var userIdClaim = userClaims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

        if (userIdClaim != null)
        {

            var userId = userIdClaim.Value;
            try
            {

                var personInformation = await _personInformationService.GetPersonByIdAsync(personInformationId);

                if (personInformation == null)
                {
                    _logger.LogInformation($"Person with Id  {personInformationId} not found.");

                    return NotFound($"Person information with Id{personInformationId} not found .");
                }

                if (Convert.ToInt32(userId) != personInformation.UserId)
                {
                    return BadRequest("Updating Data of Another User not allowed ");
                }



                if (personInformation.ProfilePhoto == null)
                {
                    return NotFound($"Photo for person information with Id - {personInformationId} not found .");
                }

                return File(personInformation.ProfilePhoto, "image/jpeg", "image.jpg");
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }
        else
        {
            return NotFound("User claim not found in the token.");
        }

    }

    private async Task<byte[]> ProcessImageAsync(IFormFile file)
    {

        using (var stream = file.OpenReadStream())
        {

            var image = Image.Load(stream);
            image.Mutate(x => x.Resize(200, 200));
            using (var outputStream = new MemoryStream())
            {
                image.Save(outputStream, new JpegEncoder());
                return outputStream.ToArray();
            }
        }
    }

}
