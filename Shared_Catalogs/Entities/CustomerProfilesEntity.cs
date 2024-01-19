using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shared_Catalogs.Entities;

public class CustomerProfilesEntity
{

    [Key]
    [ForeignKey(nameof(CustomersEntity))]
    public int CustomerId { get; set; }

    [Required]
    [Column(TypeName = "nvarchar(50)")]
    public string FirstName { get; set; } = null!;

    [Required]
    [Column(TypeName = "nvarchar(50)")]
    public string LastName { get; set; } = null!;

    public string? ProfileImg { get; set; }

    //Hämtar en Customer
    public virtual CustomersEntity Customer { get; set; } = null!;

}
