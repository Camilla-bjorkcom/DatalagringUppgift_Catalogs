using Catalog_App.Contexts;
using Catalog_App.Entities;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Shared_Catalogs.Repositories;

public class ProductRepository(ProductsDbContext context) : Repo<Product, ProductsDbContext>(context)
{
    private readonly ProductsDbContext _context = context;

    //public override IEnumerable<Product> GetAll()
    //{
    //    try
    //    {
    //        return _context.Products.Include(x => x.Category).ThenInclude()
    //    }
    //    catch (Exception ex) { Debug.WriteLine(ex); }
    //}

    //public override Product GetOne(Expression<Func<Product, bool>> predicate)
    //{
    //   try
    //    {

    //    }
    //    catch (Exception ex) { Debug.WriteLine(ex); }
    //}

    public IEnumerable<Product> GetProductsByCategoryId(int categoryId)
    {
        return _context.Products.Where(x => x.CategoryId == categoryId).ToList();
    }

}
