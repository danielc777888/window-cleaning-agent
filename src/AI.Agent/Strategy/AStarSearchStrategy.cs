using System;
using System.Collections;

namespace AI.Agent.Strategy
{
	/// <summary>
	/// Summary description for AStartSearchStrategy.
	/// </summary>
	public class AStarSearchStrategy : BreadthFirstStrategy
	{
		private IDictionary _windowLocations = null;
		private IDictionary _obstacleLocations = null;
		private IDictionary _cleanedWindowLocations = null;
		
		public AStarSearchStrategy() : base()
		{
			
		}

		protected override IList GetNodesToExpand(Node node)
		{
			IList result = new ArrayList();
			IList nodesToExpand =  base.GetNodesToExpand (node);
			
			while(nodesToExpand.Count == 0)
			{
				if (node.ParentNode == null)
				{
					return nodesToExpand;
				}

				if (!node.ParentNode.ExpandedNodes.Contains(GetKey(node.Location)))
				{
					node.ParentNode.ExpandedNodes.Add(GetKey(node.Location),true);
				}
				node = node.ParentNode;
				nodesToExpand = base.GetNodesToExpand(node);
				if (nodesToExpand.Count > 0)
				{
					break;
				}
			}
	
			//only select best node to expand using manhatten heuristic
			int currentRow = node.Location.Row;
			int currentCol = node.Location.Column;
			Node bestNode = null;
			int smallestDistance = -1;
			//work out cost for each node
			if (nodesToExpand.Count > 1)
			{
				foreach(Node candidate in nodesToExpand)
				{
					int distance = GetManhattanDistance(candidate);
					
					if (smallestDistance == -1)
					{
						smallestDistance = distance;
						bestNode = candidate;
					}
					else if(distance < smallestDistance)
					{
						smallestDistance = distance;
						bestNode = candidate;
					}
				}
			}
			else if(nodesToExpand.Count == 1)
			{
				bestNode = (Node)nodesToExpand[0];
			}

			if (_goal == Goal.CleanAllWindows)
			{
				//set window as cleaned
				_cleanedWindowLocations = (IDictionary)bestNode.WindowCleanedLocations.Clone();
			}

			result.Add(bestNode);

			return result;
            
		}

		public override void Start(Session session)
		{
			base.Start(session);
		}

		protected override void FindSolution(int initialRow, int initialColumn, Direction direction, Action firstAction)
		{
			
			_windowLocations = new Hashtable();
			_cleanedWindowLocations = new Hashtable();
			_obstacleLocations = new Hashtable();
			if (_goal == Goal.CleanAllWindows)
			{
				IDictionaryEnumerator en =  _perceptLocations.GetEnumerator();
				while(en.MoveNext())
				{
					Percept percept = (Percept)en.Value;

					if (percept.Window != null)
					{
						_windowLocations.Add(en.Key,false);
					}

					if (percept.Obstacle)
					{
						_obstacleLocations.Add(en.Key,true);
					}
				}
			}

			base.FindSolution (initialRow, initialColumn, direction, firstAction);
		}


		private int GetManhattanDistance(Node node)
		{
			int total = 0;
			int currentRow = node.Location.Row;
			int currentCol = node.Location.Column;

			if (_goal == Goal.CleanAllWindows)
			{
				IDictionaryEnumerator en =  _windowLocations.GetEnumerator();
				while(en.MoveNext())
				{
					string key = (string)en.Key;
					//only for windows still needed to be cleaned
					if (!_cleanedWindowLocations.Contains(key))
					{
						string[] location = (key).Split(',');
						int row = int.Parse(location[0]);
						int col = int.Parse(location[1]);
						total += Math.Abs(row - currentRow) + Math.Abs(col - currentCol);
					}
				}
			}
			else if(_goal == Goal.ReturnToBay)
			{
				total = Math.Abs(currentRow - 1) + Math.Abs(currentCol - 1);
			}

			return total;
		}

		private int GetObstaclesInArea(Node node)
		{
			int total =0;
			int currentRow = node.Location.Row;
			int currentCol = node.Location.Column;




			return total;
		}


	}
}
