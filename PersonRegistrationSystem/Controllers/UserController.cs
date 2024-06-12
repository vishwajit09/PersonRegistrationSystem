using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersonRegistrationSystem.BussinessLogic;
using PersonRegistrationSystem.Database;

namespace PersonRegistrationSystem;

[ApiController]
[Route("api/[controller]")]
public class UserController : Controller
{
    private IUserService _userService;
    private readonly ILogger<UserController> _logger;
    private readonly IJwtService _jwtService;
    private readonly IMapper _mapper;


    public UserController(IUserService userRepository,
    ILogger<UserController> logger,
    IJwtService jwtService,
    IMapper mapper)
    {
        _userService = userRepository;
        _logger = logger;
        _jwtService = jwtService;
        _mapper = mapper;
    }

    [HttpPost("Login")]
    [ProducesResponseType(typeof(ResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]

    public async Task<ActionResult<ResponseDto>> Login([FromBody] UserRequest request)
    {
        var response = await _userService.Login(request.Username, request.Password);
        if (!response.IsSuccess)
            return BadRequest(response.Message);
        else
        {

            response.Message = _jwtService.GetJwtToken(request.Username, response.User.Role, response.User.Id.ToString());
            return Ok(new { response.IsSuccess, response.Message });
        }

    }

    [HttpPost("Signup")]
    [ProducesResponseType(typeof(ResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]

    public async Task<ActionResult<ResponseDto>> Signup([FromBody] UserRequest request)
    {
        var response = await _userService.Signup(request.Username, request.Password);
        if (!response.IsSuccess)
            return BadRequest(response.Message);
        return Ok(response);
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("Delete")]
    [ProducesResponseType(typeof(ResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<ResponseDto>> Delete(string UserName)
    {
        var response = await _userService.DeleteUsername(UserName);
        if (!response.IsSuccess)
            return BadRequest(response.Message);
        return Ok(new { response.IsSuccess, response.Message }); ;
    }

    [Authorize(Roles = "Admin")]
    [HttpGet("User")]
    [ProducesResponseType(typeof(ResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<ResponseDto>> GetUser(string UserName)
    {
        var response = await _userService.GetUserByName(UserName);
        if (!response.IsSuccess)
            return BadRequest(response.Message);
        return Ok(new { response.IsSuccess, response.Message }); ;
    }

    [Authorize(Roles = "Admin")]
    [HttpGet("GetAllUser")]
    [ProducesResponseType(typeof(UserResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<ResponseDto>> GetAllUser()
    {
        var users = await _userService.GetAllAsync();

        if (users == null)
        {
            _logger.LogInformation("No user Found");
            return BadRequest("No User present");

        }

        var userRespone = _mapper.Map<List<UserResponse>>(users);
        return Ok(userRespone);



    }

}
