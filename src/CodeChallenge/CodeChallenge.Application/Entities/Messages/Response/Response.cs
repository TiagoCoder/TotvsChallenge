using System.Net;

namespace CodeChallenge.Application.Entities.Messages.Response
{
    // Uma resposta base que contém todos as propriedades essenciais de uma API
    public record Response<TResult>
    {
        #region Constructor
        public Response()
        {
            Success = false;
            HttpStatusCode = HttpStatusCode.OK;
            Error = null;
        }
        #endregion

        #region Properties
        public bool Success { get; set; }

        public HttpStatusCode HttpStatusCode { get; set; }

        public TResult Result { get; set; }

        public string Error { get; set; }
        #endregion
    }
}
