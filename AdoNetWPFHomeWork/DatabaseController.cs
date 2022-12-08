using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;
using System.Windows;

namespace AdoNetWPFHomeWork
{
    public class DatabaseController
    {
        private string tableName = "[Main].[Users]";


        public DatabaseController()
        {
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["defaultConnection"].ConnectionString);
            sqlConnection.Open();
            _sqlConnection = sqlConnection;
        }

        private SqlConnection _sqlConnection;

        ~DatabaseController()
        {
            _sqlConnection.Close();
        }


        public async Task<List<User>> SelectAll()
        {
            string requestText = string.Format("SELECT * FROM {0}", tableName);
            SqlCommand sqlCommand = new SqlCommand(requestText, _sqlConnection);
            SqlDataReader sqlReader = await sqlCommand.ExecuteReaderAsync();
            if (!sqlReader.HasRows) return new List<User> { };
            List<User> users = new List<User>();
            while (sqlReader.Read())
                users.Add(new User(sqlReader));
            return users;
        }

        public async void Update(User user)
        {
            SqlCommand sqlCommand = new SqlCommand("[Main].[Users_update]", _sqlConnection);
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCommand.Parameters.Add(new SqlParameter("@Id", user.Id));
            sqlCommand.Parameters.Add(new SqlParameter("@F", user.F));
            sqlCommand.Parameters.Add(new SqlParameter("@I", user.I));
            sqlCommand.Parameters.Add(new SqlParameter("@O", user.O));
            sqlCommand.Parameters.Add(new SqlParameter("@Pass_Number", user.PassNumber));
            sqlCommand.Parameters.Add(new SqlParameter("@Pass_Series", user.PassSeries));
            sqlCommand.Parameters.Add(new SqlParameter("@Pass_Issued", user.PassIssued));
            sqlCommand.Parameters.Add(new SqlParameter("@Phone", user.Phone));
            sqlCommand.Parameters.Add(new SqlParameter("@Email", user.Email));
            await sqlCommand.ExecuteNonQueryAsync();
        }

        public async Task Insert(User user)
        {
            SqlCommand sqlCommand = new SqlCommand("[Main].[Users_insert]", _sqlConnection);
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCommand.Parameters.Add(new SqlParameter("@F", user.F));
            sqlCommand.Parameters.Add(new SqlParameter("@I", user.I));
            sqlCommand.Parameters.Add(new SqlParameter("@O", user.O));
            sqlCommand.Parameters.Add(new SqlParameter("@Pass_Number", user.PassNumber));
            sqlCommand.Parameters.Add(new SqlParameter("@Pass_Series", user.PassSeries));
            sqlCommand.Parameters.Add(new SqlParameter("@Pass_Issued", user.PassIssued));
            sqlCommand.Parameters.Add(new SqlParameter("@Phone", user.Phone));
            sqlCommand.Parameters.Add(new SqlParameter("@Email", user.Email));
            await sqlCommand.ExecuteNonQueryAsync();
        }

        public async void Delete(int id)
        {
            SqlCommand sqlCommand = new SqlCommand("[Main].[Users_delete]", _sqlConnection);
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCommand.Parameters.Add(new SqlParameter("@id", id));
            await sqlCommand.ExecuteNonQueryAsync();
        }

        public async Task<int> CountUsers()
        {
            // // Код процедуры:
            // CREATE PROCEDURE[Main].[Users_Count]
            // @count int out
            // as
            //  SELECT @count = COUNT(*) FROM[Main].[Users]
            // go

            SqlCommand sqlCommand = new SqlCommand("[Main].[Users_count]", _sqlConnection);
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            SqlParameter countParameter = new SqlParameter
            {
                ParameterName = "@count",
                SqlDbType = System.Data.SqlDbType.Int,
                Direction = System.Data.ParameterDirection.Output
            };
            sqlCommand.Parameters.Add(countParameter);
            await sqlCommand.ExecuteNonQueryAsync();
            return (int) sqlCommand.Parameters["@count"].Value;
        }

        public async Task<int> MinIdUsers()
        {
            string requestText = string.Format("SELECT MIN(id) FROM {0}", tableName);
            SqlCommand sqlCommand = new SqlCommand(requestText, _sqlConnection);
            return (int)await sqlCommand.ExecuteScalarAsync();
        }

        public async Task<int> MaxIdUsers()
        {
            string requestText = string.Format("SELECT MAX(id) FROM {0}", tableName);
            SqlCommand sqlCommand = new SqlCommand(requestText, _sqlConnection);
            return (int)await sqlCommand.ExecuteScalarAsync();
        }
    }
}
