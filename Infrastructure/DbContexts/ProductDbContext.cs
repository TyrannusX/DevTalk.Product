using Domain.Contracts;
using Domain.Entities;
using Infrastructure.EntityConfigurations.Product;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.DbContexts
{
    public class ProductDbContext : DbContext, IDbContext
    {
        private readonly IMediator _mediator;

        public DbSet<Product> Products { get; set; }

        public ProductDbContext(DbContextOptions<ProductDbContext> dbContextOptions) : base(dbContextOptions) { }
        public ProductDbContext(DbContextOptions<ProductDbContext> dbContextOptions, IMediator mediator) : base(dbContextOptions)
        {
            _mediator = mediator;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProductEntityConfiguration());
        }

        public async Task<int> SaveDomainAsync(CancellationToken cancellationToken = default)
        {
            //Publish customer domain events and then commit
            var domainEntities = ChangeTracker.Entries<Entity>().Where(x => x.Entity.GetDomainEvents() != null).ToList();

            foreach (var entry in domainEntities)
            {
                foreach (var domainEvent in entry.Entity.GetDomainEvents())
                {
                    await _mediator.Publish(domainEvent);
                }
            }

            return await SaveChangesAsync();
        }
    }
}
