using System;
using System.Collections;

using AI.Agent.DataAccess;

namespace AI.Agent.Strategy
{
	/// <summary>
	/// Summary description for TreeSearchStrategy.
	/// </summary>
	public class BreadthFirstStrategy : SearchStrategy
	{
		protected IList _actionStates = new ArrayList();
		protected IDictionary _perceptLocations = new Hashtable();
		protected IDictionary _exploredLocations = new Hashtable();
	
		//normal action list to clean all windows
		protected Stack _actionList = new Stack();
		protected Stack _currentActionSequence = new Stack();
		
		protected Action _lastAction = Action.None;
		protected Goal _goal = Goal.CleanAllWindows;
		
		public BreadthFirstStrategy(){}

		public override void Start(Session session)
		{
			_session = session;
			//get the list of actions for the session
			ActionStateDAO actionStateDAO = new ActionStateDAO();
			_actionStates = actionStateDAO.FindBySession(_session.Id);
			
			ActionState previousActionState =null;
			//locate obstacles,windows,bay, and edge.
			foreach(ActionState actionState in _actionStates)
			{
				string exploredKey = string.Format("{0},{1}",actionState.Row,actionState.Column);
				if (!_exploredLocations.Contains(exploredKey))
				{
					_exploredLocations.Add(exploredKey,true);
				}

				if (previousActionState == null)
					previousActionState = actionState;

				//get key location for current percept
				string key = GetKey(previousActionState);

				if (actionState.Percept.Window != null || actionState.Percept.Obstacle
					|| actionState.Percept.BuildingEdge || actionState.Percept.Bay)
				{
					if (!_perceptLocations.Contains(key))
					{
						_perceptLocations.Add(key,actionState.Percept);
					}
				}
				previousActionState = actionState;
			}

			FindSolution(1,1,Direction.South,Action.Forward);			
			
		}

		protected virtual void FindSolution(int initialRow,int initialColumn,Direction direction,Action firstAction)
		{
			
			//_expandedNodes.Clear();
			Hashtable expandedNodes = new Hashtable();

			//by default bay node is already expanded.
			if (_goal == Goal.CleanAllWindows)
			{
				Location firstLocation = new Location();
				firstLocation.Row =0;
				firstLocation.Column = 1;
				expandedNodes.Add(GetKey(firstLocation),true);
			}

			Node initialNode = new Node();

			Location location = new Location();
			location.Column =initialColumn;
			location.Row = initialRow;
			initialNode.Location = location;

			Stack actions = new Stack();
			actions.Push(firstAction);
			initialNode.Actions = actions;
			initialNode.Direction = direction;
			initialNode.WindowCleanedLocations = new Hashtable();
			initialNode.ExpandedNodes = expandedNodes;
					
			if (_goal == Goal.CleanAllWindows)
			{
				initialNode.WindowsCleaned += CheckForWindows(location,actions,direction,initialNode.WindowCleanedLocations);
			}

			Queue fringe = new Queue();
			fringe.Enqueue(initialNode);
            Node nextNode = null;
			while(true)
			{
				if(fringe.Count == 0 || _halt)
				{
					throw new Exception("No solution found!");
				}
				else
				{
					nextNode = (Node)fringe.Dequeue();
				}

				if (!nextNode.ExpandedNodes.Contains(GetKey(nextNode.Location)))
				{
					nextNode.ExpandedNodes.Add(GetKey(nextNode.Location),true);
				}

				//Console.WriteLine(string.Format("Expanding : {0},{1}",nextNode.Location.Row,nextNode.Location.Column));
				if (ReachedGoal(nextNode))
				{
					SetActions(nextNode);
					break;
				}
				else
				{
					IList nodesToExpand = GetNodesToExpand(nextNode);

					foreach(Node node in nodesToExpand)
					{
						//Console.WriteLine(string.Format("Adding to fringe : {0},{1}",node.Location.Row,node.Location.Column));
						fringe.Enqueue(node);
					}
				}
			}
		}

		/// <summary>
		/// Go through all parent to get actions.
		/// </summary>
		/// <param name="goalNode"></param>
		private void SetActions(Node node)
		{
            while (node != null)
			{
                _actionList.Push(node.Actions);
                node = node.ParentNode;
			}
		}

		public override Action GetAction(Percept percept, WindowCleaningAgent agent)
		{
			if (MustCleanWindow(percept.Window))
			{
				return Action.Clean;
			}
			else
			{
				if (agent.Goal == Goal.ReturnToBay && _goal == Goal.CleanAllWindows)
				{
					_goal = Goal.ReturnToBay;
					FindSolution(agent.Row,agent.Column,agent.Direction,_lastAction);
				}
				if (_currentActionSequence.Count == 0)
				{
						if (_actionList.Count == 0)
						{
							return Action.None;
						}
						_currentActionSequence = (Stack)_actionList.Pop();
				}

				_lastAction = (Action)_currentActionSequence.Pop();
				return _lastAction;

			}
		}

		private int CheckForWindows(Location currentLocation,Stack actions, Direction direction,Hashtable windowCleanedLocations)
		{
			int result = 0;
			//check for window, up
            string key = string.Format("{0},{1}",currentLocation.Row-1,currentLocation.Column);
			Percept percept = (Percept)_perceptLocations[key];
            if (percept != null && percept.Window != null && !windowCleanedLocations.Contains(key))
			{
				if (direction == Direction.South)
				{
					actions.Push(Action.TurnLeft);
					actions.Push(Action.TurnLeft);
					actions.Push(Action.TurnRight);
					actions.Push(Action.TurnRight);
				}
				else if(direction == Direction.West)
				{
					actions.Push(Action.TurnLeft);
					actions.Push(Action.TurnRight);
					
				}
				else if(direction == Direction.East)
				{
					actions.Push(Action.TurnRight);
					actions.Push(Action.TurnLeft);
				}

				windowCleanedLocations.Add(key,true);
				result++;
			}

			//check for window , down
			key = string.Format("{0},{1}",currentLocation.Row+1,currentLocation.Column);
			percept = (Percept)_perceptLocations[key];
			if (percept != null && percept.Window != null && !windowCleanedLocations.Contains(key))
			{
				if (direction == Direction.North)
				{
					actions.Push(Action.TurnLeft);
					actions.Push(Action.TurnLeft);
					actions.Push(Action.TurnRight);
					actions.Push(Action.TurnRight);
				}
				else if(direction == Direction.West)
				{
					actions.Push(Action.TurnRight);
					actions.Push(Action.TurnLeft);
				}
				else if(direction == Direction.East)
				{
					actions.Push(Action.TurnLeft);
					actions.Push(Action.TurnRight);
				}
				windowCleanedLocations.Add(key,true);
				result++;
			}

			//check for window , left
			key = string.Format("{0},{1}",currentLocation.Row,currentLocation.Column-1);
			percept = (Percept)_perceptLocations[key];
			if (percept != null && percept.Window != null  && !windowCleanedLocations.Contains(key))
			{
				if (direction == Direction.North)
				{
					actions.Push(Action.TurnRight);
					actions.Push(Action.TurnLeft);
				}
				else if(direction == Direction.South)
				{
					actions.Push(Action.TurnLeft);
					actions.Push(Action.TurnRight);
				}
				else if(direction == Direction.East)
				{
					actions.Push(Action.TurnLeft);
					actions.Push(Action.TurnLeft);
					actions.Push(Action.TurnRight);
					actions.Push(Action.TurnRight);
				}
				windowCleanedLocations.Add(key,true);
				result++;
			}

			//check for window , right
			key = string.Format("{0},{1}",currentLocation.Row,currentLocation.Column+1);
			percept = (Percept)_perceptLocations[key];
			if (percept != null && percept.Window != null  && !windowCleanedLocations.Contains(key))
			{
				if (direction == Direction.North)
				{
					actions.Push(Action.TurnLeft);
					actions.Push(Action.TurnRight);
				}
				else if(direction == Direction.South)
				{
					actions.Push(Action.TurnRight);
					actions.Push(Action.TurnLeft);
				}
				else if(direction == Direction.West)
				{
					actions.Push(Action.TurnLeft);
					actions.Push(Action.TurnLeft);
					actions.Push(Action.TurnRight);
					actions.Push(Action.TurnRight);
				}
				windowCleanedLocations.Add(key,true);
				result++;
			}
			return result;
		}

		private bool ReachedGoal(Node node)
		{
			//Console.WriteLine(string.Format("WindowsCleaned : {0} WindowsToClean : {1}",node.WindowsCleaned,_session.WindowsToClean));
			if (_goal == Goal.CleanAllWindows)
			{
                if (node.WindowsCleaned == _session.WindowsToClean)
				{
					return true;
				}
				else
				{
					return false;
				}
			}
			else if(_goal == Goal.ReturnToBay)
			{
				if (node.Location.Column == 1 && node.Location.Row == 0)
				{
					return true;
				}
				else
				{
					return false;
				}
			}
			return false;
		}

		protected virtual IList GetNodesToExpand(Node node)
		{
			IList nodes = new ArrayList();

			//percept, up
			int row = node.Location.Row-1;
			int col = node.Location.Column;
			string key = string.Format("{0},{1}",row,col);
			Percept percept = (Percept)_perceptLocations[key];
			//only add node for explored location, for location never expanded, and if location is accessible
			if (!node.ExpandedNodes.Contains(key) && (_exploredLocations.Contains(key) ||(percept != null && !percept.InAccessible)))
			{
				AddNewNode(nodes,node,row,col);
			}

			//percept, down
			//down add nodes wich are away from bay
			//if (_goal != Goal.ReturnToBay)
			//{
				row = node.Location.Row+1;
				col = node.Location.Column;
				key = string.Format("{0},{1}",row,col);
				percept = (Percept)_perceptLocations[key];
				if (!node.ExpandedNodes.Contains(key) && (_exploredLocations.Contains(key) ||(percept != null && !percept.InAccessible)))
				{
					AddNewNode(nodes,node,row,col);
				}
			//}

			//percept, left
			row = node.Location.Row;
			col = node.Location.Column-1;
			key = string.Format("{0},{1}",row,col);
			percept = (Percept)_perceptLocations[key];
			if (!node.ExpandedNodes.Contains(key) && (_exploredLocations.Contains(key) ||(percept != null && !percept.InAccessible)))
			{
				AddNewNode(nodes,node,row,col);
			}

			//percept, right
			row =node.Location.Row;
			col = node.Location.Column+1;
			key = string.Format("{0},{1}",row,col);
			percept = (Percept)_perceptLocations[key];
			if (!node.ExpandedNodes.Contains(key) && (_exploredLocations.Contains(key) ||(percept != null && !percept.InAccessible)))
			{
				AddNewNode(nodes,node,row,col);
			}
         
			return nodes;
		}

		private void AddNewNode(IList nodes,Node node,int row, int col)
		{
			Location location = new Location();
			location.Row = row;
			location.Column = col;
			Direction newDirection = Direction.South;
			Stack actions = GetActionSequence(node.Location,location,node.Direction,ref newDirection);

			Node newNode = new Node();
			
			newNode.Location = location;
			newNode.Direction = newDirection;
			newNode.ParentNode = node;
			newNode.ExpandedNodes = (Hashtable)node.ExpandedNodes.Clone();
			

			if (_goal == Goal.CleanAllWindows)
			{
				newNode.WindowsCleaned = node.WindowsCleaned;
				Stack windowActions = new Stack();
				newNode.WindowCleanedLocations = (Hashtable)node.WindowCleanedLocations.Clone();
				newNode.WindowsCleaned += CheckForWindows(location,windowActions,newDirection,newNode.WindowCleanedLocations);
				
				object[] actionArray = actions.ToArray();
				for(int i=actionArray.Length-1; i >= 0;i--)
				{
					windowActions.Push(actionArray[i]);
				}

				newNode.Actions = windowActions;
			}
			else
			{
				newNode.Actions = actions;
			}
			nodes.Add(newNode);
		}

		private string GetKey(ActionState actionState)
		{
			int row = 0;
			int col = 0;
			if (actionState.Direction == Direction.South)
			{
				row = actionState.Row + 1;
				col = actionState.Column;
			}
			else if(actionState.Direction == Direction.North)
			{
				row = actionState.Row - 1;
				col = actionState.Column;
			}
			else if(actionState.Direction == Direction.West)
			{
				row = actionState.Row;
				col = actionState.Column - 1;
			}
			else if(actionState.Direction == Direction.East)
			{
				row = actionState.Row;
				col = actionState.Column + 1;
			}
			return string.Format("{0},{1}",row,col);
		}

	}
}
