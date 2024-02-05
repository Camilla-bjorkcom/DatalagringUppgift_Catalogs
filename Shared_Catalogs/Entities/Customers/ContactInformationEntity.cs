using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Shared_Catalogs.Interfaces;

namespace Shared_Catalogs.Entities.Customers;

public class ContactInformationEntity 
{

    [Key]
    public int Id { get; set; }

    [Required]
    [Column(TypeName = "varchar(100)")]
    public string Email { get; set; } = null!;

    public int CustomerId { get; set; }

    //Hämtar en kontakt
    public CustomersEntity Customer { get; set; } = null!;

    //En kontaktinformation kan vara kopplad till en eller flera telefonnummer
    public virtual ICollection<CustomerPhoneNumbersEntity> PhoneNumbers { get; set; } = new HashSet<CustomerPhoneNumbersEntity>();
}
