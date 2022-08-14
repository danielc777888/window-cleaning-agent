using System;
using System.Collections;

namespace AI.Agent.Strategy
{
	internal class Location
	{
		public int Row=0;
		public int Column=0;
		
	}
	/// <summary>
	/// Summary description for SearchStrategy.
	/// </summary>
	public abstract class SearchStrategy
	{
		protected Session _session = null;
		protected bool _halt = false;

		public abstract Action GetAction(Percept percept, WindowCleaningAgent agent);
		public abstract void Start(Session session);

		public void Halt()
		{
			_halt = true;
		}


		
		public static SearchStrategy Create(string name)
		{
			SearchStrategy strategy = null;
			if (name.Equals("OnlineDFSSearchStrategy"))
			{
				strategy = new OnlineDFSSearchStrategy();
			}
			else if(name.Equals("RandomWalkStrategy"))
			{
				strategy = new RandomWalkStrategy();
			}
			else if(name.Equals("ManualSearchStrategy"))
			{
				strategy = new ManualSearchStrategy();
			}
			else if(name.Equals("BreadthFirstStrategy"))
			{
				strategy = new BreadthFirstStrategy();
			}
			else if(name.Equals("AStarSearchStrategy"))
			{
				strategy = new AStarSearchStrategy();
			}
			else if(name.Equals("ReplayStrategy"))
			{
				strategy = new ReplayStrategy();
			}

			return strategy;
		}

		protected bool MustCleanWindow(Window window)
		{
			if (window != null && !window.Clean)
			{
				return true;
			}
			return false;
		}

		/// <summary>
		/// Get action sequence to get from on location to another
		/// </summary>
		/// <param name="currentLocation"></param>
		/// <param name="unexploredLocation"></param>
		/// <param name="agent"></param>
		internal Stack GetActionSequence(Location fromLocation, Location toLocation,Direction direction,ref Direction newDirection)
		{

			Stack actionSequence = new Stack();
			
			if (toLocation.Row < fromLocation.Row)
			{
				newDirection = Direction.North;
				if (direction == Direction.North)
				{
					actionSequence.Push(Action.Forward);
				}
				else if(direction == Direction.South)
				{
					actionSequence.Push(Action.Forward);
					actionSequence.Push(Action.TurnLeft);
					actionSequence.Push(Action.TurnLeft);
				}
				else if(direction == Direction.West)
				{
					actionSequence.Push(Action.Forward);
					actionSequence.Push(Action.TurnRight);
				}
				else if(direction == Direction.East)
				{
					actionSequence.Push(Action.Forward);
					actionSequence.Push(Action.TurnLeft);
				}

			}
			else if(toLocation.Row > fromLocation.Row)
			{
				newDirection = Direction.South;
				if (direction == Direction.North)
				{
					actionSequence.Push(Action.Forward);
					actionSequence.Push(Action.TurnLeft);
					actionSequence.Push(Action.TurnLeft);
					
				}
				else if(direction == Direction.South)
				{
					actionSequence.Push(Action.Forward);
				}
				else if(direction == Direction.West)
				{
					actionSequence.Push(Action.Forward);
					actionSequence.Push(Action.TurnLeft);
				}
				else if(direction == Direction.East)
				{
					actionSequence.Push(Action.Forward);
					actionSequence.Push(Action.TurnRight);
				}

			}
			else if(toLocation.Column > fromLocation.Column)
			{
				newDirection = Direction.East;
				if (direction == Direction.North)
				{
					actionSequence.Push(Action.Forward);
					actionSequence.Push(Action.TurnRight);
					
				}
				else if(direction == Direction.South)
				{
					actionSequence.Push(Action.Forward);
					actionSequence.Push(Action.TurnLeft);
				}
				else if(direction == Direction.West)
				{
					actionSequence.Push(Action.Forward);
					actionSequence.Push(Action.TurnLeft);
					actionSequence.Push(Action.TurnLeft);
				}
				else if(direction == Direction.East)
				{
					actionSequence.Push(Action.Forward);
				}
			}
			else if(toLocation.Column < fromLocation.Column)
			{
				newDirection = Direction.West;
				if (direction == Direction.North)
				{
					actionSequence.Push(Action.Forward);
					actionSequence.Push(Action.TurnLeft);
					
				}
				else if(direction == Direction.South)
				{
					actionSequence.Push(Action.Forward);
					actionSequence.Push(Action.TurnRight);
				}
				else if(direction == Direction.West)
				{
					actionSequence.Push(Action.Forward);
					
				}
				else if(direction == Direction.East)
				{
					actionSequence.Push(Action.Forward);
					actionSequence.Push(Action.TurnLeft);
					actionSequence.Push(Action.TurnLeft);
				}
			}
			return actionSequence;
		}

	
		
		

		internal string GetKey(Location location)
		{
			return string.Format("{0},{1}",location.Row,location.Column);
		}


		
	}
}
