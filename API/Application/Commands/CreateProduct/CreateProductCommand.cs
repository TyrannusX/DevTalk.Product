using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Application.Commands.CreateProduct
{
    public class CreateProductCommand : IRequest<bool>
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}
