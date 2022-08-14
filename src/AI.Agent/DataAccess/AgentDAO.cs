using System;
using System.Data;
using System.Data.SqlClient;

using AI.Agent;

namespace AI.Agent.DataAccess
{
	/// <summary>
	/// Summary description for AgentDAO.
	/// </summary>
	public class AgentDAO : BaseDAO
	{
		public WindowCleaningAgent Find(int id)
		{
			WindowCleaningAgent agent = null;
            SqlDataReader reader = ExecuteReader(String.Format("sel_Agent {0}",id),GetConnection());
			while(reader.Read())
			{
				agent = new WindowCleaningAgent();
				agent.Id = (int)reader["Id"];
				agent.Name = (string)reader["Name"];
			}
			CloseConnection();
			return agent;
		}
	}
}
