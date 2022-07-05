using System.Runtime.Serialization;

namespace Utils.Exception
{
    [Serializable]
    public class ClientException : System.Exception
    {
        public ClientException(string message) : base(message) { }

        public ClientException(string message, System.Exception exception) : base(message, exception) { }
        protected ClientException(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
        {
            throw new NotImplementedException();
        }
    }
}
