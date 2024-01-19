using System.ComponentModel.DataAnnotations;

namespace Shared_Catalogs.Entities;

public class CustomerTypeEntity
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(20)]
    public string CustomerType { get; set; } = null!;

    //En roll kan vara kopplad till en eller flera customers
    public virtual ICollection<CustomersEntity> Customers { get; set; } = new HashSet<CustomersEntity>();

}
