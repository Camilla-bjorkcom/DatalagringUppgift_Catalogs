using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Shared_Catalogs.Entities.Customers;

public class AddressesEntity
{
    [Key]
    public int Id { get; set; }

    [Required]
    [Column(TypeName = "nvarchar(50)")]
    public string StreetName { get; set; } = null!;

    [Required]
    [Column(TypeName = "char(5)")]
    public string PostalCode { get; set; } = null!;

    [Required]
    [Column(TypeName = "nvarchar(50)")]
    public string City { get; set; } = null!;

    //En address kan vara kopplad till en eller flera customers, en till många-relation
    public virtual ICollection<CustomersEntity> Customers { get; set; } = new HashSet<CustomersEntity>();

}
