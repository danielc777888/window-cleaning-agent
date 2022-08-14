using System;
using System.Data;
using System.Data.SqlClient;

namespace AI.Agent.DataAccess
{
	/// <summary>
	/// Summary description for Class1.
	/// </summary>
	public class AgentDAO
	{
	
		public Agent

		[STAThread]
		static void Main(string[] args)
		{
			SqlConnection conn = new SqlConnection("Initial Catalog=Northwind;Data Source=localhost;User=sa;Password=PleaseUse123;");
            conn.Open();
			SqlCommand comm = new SqlCommand("SELECT * FROM EMPLOYEES",conn);
			SqlDataReader reader =  comm.ExecuteReader();
			while(reader.Read())
			{
				string lastName = (string)reader["LastName"];
				Console.WriteLine("LastName :" + lastName);
			}

			conn.Close();
			conn.Dispose();
			


		}
	}
}
