using Shared_Catalogs.Models;

namespace Shared_Catalogs.Services;

public class MenuService(ProductService productService)
{

    private readonly ProductService _productService = productService;

    //public void CreateProductMenu()
    //{
    //    var product = new ProductModel();

    //    product.Manufacturer
    //        product.Description
    //        product.Category
    //        product.Title

    //}
}

