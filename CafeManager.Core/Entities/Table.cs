namespace CafeManager.Core.Entities;

public class Table : EntityBase
{
    public int PlacesNumber { get; set; }
    public Waiter Waiter { get; set; }
}