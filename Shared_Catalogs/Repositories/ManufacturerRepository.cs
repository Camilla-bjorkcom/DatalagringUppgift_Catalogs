
using Shared_Catalogs.Contexts;
using Shared_Catalogs.Entities.Products;
using System.Diagnostics;

namespace Shared_Catalogs.Repositories;

public class ManufacturerRepository(ProductsDbContext context) : Repo<Manufacturer, ProductsDbContext>(context)
{
    private readonly ProductsDbContext _context = context;

    public override Manufacturer Update(Manufacturer entity)
    {

        try
        {
            var entityToUpdate = _context.Manufacturers.Find(entity.Id);
            if (entityToUpdate != null)
            {
                entityToUpdate = entity;
                _context.Manufacturers.Update(entityToUpdate);
                _context.SaveChanges();

                return entityToUpdate;
            }
        }
        catch (Exception ex) { Debug.WriteLine("Error :: " + ex.Message); }

        return null!;
    }
}
