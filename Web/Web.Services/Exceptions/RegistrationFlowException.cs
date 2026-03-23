using System.Net;
using Web.Services.DTOs;

namespace Web.Services.Exceptions;

public class RegistrationFlowException : Exception
{
    public HttpStatusCode StatusCode { get; }
    public RegistrationErrorResponseDto Response { get; }

    public RegistrationFlowException(HttpStatusCode statusCode, RegistrationErrorResponseDto response)
        : base(response.Message)
    {
        StatusCode = statusCode;
        Response = response;
    }
}
