using System;

namespace AI.Agent
{
	/// <summary>
	/// Summary description for Percept.
	/// </summary>
	public class Percept
	{
		private bool _bay = false;
		private Window _window = null;
		private bool _buildingEdge = false;
		private bool _obstacle = false;
		//manually specify action
		private Action _action = Action.None;
		
		
		public Action Action
		{
			get
			{
				return _action;
			}
			set
			{
				_action = value;
			}
		}

		public bool Bay
		{
			get
			{
				return _bay;
			}
			set
			{
				_bay = value;
			}
		}

		public Window Window
		{
			get
			{
				return _window;
			}
			set
			{
				_window = value;
			}
		}

		public bool BuildingEdge
		{
			get
			{
				return _buildingEdge;
			}
			set
			{
				_buildingEdge = value;
			}
			
		}

		public bool Obstacle
		{
			get
			{
				return _obstacle;
			}
			set
			{
				_obstacle = value;
			}
		}

		public bool InAccessible
		{
			get
			{
				return (BuildingEdge || Window != null || Obstacle);
			}
		}

	}
}
