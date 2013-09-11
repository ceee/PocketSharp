
namespace PocketSharp.Models
{
  /// <summary>
  /// Parameter
  /// </summary>
  public class Parameter
  {
    /// <summary>
    /// Name of the parameter
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Value of the parameter
    /// </summary>
    public object Value { get; set; }

    /// <summary>
    /// Return a human-readable representation of this parameter
    /// </summary>
    /// <returns>String</returns>
    public override string ToString()
    {
      return string.Format("{0}={1}", Name, Value);
    }
  }
}
