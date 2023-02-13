using System.ComponentModel.DataAnnotations;

namespace Arthur.App.Domain;

public class Stats
{
    [Key]
    public int Id { get; set; }
    public int Date { get; set; }
    public int ResponseTime { get; set; }
    public ApiEndpoint Type { get; set; }
}
