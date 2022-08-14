using System;
using System.Collections;

using AI.Agent.Strategy;
using AI.Agent.DataAccess;

namespace AI.Agent
{
	public enum Goal
	{
        CleanAllWindows,
		ReturnToBay
	}

	public enum Action
	{
		None = 0,
        Forward = 1,
		TurnLeft = 2,
		TurnRight = 3,
		Clean  = 5
	}

	public enum Direction
	{
		North = 0,
		East = 1,
		South  = 2,
		West = 3
	}
	/// <summary>
	/// Summary description for Class1.
	/// </summary>
	public class WindowCleaningAgent
	{
		private int _id = 0;
		private string _name;
		private Direction _direction = Direction.South;
		private Goal _goal = Goal.CleanAllWindows;
		private int _column = 1;
		private int _row = 0;
		private Percept _percept = null;

		private Session _session = null;
		private SessionDAO _sessionDAO = new SessionDAO();
		private ActionStateDAO _actionStateDAO = new ActionStateDAO();
		//keep a mapping of which windows were cleaned or are already cleaned.
		private IDictionary _cleanWindows = new Hashtable();

	        
		public WindowCleaningAgent()
		{
          
       	}

		#region Properties

		public int Id
		{
			get
			{
				return _id;
			}
			set
			{
				_id = value;
			}
		}

		public string Name
		{
			get
			{
				return _name;
			}
			set
			{
				_name = value;
			}
		}

		public Direction Direction
		{
			get
			{
				return _direction;
			}
			set
			{
				_direction = value;
			}
		}

		public int Column
		{
			get
			{
				return _column;
			}
			set
			{
				_column  = value;
			}
		}

		public int Row
		{
			get
			{
				return _row;
			}
			set
			{
				_row = value;
			}
		}

		public bool InBay
		{
			get
			{
				return (Column == 1 && Row == 0);
			}
			
		}

		public Percept Percept
		{
			get
			{
				return _percept;
			}
		}

		public Session Session
		{
			get
			{
				return _session;
			}
			set
			{
				_session = value;
			}
		}

		public Goal Goal
		{
			get
			{
				return _goal;
			}set
			 {
				 _goal = value;
			 }
		}

		#endregion

		#region Methods

		public void StartNewSession(Session selectedSession,SearchStrategy searchStrategy,int buildingId,int windowsToClean,int tick)
		{
			_session = new Session();
			_session.SearchStrategy = searchStrategy;
			_session.AgentId = Id;
			_session.BuildingId = buildingId;
			_session.WindowsToClean = windowsToClean;
			_session.StartTime = DateTime.Now;
			_session.Tick = tick;

			if (!_session.StrategyName.Equals("ReplayStrategy"))
			{
				_sessionDAO.Insert(_session);
			}

			//try to start search strategy using a selected session
			searchStrategy.Start(selectedSession);
		}

		public void EndSession()
		{

			_session.EndTime = DateTime.Now;
			if (!_session.StrategyName.Equals("ReplayStrategy"))
			{
				_sessionDAO.Update(_session);
			}
		}

		public Action AddPercept(Percept percept)
		{
			_percept = percept;

			Action action = Session.SearchStrategy.GetAction(percept,this);

			switch(action)
			{
				case Action.Forward:
					MoveForward();
					break;
				case Action.TurnLeft:
					TurnLeft();
					break;
				case Action.TurnRight:
					TurnRight();
					break;
				case Action.Clean:
					CleanWindow();
					break;
				default:
					System.Console.WriteLine("No action");
					break;
			
			}

			if (action != Action.None && !_session.StrategyName.Equals("ReplayStrategy"))
			{
				_actionStateDAO.Insert(new ActionState(Session.Id,Session.StepCost,action,percept,Row,Column,Direction));
			}

			return action;
		
		}
	
		private Action RandomAction()
		{
			Random random = new Random();
			return (Action)random.Next(1,4);

		}

		public void CleanWindow()
		{
			Session.StepCost++;
            Percept.Window.Dirt--;

			DetectCleanWindow();

		}

		private void DetectCleanWindow()
		{
			if (Percept.Window.Clean)
			{
				Session.WindowsCleaned++;
			}

			if (!Session.StrategyName.Equals("ReplayStrategy"))
			{
				_sessionDAO.Update(Session);
			}

			if (Session.CleanedAllWindows)
			{
				Goal = Goal.ReturnToBay;
			}
		}

		public void MoveForward()
		{
			Session.StepCost++;
			if (!Percept.InAccessible)
			{
				if (Direction == Direction.North)
				{
					Row--;
				}
				else if(Direction == Direction.South)
				{
					Row++;
				}
				else if(Direction == Direction.West)
				{
					Column--;
				}
				else if(Direction == Direction.East)
				{
					Column++;
				}

				if (InBay)
				{
					Direction = Direction.South;
				}
			}
			if (!_session.StrategyName.Equals("ReplayStrategy"))
			{
				_sessionDAO.Update(Session);
			}
		}

		public void TurnLeft()
		{
			Session.StepCost++;
			if (Direction == Direction.North)
			{
				Direction = Direction.West;
			}
			else if(Direction == Direction.South)
			{
				Direction = Direction.East;
			}
			else if(Direction == Direction.West)
			{
				Direction = Direction.South;
			}
			else if(Direction == Direction.East)
			{
				Direction = Direction.North;
			}
			if (!_session.StrategyName.Equals("ReplayStrategy"))
			{
				_sessionDAO.Update(Session);
			}
		}

		public void TurnRight()
		{
			Session.StepCost++;
			if (Direction == Direction.North)
			{
				Direction = Direction.East;
			}
			else if(Direction == Direction.South)
			{
				Direction = Direction.West;
			}
			else if(Direction == Direction.West)
			{
				Direction = Direction.North;
			}
			else if(Direction == Direction.East)
			{
				Direction = Direction.South;
			}
			if (!_session.StrategyName.Equals("ReplayStrategy"))
			{
				_sessionDAO.Update(Session);
			}
		}

		private string GetWindowMappingKey(Action action)
		{
			return string.Format("{0},{1},{2}",Row,Column,action);
		}

		
		#endregion
	}
}
