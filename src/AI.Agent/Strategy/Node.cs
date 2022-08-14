using System;
using System.Collections;

namespace AI.Agent.Strategy
{
	/// <summary>
	/// Summary description for Node.
	/// </summary>
	public class Node
	{
		private Node _parentNode = null;
		private Location _location  = null;
		private Stack _actions = new Stack();
		private Direction _direction = Direction.South;
		private int _windowsCleaned = 0;
		private Hashtable _windowCleanedLocations = null;
		private Hashtable _expandedNodes = null;
        
		public Node()
		{
		
		}

		public Node ParentNode
		{
			get
			{
				return _parentNode;
			}
			set
			{
				_parentNode = value;
			}
		}

		internal Location Location
		{
			get
			{
				return _location;
			}
			set
			{
				_location = value;
			}
		}

		public Stack Actions
		{
			get
			{
				return _actions;
			}
			set
			{
				_actions = value;
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

		public int WindowsCleaned
		{
			get
			{
				return _windowsCleaned;
			}
			set
			{
				_windowsCleaned = value;
			}
		}

		public Hashtable WindowCleanedLocations
		{
			get
			{
				return _windowCleanedLocations;
			}
			set
			{
				_windowCleanedLocations = value;
			}
		}

		public Hashtable ExpandedNodes
		{
			get
			{
				return _expandedNodes;
			}
			set
			{
				_expandedNodes = value;
			}
		}

		public int Depth
		{
			get
			{
				int result = 0;
				Node parent = ParentNode;
				while(parent != null)
				{
					parent = parent.ParentNode;
					result++;
				}
				return result;
			}
		}
	}
}
