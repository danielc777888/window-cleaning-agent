using System;
using System.Collections;

namespace AI.Agent.Strategy
{
	
	/// <summary>
	/// Summary description for OnlineDFSSearchStrategy.
	/// </summary>
	public class OnlineDFSSearchStrategy : SearchStrategy
	{
		private Hashtable _result = new Hashtable();
		private Hashtable _unexplored = new Hashtable();
		private Hashtable _unbackTracked = new Hashtable();
		private Location _previousLocation = null;
		private Location _currentLocation = null;
		private Stack _currentActionSequence = new Stack();


		public override Action GetAction(Percept percept, WindowCleaningAgent agent)
		{
			if ((percept.Bay && agent.Goal == Goal.ReturnToBay)
				|| agent.InBay)
			{
				if (agent.InBay)
				{
					_previousLocation = new Location();
					_previousLocation.Row = 0;
					_previousLocation.Column = 0;
				}
				return Action.Forward;

			}
			else if (MustCleanWindow(percept.Window))
			{
				return Action.Clean;
			}
			else
			{
				if(_currentActionSequence.Count != 0)
				{
					Action action =  (Action)_currentActionSequence.Pop();

					if ((action  == Action.Forward && !percept.InAccessible)
						|| (action == Action.Forward && percept.Bay && agent.Goal != Goal.CleanAllWindows)
						|| action == Action.TurnLeft || action == Action.TurnRight)
					{
                    	return action;				
					}
				}

				_currentLocation =  new Location();
				_currentLocation.Row = agent.Row;
				_currentLocation.Column = agent.Column;

				//get available unexplored locations, for current state
				string key = GetKey(_currentLocation);
				if (!_unexplored.ContainsKey(key))
				{
					_unexplored.Add(key,GetUnExploredLocations(_previousLocation,percept,agent));
				}

				//if previous location present, push on backtrack stack
				if (_previousLocation != null)
				{
					if (!_unbackTracked.ContainsKey(key))
					{
						_unbackTracked.Add(key,new Stack());
					}

					Stack st = (Stack)_unbackTracked[key];
					string pKey = GetKey(_previousLocation);
					if ((_previousLocation.Row != _currentLocation.Row
						|| _previousLocation.Column != _currentLocation.Column)
						&& st.Count == 0)
					{
						Location unbackTrackLocation = new Location();
						unbackTrackLocation.Row = _previousLocation.Row;
						unbackTrackLocation.Column = _previousLocation.Column;
						st.Push(unbackTrackLocation);
					}
				}

				Stack locations = (Stack)_unexplored[key];

				//if goal is now to return to bay, clear all locations to explore and back track
				if (agent.Goal == Goal.ReturnToBay)
				{
					locations.Clear();
				}

				//search for unexplored location
				while (locations.Count != 0 && agent.Goal == Goal.CleanAllWindows)
				{
					//get unexplored location and set action sequence to get to location
                    Location location = (Location)locations.Pop();
					//check if location has been explored, if it has back track
					if (!_unexplored.ContainsKey(GetKey(location)))
					{
						Direction newDirection = Direction.South;
                        _currentActionSequence =  GetActionSequence(_currentLocation,location,agent.Direction,ref newDirection);
						break;
					}
					else
					{
						continue;
					}
				}

				//back track
				if (locations.Count == 0 && _currentActionSequence.Count == 0)
				{
					//if no more unexplored locations then use backtrack location
					Stack unbackTracks = (Stack)_unbackTracked[key];
					if (unbackTracks.Count == 0)
					{
						_currentActionSequence.Push(Action.None);
					}
					else
					{
						
						Location backTrackLocation = (Location)unbackTracks.Pop();
						//set action sequence to go to backtrack location
						Direction newDirection = Direction.South;
						_currentActionSequence = GetActionSequence(_currentLocation,backTrackLocation,agent.Direction,ref newDirection);
					}

				}
			
				//set previous location values
				_previousLocation.Row = _currentLocation.Row;
				_previousLocation.Column = _currentLocation.Column;

				try
				{
					//return next action
					return (Action)_currentActionSequence.Pop();
				}
				catch(Exception ex)
				{
					Console.WriteLine(ex);
				}
				return Action.None;
			}
		}

		/// <summary>
		/// Gets unexplored locations depending on previous location and state.
		/// </summary>
		/// <param name="percept"></param>
		/// <param name="agent"></param>
		/// <returns></returns>
		private Stack GetUnExploredLocations(Location previousLocation,Percept percept, WindowCleaningAgent agent)
		{
			Stack stack = new Stack();
			
			bool moveDown = true;
			bool moveUp = true;
			bool moveRight = true;
			bool moveLeft = true;
			
			if (agent.Row > previousLocation.Row)
			{
				moveUp = false;
			}
			else if(agent.Row < previousLocation.Row)
			{
				moveDown = false;
			}
			else if(agent.Column > previousLocation.Column)
			{
				moveLeft = false;
			}
			else if(agent.Column < previousLocation.Column)
			{
				moveRight = false;

			}

			Location leftLocation = new Location();
			Location rightLocation = new Location();
			Location upLocation = new Location();
			Location downLocation = new Location();
			Location lastLocation = null;

			

			leftLocation.Row = agent.Row;
			leftLocation.Column = agent.Column - 1;

			rightLocation.Row = agent.Row;
			rightLocation.Column = agent.Column + 1;

			upLocation.Row = agent.Row - 1;
			upLocation.Column = agent.Column;
			
			downLocation.Row = agent.Row + 1;
			downLocation.Column = agent.Column;

			if (agent.Direction == Direction.West && moveLeft)
			{
				lastLocation = leftLocation;
				moveLeft = false;
			}
			if (agent.Direction == Direction.East && moveRight)
			{
				lastLocation = rightLocation;
				moveRight = false;
			}
			if (agent.Direction == Direction.North && moveUp)
			{
				lastLocation = upLocation;
				moveUp = false;
			}
			if (agent.Direction == Direction.South && moveDown)
			{
				lastLocation = downLocation;
				moveDown = false;
			}

			if (lastLocation != null)
			{
				stack.Push(lastLocation);
			}
			
			if (agent.Row != 1 && moveUp)
				stack.Push(upLocation);
			if (moveDown)
				stack.Push(downLocation);
			if (moveRight)
				stack.Push(rightLocation);
			if (agent.Column != 1 && moveLeft)
				stack.Push(leftLocation);
			
			return stack;

		}

		public override void Start(Session session)
		{
			_session = session;
		}


				
	}
}
