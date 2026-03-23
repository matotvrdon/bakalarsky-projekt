using System.Net;
using Web.Services.DTOs;

namespace Web.Services.Exceptions;

public class SubmissionFlowException : Exception
{
    public HttpStatusCode StatusCode { get; }
    public SubmissionErrorResponseDto Response { get; }

    public SubmissionFlowException(HttpStatusCode statusCode, SubmissionErrorResponseDto response)
        : base(response.Message)
    {
        StatusCode = statusCode;
        Response = response;
    }
}
