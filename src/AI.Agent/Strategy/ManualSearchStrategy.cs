using System; 

namespace AI.Agent.Strategy
{
	/// <summary>
	/// Summary description for Class1.
	/// </summary>
	public class ManualSearchStrategy : SearchStrategy
	{
		public override Action GetAction(Percept percept,WindowCleaningAgent agent)
		{
			if (MustCleanWindow(percept.Window))
			{
				return Action.Clean;
			}
			return percept.Action;
		}

		public override void Start(Session session)
		{
			_session = session;
		}

	}
}
