using Master.Models;
using Master.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;

namespace Master.Services.Data
{
    public class InventoryRepository : IInventoryRepository
    {
        private readonly string? _connectionString;

        public InventoryRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<List<InventoryObject>> GetAvailabilityByProductIdAsync(InventoryInputObject input)
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
                return (await connection.QueryAsync<InventoryObject>(sql, new { input.prod_id })).AsList();
            }
        }
        public async Task<OperationStatus> UpdateAvailabilityByProductIdAsync(InventoryObject input)
        {
            var output = new OperationStatus();
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    string sql = @"UPDATE invntry_dtls SET stock_aval=@stock_aval,mod_by_usr_cd=@mod_by_usr_cd,mod_dttm=GETDATE WHERE invnry_id=@invnry_id AND prod_id = @prod_id;";
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
    }
}