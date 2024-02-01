using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Shared_Catalogs.Interfaces;

namespace Shared_Catalogs.Entities.Customers;

public class CustomerProfilesEntity : ICustomerProfilesEntity
{

    [Key]
    [ForeignKey(nameof(CustomersEntity))]
    public Guid CustomerId { get; set; }

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
