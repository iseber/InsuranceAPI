using System;
using System.Net;

namespace Insurance.Api.Config
{
    public static class ExceptionStatusCodeMapper
    {
        public static HttpStatusCode MapExceptions(Exception exception)
        {
            return HttpStatusCode.InternalServerError;
        }
    }
}
