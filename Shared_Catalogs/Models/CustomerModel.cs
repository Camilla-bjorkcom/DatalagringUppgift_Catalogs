using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared_Catalogs.Interfaces;

namespace Shared_Catalogs.Models;

public class CustomerModel : ICustomerModel
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string StreetName { get; set; } = null!;
    public string City { get; set; } = null!;
    public string PostalCode { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string CustomerType { get; set; } = null!;
}
