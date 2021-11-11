using System.Net;

namespace CodeChallenge.Application.Entities.Messages.Response
{
    public record Response<TResult>
    {
        public Response()
        {
            Success = false;
            HttpStatusCode = HttpStatusCode.OK;
            Error = null;
        }

        public bool Success { get; set; }

        public HttpStatusCode HttpStatusCode { get; set; }

        public TResult Result { get; set; }

        public string Error { get; set; }
    }
}
