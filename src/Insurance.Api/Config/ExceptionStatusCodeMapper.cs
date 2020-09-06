using System;
using System.Net;
using Insurance.Api.Exceptions;

namespace Insurance.Api.Config
{
    public static class ExceptionStatusCodeMapper
    {
        public static HttpStatusCode MapExceptions(Exception exception)
        {
            if (exception is ProductAPIException) return HttpStatusCode.InternalServerError;
            if (exception is ProductAPITimeoutException) return HttpStatusCode.RequestTimeout;
            if (exception is ProductAPIRequestUrlException) return HttpStatusCode.BadGateway;
            
            return HttpStatusCode.InternalServerError;
        }
    }
}
