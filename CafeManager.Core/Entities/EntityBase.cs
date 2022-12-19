using System.ComponentModel.DataAnnotations;

namespace CafeManager.Core.Entities;

public class EntityBase
{
    [Key] 
    public int Id { get; set; }
}