using System;

namespace Insurance.Api.Exceptions
{
    public class ProductAPITimeoutException : Exception
    {
        public ProductAPITimeoutException(string message)
            : base(message)
        {

        }
    }
}