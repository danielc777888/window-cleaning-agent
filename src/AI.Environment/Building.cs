using System;
using System.Collections;

using AI.Agent;

namespace AI.Environment
{
	[Serializable]
	public enum Arrangement
	{
		Uniform = 0,
		Irregular = 1
	}
	/// <summary>
	/// Summary description for Building.
	/// </summary>
	[Serializable]
	public class Building
	{
		private int _id =0;
		private string _name;
		private int _width = 0;
		private int _height = 0;
		private int _x = 0;
		private int _y = 0;
		private int _columns = 0;
		private int _rows = 0;
		private Location[,] _locations = null;
		private int _totalWindows = 0;
		private int _totalObstacles = 0;
		private Arrangement _arrangement = Arrangement.Uniform;

		

        public static Building Create(string name,int x, int y,int width, int height,int cols, int rows, int numWindows, int numObstacles, Arrangement arrangement)
		{


			Building building = new Building();
			building.X = x;
			building.Y = y;
			building.Width = width;
			building.Height = height;
			building.Columns = cols;
			building.Rows = rows;
			building.TotalWindows = numWindows;
			building.TotalObstacles = numObstacles;
			building.Arrangement = arrangement;
			building.Locations = CreateLocations(building);
			building.Id = 1;
			building.Name = name;
			
			
			return building;
		}

		public static Location[,] CreateLocations(Building building)
		{
			int rows = building.Rows;
			int cols = building.Columns;
			Location[,] locations = new Location[cols,rows];
			Random random = new Random();

			int offSetY =building.Y + Environment.UNIT_HEIGHT;
			int createdWindows = 0;

			for (int i=0; i < rows;i++)
			{
				for (int j=0; j < cols;j++)
				{
					
					Location location = new Location();
					
					if (i == 1 && j == 0)
					{
						location.Bay = true;
					}
					else if(i == 0 || i == cols -1 || j == 0 || j == rows - 1)
					{
						location.BuildingEdge = true;
					}
					//if row, col a multiple of 2 and not a building edge
					else 
					{
						
							if (building.Arrangement == Arrangement.Uniform && createdWindows < building.TotalWindows)
						
							{
								if( (i != 0 && (i % 2) == 0 && i != cols - 1) && (j != 0 && (j % 2) == 0) && j != j -1)
								{
									location.Window = new Window(random.Next(1,6));
									createdWindows++;
								}
							}
					}

                  locations[i,j] = location;
                    					
				}
				
			}

			//now try to allocation obstacles randomly
			int allocatedObstacles = 0;
		
			while(allocatedObstacles < building.TotalObstacles)
			{
				int row = random.Next(1,rows - 1);
				int col = random.Next(1,cols - 1);

				Location location = locations[row,col];

				if (location.Window == null && !location.Obstacle)
				{
					location.Obstacle =true;
					allocatedObstacles++;
				}
			}

			//if arrangement is irregular, try to allocate windows randomly
			if (building.Arrangement == Arrangement.Irregular)
			{
				int allocatedWindows = 0;
		
				while(allocatedWindows < building.TotalWindows)
				{
					int row = random.Next(1,rows - 1);
					int col = random.Next(1,cols - 1);

					Location location = locations[row,col];

					if (location.Window == null && !location.Obstacle)
					{
						location.Window =new Window(random.Next(1,6));
						allocatedWindows++;
					}
				}
			}

			return locations;

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

		public string Name
		{
			get
			{
				return _name;
			}
			set
			{
				_name = value;
			}
		}
	
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
				_height = value;
			}
		}

		public int X
		{
			get
			{
				return _x;
			}
			set
			{
				_x = value;
			}
		}

		public int Y
		{
			get
			{
				return _y;
			}
			set
			{
				_y = value;
			}
		}

		public int Rows
		{
			get
			{
				return _rows;
			}
			set
			{
				_rows = value;
			}
		}

		public int Columns
		{
			get
			{
				return _columns;
			}
			set
			{
				_columns = value;
			}
		}


		public Location[,] Locations
		{
			get
			{
				return _locations;
			}
			set
			{
				_locations = value;
			}
			
		}	

		public int TotalWindows
		{
			get
			{
				return _totalWindows;
			}
			set
			{
				_totalWindows =value;
			}
		}

		public int TotalObstacles
		{
			get
			{
				return _totalObstacles;
			}
			set
			{
				_totalObstacles =value;
			}
		}

		public Arrangement Arrangement
		{
			get
			{
				return _arrangement;
			}
			set
			{
				_arrangement = value;
			}
		}



		
		public Location GetFacingLocation(int col, int row, Direction direction)
		{
			Location location = null;
			if (direction == Direction.South)
			{
				location = Locations[col,row +1];
			}
			else if(direction == Direction.North)
			{
				location = Locations[col,row -1];
			}
			else if(direction == Direction.West)
			{
				location = Locations[col -1,row];
			}
			else if(direction == Direction.East)
			{
				location = Locations[col + 1,row];
			}
			return location;
		}
	}
}
