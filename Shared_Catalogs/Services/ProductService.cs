using Shared_Catalogs.Dtos;
using Shared_Catalogs.Entities.Products;
using Shared_Catalogs.Interfaces;
using Shared_Catalogs.Models;
using Shared_Catalogs.Repositories;
using System.Diagnostics;


namespace Shared_Catalogs.Services;

public class ProductService(ProductRepository productRepository, CategoryRepository categoryRepository, StockQuantityRepository stockQuantityRepository, ManufacturerRepository manufacturerRepository)
{
    private readonly ProductRepository _productRepository = productRepository;
    private readonly CategoryRepository _categoryRepository = categoryRepository;
    private readonly StockQuantityRepository _stockQuantityRepository = stockQuantityRepository;
    private readonly ManufacturerRepository _manufacturerRepository = manufacturerRepository;



    public bool CreateProductAsync(ICreateProductDto product)
    {
        try
        {
            if ( _productRepository.Exists(x => x.Title == product.Title))
            {
                var categoryEntity =  _categoryRepository.GetOne(x => x.CategoryName == product.Category);
                if (categoryEntity == null)
                {
                    categoryEntity = _categoryRepository.Create(new Category { CategoryName = product.Category });
                }

                var manufacturerEntity =  _manufacturerRepository.GetOne(x => x.ManufactureName == product.Manufacturer);
                if (manufacturerEntity == null)
                {
                    manufacturerEntity =  _manufacturerRepository.Create(new Manufacturer { ManufactureName = product.Manufacturer });
                }

                var stockQuantityEntity = _stockQuantityRepository.Create(new StockQuantity { Quantity = product.Quantity });

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
                    return true;
                }
            }
        }
        catch (Exception ex) { Debug.WriteLine("ERROR: " + ex.Message); }
        return false;
    }


    public IProductDto GetProductByTitle(string title)
    {
        try
        {
            var productEntity = _productRepository.GetOne(x => x.Title == title);
            if (productEntity != null)
            {
                var productDto = new ProductDto()
                {
                    Title = productEntity.Title,
                    Description = productEntity.Description,
                    Category = productEntity.Category.CategoryName,
                    Manufacturer = productEntity.ManufactureName.ManufactureName,
                };
                return productDto;
            }
        }
        catch (Exception ex) { Debug.WriteLine("ERROR: " + ex.Message); }
        return null!;
    }

    //EJ KLAR MED DENNA?
    public IEnumerable<IProductDto> GetProductsByCategoryName(string categoryName)
    {
        try
        {
            var categoryEntity = _categoryRepository.GetOne(x => x.CategoryName == categoryName);

            if (categoryEntity != null)
            {
                var result = _productRepository.GetOne(x => x.CategoryId == categoryEntity.Id);
                return (IEnumerable<IProductDto>)result;

            }
            else
                //Empty List
                return Enumerable.Empty<IProductDto>();
        }
        catch (Exception ex) { Debug.WriteLine("ERROR: " + ex.Message); }
        return null!;
    }

    public IEnumerable<IProductDto> GetAllProducts()
    {
        var products = new List<IProductDto>();
        try
        {
            var result = _productRepository.GetAll();

            foreach (var item in result)
            {
                products.Add(new ProductDto
                {
                    Title = item.Title,
                    Description = item.Description,
                    Category = item.Category.CategoryName,
                    Manufacturer = item.ManufactureName.ManufactureName,
                });
            }

        }
        catch (Exception ex) { Debug.WriteLine("ERROR : " + ex.Message); }

        return products;
    }

    public bool UpdateProduct(IUpdateProductDto productToBeUpdated)
    {
        var existingProduct = _productRepository.GetOne(x => x.ArticleNumber == productToBeUpdated.ArticleNumber);

        try
        {
            if (existingProduct != null)
            {
                existingProduct.Title = productToBeUpdated.Title;
                existingProduct.Description = productToBeUpdated.Description;
                existingProduct.Category.CategoryName = productToBeUpdated.Category;
                existingProduct.StockQuantity.Quantity = productToBeUpdated.Quantity;

                // Assuming Category and StockQuantity are navigation properties,
                // you should update their IDs or navigate through the associations properly
                // For example, update CategoryId if it's a foreign key property
                //existingProduct.CategoryId = _categoryRepository.GetCategoryIdByName(productToBeUpdated.Category);
                //existingProduct.StockQuantityId = _stockQuantityRepository.GetStockQuantityIdByQuantity(productToBeUpdated.Quantity);

                _productRepository.Update(existingProduct);
                return true;
            }
        }
        catch (Exception ex) { Debug.WriteLine("ERROR : " + ex.Message); }

        return false;
    }

    public bool DeleteProduct(IProduct product)
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