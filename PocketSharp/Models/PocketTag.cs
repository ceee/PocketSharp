using System.Runtime.Serialization;

namespace PocketSharp.Models
{
  [DataContract]
  public class PocketTag
  {
    [DataMember(Name = "tag")]
    public string Name { get; set; }
  }
}
