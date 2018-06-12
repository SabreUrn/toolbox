using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToolboxSQL {
	class Program {
		static void Main(string[] args) {
		}

		private void InsertData(string value1, double value2) {
			DateTime timestamp = DateTime.Now;
			string connectionString = "";
			string cmdText = "INSERT INTO Table " +
				"( [Value1], [Value2] )" +
				"VALUES (@value1, @value2)";

			using (SqlConnection connection = new SqlConnection(connectionString)) {
				SqlCommand command = new SqlCommand(cmdText, connection);
				command.Parameters.AddWithValue("@value1", value1);
				command.Parameters.AddWithValue("@value2", value2);

				connection.Open();
				int rows = command.ExecuteNonQuery();
				connection.Close();
			}
		}

		private string GetOneRow(string value1) {
			string connectionString = "";
			string cmdText = "SELECT * FROM Table WHERE Value1=@value1";

			string result = "Error in fetching row.";

			using (SqlConnection connection = new SqlConnection(connectionString)) {
				SqlCommand command = new SqlCommand(cmdText, connection);
				using(SqlDataReader reader = command.ExecuteReader()) {
					if(reader.Read()) {
						result = $"Id {reader["id"]}: {reader["value1"]}";
					}
				}
			}
			return result;
		}

		//probably change to return List<Object>
		private List<string> GetAllRows(string value1) {
			string connectionString = "";
			string cmdText = "SELECT * FROM Table";

			List<string> result = new List<string> { "Error in fetching rows." };

			using (SqlConnection connection = new SqlConnection(connectionString)) {
				SqlCommand command = new SqlCommand(cmdText, connection);
				using(SqlDataReader reader = command.ExecuteReader()) {
					while(reader.Read()) {
						result.Add($"Id {reader["id"]}: {reader["value1"]}");
					}
				}
			}
			return result;
		}
	}
}
