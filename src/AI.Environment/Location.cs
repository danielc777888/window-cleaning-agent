using System;

using AI.Agent;

namespace AI.Environment
{
	/// <summary>
	/// Summary description for Location.
	/// </summary>
	[Serializable]
	public class Location
	{
		private Window _window;
		private bool _bay;
		private bool _buildingEdge = false;
		private bool _obstacle =false;

		public Location()
		{
			
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
	}
}
