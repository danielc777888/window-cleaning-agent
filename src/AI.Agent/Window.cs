using System;

namespace AI.Agent
{
	/// <summary>
	/// Summary description for Window.
	/// </summary>
	[Serializable]
	public class Window
	{
		
		private int _dirt = 0;

		public Window( int dirt)
		{
						
			_dirt = dirt;
		}

		public Window()
		{
			
		}

		
		public int Dirt
		{
			get
			{
				return _dirt;
			}
			set
			{
				_dirt = value;
			}
		}

		public bool Clean
		{
			get
			{
				if (_dirt == 0)
				{
					return true;
				}
				else
				{
					return false;
				}
			}
		}	
	}
}
