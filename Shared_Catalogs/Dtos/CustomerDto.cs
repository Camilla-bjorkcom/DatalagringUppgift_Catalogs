﻿using Shared_Catalogs.Interfaces;

namespace Shared_Catalogs.Dtos;

public class CustomerDto : ICustomerDto
{
    public Guid CustomerId { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string CustomerType { get; set; } = null!;
}