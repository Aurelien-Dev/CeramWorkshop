namespace Utils.Exception
{
    public class ApiCallErrorEsception : System.Exception
    {
        public ApiCallErrorEsception(string message) : base(message) { }
        public ApiCallErrorEsception(string message, System.Exception exception) : base(message, exception) { }

    }
}