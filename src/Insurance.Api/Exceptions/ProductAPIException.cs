using System;

namespace Insurance.Api.Exceptions
{
    public class ProductAPIException : Exception
    {
        public ProductAPIException(string message)
            : base(message)
        {

        }
    }
}
