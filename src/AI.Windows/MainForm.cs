using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Threading;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Formatters;
using System.Runtime.Serialization;

using AI.Environment;
using AI.Agent;
using AI.Agent.Strategy;
using AI.Agent.DataAccess;

namespace AI.Windows
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class MainForm : System.Windows.Forms.Form
	{
		private System.Windows.Forms.PictureBox pEnvironment;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.Button btnStartAgent;
		private System.Windows.Forms.Button btnEndAgent;

		private Environment.Environment _environment = null;
		
		private WindowCleaningAgent _agent = null;
		private AgentDAO _agentDAO = new AgentDAO();
		private Action _action = Action.None;

		private System.Windows.Forms.Panel pnlCommand;
		private System.Windows.Forms.RadioButton rdbDFS;
		private System.Windows.Forms.Label lblOnlineSearch;
		private System.Windows.Forms.RadioButton rdbLRTA;
		private System.Windows.Forms.RadioButton rdbDepthFirst;
		private System.Windows.Forms.Label lblOfflineSearch;
		private System.Windows.Forms.RadioButton rdbBreadthFirst;
		private System.Windows.Forms.RadioButton rdbAstar;
		private System.Windows.Forms.RadioButton rdbRandom;
		private System.Windows.Forms.Label lblOther;
		private System.Windows.Forms.RadioButton rdbManual;
		private bool _endAgent = false;
		private System.Windows.Forms.TextBox txtTick;
		private System.Windows.Forms.Label lblTick;
		private System.Windows.Forms.Label lblSeconds;
		private const int DEFAULT_TICK = 100;
		private System.Windows.Forms.TextBox txtInformation;
		private System.Windows.Forms.Panel pnlStats;
		private System.Windows.Forms.Label lblGoal;
		private System.Windows.Forms.Label lblTime;
		private System.Windows.Forms.Label lblCleaned;
		private int _tick = DEFAULT_TICK;
		private System.Windows.Forms.Label lblStepCost;
		private System.Windows.Forms.Panel pnlStrategies;
		private System.Windows.Forms.Button btnRestoreEnvironment;
		private System.Windows.Forms.DataGrid dtgSessions;
		private System.Windows.Forms.RadioButton rdbReplay;

		private static string FOLDER = "C:\\";
		
		public MainForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
			CreateMainMenu();
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.pEnvironment = new System.Windows.Forms.PictureBox();
			this.btnStartAgent = new System.Windows.Forms.Button();
			this.btnEndAgent = new System.Windows.Forms.Button();
			this.pnlCommand = new System.Windows.Forms.Panel();
			this.btnRestoreEnvironment = new System.Windows.Forms.Button();
			this.lblSeconds = new System.Windows.Forms.Label();
			this.lblTick = new System.Windows.Forms.Label();
			this.txtTick = new System.Windows.Forms.TextBox();
			this.rdbDFS = new System.Windows.Forms.RadioButton();
			this.lblOnlineSearch = new System.Windows.Forms.Label();
			this.rdbLRTA = new System.Windows.Forms.RadioButton();
			this.pnlStrategies = new System.Windows.Forms.Panel();
			this.rdbManual = new System.Windows.Forms.RadioButton();
			this.lblOther = new System.Windows.Forms.Label();
			this.rdbRandom = new System.Windows.Forms.RadioButton();
			this.rdbAstar = new System.Windows.Forms.RadioButton();
			this.rdbDepthFirst = new System.Windows.Forms.RadioButton();
			this.lblOfflineSearch = new System.Windows.Forms.Label();
			this.rdbBreadthFirst = new System.Windows.Forms.RadioButton();
			this.txtInformation = new System.Windows.Forms.TextBox();
			this.pnlStats = new System.Windows.Forms.Panel();
			this.lblStepCost = new System.Windows.Forms.Label();
			this.lblCleaned = new System.Windows.Forms.Label();
			this.lblTime = new System.Windows.Forms.Label();
			this.lblGoal = new System.Windows.Forms.Label();
			this.dtgSessions = new System.Windows.Forms.DataGrid();
			this.rdbReplay = new System.Windows.Forms.RadioButton();
			this.pnlCommand.SuspendLayout();
			this.pnlStrategies.SuspendLayout();
			this.pnlStats.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dtgSessions)).BeginInit();
			this.SuspendLayout();
			// 
			// pEnvironment
			// 
			this.pEnvironment.BackColor = System.Drawing.Color.DeepSkyBlue;
			this.pEnvironment.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.pEnvironment.Location = new System.Drawing.Point(-8, 0);
			this.pEnvironment.Name = "pEnvironment";
			this.pEnvironment.Size = new System.Drawing.Size(648, 520);
			this.pEnvironment.TabIndex = 0;
			this.pEnvironment.TabStop = false;
			this.pEnvironment.Click += new System.EventHandler(this.pEnvironment_Click);
			this.pEnvironment.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.pEnvironment_KeyPress);
			// 
			// btnStartAgent
			// 
			this.btnStartAgent.Location = new System.Drawing.Point(16, 16);
			this.btnStartAgent.Name = "btnStartAgent";
			this.btnStartAgent.TabIndex = 1;
			this.btnStartAgent.Text = "Start Agent";
			this.btnStartAgent.Click += new System.EventHandler(this.btnStartAgent_Click);
			// 
			// btnEndAgent
			// 
			this.btnEndAgent.Enabled = false;
			this.btnEndAgent.Location = new System.Drawing.Point(16, 48);
			this.btnEndAgent.Name = "btnEndAgent";
			this.btnEndAgent.TabIndex = 2;
			this.btnEndAgent.Text = "End Agent";
			this.btnEndAgent.Click += new System.EventHandler(this.btnEndAgent_Click);
			// 
			// pnlCommand
			// 
			this.pnlCommand.Controls.Add(this.btnRestoreEnvironment);
			this.pnlCommand.Controls.Add(this.lblSeconds);
			this.pnlCommand.Controls.Add(this.lblTick);
			this.pnlCommand.Controls.Add(this.txtTick);
			this.pnlCommand.Controls.Add(this.btnStartAgent);
			this.pnlCommand.Controls.Add(this.btnEndAgent);
			this.pnlCommand.Location = new System.Drawing.Point(664, 336);
			this.pnlCommand.Name = "pnlCommand";
			this.pnlCommand.Size = new System.Drawing.Size(296, 176);
			this.pnlCommand.TabIndex = 3;
			// 
			// btnRestoreEnvironment
			// 
			this.btnRestoreEnvironment.Location = new System.Drawing.Point(16, 80);
			this.btnRestoreEnvironment.Name = "btnRestoreEnvironment";
			this.btnRestoreEnvironment.Size = new System.Drawing.Size(80, 32);
			this.btnRestoreEnvironment.TabIndex = 6;
			this.btnRestoreEnvironment.Text = "Restore Environment";
			this.btnRestoreEnvironment.Click += new System.EventHandler(this.btnRestoreEnvironment_Click);
			// 
			// lblSeconds
			// 
			this.lblSeconds.Location = new System.Drawing.Point(176, 16);
			this.lblSeconds.Name = "lblSeconds";
			this.lblSeconds.Size = new System.Drawing.Size(24, 16);
			this.lblSeconds.TabIndex = 5;
			this.lblSeconds.Text = "ms";
			// 
			// lblTick
			// 
			this.lblTick.Location = new System.Drawing.Point(104, 16);
			this.lblTick.Name = "lblTick";
			this.lblTick.Size = new System.Drawing.Size(32, 16);
			this.lblTick.TabIndex = 4;
			this.lblTick.Text = "Tick";
			// 
			// txtTick
			// 
			this.txtTick.Location = new System.Drawing.Point(136, 16);
			this.txtTick.MaxLength = 4;
			this.txtTick.Name = "txtTick";
			this.txtTick.Size = new System.Drawing.Size(32, 20);
			this.txtTick.TabIndex = 3;
			this.txtTick.Text = "100";
			this.txtTick.TextChanged += new System.EventHandler(this.txtTick_TextChanged);
			// 
			// rdbDFS
			// 
			this.rdbDFS.Checked = true;
			this.rdbDFS.Location = new System.Drawing.Point(16, 152);
			this.rdbDFS.Name = "rdbDFS";
			this.rdbDFS.TabIndex = 0;
			this.rdbDFS.TabStop = true;
			this.rdbDFS.Text = "Depth First";
			// 
			// lblOnlineSearch
			// 
			this.lblOnlineSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblOnlineSearch.Location = new System.Drawing.Point(16, 128);
			this.lblOnlineSearch.Name = "lblOnlineSearch";
			this.lblOnlineSearch.Size = new System.Drawing.Size(104, 16);
			this.lblOnlineSearch.TabIndex = 1;
			this.lblOnlineSearch.Text = "Online Search";
			// 
			// rdbLRTA
			// 
			this.rdbLRTA.Enabled = false;
			this.rdbLRTA.Location = new System.Drawing.Point(16, 184);
			this.rdbLRTA.Name = "rdbLRTA";
			this.rdbLRTA.TabIndex = 2;
			this.rdbLRTA.Text = "LRTA*";
			// 
			// pnlStrategies
			// 
			this.pnlStrategies.Controls.Add(this.rdbReplay);
			this.pnlStrategies.Controls.Add(this.rdbManual);
			this.pnlStrategies.Controls.Add(this.lblOther);
			this.pnlStrategies.Controls.Add(this.rdbRandom);
			this.pnlStrategies.Controls.Add(this.rdbAstar);
			this.pnlStrategies.Controls.Add(this.rdbDepthFirst);
			this.pnlStrategies.Controls.Add(this.lblOfflineSearch);
			this.pnlStrategies.Controls.Add(this.rdbBreadthFirst);
			this.pnlStrategies.Controls.Add(this.lblOnlineSearch);
			this.pnlStrategies.Controls.Add(this.rdbDFS);
			this.pnlStrategies.Controls.Add(this.rdbLRTA);
			this.pnlStrategies.Location = new System.Drawing.Point(688, 8);
			this.pnlStrategies.Name = "pnlStrategies";
			this.pnlStrategies.Size = new System.Drawing.Size(136, 320);
			this.pnlStrategies.TabIndex = 5;
			// 
			// rdbManual
			// 
			this.rdbManual.Location = new System.Drawing.Point(16, 272);
			this.rdbManual.Name = "rdbManual";
			this.rdbManual.TabIndex = 6;
			this.rdbManual.Text = "Manual";
			// 
			// lblOther
			// 
			this.lblOther.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblOther.Location = new System.Drawing.Point(16, 248);
			this.lblOther.Name = "lblOther";
			this.lblOther.Size = new System.Drawing.Size(104, 24);
			this.lblOther.TabIndex = 5;
			this.lblOther.Text = "Other";
			// 
			// rdbRandom
			// 
			this.rdbRandom.Location = new System.Drawing.Point(16, 216);
			this.rdbRandom.Name = "rdbRandom";
			this.rdbRandom.TabIndex = 4;
			this.rdbRandom.Text = "Random";
			// 
			// rdbAstar
			// 
			this.rdbAstar.Location = new System.Drawing.Point(16, 80);
			this.rdbAstar.Name = "rdbAstar";
			this.rdbAstar.TabIndex = 3;
			this.rdbAstar.Text = "A*";
			// 
			// rdbDepthFirst
			// 
			this.rdbDepthFirst.Enabled = false;
			this.rdbDepthFirst.Location = new System.Drawing.Point(16, 56);
			this.rdbDepthFirst.Name = "rdbDepthFirst";
			this.rdbDepthFirst.TabIndex = 2;
			this.rdbDepthFirst.Text = "Depth First";
			// 
			// lblOfflineSearch
			// 
			this.lblOfflineSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblOfflineSearch.Location = new System.Drawing.Point(16, 8);
			this.lblOfflineSearch.Name = "lblOfflineSearch";
			this.lblOfflineSearch.Size = new System.Drawing.Size(104, 16);
			this.lblOfflineSearch.TabIndex = 1;
			this.lblOfflineSearch.Text = "Offline Search";
			// 
			// rdbBreadthFirst
			// 
			this.rdbBreadthFirst.Location = new System.Drawing.Point(16, 32);
			this.rdbBreadthFirst.Name = "rdbBreadthFirst";
			this.rdbBreadthFirst.TabIndex = 0;
			this.rdbBreadthFirst.Text = "Breadth First";
			// 
			// txtInformation
			// 
			this.txtInformation.BackColor = System.Drawing.SystemColors.InfoText;
			this.txtInformation.ForeColor = System.Drawing.Color.Lime;
			this.txtInformation.Location = new System.Drawing.Point(8, 536);
			this.txtInformation.Multiline = true;
			this.txtInformation.Name = "txtInformation";
			this.txtInformation.ReadOnly = true;
			this.txtInformation.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.txtInformation.Size = new System.Drawing.Size(312, 168);
			this.txtInformation.TabIndex = 6;
			this.txtInformation.Text = "";
			// 
			// pnlStats
			// 
			this.pnlStats.Controls.Add(this.lblStepCost);
			this.pnlStats.Controls.Add(this.lblCleaned);
			this.pnlStats.Controls.Add(this.lblTime);
			this.pnlStats.Controls.Add(this.lblGoal);
			this.pnlStats.Location = new System.Drawing.Point(328, 536);
			this.pnlStats.Name = "pnlStats";
			this.pnlStats.Size = new System.Drawing.Size(200, 168);
			this.pnlStats.TabIndex = 7;
			// 
			// lblStepCost
			// 
			this.lblStepCost.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblStepCost.Location = new System.Drawing.Point(16, 72);
			this.lblStepCost.Name = "lblStepCost";
			this.lblStepCost.Size = new System.Drawing.Size(176, 32);
			this.lblStepCost.TabIndex = 3;
			this.lblStepCost.Text = "Step Cost:";
			// 
			// lblCleaned
			// 
			this.lblCleaned.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblCleaned.Location = new System.Drawing.Point(16, 40);
			this.lblCleaned.Name = "lblCleaned";
			this.lblCleaned.Size = new System.Drawing.Size(176, 24);
			this.lblCleaned.TabIndex = 2;
			this.lblCleaned.Text = "Cleaned:";
			// 
			// lblTime
			// 
			this.lblTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblTime.Location = new System.Drawing.Point(16, 136);
			this.lblTime.Name = "lblTime";
			this.lblTime.Size = new System.Drawing.Size(176, 24);
			this.lblTime.TabIndex = 1;
			this.lblTime.Text = "Time:";
			// 
			// lblGoal
			// 
			this.lblGoal.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblGoal.Location = new System.Drawing.Point(16, 8);
			this.lblGoal.Name = "lblGoal";
			this.lblGoal.Size = new System.Drawing.Size(176, 24);
			this.lblGoal.TabIndex = 0;
			this.lblGoal.Text = "Goal:";
			// 
			// dtgSessions
			// 
			this.dtgSessions.CaptionText = "Sessions";
			this.dtgSessions.DataMember = "";
			this.dtgSessions.HeaderForeColor = System.Drawing.SystemColors.ControlText;
			this.dtgSessions.Location = new System.Drawing.Point(536, 536);
			this.dtgSessions.Name = "dtgSessions";
			this.dtgSessions.Size = new System.Drawing.Size(448, 176);
			this.dtgSessions.TabIndex = 8;
			// 
			// rdbReplay
			// 
			this.rdbReplay.Location = new System.Drawing.Point(16, 296);
			this.rdbReplay.Name = "rdbReplay";
			this.rdbReplay.Size = new System.Drawing.Size(104, 16);
			this.rdbReplay.TabIndex = 7;
			this.rdbReplay.Text = "Replay";
			// 
			// MainForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.BackColor = System.Drawing.SystemColors.ControlDark;
			this.ClientSize = new System.Drawing.Size(992, 716);
			this.Controls.Add(this.dtgSessions);
			this.Controls.Add(this.pnlStats);
			this.Controls.Add(this.txtInformation);
			this.Controls.Add(this.pnlStrategies);
			this.Controls.Add(this.pEnvironment);
			this.Controls.Add(this.pnlCommand);
			this.Name = "MainForm";
			this.Text = "Window Cleaning Agent";
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			this.pnlCommand.ResumeLayout(false);
			this.pnlStrategies.ResumeLayout(false);
			this.pnlStats.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dtgSessions)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		public void CreateMainMenu()
		{
			MainMenu mainMenu = new MainMenu();
			MenuItem menuItemEnv = new MenuItem("Environment");

			MenuItem menuItemEnv_New = new MenuItem("New");
			menuItemEnv_New.Click +=new EventHandler(menuItemEnv_New_Click);

			MenuItem menuItemEnv_Load = new MenuItem("Load");
			menuItemEnv_Load.Click += new EventHandler(menuItemEnv_Load_Click);

			menuItemEnv.MenuItems.Add(menuItemEnv_New);
			menuItemEnv.MenuItems.Add(menuItemEnv_Load);
			mainMenu.MenuItems.Add(menuItemEnv);

			this.Menu = mainMenu;
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint (e);
			if (_environment != null)
			{
				DrawEnvironment();
			}
		}

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new MainForm());
		}

		#region Drawing methods

		private void DrawEnvironment()
		{
			Brush brush = new SolidBrush(Color.Yellow);
			Brush blackBrush = new SolidBrush(Color.Black);
			Pen pen = new Pen(Color.Black);
			Font f = new Font("Ariel",5,FontStyle.Bold);

			Graphics g = pEnvironment.CreateGraphics();

				//draw building
				g.FillRectangle(brush,_environment.Building.X + Environment.Environment.UNIT_WIDTH,_environment.Building.Y + Environment.Environment.UNIT_HEIGHT,_environment.Building.Width - (Environment.Environment.UNIT_WIDTH*2),_environment.Building.Height - (Environment.Environment.UNIT_HEIGHT*2));
				g.DrawRectangle(pen,_environment.Building.X+ Environment.Environment.UNIT_WIDTH,_environment.Building.Y+ Environment.Environment.UNIT_HEIGHT,_environment.Building.Width - (Environment.Environment.UNIT_WIDTH*2),_environment.Building.Height - (Environment.Environment.UNIT_HEIGHT*2));

			 //draw basement
			g.FillRectangle(blackBrush,_environment.Building.X + Environment.Environment.UNIT_WIDTH,_environment.Building.Y +_environment.Building.Height - Environment.Environment.UNIT_HEIGHT,_environment.Building.Width - (Environment.Environment.UNIT_WIDTH*2),Environment.Environment.UNIT_HEIGHT); 

				Brush dirtyWindow = new SolidBrush(Color.Brown);
				Brush cleanWindow = new SolidBrush(Color.Blue);
				Brush obstacleBrush =new SolidBrush(Color.Orange);

				//draw locations of building
			for (int i=0; i < _environment.Building.Locations.GetUpperBound(0);i++)
			{
				for (int j=0; j < _environment.Building.Locations.GetUpperBound(1);j++)
			   {
					Location location = _environment.Building.Locations[i,j];
					if (location.Bay)
					{
						//draw bay;
						Brush bayBrush = new SolidBrush(Color.Black);
						g.FillRectangle(bayBrush,_environment.Building.X+ Environment.Environment.UNIT_HEIGHT,_environment.Building.Y,
							Environment.Environment.UNIT_WIDTH,Environment.Environment.UNIT_HEIGHT);
					}
					else if(location.BuildingEdge)
					{

					}
					else if(location.Window != null)
					{
						int x = _environment.Building.X + (Environment.Environment.UNIT_WIDTH * i);
						int y = _environment.Building.Y + (Environment.Environment.UNIT_HEIGHT * j);
						int width = Environment.Environment.UNIT_WIDTH;
						int height = Environment.Environment.UNIT_HEIGHT;
						if (!location.Window.Clean)
						{
							g.FillRectangle(dirtyWindow,x,y,width,height);
							g.DrawRectangle(pen,x,y,width,height);
						}
						else
						{
							g.FillRectangle(cleanWindow,x,y,width,height);
							g.DrawRectangle(pen,x,y,width,height);
						}
						//draw dirt level
						g.DrawString(location.Window.Dirt.ToString(),f,blackBrush,x + (width/2), y + (height/2));

					}
					else if(location.Obstacle)
					{
						int x = _environment.Building.X + (Environment.Environment.UNIT_WIDTH * i);
						int y = _environment.Building.Y + (Environment.Environment.UNIT_HEIGHT * j);
						int width = Environment.Environment.UNIT_WIDTH;
						int height = Environment.Environment.UNIT_HEIGHT;
						g.FillRectangle(obstacleBrush,x,y,width,height);
						g.DrawRectangle(pen,x,y,width,height);
					}
			   }
			}
				

				Pen gridPen = new Pen(Color.Gray);
				//draw row unit grids
				for (int i = 0; i < _environment.Building.Locations.GetUpperBound(1);i++)
				{
					if (i != 0 && i != _environment.Building.Locations.GetUpperBound(1) -1)
					{
						int x1 = _environment.Building.X + Environment.Environment.UNIT_WIDTH;
						int y1 = _environment.Building.Y + (Environment.Environment.UNIT_HEIGHT * i) + Environment.Environment.UNIT_HEIGHT;
						int x2 = _environment.Building.X + _environment.Building.Width - (Environment.Environment.UNIT_WIDTH);
						int y2 = y1;
						g.DrawLine(gridPen,x1,y1,x2,y2);
					}
				}
	
				//draw col unit grids
			for (int i = 0; i < _environment.Building.Locations.GetUpperBound(0);i++)
			{
				if (i != 0 && i != _environment.Building.Locations.GetUpperBound(0) -1)
				{
					int x1 = _environment.Building.X + Environment.Environment.UNIT_WIDTH + (Environment.Environment.UNIT_WIDTH * i);
					int y1 = _environment.Building.Y + Environment.Environment.UNIT_HEIGHT;
					int x2 = x1;
					int y2 = _environment.Building.Y + _environment.Building.Height - (Environment.Environment.UNIT_HEIGHT);
					g.DrawLine(gridPen,x1,y1,x2,y2);
				}
				}
				

			//draw Agent
			if (_agent != null)
			{
				DrawAgent();
			}


		}

		private void DrawAgent()
		{
			Brush brush = new SolidBrush(Color.Red);
			Brush cleaningBrush = new SolidBrush(Color.White);
			
			Graphics g = pEnvironment.CreateGraphics();
					
			int x = (_agent.Column * Environment.Environment.UNIT_WIDTH) + _environment.Building.X;
			int y = (_agent.Row * Environment.Environment.UNIT_HEIGHT) + _environment.Building.Y;
			int width = 0;
			int height = 0;
			int cleanBrushWidth = 4;

			if (!_agent.InBay)
			{	
				//draw agent
				if (_agent.Direction == Direction.South)
				{
					width = Environment.Environment.UNIT_WIDTH /2;
					height = Environment.Environment.UNIT_HEIGHT - (Environment.Environment.UNIT_HEIGHT/4);
					x = x + (width /2);
					y = y + (Environment.Environment.UNIT_HEIGHT/4);
					g.FillRectangle(brush,x,y,width,height);
					g.FillRectangle(cleaningBrush,x,y + (height-cleanBrushWidth),width,cleanBrushWidth);
				}
				else if(_agent.Direction == Direction.East)
				{
					height = Environment.Environment.UNIT_WIDTH /2;
					width = Environment.Environment.UNIT_HEIGHT - (Environment.Environment.UNIT_HEIGHT/4);
					x = x + (width /2);
					y = y + (Environment.Environment.UNIT_HEIGHT/4);
					g.FillRectangle(brush,x,y,width,height);
					g.FillRectangle(cleaningBrush,x + width -cleanBrushWidth,y,cleanBrushWidth,height);

				}
				else if(_agent.Direction == Direction.West)
				{
					height = Environment.Environment.UNIT_WIDTH /2;
					width = Environment.Environment.UNIT_HEIGHT - (Environment.Environment.UNIT_HEIGHT/4);
					x = x;
					y = y + (Environment.Environment.UNIT_HEIGHT/4);
					g.FillRectangle(brush,x,y,width,height);
					g.FillRectangle(cleaningBrush,x,y,cleanBrushWidth,height);

				}
				else if(_agent.Direction == Direction.North)
				{
					width = Environment.Environment.UNIT_WIDTH /2;
					height = Environment.Environment.UNIT_HEIGHT - (Environment.Environment.UNIT_HEIGHT/4);
					x = x + (width /2);
					y = y + (Environment.Environment.UNIT_HEIGHT/4);
					g.FillRectangle(brush,x,y,width,height);
					g.FillRectangle(cleaningBrush,x,y,width,cleanBrushWidth);
				}
			}
		}

		#endregion

		#region Menu events

		private void menuItemEnv_New_Click(object sender, EventArgs e)
		{
			NewEnvironmentForm eForm = new NewEnvironmentForm();

			if (eForm.ShowDialog(this) == DialogResult.OK)
			{
				string name = eForm.txtBuildingName.Text;
				int rows = 0;
				int cols = 0;
				Arrangement arr = Arrangement.Uniform;

				if (eForm.rdbIrregular.Checked)
				{
					arr = Arrangement.Irregular;
				}

				if (eForm.rdbSmall.Checked)
				{
					rows = 7;
					cols = 7;
				}
				else if(eForm.rdbMedium.Checked)
				{
					rows = 11;
					cols = 11;
				}
				else if(eForm.rdbLarge.Checked)
				{
					rows = 15;
					cols = 15;
				}

				int numWindows = int.Parse(eForm.txtNumWindows.Text);
				int numObstacles = int.Parse(eForm.txtNumObstacles.Text);

				_environment = Environment.Environment.Generate(name,pEnvironment.Width,pEnvironment.Height,cols,rows,numWindows,numObstacles,arr);
				//save building to db
				BuildingDAO buildingDAO = new BuildingDAO();
				_environment.Building.Id = buildingDAO.Insert(_environment.Building.Name);

				//save environment as xml file
				IFormatter formatter = new BinaryFormatter();
			
				Stream stream = new FileStream(string.Format("{0}\\{1}.env",FOLDER,name), FileMode.Create, FileAccess.Write, FileShare.None);
				formatter.Serialize(stream, _environment);
				stream.Close();
			
				DrawEnvironment();
			}

			eForm.Dispose();

		}

		private void menuItemEnv_Load_Click(object sender, EventArgs e)
		{
			OpenFileDialog openFileDialog1 = new OpenFileDialog();

			openFileDialog1.InitialDirectory = FOLDER ;
			openFileDialog1.Filter = "env files (*.env)|*.env" ;
			openFileDialog1.RestoreDirectory = true ;

			if(openFileDialog1.ShowDialog() == DialogResult.OK)
			{
				RestoreEnvironment(openFileDialog1.FileName);
			}

		}

		private void RestoreEnvironment(string fileName)
		{
			IFormatter formatter = new BinaryFormatter();
			Stream stream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read);
			_environment = (Environment.Environment) formatter.Deserialize(stream);

			this.Text = "Window Cleaning Agent : " + _environment.Building.Name;
			stream.Close();

			//get sessions
			SessionDAO sessionDAO = new SessionDAO();
			IList sessions  = sessionDAO.FindByBuilding(_environment.Building.Id);

			dtgSessions.TableStyles.Clear();

			dtgSessions.DataSource = (ArrayList)sessions;
			
			DataGridTableStyle tsSessions = new DataGridTableStyle();
			tsSessions.MappingName = "ArrayList";

			DataGridColumnStyle id = new DataGridTextBoxColumn();
			id.MappingName = "Id";
			id.HeaderText = "Id";
			id.Width = 0;
			tsSessions.GridColumnStyles.Add(id);

			DataGridColumnStyle strategy = new DataGridTextBoxColumn();
			strategy.MappingName = "StrategyName";
			strategy.HeaderText = "Strategy";
			strategy.Width = 150;
			tsSessions.GridColumnStyles.Add(strategy);

			DataGridColumnStyle duration = new DataGridTextBoxColumn();
			duration.MappingName = "Duration";
			duration.HeaderText = "Duration";
			duration.Width = 50;
			tsSessions.GridColumnStyles.Add(duration);

			DataGridColumnStyle stepCost = new DataGridTextBoxColumn();
			stepCost.MappingName = "StepCost";
			stepCost.HeaderText = "Step Cost";
			stepCost.Width = 50;
			tsSessions.GridColumnStyles.Add(stepCost);

			DataGridColumnStyle windowsCleaned = new DataGridTextBoxColumn();
			windowsCleaned.MappingName = "WindowsCleaned";
			windowsCleaned.HeaderText = "Windows Cleaned";
			windowsCleaned.Width = 100;
			tsSessions.GridColumnStyles.Add(windowsCleaned);

			DataGridColumnStyle tick = new DataGridTextBoxColumn();
			tick.MappingName = "Tick";
			tick.HeaderText = "Tick";
			tick.Width = 50;
			tsSessions.GridColumnStyles.Add(tick);

			dtgSessions.TableStyles.Add(tsSessions);

            pEnvironment.CreateGraphics().Clear(Color.DeepSkyBlue);			
			DrawEnvironment();
		}

		#endregion

		private void btnStartAgent_Click(object sender, System.EventArgs e)
		{
			pEnvironment.Focus();
            Thread t = new Thread(new ThreadStart(StartAgent));
			t.Start();
		}

		private void btnEndAgent_Click(object sender, System.EventArgs e)
		{
			EndAgent();
		}

		private void StartAgent()
		{
			try
			{

				Session selectedSession = null;
				//if offline search selected, ensure a session is selected
				if (rdbAstar.Checked || rdbBreadthFirst.Checked || rdbReplay.Checked)
				{
					if (dtgSessions.CurrentRowIndex == -1)
					{
						MessageBox.Show("No session is selected for offline search.","Error",MessageBoxButtons.OK);
						return;
					}
					object obj  = dtgSessions[dtgSessions.CurrentRowIndex,0];
					int id = int.Parse(obj.ToString());
					SessionDAO sessionDAO = new SessionDAO();
					selectedSession = sessionDAO.Find(id);
				}

			btnEndAgent.Enabled = true;
			btnStartAgent.Enabled = false;
			txtTick.Enabled = false;
			btnRestoreEnvironment.Enabled = false;
			pnlStrategies.Enabled = false;
			txtInformation.Clear();
           
				_agent = _agentDAO.Find(1);
				_endAgent = false;
				int _tickCount = System.Environment.TickCount;
				SearchStrategy strategy = GetSearchStrategy();
				

				_agent.StartNewSession(selectedSession, strategy,_environment.Building.Id,_environment.Building.TotalWindows,_tick);

				while(!_endAgent)
				{
				
					if (System.Environment.TickCount - _tickCount > _tick)
					{
						UpdateAgent();
						DrawEnvironment();
						_tickCount = System.Environment.TickCount;
						RefreshStats();
					}
				}
			}
			catch(Exception eX)
			{
				EndAgent();
				MessageBox.Show(eX.StackTrace, eX.Message,MessageBoxButtons.OK, MessageBoxIcon.Error);

			}
		}

		private void UpdateAgent()
		{
			
			Location facingLocation = null;
			
			if (_agent.InBay)
			{
                facingLocation = _environment.Building.Locations[1,1];
			}
			else
			{
				facingLocation = _environment.Building.GetFacingLocation(_agent.Column,_agent.Row,_agent.Direction);
			}
			
			Percept percept = new Percept();
			percept.Bay = facingLocation.Bay;
			percept.Window = facingLocation.Window;
			percept.BuildingEdge = facingLocation.BuildingEdge;
			percept.Obstacle = facingLocation.Obstacle;

			if (rdbManual.Checked)
			{
				percept.Action = _action;
			}

			Action action = _agent.AddPercept(percept);

			PrintInformation(GetActionDescription(action));
			
			if (rdbManual.Checked)
			{
				_action = Action.None;
			}

			if (_agent.InBay && _agent.Session.CleanedAllWindows)
			{
				EndAgent();
			}
            
			
		}

		
		private void EndAgent()
		{
			_endAgent = true;
			_agent.Session.SearchStrategy.Halt();
			_agent.EndSession();
			btnStartAgent.Enabled = true;
			btnEndAgent.Enabled = false;
			txtTick.Enabled = true;
			btnRestoreEnvironment.Enabled = true;
			pnlStrategies.Enabled = true;
		}	

		private SearchStrategy GetSearchStrategy()
		{
			SearchStrategy searchStrategy = null;
			if (rdbManual.Checked)
			{
				searchStrategy = new ManualSearchStrategy();
			}
			else if(rdbRandom.Checked)
			{
				searchStrategy = new RandomWalkStrategy();
			}
			else if(rdbDFS.Checked)
			{
				searchStrategy = new OnlineDFSSearchStrategy();
			}
			else if(rdbBreadthFirst.Checked)
			{
				searchStrategy = new BreadthFirstStrategy();
			}
			else if(rdbReplay.Checked)
			{
				searchStrategy = new ReplayStrategy();
			}
			else if(rdbAstar.Checked)
			{
				searchStrategy = new AStarSearchStrategy();
			}

			return searchStrategy;

		}

		private void pEnvironment_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (rdbManual.Checked)
			{
				if (e.KeyChar == 'w')
				{
					_action = Action.Forward;
					e.Handled =true;
				}
				else if(e.KeyChar == 'a')
				{
					_action = Action.TurnLeft;
					e.Handled =true;
				}
				else if(e.KeyChar == 'd')
				{
					_action = Action.TurnRight;
					e.Handled =true;
				}
								
			}
		}

		private void pEnvironment_Click(object sender, EventArgs e)
		{
			pEnvironment.Focus();
		}

		private void txtTick_TextChanged(object sender, System.EventArgs e)
		{
			try
			{
				if (txtTick.Text.Length >= 3)
				{
					_tick = int.Parse(txtTick.Text);
				}
			}
			catch
			{
				_tick = DEFAULT_TICK;
				txtTick.Text = _tick.ToString();
			}

		
		}

		private void PrintInformation(string text)
		{
			txtInformation.AppendText(text + "\n");
		}

		private string GetActionDescription(Action action)
		{
			if (action == Action.Forward)
			{
				return "Agent moving forward.";
			}
			else if(action == Action.TurnLeft)
			{
				return "Agent turning left.";
			}
			else if(action == Action.TurnRight)
			{
				return "Agent turning right.";
			}
			else if(action == Action.Clean)
			{
				return "Agent cleaning window.";
			}
			else 
			{
				return "Agent taking no action";
			}

		}

		private void RefreshStats()
		{
			lblGoal.Text = String.Format("Goal : {0} windows",_environment.Building.TotalWindows);
			lblCleaned.Text = String.Format("Cleaned : {0} windows",_agent.Session.WindowsCleaned);
			lblTime.Text = String.Format("Time : {0}s",_agent.Session.Duration.TotalSeconds);
			lblStepCost.Text = String.Format("Step Cost : {0}",_agent.Session.StepCost);

		}

		private void btnRestoreEnvironment_Click(object sender, System.EventArgs e)
		{
			RestoreEnvironment(FOLDER + "\\" +  _environment.Building.Name + ".env");
		}


	
	}
}
