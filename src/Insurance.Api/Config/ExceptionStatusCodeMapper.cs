using System;
using System.Net;

namespace Insurance.Api.Config
{
    public static class ExceptionStatusCodeMapper
    {
        public static HttpStatusCode MapExceptions(Exception exception)
        {
   //          if (exception is FacetNotFoundException) return HttpStatusCode.NotFound;
   //          if (exception is ResultModificationNotFoundException) return HttpStatusCode.NotFound;
   //          if (exception is CategoryNotFoundException) return HttpStatusCode.NotFound;
   //          if (exception is FacetIsAlreadyInRequestedOrderException) return HttpStatusCode.NotModified;
   //          if (exception is AttributeNotFoundException) return HttpStatusCode.NotFound;
   //          if (exception is AttributeKeyNotDefined) return HttpStatusCode.BadRequest;
   //          if (exception is ConditionTypeNotSupportedException) return HttpStatusCode.NotAcceptable;
   //          if (exception is InvalidFacetOrderException) return HttpStatusCode.BadRequest;
   //          if (exception is FacetIdIsNullOrEmptyException) return HttpStatusCode.BadRequest;
   //          if (exception is ResultModificationIdIsNullOrEmptyException) return HttpStatusCode.BadRequest;
   //          if (exception is InvalidFacetForUserLocationException) return HttpStatusCode.BadRequest;
   //          if (exception is InvalidTopSellingRuleOrderException) return HttpStatusCode.BadRequest;
   //          if (exception is TopSellingRuleNotFoundException) return HttpStatusCode.NotFound;
   //          if (exception is MinimumSearchTermLengthException) return HttpStatusCode.BadRequest;
			// if (exception is SynonymNotFoundException) return HttpStatusCode.NotFound;
   //          if (exception is RedirectionRuleNotFoundException) return HttpStatusCode.NotFound;
   //          if (exception is InvalidFilterFormatException) return HttpStatusCode.BadRequest;
   //          if (exception is InvalidResultModificationValidThruException) return HttpStatusCode.BadRequest;
   //          if (exception is ResultModificationCannotBeLongerThanOneYearException) return HttpStatusCode.BadRequest;
   //          if (exception is ResultModificationIsAlreadyHaveTheSameValidThruDateException) return HttpStatusCode.BadRequest;

            return HttpStatusCode.InternalServerError;
        }
    }
}
