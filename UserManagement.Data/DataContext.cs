using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UserManagement.Models;

namespace UserManagement.Data;

public class DataContext : DbContext, IDataContext
{
    public DataContext() => Database.EnsureCreated();

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseInMemoryDatabase("UserManagement.Data.DataContext");

    protected override void OnModelCreating(ModelBuilder model)
        => model.Entity<User>().HasData(new[]
        {
            new User { Id = 1, Forename = "Peter", Surname = "Loew", Email = "ploew@example.com", IsActive = true, DateOfBirth = DateTime.Parse("1990-01-01") },
            new User { Id = 2, Forename = "Benjamin Franklin", Surname = "Gates", Email = "bfgates@example.com", IsActive = true, DateOfBirth = DateTime.Parse("1991-07-22") },
            new User { Id = 3, Forename = "Castor", Surname = "Troy", Email = "ctroy@example.com", IsActive = false, DateOfBirth = DateTime.Parse("1992-06-12") },
            new User { Id = 4, Forename = "Memphis", Surname = "Raines", Email = "mraines@example.com", IsActive = true, DateOfBirth = DateTime.Parse("1993-11-27") },
            new User { Id = 5, Forename = "Stanley", Surname = "Goodspeed", Email = "sgodspeed@example.com", IsActive = true, DateOfBirth = DateTime.Parse("1994-08-01") },
            new User { Id = 6, Forename = "H.I.", Surname = "McDunnough", Email = "himcdunnough@example.com", IsActive = true, DateOfBirth = DateTime.Parse("1995-12-25") },
            new User { Id = 7, Forename = "Cameron", Surname = "Poe", Email = "cpoe@example.com", IsActive = false, DateOfBirth = DateTime.Parse("1970-09-18") },
            new User { Id = 8, Forename = "Edward", Surname = "Malus", Email = "emalus@example.com", IsActive = false, DateOfBirth = DateTime.Parse("1990-06-23") },
            new User { Id = 9, Forename = "Damon", Surname = "Macready", Email = "dmacready@example.com", IsActive = false, DateOfBirth = DateTime.Parse("1990-08-01") },
            new User { Id = 10, Forename = "Johnny", Surname = "Blaze", Email = "jblaze@example.com", IsActive = true, DateOfBirth = DateTime.Parse("1990-10-01") },
            new User { Id = 11, Forename = "Robin", Surname = "Feld", Email = "rfeld@example.com", IsActive = true, DateOfBirth = DateTime.Parse("1997-11-01") },
        });

    public DbSet<User>? Users { get; set; }

    public IQueryable<TEntity> GetAll<TEntity>() where TEntity : class
        => base.Set<TEntity>();

    public async ValueTask<TEntity> CreateAsync<TEntity>(TEntity entity) where TEntity : class
    {
        var result = base.Add(entity);
        await SaveChangesAsync();
        return result.Entity;
    }
    public async ValueTask<TEntity> UpdateAsync<TEntity>(TEntity entity) where TEntity : class
    {
        var updatedEntity = base.Update(entity);
        await SaveChangesAsync();
        return updatedEntity.Entity;
    }
    public async ValueTask<bool> DeleteAsync<TEntity>(TEntity entity) where TEntity : class
    {
        var result = base.Remove(entity);
        if (result.State != EntityState.Deleted) return false;
        await SaveChangesAsync();
        return result.State == EntityState.Detached;
    }
    public ValueTask<TEntity?> GetByIdAsync<TEntity>(long id) where TEntity : class
    {
        return base.FindAsync<TEntity>(id);
    }
}
