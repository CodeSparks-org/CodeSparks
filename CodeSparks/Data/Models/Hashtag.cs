using System.ComponentModel.DataAnnotations.Schema;

namespace CodeSparks.Data.Models
{
  public class Hashtag
  {
    public Guid Id {get; set;}
    public required string Name {get; set;} = string.Empty;

    [ForeignKey("Spark")]
    public required Guid SparkId {get; set;}
    public virtual Spark? Spark {get; set;}
    }
}