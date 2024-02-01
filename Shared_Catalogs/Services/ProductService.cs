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


   
    public async Task<bool> CreateProductAsync(ICreateProductDto product)
    {
        try
        {
            if (!await _productRepository.ExistsAsync(x => x.Title == product.Title))
            {
                var categoryEntity = await _categoryRepository.GetOneAsync(x => x.CategoryName == product.Category);
                if (categoryEntity == null)
                {
                    categoryEntity = await _categoryRepository.CreateAsync(new Category { CategoryName = product.Category });
                }

                var manufacturerEntity = await _manufacturerRepository.GetOneAsync(x => x.ManufactureName == product.Manufacturer);
                if (manufacturerEntity == null)
                {
                    manufacturerEntity = await _manufacturerRepository.CreateAsync(new Manufacturer { ManufactureName = product.Manufacturer });
                }

                var stockQuantityEntity = await _stockQuantityRepository.CreateAsync(new StockQuantity { Quantity = product.Quantity });

                var productEntity = new Product()
                {
                    ArticleNumber = Guid.NewGuid().ToString(),
                    Title = product.Title,
                    Description = product.Description,
                    CategoryId = categoryEntity.Id,
                    ManufacturerId = manufacturerEntity.Id,
                    StockQuantityId = stockQuantityEntity.Id,
                };

                var entity = await _productRepository.CreateAsync(productEntity);
                if (entity != null)
                {
                    return true;
                }
            }
        }
        catch (Exception ex) { Debug.WriteLine("ERROR: " + ex.Message); }
        return false;
    }


    public async Task<IProductDto> GetProductByTitleAsync(string title)
    {
        try
        {
            var productEntity = await _productRepository.GetOneAsync(x => x.Title == title);
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
    public async Task<IEnumerable<IProductDto>> GetProductsByCategoryNameAsync(string categoryName)
    {
        try
        {
            var categoryEntity = await _categoryRepository.GetOneAsync(x => x.CategoryName == categoryName);

            if (categoryEntity != null)
            {
                var result = await _productRepository.GetOneAsync(x => x.CategoryId == categoryEntity.Id);
                return (IEnumerable<IProductDto>)result;

            }
            else
                //Empty List
                return Enumerable.Empty<IProductDto>();
        }
        catch (Exception ex) { Debug.WriteLine("ERROR: " + ex.Message); }
        return null!;
    }

    public async Task<IEnumerable<IProductDto>> GetAllProductsAsync()
    {
        var products = new List<IProductDto>();
        try
        {
            var result = _productRepository.GetAllAsync();
           
            foreach (var item in await result)
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

    public async Task<bool> UpdateProductAsync(IUpdateProductDto productToBeUpdated)
    {
        var existingProduct = await _productRepository.GetOneAsync(x => x.ArticleNumber == productToBeUpdated.ArticleNumber);

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

                await _productRepository.UpdateAsync(existingProduct);
                return true;
            }
        }
        catch (Exception ex) { Debug.WriteLine("ERROR : " + ex.Message); }

        return false;
    }

    public async Task <bool> DeleteProductAsync(IProduct product)
    {
        try
        {
            if (await _productRepository.ExistsAsync(x => x.ArticleNumber == product.ArticleNumber))
            {
                 await _productRepository.DeleteAsync(x => x.ArticleNumber == product.ArticleNumber);
                 return true;
            }
           
        }
        catch (Exception ex) { Debug.WriteLine("ERROR : " + ex.Message); }

        return false;
    }


}