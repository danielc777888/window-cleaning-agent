using System;

using AI.Agent.Strategy;

namespace AI.Agent
{
	/// <summary>
	/// Summary description for Session.
	/// </summary>
	public class Session
	{
		private int _id = 0;
		private int _agentId = 0;
		private int _buildingId = 0;
		private SearchStrategy _searchStrategy = null;
		private DateTime _startTime = DateTime.MinValue;
		private DateTime _endTime = DateTime.MinValue;
		private int _stepCost = 0;
		private int _windowsCleaned = 0;
		private int _windowsToClean = 0;
		private int _tick = 0;

		public Session()
		{
			
		}

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

		public int AgentId
		{
			get
			{
				return _agentId;
			}
			set
			{
				_agentId = value;
			}
		}

		public int BuildingId
		{
			get
			{
				return _buildingId;
			}
			set
			{
				_buildingId = value;
			}
		}

		public SearchStrategy SearchStrategy
		{
			get
			{
				return _searchStrategy;
			}
			set
			{
				_searchStrategy = value;
			}
		}

		public string StrategyName
		{
			get
			{
				return _searchStrategy.GetType().Name;
			}
		}

			public DateTime StartTime
		{
			get
			{
				return _startTime;
			}
			set
			{
				_startTime = value;
			}
		}

		public DateTime EndTime
		{
			get
			{
				return _endTime;
			}
			set
			{
				_endTime = value;
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

		public int WindowsToClean
		{
			get
			{
				return _windowsToClean;
			}
			set
			{
				_windowsToClean = value;
			}
		}

		
		public bool CleanedAllWindows
		{
			get
			{
				return _windowsToClean == _windowsCleaned;
			}
		}

		public int StepCost
		{
			get
			{
				return _stepCost;
			}
			set
			{
				_stepCost = value;
			}
		}

		public int Tick
		{
			get
			{
				return _tick;
			}
			set
			{
				_tick = value;
			}
		}

		public TimeSpan Duration
		{
			get
			{
				if (!EndTime.Equals(DateTime.MinValue))
				{
					return EndTime.Subtract(StartTime);
				}
				else
				{
					return DateTime.Now.Subtract(StartTime);
				}
			}
		}
	}
}
