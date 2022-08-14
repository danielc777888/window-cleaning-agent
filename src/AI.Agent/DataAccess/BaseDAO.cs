using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace AI.Agent.DataAccess
{
	/// <summary>
	/// Summary description for BaseDAO.
	/// </summary>
	public class BaseDAO
        {
		private SqlConnection _connection = null;
		protected SqlConnection GetConnection()
		{
			_connection = new SqlConnection((string)ConfigurationSettings.AppSettings["ConnectionString"]);
			_connection.Open();
			return _connection;
		}

		protected SqlDataReader ExecuteReader(string command,SqlConnection connection)
		{
	
			SqlCommand comm = new SqlCommand(command,connection);
			SqlDataReader reader =  comm.ExecuteReader();
			return reader;			
		}

		protected int ExecuteNonQuery(string command,SqlConnection connection)
		{
	
			SqlCommand comm = new SqlCommand(command,connection);
			return comm.ExecuteNonQuery();
		}

		protected object ExecuteScalar(string command, SqlConnection connection)
		{
			SqlCommand comm = new SqlCommand(command,connection);
			return comm.ExecuteScalar();
		}


		protected void CloseConnection()
		{
			_connection.Close();
			_connection.Dispose();
		}
		
	}


}

