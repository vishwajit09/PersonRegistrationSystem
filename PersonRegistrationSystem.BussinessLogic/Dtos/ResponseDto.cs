using PersonRegistrationSystem.Database;

namespace PersonRegistrationSystem.BussinessLogic;

public class ResponseDto
{
    public bool IsSuccess { get; set; }
    public string Message { get; set; }
    public string Role { get; set; }
    public User User { get; set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    public ResponseDto(bool isSuccess, string message, string role)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    {
        IsSuccess = isSuccess;
        Message = message;
        Role = role;
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    public ResponseDto(bool isSuccess)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    {
        IsSuccess = isSuccess;

    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    public ResponseDto(bool isSuccess, User user)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    {
        IsSuccess = isSuccess;
        User = user;
    }

    public ResponseDto()
    {
    }
}

