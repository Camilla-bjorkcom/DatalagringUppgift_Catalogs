using Shared_Catalogs.Dtos;
using Shared_Catalogs.Entities.Customers;
using Shared_Catalogs.Entities.Products;
using Shared_Catalogs.Repositories;
using System.Diagnostics;


namespace Shared_Catalogs.Services;

public class ProductService(CategoryRepository categoryRepository, ProductRepository productRepository, ProductReviewsRepository productReviewsRepository, ManufacturerRepository manufacturerRepository, ProductReviewsService productReviewsService, CategoryService categoryService)
{


    private readonly CategoryRepository _categoryRepository = categoryRepository;
    private readonly ProductRepository _productRepository = productRepository;
    private readonly ProductReviewsRepository _productReviewsRepository = productReviewsRepository;
    private readonly ManufacturerRepository _manufacturerRepository = manufacturerRepository;
    private readonly ProductReviewsService _productReviewsService = productReviewsService;
    private readonly CategoryService _categoryService = categoryService;

    public Product CreateProduct(CreateProductDto product)
{
    try
    {
        if (!_productRepository.Exists(x => x.Title == product.Title))
        {
            var categoryEntity = _categoryService.CreateCategory(product.Category);


            var manufacturerEntity = _manufacturerRepository.GetOne(x => x.ManufactureName == product.Manufacturer);
            if (manufacturerEntity == null)
            {
                manufacturerEntity = _manufacturerRepository.Create(new Manufacturer
                {
                    ManufactureName = product.Manufacturer
                });
            }

            var productEntity = new Product()
            {
                ArticleNumber = Guid.NewGuid().ToString(),
                Title = product.Title,
                Description = product.Description,
                CategoryId = categoryEntity.Id,
                ManufacturerId = manufacturerEntity.Id,

            };

            if (productEntity != null)
            {
                var entity = _productRepository.Create(productEntity);
                if (entity != null)
                {
                    return productEntity;
                }

            }

        }
    }
    catch (Exception ex)
    {
        Exception innerException = ex;

        Console.WriteLine(innerException.Message);

    }
    return null!;
}


public Product GetProductByTitle(string title)
{
    try
    {
        var productEntity = _productRepository.GetOne(x => x.Title == title);
        if (productEntity != null)
        {
            return productEntity;
        }
    }
    catch (Exception ex) { Debug.WriteLine("ERROR: " + ex.Message); }
    return null!;
}


public IEnumerable<Product> GetAllProducts()
{
    try
    {
        var result = _productRepository.GetAll();

        if (result != null)
        {
            return result;
        }
    }
    catch (Exception ex) { Debug.WriteLine("ERROR : " + ex.Message); }

    return null!;
}

public Product UpdateProduct(Product product)
{
    var existingProduct = _productRepository.GetOne(x => x.ArticleNumber == product.ArticleNumber);

    try
    {
        if (existingProduct != null)
        {
            var updatedProdictEntity = _productRepository.Update(existingProduct);
            return updatedProdictEntity;
        }

    }
    catch (Exception ex) { Debug.WriteLine("ERROR: " + ex.Message); }
    return null!;
}


public bool DeleteProduct(Product product)
{
    try
    {
        if (_productRepository.Exists(x => x.ArticleNumber == product.ArticleNumber))
        {
                if (_productReviewsRepository.Exists(x => x.ArticleNumber == product.ArticleNumber))
                {
                    _productReviewsRepository.Delete(x => x.ArticleNumber == product.ArticleNumber);
                }
            _productRepository.Delete(x => x.ArticleNumber == product.ArticleNumber);
            return true;
        }

    }
    catch (Exception ex) { Debug.WriteLine("ERROR : " + ex.Message); }

    return false;
}
}