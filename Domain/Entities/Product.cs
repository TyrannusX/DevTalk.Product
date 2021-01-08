using Domain.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Product : Entity
    {
        public Guid ProductId { get; private set; }
        public string Name { get; private set; }
        public decimal Price { get; private set; }

        public Product()
        {

        }

        public Product(string name, decimal price)
        {
            Name = name;
            Price = price;
        }

        public void UpdatePrice(decimal price)
        {
            Price = price;
            AddDomainEvent(new PriceUpdatedEvent(this));
        }
    }
}
