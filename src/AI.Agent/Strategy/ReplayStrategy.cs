using System;
using System.Collections;

using AI.Agent.DataAccess;


namespace AI.Agent.Strategy
{
	/// <summary>
	/// Summary description for ReplayStrategy.
	/// </summary>
	public class ReplayStrategy : SearchStrategy
	{
		private int _actionIdx = 0;
		private IList _actionStates = new ArrayList();

		public ReplayStrategy()
		{
			
		}

		public override void Start(Session session)
		{
			_session = session;
			ActionStateDAO actionStateDAO = new ActionStateDAO();
			_actionStates = actionStateDAO.FindBySession(_session.Id);
		}

		public override Action GetAction(Percept percept, WindowCleaningAgent agent)
		{
			ActionState actionState = (ActionState)_actionStates[_actionIdx];	
			_actionIdx++;
			return actionState.Action;
		}


	}
}
