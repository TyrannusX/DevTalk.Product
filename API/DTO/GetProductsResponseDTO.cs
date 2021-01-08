using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTO
{
    public class GetProductsResponseDTO
    {
        public List<GetProductResponseDTO> Products { get; set; }

        public GetProductsResponseDTO(IReadOnlyCollection<Product> products)
        {
            Products = new List<GetProductResponseDTO>();
            products.ToList().ForEach(x =>
            {
                Products.Add(new GetProductResponseDTO(x));
            });
        }
    }
}
