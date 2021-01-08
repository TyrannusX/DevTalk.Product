using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Application.Commands.EditProduct
{
    public class EditProductCommand : IRequest<bool>
    {
        public Guid ProductId { get; set; }
        public decimal Price { get; set; }
    }
}
