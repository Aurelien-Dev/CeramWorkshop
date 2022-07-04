using System.Runtime.Serialization;

namespace Utils.Exception
{
    [Serializable]
    public class ParameterPageNullException : System.Exception
    {
        public ParameterPageNullException(params string[] parametres) : base(string.Format("Invalid parameter : {0}", string.Join(',', parametres))) { }

        public ParameterPageNullException(string message, System.Exception exception) : base(message, exception) { }
    }
}
