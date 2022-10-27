using Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class Discount:AuditableEntity
{
    public Guid Id { get; set; }
    public string Type { get; set; }
    public string Description { get; set; }
    public decimal Percentage { get; set; }
}