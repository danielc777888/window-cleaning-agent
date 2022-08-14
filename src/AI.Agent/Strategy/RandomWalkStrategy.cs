using System;
using System.Collections;

namespace AI.Agent.Strategy
{
	/// <summary>
	/// Summary description for RandomWalkStrategy.
	/// </summary>
	public class RandomWalkStrategy : SearchStrategy
	{
		private Hashtable _stateMap = new Hashtable();
		private Action _previousAction = Action.None;

		public override Action GetAction(Percept percept,WindowCleaningAgent agent)
		{
			Action action = Action.None;
			if ((percept.Bay && agent.Goal == Goal.ReturnToBay)
				|| agent.InBay)
			{
				action =  Action.Forward;

			}else if (MustCleanWindow(percept.Window))
			{
				action =  Action.Clean;
			}
			else
			{
				string key = GetKey(agent);

				if (!_stateMap.ContainsKey(key))
				{
					_stateMap.Add(key,new Action[3]); 
				}
			
				action = GetAction(percept,(Action[])_stateMap[key],key);
			}

			_previousAction = action;

			return action;
		}

		private string GetKey(WindowCleaningAgent agent)
		{
			return string.Format("{0},{1},{2}",agent.Column,agent.Row,agent.Direction);
		}

		private Action GetAction(Percept percept,Action[] actions,string key)
		{
			Action action = Action.None;
					
			int i = GetRandomAction(percept);

			if ((actions[0] != Action.None && actions[1] != Action.None && actions[2] != Action.None && !percept.InAccessible)
				|| ( actions[1] != Action.None && actions[2] != Action.None && percept.InAccessible))
			{
				//just get any random action
				action = (Action)i;
			}
			else
			{
				bool foundAction = false;
				while (!foundAction)
				{
					//prefer random action not selected
					if (actions[i-1] != Action.None)
					{
						i = GetRandomAction(percept);
						continue;
					}
					else
					{
						action = (Action)i;
						Console.WriteLine("new action:" + action + " : " + key);
						actions[i-1] = action;
						foundAction = true;
					}
				}
			}
			return action;
		}

		private int GetRandomAction(Percept percept)
		{
			Random random = new Random();
			int i=0;
			if (percept.InAccessible)
			{
				//can only turn right or left
				i = random.Next(2,4);
			}
			else
			{
				//can move forward
				i = random.Next(1,4);
			}
			return i;
		}

		public override void Start(Session session)
		{
			_session = session;
		}


	}
}
