namespace CafeManager.Core.Entities;

public class Waiter : EntityBase
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public Table Table { get; set; }
}