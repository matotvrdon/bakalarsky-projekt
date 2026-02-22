using Web.Services.DTOs;

namespace Web.Services.Exceptions;

public class RegistrationConflictException : Exception
{
    public RegistrationSimpleResponseDto Response { get; }
    
    public RegistrationConflictException(RegistrationSimpleResponseDto response) : base(response.Message)
    {
        Response = response;
    }
}