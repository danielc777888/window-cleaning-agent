using System;
using System.Data;
using System.Data.SqlClient;

using AI.Agent;
using AI.Agent.Strategy;

namespace AI.Agent.DataAccess
{
	/// <summary>
	/// Summary description for BuildingDAO.
	/// </summary>
	public class BuildingDAO : BaseDAO
	{
		public int Insert(string buildingName)
		{
			string cmd = String.Format("ins_Building '{0}'",buildingName);
			object obj = ExecuteScalar(cmd,GetConnection());
			int id = Convert.ToInt32(obj);
			CloseConnection();
			return id;
		}
	}
}