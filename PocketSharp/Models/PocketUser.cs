using Newtonsoft.Json;
using System;

namespace PocketSharp.Models
{
  /// <summary>
  /// Access Code
  /// </summary>
  [JsonObject]
  public class PocketUser
  {
    /// <summary>
    /// Pocket user id.
    /// </summary>
    public string Id { get; set; }

    /// <summary>
    /// The access code.
    /// </summary>
    public string Code { get; set; }

    /// <summary>
    /// Pocket username.
    /// </summary>
    public string Username { get; set; }

    /// <summary>
    /// Email address.
    /// </summary>
    public string Email { get; set; }

    /// <summary>
    /// First name.
    /// </summary>
    public string FirstName { get; set; }

    /// <summary>
    /// Last name.
    /// </summary>
    public string LastName { get; set; }

    /// <summary>
    /// Profile avatar.
    /// </summary>
    public Uri Avatar { get; set; }

    /// <summary>
    /// Is default avatar.
    /// </summary>
    public bool IsDefaultAvatar { get; set; } = true;

    /// <summary>
    /// Follower count.
    /// </summary>
    public int Followers { get; set; }

    /// <summary>
    /// Follow count.
    /// </summary>
    public int Follows { get; set; }

    /// <summary>
    /// Profile text.
    /// </summary>
    public string Description { get; set; }
  }
}
