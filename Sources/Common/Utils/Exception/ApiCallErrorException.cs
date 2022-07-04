﻿using System.Runtime.Serialization;

namespace Utils.Exception
{
    [Serializable]
    public class ApiCallErrorException : System.Exception
    {
        public ApiCallErrorException(string message) : base(message) { }

        public ApiCallErrorException(string message, System.Exception exception) : base(message, exception) { }

    }
}