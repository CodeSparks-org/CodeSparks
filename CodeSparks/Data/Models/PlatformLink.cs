using System.ComponentModel.DataAnnotations.Schema;

namespace CodeSparks.Data.Models 
{
  public class PlatformLink
  {
   public Guid Id {get; set;}
   [ForeignKey("Id")]
   public Guid UserId {get; set;}
   [ForeignKey("UserId")]
   public string Name {get; set;} = string.Empty;
   public string Url {get; set;} = string.Empty;
  //  public AppUser User {get; set;}
  }
}