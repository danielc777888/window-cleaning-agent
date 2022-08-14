using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

using AI.Agent;
using AI.Agent.Strategy;

namespace AI.Agent.DataAccess
{
	/// <summary>
	/// Summary description for SessionDAO.
	/// </summary>
	public class SessionDAO : BaseDAO
	{
		public void Insert(Session session)
		{
			string cmd = String.Format("ins_Session {0},{1},'{2}','{3}',{4},{5},{6},{7}",session.AgentId,session.BuildingId,session.SearchStrategy.GetType().Name,session.StartTime.ToString("MM/dd/yyyy HH:mm:ss"),session.WindowsCleaned,session.WindowsToClean,session.StepCost,session.Tick);
			object obj = ExecuteScalar(cmd,GetConnection());
			int id = Convert.ToInt32(obj);
			session.Id = id;
			CloseConnection();
		}

		public void Update(Session session)
		{
			string endTime = null;
			if (!session.EndTime.Equals(DateTime.MinValue))
			{
				endTime = session.EndTime.ToString("MM/dd/yyyy HH:mm:ss");
			}
			string cmd = String.Format("upd_Session {0},{1},{2},'{3}','{4}','{5}',{6},{7},{8},{9}",session.Id,session.AgentId,session.BuildingId,session.SearchStrategy.GetType().Name,session.StartTime.ToString("MM/dd/yyyy HH:mm:ss"),endTime,session.WindowsCleaned,session.WindowsToClean,session.StepCost,session.Tick);
			ExecuteNonQuery(cmd,GetConnection());
			CloseConnection();
		}

		public Session Find(int sessionId)
		{
			Session session = new Session();
			string cmd = string.Format("sel_Session {0}",sessionId);
			SqlDataReader reader =  ExecuteReader(cmd,GetConnection());
			while(reader.Read())
			{
				session.Id = Convert.ToInt32(reader["Id"]);
				session.AgentId = Convert.ToInt32(reader["AgentId"]);
				session.BuildingId = Convert.ToInt32(reader["BuildingId"]);
				session.SearchStrategy = SearchStrategy.Create(Convert.ToString(reader["SearchStrategy"]));
				session.StartTime = Convert.ToDateTime(reader["StartTime"]);
				session.EndTime = Convert.ToDateTime(reader["EndTime"]);
				session.StepCost = Convert.ToInt32(reader["StepCost"]);
				session.Tick = Convert.ToInt32(reader["Tick"]);
				session.WindowsCleaned = Convert.ToInt32(reader["WindowsCleaned"]);
				session.WindowsToClean = Convert.ToInt32(reader["WindowsToClean"]);
			}
			CloseConnection();
			return session;
		}
		public IList FindByBuilding(int buildingId)
		{
			IList result = new ArrayList();
			string cmd = string.Format("sel_SessionByBuilding {0}",buildingId);
			SqlDataReader reader =  ExecuteReader(cmd,GetConnection());
			while(reader.Read())
			{
				Session session = new Session();
				session.Id = Convert.ToInt32(reader["Id"]);
				session.AgentId = Convert.ToInt32(reader["AgentId"]);
				session.BuildingId = Convert.ToInt32(reader["BuildingId"]);
				session.SearchStrategy = SearchStrategy.Create(Convert.ToString(reader["SearchStrategy"]));
				session.StartTime = Convert.ToDateTime(reader["StartTime"]);
				if (!Convert.IsDBNull(reader["EndTime"]))
				{
					session.EndTime = Convert.ToDateTime(reader["EndTime"]);
				}
			
				session.StepCost = Convert.ToInt32(reader["StepCost"]);
				session.Tick = Convert.ToInt32(reader["Tick"]);
				session.WindowsCleaned = Convert.ToInt32(reader["WindowsCleaned"]);
				session.WindowsToClean = Convert.ToInt32(reader["WindowsToClean"]);
				result.Add(session);
			}
			CloseConnection();
			return result;
		}
	}
}
