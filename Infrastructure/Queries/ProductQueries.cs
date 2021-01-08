using Dapper;
using Domain.Contracts;
using Domain.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Queries
{
    public class ProductQueries : IQueries<Product>
    {
        private readonly IConfiguration _configuration;

        public ProductQueries(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<IReadOnlyCollection<Product>> GetAllAsync()
        {
            var sql = "SELECT * FROM Products";

            using (var connection = new SqlConnection(_configuration["ProductsConnectionString"]))
            {
                var result = await connection.QueryAsync<Product>(sql).ConfigureAwait(false);
                return result.ToList();
            }
        }

        public async Task<Product> GetByIdAsync(Guid id)
        {
            var sql = "SELECT * FROM Products WHERE ProductId = @Id";

            using (var connection = new SqlConnection(_configuration["ProductsConnectionString"]))
            {
                return await connection.QuerySingleOrDefaultAsync<Product>(sql, new { Id = id }).ConfigureAwait(false);
            }
        }
    }
}
