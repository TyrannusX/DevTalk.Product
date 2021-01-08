using API.Exceptions;
using Domain.Contracts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace API.Application.Commands.EditProduct
{
    public class EditProductCommandHandler : IRequestHandler<EditProductCommand, bool>
    {
        private readonly IDbContext _dbContext;

        public EditProductCommandHandler(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> Handle(EditProductCommand request, CancellationToken cancellationToken)
        {
            if(request.Price <= 0)
            {
                throw new BadRequestException("Price must be greater than 0");
            }

            var product = _dbContext.Products.SingleOrDefault(x => x.ProductId == request.ProductId);
            if(product == null)
            {
                throw new NotFoundException($"Product with id {request.ProductId} not found");
            }

            product.UpdatePrice(request.Price);
            await _dbContext.SaveDomainAsync().ConfigureAwait(false);

            return true;
        }
    }
}
