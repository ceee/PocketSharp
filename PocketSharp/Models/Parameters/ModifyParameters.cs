using RestSharp;
using ServiceStack.Text;
using System.Collections.Generic;

namespace PocketSharp.Models
{
  /// <summary>
  /// All parameters which can be passed to modify an item
  /// </summary>
  public class ModifyParameters
  {
    /// <summary>
    /// Gets or sets the actions.
    /// </summary>
    /// <value>
    /// The actions.
    /// </value>
    public List<ActionParameter> Actions { get; set; }


    /// <summary>
    /// Converts this instance to a parameter list.
    /// </summary>
    /// <returns></returns>
    public List<Parameter> Convert()
    {
      List<object> actions = new List<object>();

      Actions.ForEach(action => actions.Add(action.Convert()));

      return new List<Parameter>()
      {
        Utilities.CreateParam("actions", JsonSerializer.SerializeToString(actions))
      };
    }
  }
}
