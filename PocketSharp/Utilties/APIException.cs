using System;
using System.Runtime.Serialization;

namespace PocketSharp.Utilities
{
  [Serializable]
  public class APIException : Exception
  {
    /// <summary>
    /// necessary constructors for the exception 
    /// including inner exceptions and serialization
    /// </summary>
    public APIException()
      : base() { }

    public APIException(string message)
      : base(message) { }

    public APIException(string message, Exception innerException)
      : base(message, innerException) { }

    protected APIException(SerializationInfo info, StreamingContext context)
      : base(info, context) { }
  }
}
