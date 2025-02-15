﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Shared_Catalogs.Entities.Products;

[Index("ManufactureName", Name = "UQ__Manufact__00DD03CE55DAD547", IsUnique = true)]
public partial class Manufacturer
{
    [Key]
    public int Id { get; set; }

    [StringLength(50)]
    public string ManufactureName { get; set; } = null!;

    [InverseProperty("Manufacturer")]
    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
