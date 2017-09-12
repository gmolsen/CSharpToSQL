using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Threading.Tasks;
using CSharpToSQL;

namespace TestCSharpToSQL {
	class Program {
		void Run () {

			string connStr = "Server=STUDENT05;Database=DotNetDatabase;Trusted_Connection=yes";
			SqlConnection connection = new SqlConnection(connStr);
			connection.Open();
			if (connection.State != System.Data.ConnectionState.Open) {
				Console.WriteLine("SQL connection did not open.");
				return;
			}
			Console.WriteLine("SQL connection opened successfully.");
			var sql = "select * from Student";
			SqlCommand cmd = new SqlCommand(sql, connection);
			SqlDataReader reader = cmd.ExecuteReader();
			List<Student> students = new List<Student>();
			while(reader.Read()) {
				// GetOrdinal finds string regardless of what column it is in
				var id = reader.GetInt32(reader.GetOrdinal("Id"));
				var firstName = reader.GetString(reader.GetOrdinal("FirstName"));
				var lastName = reader.GetString(reader.GetOrdinal("LastName"));
				var address = reader.GetString(reader.GetOrdinal("Address"));
				var city = reader.GetString(reader.GetOrdinal("City"));
				var state = reader.GetString(reader.GetOrdinal("State"));
				var zip = reader.GetString(reader.GetOrdinal("Zipcode"));
				var phone = reader.GetString(reader.GetOrdinal("PhoneNumber"));
				var email = reader.GetString(reader.GetOrdinal("Email"));
				var birthday = reader.GetDateTime(reader.GetOrdinal("Birthday"));

				//set major id to null value before reading the database value
				var majorId = 0;
				//check the value in the database
				//if it is NOT NULL
				if (!reader.GetValue(reader.GetOrdinal("MajorId")).Equals(DBNull.Value)) {
					//then do this
					majorId = reader.GetInt32(reader.GetOrdinal("MajorId"));
							}
				

				Console.WriteLine($"{id}, {firstName} {lastName}, lives on {address} in {city}, {state} {zip} and was born on {birthday};");
				Student student = new Student();
				student.Id = id;
				student.FirstName = firstName;
				student.LastName = lastName;
				student.Address = address;
				student.City = city;
				student.State = state;
				student.Zip = zip;
				student.Phone = phone;
				student.Email = email;
				student.Birthday = birthday;
				student.MajorId = majorId;
				students.Add(student);
			}
			reader.Close();
			connection.Close();
		}

		static void Main(string[] args) {
			new Program().Run();
		}
	}
}
