using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Step 0
// add nuget package 
//https://makolyte.com/system-data-sqlclient-is-missing-in-a-dotnet-core-project/
//  System.Data.SqlClient

// step 1
using System.Data;
// step 2
using System.Data.SqlClient;


namespace ChuckNorris.Tools
{
    public class Db
    {
        SqlConnection _connection;
        public Db(string connectionString)
        {
            _connection = new SqlConnection(connectionString);

        }
        public string AddJoke(JokeChuckNorris newJoke)
        {
            string result = "Already in the database";
            String sql = "SELECT count(id) FROM Joke WHERE id ='" + newJoke.id + "'";
            SqlCommand command = new SqlCommand(sql, _connection);
            _connection.Open();
            var inTheDatabase = (int)command.ExecuteScalar();
            if (inTheDatabase == 0)
            {
                sql = "INSERT INTO joke VALUES ('" + newJoke.id + "','" + newJoke.value.Replace("'", "''") + "' )";
                command.CommandText = sql;
                command.ExecuteNonQuery();
                result = "Added";
            }
            _connection.Close();
            return result;
        }




    }
}
