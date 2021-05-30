using Bassi.CrudBassi.Repository.VOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bassi.CrudBassi.Repository
{
    public class DbContextEf : DbContext
    {
        public DbContextEf(DbContextOptions<DbContextEf> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            foreach (var relationship in builder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            builder.Entity<ReceiptReturnVO>().Property<int>("IdStatus");
            builder.Entity<ReceiptReturnVO>().HasQueryFilter(m => EF.Property<int>(m, "IdStatus") != (int)EntityStatus.Deleted);
 
            builder.Entity<ReceiptDetailReturnVO>().Property<int>("IdStatus");
            builder.Entity<ReceiptDetailReturnVO>().HasQueryFilter(m => EF.Property<int>(m, "IdStatus") != (int)EntityStatus.Deleted);
 
            builder.Entity<ClientReturnVO>().Property<int>("IdStatus");
            builder.Entity<ClientReturnVO>().HasQueryFilter(m => EF.Property<int>(m, "IdStatus") != (int)EntityStatus.Deleted);
 
            builder.Entity<ProductReturnVO>().Property<int>("IdStatus");
            builder.Entity<ProductReturnVO>().HasQueryFilter(m => EF.Property<int>(m, "IdStatus") != (int)EntityStatus.Deleted);
 
 
        }

        public override int SaveChanges()
        {
            Guid _userGuid = new Guid();

            var entries = ChangeTracker
        .Entries()
        .Where(e => e.Entity is BaseEntityVO && (
                e.State == EntityState.Added
                || e.State == EntityState.Modified
                || e.State == EntityState.Deleted));

            foreach (var entityEntry in entries)
            {


                DateTime now = DateTime.Now;

                ((BaseEntityVO)entityEntry.Entity).ModifiedDate = now;

                if (entityEntry.State == EntityState.Modified)
                {
                    
                    var originalValues = entityEntry.GetDatabaseValues();

                    ((BaseEntityVO)entityEntry.Entity).ITime = originalValues.GetValue<DateTime>("ITime");
                    ((BaseEntityVO)entityEntry.Entity).IdStatus = (int)EntityStatus.Modified;
                }

                if (entityEntry.State == EntityState.Added)
                {
                    ((BaseEntityVO)entityEntry.Entity).ITime = now;
                    ((BaseEntityVO)entityEntry.Entity).IdStatus = (int)EntityStatus.New;
                }

                if (entityEntry.State == EntityState.Deleted)
                {
                    entityEntry.State = EntityState.Modified;
                    ((BaseEntityVO)entityEntry.Entity).IdStatus = (int)EntityStatus.Deleted;
                }
            }

           
            return base.SaveChanges();
        }

        public Task<int> SaveChangesAsync()
        {
            Guid _userGuid = new Guid();

            var entries = ChangeTracker
        .Entries()
        .Where(e => e.Entity is BaseEntityVO && (
                e.State == EntityState.Added
                || e.State == EntityState.Modified
                || e.State == EntityState.Deleted));

            foreach (var entityEntry in entries)
            {


                DateTime now = DateTime.Now;

                ((BaseEntityVO)entityEntry.Entity).ModifiedDate = now;

                if (entityEntry.State == EntityState.Modified)
                {
                   
                    var originalValues = entityEntry.GetDatabaseValues();

                    ((BaseEntityVO)entityEntry.Entity).ITime = originalValues.GetValue<DateTime>("ITime");
                    ((BaseEntityVO)entityEntry.Entity).IdStatus = (int)EntityStatus.Modified;
                }

                if (entityEntry.State == EntityState.Added)
                {
                    ((BaseEntityVO)entityEntry.Entity).ITime = now;
                    ((BaseEntityVO)entityEntry.Entity).IdStatus = (int)EntityStatus.New;
                }

                if (entityEntry.State == EntityState.Deleted)
                {
                    entityEntry.State = EntityState.Modified;
                    ((BaseEntityVO)entityEntry.Entity).IdStatus = (int)EntityStatus.Deleted;
                }
            }

            return base.SaveChangesAsync();
        }

        public DbSet<IdsVO> IdsVO { get; set; }
        public DbSet<ReceiptReturnVO> ReceiptReturnVO { get; set; }
        public DbSet<ReceiptDetailReturnVO> ReceiptDetailReturnVO { get; set; }
        public DbSet<ClientReturnVO> ClientReturnVO { get; set; }
        public DbSet<ProductReturnVO> ProductReturnVO { get; set; }
     
    }
}

