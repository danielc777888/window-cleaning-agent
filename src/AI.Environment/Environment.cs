using System;

namespace AI.Environment
{
	/// <summary>
	/// Summary description for Class1.
	/// </summary>
	[Serializable]
	public class Environment
	{
		private Building _building;
		public const int UNIT_WIDTH = 25;
		public const int UNIT_HEIGHT = 25;
		private int _width = 0;
		private int _height = 0;

		public int Width
		{
			get
			{
				return _width;
			}
			set
			{
				_width = value;
			}
		}

		public int Height
		{
			get
			{
				return _height;
			}
			set
			{
				_height =value;
			}

		}	

		public Building Building
		{
			get
			{
				return _building;
			}
			set
			{
				_building = value;
			}
		}

		public static Environment Generate(string name,int width,int height,int cols, int rows,int numWindows,int numObstacles,Arrangement arrangement)
		{
			
			Environment environment = new Environment();
			environment.Width = width;
			environment.Height = height;
			int buildingWidth = UNIT_WIDTH * cols;
			int buildingHeight = UNIT_HEIGHT * rows;
			int buildingX = (width - buildingWidth)/ 2;
			int buildingY = height - buildingHeight;
			environment.Building = Building.Create(name,buildingX,buildingY,buildingWidth,buildingHeight,cols,rows,numWindows,numObstacles,arrangement);
			environment.Building.Name = name;
			return environment;
		}
	}
}
