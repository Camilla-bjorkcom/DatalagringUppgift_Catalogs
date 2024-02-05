using Shared_Catalogs.Dtos;
using Shared_Catalogs.Entities.Customers;
using Shared_Catalogs.Entities.Products;
using Shared_Catalogs.Interfaces;
using Shared_Catalogs.Repositories;
using System.Diagnostics;


namespace Shared_Catalogs.Services;

public class ProductService(ProductRepository productRepository, CategoryRepository categoryRepository, StockQuantityRepository stockQuantityRepository, ManufacturerRepository manufacturerRepository, CategoryService categoryService)
{
    private readonly ProductRepository _productRepository = productRepository;
    private readonly CategoryRepository _categoryRepository = categoryRepository;
    private readonly StockQuantityRepository _stockQuantityRepository = stockQuantityRepository;
    private readonly ManufacturerRepository _manufacturerRepository = manufacturerRepository;
    private readonly CategoryService _categoryService = categoryService;



    public Product CreateProduct(CreateProductDto product)
    {
        try
        {
            if (!_productRepository.Exists(x => x.Title == product.Title))
            {
                var categoryEntity = _categoryService.CreateCategory(product.Category);
                //var categoryEntity =  _categoryRepository.GetOne(x => x.CategoryName == product.Category);
                //if (categoryEntity == null)
                //{
                //    categoryEntity = _categoryRepository.Create(new Category 
                //    { 
                //        CategoryName = product.Category 
                //    });
                //}

                var manufacturerEntity =  _manufacturerRepository.GetOne(x => x.ManufactureName == product.Manufacturer);
                if (manufacturerEntity == null)
                {
                    manufacturerEntity =  _manufacturerRepository.Create(new Manufacturer 
                    {
                        ManufactureName = product.Manufacturer 
                    });
                }

                var stockQuantityEntity = _stockQuantityRepository.Create(new StockQuantity 
                {
                    Quantity = product.Quantity 
                });

                var productEntity = new Product()
                {
                    ArticleNumber = Guid.NewGuid().ToString(),
                    Title = product.Title,
                    Description = product.Description,
                    CategoryId = categoryEntity.Id,
                    ManufacturerId = manufacturerEntity.Id,
                    StockQuantityId = stockQuantityEntity.Id,
                };

                var entity =  _productRepository.Create(productEntity);
                if (entity != null)
                {
                    return productEntity;
                }
            }
        }
        catch (Exception ex) { Debug.WriteLine("ERROR: " + ex.Message); }
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

    //EJ KLAR MED DENNA?
    //public IEnumerable<Product> GetProductsByCategoryName(string categoryName)
    //{
    //    try
    //    {
    //        var categoryEntity = _categoryRepository.GetOne(x => x.CategoryName == categoryName);

    //        if (categoryEntity != null)
    //        {
    //            var result = _productRepository.GetOne(x => x.CategoryId == categoryEntity.Id);
    //            return result;

    //        }
    //        else
    //            //Empty List
    //            return Enumerable.Empty<IProductDto>();
    //    }
    //    catch (Exception ex) { Debug.WriteLine("ERROR: " + ex.Message); }
    //    return null!;
    //}

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
                _productRepository.Delete(x => x.ArticleNumber == product.ArticleNumber);
                return true;
            }

        }
        catch (Exception ex) { Debug.WriteLine("ERROR : " + ex.Message); }

        return false;
    }
}