﻿using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Shared_Catalogs.Repositories;

public abstract class Repo<TEntity, TContext> where TEntity : class where TContext : DbContext
{
    private readonly TContext _context;

    protected Repo(TContext context)
    {
        _context = context;
    }

    public virtual TEntity Create(TEntity entity)
    {
        try
        {
            _context.Set<TEntity>().Add(entity);
            _context.SaveChanges();

            return entity;
        }
        catch (Exception ex) { Debug.WriteLine("Error :: " + ex.Message); }

        return null!;

    }

    public virtual IEnumerable<TEntity> GetAll()
    {
        try
        {
            var entities = _context.Set<TEntity>().ToList();
            if (entities.Count != 0)
            {
                return entities;
            }
        }
        catch (Exception ex) { Debug.WriteLine("Error :: " + ex.Message); }

        return null!;
    }

    public virtual TEntity GetOne(Expression<Func<TEntity, bool>> predicate)
    {
        try
        {
            //söker fram enititen beroende på vad man hämtat, t.ex. mail, id och returnerar den sen.
            var entity = _context.Set<TEntity>().FirstOrDefault(predicate);
            if (entity != null)
            {
                return entity;
            }
        }
        catch (Exception ex) { Debug.WriteLine("Error :: " + ex.Message); }

        return null!;
    }

    public virtual TEntity Update(TEntity entity)
    {
        try
        {
            var entityToUpdate = _context.Set<TEntity>().Find(entity);
            if (entityToUpdate != null)
            {
                entityToUpdate = entity;
                _context.Set<TEntity>().Update(entityToUpdate);
                _context.SaveChanges();

                return entityToUpdate;
            }
        }
        catch (Exception ex) { Debug.WriteLine("Error :: " + ex.Message); }

        return null!;
    }


    public virtual bool Delete(Expression<Func<TEntity, bool>> predicate)
    {
        try
        {
            var entity = _context.Set<TEntity>().FirstOrDefault(predicate);
            if (entity != null)
            {

                _context.Set<TEntity>().Remove(entity);
                _context.SaveChanges();

                return true;
            }
        }
        catch (Exception ex) { Debug.WriteLine("Error :: " + ex.Message); }

        return false;
    }

    public virtual bool Exists(Expression<Func<TEntity, bool>> predicate)
    {
        try
        {
            var exisiting = _context.Set<TEntity>().Any(predicate);

            return exisiting;
        }
        catch (Exception ex) { Debug.WriteLine("Error :: " + ex.Message); }

        return false;
    }


}
