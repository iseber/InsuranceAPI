using System;

namespace Insurance.Api.Exceptions
{
    public class ProductAPIRequestUrlException : Exception
    {
        
        public ProductAPIRequestUrlException(string message)
            : base(message)
        {

        }
    }
}