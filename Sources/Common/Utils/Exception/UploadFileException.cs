using System.Runtime.Serialization;

namespace Utils.Exception
{
    [Serializable]
    public class UploadFileException : System.Exception, ISerializable
    {
        public UploadFileException(string message) : base(message) { }

        public UploadFileException(string message, System.Exception exception) : base(message, exception) { }
    }
}
