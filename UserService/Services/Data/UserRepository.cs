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
        public async Task<OperationStatus> LoginUserAsync(string userID, string userPassword)
        {
            var output = new OperationStatus();
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    string sql = @"SELECT usr_id,usr_name,usr_role,pass_word FROM user_dtls WHERE usr_id=@usr_id";

                    var user = await connection.QueryAsync<UserObject>(sql, new { usr_id = userID });
                    output.Data = user;
                }
            }
            catch (Exception ex)
            {
                output.IsSuccess = false;
                output.Message = "Something went wrong";
                output.Data = null;
            }
            return output;
        }
    }
}