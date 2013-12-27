using System;

namespace PocketSharp
{
  /// <summary>
  /// custom Pocket API Exceptions
  /// </summary>
  public class PocketException : Exception
  {
    /// <summary>
    /// Gets or sets the pocket error code.
    /// </summary>
    /// <value>
    /// The pocket error code.
    /// </value>
    public int? PocketErrorCode { get; set; }

    /// <summary>
    /// Gets or sets the pocket error.
    /// </summary>
    /// <value>
    /// The pocket error.
    /// </value>
    public string PocketError { get; set; }


    /// <summary>
    /// Initializes a new instance of the <see cref="PocketException"/> class.
    /// </summary>
    public PocketException()
      : base() { }


    /// <summary>
    /// Initializes a new instance of the <see cref="PocketException"/> class.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    public PocketException(string message)
      : base(message) { }


    /// <summary>
    /// Initializes a new instance of the <see cref="PocketException"/> class.
    /// </summary>
    /// <param name="message">The error message that explains the reason for the exception.</param>
    /// <param name="innerException">The exception that is the cause of the current exception, or a null reference (Nothing in Visual Basic) if no inner exception is specified.</param>
    public PocketException(string message, Exception innerException)
      : base(message, innerException) { }
  }
}
