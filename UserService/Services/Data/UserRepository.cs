using Master.Models;
using Master.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;

namespace Master.Services.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly string? _connectionString;

        public UserRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<OperationStatus> RegisterUserAsync(UserObject input)
        {
            var output = new OperationStatus();

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    // Replace with your actual SQL query and parameters
                    string sql = @"INSERT INTO user_dtls(usr_id,usr_name,email,pass_word,usr_role,created_at)
                               VALUES(@usr_id,@usr_name,@email,@pass_word,@usr_role,@created_at)";
                    int rowsAffected = await connection.ExecuteAsync(sql, input);

                    output.IsSuccess = true;
                    output.Message = "Register user successfully";
                }
            }
            catch (Exception ex)
            {
                output.IsSuccess = false;
                output.Message = "Something went wrong";

            }
            return output;
        }
        public async Task<OperationStatus> UpdateAvailabilityByProductIdAsync(UserObject input)
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