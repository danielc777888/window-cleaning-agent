using System;

namespace AI.Agent
{
	/// <summary>
	/// Summary description for ActionState.
	/// </summary>
	public class ActionState
	{
		private int _step = 0;
		private int _sessionId =0;
		private Action _action = Action.None;
		private Percept _percept = null;
		private int _row = 0;
		private int _column = 0;
		private Direction _direction = Direction.South;

	
		public ActionState(int sessionId,int step,Action action,Percept percept,int row, int column,Direction direction)
		{
			_step = step;
			_sessionId = sessionId;
			_action = action;
			_percept = percept;
			_row = row;
			_column = column;
			_direction = direction;
		}



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

		public Percept Percept
		{
			get
			{
				return _percept;
			}
			set
			{
				_percept = value;
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

		public int SessionId
		{
			get
			{
				return _sessionId;
			}
			set
			{
				_sessionId  = value;
			}
		}

		public int Step
		{
			get
			{
				return _step;
			}
			set
			{
				_step  = value;
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



	}
}
