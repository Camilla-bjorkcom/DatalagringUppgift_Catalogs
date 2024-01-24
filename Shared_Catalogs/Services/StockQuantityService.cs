using Shared_Catalogs.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared_Catalogs.Services;

public class StockQuantityService(StockQuantityRepository stockQuantityRepository, ProductRepository productRepository)
{
    private readonly StockQuantityRepository _stockQuantityRepository = stockQuantityRepository;
    private readonly ProductRepository _productRepository = productRepository;

    public void UpdateQuantity()
    { 

    }
}
