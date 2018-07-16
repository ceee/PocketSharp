using Newtonsoft.Json;

namespace PocketSharp.Models
{
  /// <summary>
  /// Response from the GetUser() method
  /// </summary>
  [JsonObject]
  internal class GetUserResponse
  {
    public string Access_token { get; set; }

    public string Username { get; set; }

    public GetUserAccountResponse Account { get; set; }
  }


  [JsonObject]
  internal class GetUserAccountResponse
  {
    public string Email { get; set; }

    public string First_name { get; set; }

    public string Last_name { get; set; }

    public string User_id { get; set; }

    //public bool Premium_on_trial { get; set; }

    //public bool Premium_status { get; set; }

    public GetUserAccountProfileResponse Profile { get; set; }
  }


  [JsonObject]
  internal class GetUserAccountProfileResponse
  {
    public string Avatar_url { get; set; }

    public int Follow_count { get; set; }

    public int Follower_count { get; set; }

    public string Description { get; set; }
  }
}
