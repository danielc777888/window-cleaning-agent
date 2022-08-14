using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace AI.Agent.DataAccess
{
	/// <summary>
	/// Summary description for ActionDAO.
	/// </summary>
	public class ActionStateDAO : BaseDAO
	{
		public void Insert(ActionState actionState)
		{
			int dirtLevel = 0;
			bool window = false;
			if (actionState.Percept.Window != null)
			{
				window = true;
				dirtLevel = actionState.Percept.Window.Dirt;
			}
			string cmd = String.Format("ins_ActionState {0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10}",actionState.SessionId,actionState.Step,(int)actionState.Action,(int)actionState.Direction,actionState.Column,
										actionState.Row,Convert.ToInt32(actionState.Percept.Bay),Convert.ToInt32(actionState.Percept.BuildingEdge),Convert.ToInt32(window),dirtLevel,Convert.ToInt32(actionState.Percept.Obstacle));
			ExecuteNonQuery(cmd,GetConnection());
			CloseConnection();
		}

		public IList FindBySession(int sessionId)
		{
			string cmd = string.Format("sel_ActionState {0}",sessionId);
            SqlDataReader reader = ExecuteReader(cmd,GetConnection());
            IList actionStates = new ArrayList();
			while(reader.Read())
			{
				Action action = (Action)Convert.ToInt32(reader["Action"]);
				int step = Convert.ToInt32(reader["Step"]);
				int row = Convert.ToInt32(reader["Row"]);
				int column = Convert.ToInt32(reader["Column"]);
				Direction direction = (Direction)Convert.ToInt32(reader["Direction"]);
				Percept percept = new Percept();
				percept.Bay = Convert.ToBoolean(reader["Bay"]);
				percept.BuildingEdge = Convert.ToBoolean(reader["BuildingEdge"]);
				percept.Obstacle = Convert.ToBoolean(reader["Obstacle"]);
				bool window = Convert.ToBoolean(reader["Window"]);
				int dirtLevel = Convert.ToInt32(reader["DirtLevel"]);
				if (window)
				{
					percept.Window = new Window(dirtLevel);
				}

				ActionState actionState = new ActionState(sessionId,step,action,percept,row,column,direction);
				actionStates.Add(actionState);
			}
            CloseConnection();
			return actionStates;
		}
	}
}
