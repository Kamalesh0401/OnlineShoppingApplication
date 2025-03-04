using Master.Models;
using Common.Models;
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

        public async Task<List<ProductObject>> GetAllProductsAsync(ProductInputObject input)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                string sql = @"SELECT prod.prod_id,prod_name,brand,prod_desc,prod.ctgry_id,ctgry.ctgry_name,prod.mod_by_usr_cd,prod.mod_dttm,
                                price.price, price.currency,invntry.stock_aval FROM prod_dtls prod
                                INNER JOIN prod_ctgry_mast ctgry ON ctgry.ctgry_id = prod.ctgry_id
                                INNER JOIN prod_price_dtls price ON price.prod_id = prod.prod_id
                                INNER JOIN invntry_dtls invntry ON invntry.prod_id = prod.prod_id;";
                return (await connection.QueryAsync<ProductObject>(sql)).AsList();
            }
        }

        public async Task<List<ProductObject>> GetProductsByIdAsync(ProductInputObject input)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                // Replace with your actual SQL query and parameters
                string sql = @"SELECT prod.prod_id,prod_name,brand,prod_desc,prod.ctgry_id,ctgry.ctgry_name,prod.mod_by_usr_cd,prod.mod_dttm,
                                price.price, price.currency,invntry.stock_aval FROM prod_dtls prod
                                INNER JOIN prod_ctgry_mast ctgry ON ctgry.ctgry_id = prod.ctgry_id
                                INNER JOIN prod_price_dtls price ON price.prod_id = prod.prod_id
                                INNER JOIN invntry_dtls invntry ON invntry.prod_id = prod.prod_id
                                WHERE prod.prod_id = @prod_id;
";
                return (await connection.QueryAsync<ProductObject>(sql, new { input.prod_id })).AsList();
            }
        }

        public async Task<List<ProductObject>> GetProductsByCategoryIdAsync(ProductInputObject input)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                // Replace with your actual SQL query and parameters
                string sql = @"SELECT prod.prod_id,prod_name,brand,prod_desc,prod.ctgry_id,ctgry.ctgry_name,prod.mod_by_usr_cd,prod.mod_dttm,
                                price.price, price.currency,invntry.stock_aval FROM prod_dtls prod
                                INNER JOIN prod_ctgry_mast ctgry ON ctgry.ctgry_id = prod.ctgry_id
                                INNER JOIN prod_price_dtls price ON price.prod_id = prod.prod_id
                                INNER JOIN invntry_dtls invntry ON invntry.prod_id = prod.prod_id
                                WHERE prod.ctgry_id = @ctgry_id;
";
                return (await connection.QueryAsync<ProductObject>(sql, new { input.ctgry_id })).AsList();
            }
        }

        public async Task<OperationStatus> AddProductAsync(ProductObject input)
        {
            var output = new OperationStatus();
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    string sql = @"INSERT INTO prod_dtls (prod_id, prod_name, brand, prod_desc, ctgry_id,mod_by_usr_cd,mod_dttm) 
                               VALUES (@prod_id, prod_name, brand, ctgry_id, mod_by_usr_cd,GETDATE);";
                    int rowsAffected = await connection.ExecuteAsync(sql, input);

                    output.IsSuccess = true;
                    output.Message = "Data saved successfully";

                }
            }
            catch (Exception ex)
            {
                output.IsSuccess = false;
                output.Message = "Something went wrong";

            }
            return output;
        }
        public async Task<OperationStatus> UpdateProductByIdAsync(ProductObject input)
        {
            var output = new OperationStatus();
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    string sql = @"UPDATE prod_dtls SET prod_name=@prod_name,brand=@brand,prod_desc=@prod_desc,
                               ctgry_id=@ctgry_id,mod_by_usr_cd=@mod_by_usr_cd,mod_dttm=GETDATE WHERE prod_id = @prod_id;";
                    int rowsAffected = await connection.ExecuteAsync(sql, input);

                    output.IsSuccess = true;
                    output.Message = "Data updated successfully";
                }
            }
            catch (Exception ex)
            {
                output.IsSuccess = false;
                output.Message = "Something went wrong";

            }
            return output;
        }

        public async Task<OperationStatus> DeleteProductByIdAsync(string prod_id)
        {
            var output = new OperationStatus();
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    string sql = @"DELETE FROM prod_dtls WHERE prod_id = @prod_id;";
                    int rowsAffected = await connection.ExecuteAsync(sql, prod_id);

                    output.IsSuccess = true;
                    output.Message = "Data saved successfully";

                    return output;
                }
            }
            catch (Exception ex)
            {
                output.IsSuccess = false;
                output.Message = "Something went wrong";

            }
            return output;



        }

    }
}