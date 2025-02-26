using Master.Models;
using Master.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;

namespace Master.Services.Data
{
    public class ProductRepository : IProductRepository
    {
        private readonly string? _connectionString;

        public ProductRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<List<ProductObject>> GetAllProductsAsync(ProductObject input)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                // Replace with your actual SQL query and parameters
                string sql = "SELECT prod_id, prod_nam FROM prod_dtls;";
                return (await connection.QueryAsync<ProductObject>(sql)).AsList();
            }
        }

        public async Task<OperationStatus> AddProductAsync(ProductObject input)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var output = new OperationStatus();

                string sql = "INSERT INTO Products (prod_id, prod_nam, act_inact_ind, sort_ordr, category) VALUES (@prod_id, @prod_nam, @act_inact_ind, @sort_ordr, @category)";
                int rowsAffected = await connection.ExecuteAsync(sql, input);

                output.IsSuccess = true;
                output.Message = "Data saved successfully";

                return output;
            }
        }
    }
}