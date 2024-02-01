using Shared_Catalogs.Interfaces;

namespace Shared_Catalogs.Dtos;

public class CategoryDto : ICategoryDto
{
    public CategoryDto(int id, string categoryName)
    {
        Id = id;
        CategoryName = categoryName;
    }

    public int Id { get; set; }
    public string CategoryName { get; set; } = null!;


}
