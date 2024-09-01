using System.ComponentModel.DataAnnotations.Schema;

namespace CodeSparks.Data.Models 
{
  public class PlatformLink
  {
   public Guid Id {get; set;}
   public string Name {get; set;} = string.Empty;
   public string? Url {get; set;} = string.Empty;

   [ForeignKey("User")]
   public Guid UserId {get; set;}
   public AppUser? User {get; set;}
  }
}