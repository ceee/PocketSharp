using System.Xml.Linq;

namespace PocketSharp.Ports.NReadability
{
  public class AttributeTransformationInput
  {
    public string AttributeValue { get; set; }

    public XElement Element { get; set; }
  }
}
