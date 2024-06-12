using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersonRegistrationSystem.BussinessLogic;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using PersonRegistrationSystem.Database;
using System.Security.Claims;
namespace PersonRegistrationSystem;

[ApiController]

[Route("api/[controller]")]
public class PlaceOfResidenceController : ControllerBase
{
    private readonly IPlaceOfResidenceService _placeOfResidenceService;
    private readonly IMapper _mapper;

    public PlaceOfResidenceController(IPlaceOfResidenceService placeOfResidenceService, IMapper mapper)
    {
        _placeOfResidenceService = placeOfResidenceService;
        _mapper = mapper;
    }
    [Authorize(Roles = "Admin")]
    [HttpGet("{id}")]
    public async Task<ActionResult<PlaceOfResidenceDto>> Get(int id)
    {

        var placeOfResidenceDto = await _placeOfResidenceService.GetPlaceOfResidenceByIdAsync(id);
        if (placeOfResidenceDto == null)
            return NotFound();

        return Ok(placeOfResidenceDto);


    }


    [HttpPut]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<PlaceOfResidenceDto>> Update([FromQuery] int placeOfResidenceId, PlaceOfResidenceDto placeOfResidenceDto)
    {

        var placeofResidence = _mapper.Map<PlaceOfResidence>(placeOfResidenceDto);
        await _placeOfResidenceService.UpdatePlaceOfResidenceAsync(placeOfResidenceId, placeOfResidenceDto);
        return Ok("Update Sucessfully");
    }
}

