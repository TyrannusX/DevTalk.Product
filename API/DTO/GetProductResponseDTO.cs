using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTO
{
    public class GetProductResponseDTO
    {
        public Guid ProductId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }

        public GetProductResponseDTO(Product product)
        {
            ProductId = product.ProductId;
            Name = product.Name;
            Price = product.Price;
        }
    }
}
