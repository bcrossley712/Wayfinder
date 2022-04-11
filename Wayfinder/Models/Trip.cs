namespace Wayfinder.Models
{
  public class Trip : Virtual<int>
  {
    public string Title { get; set; }
    public string CreatorId { get; set; }
  }
}