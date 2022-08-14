using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace AI.Windows
{
	/// <summary>
	/// Summary description for NewEnvironment.
	/// </summary>
	public class NewEnvironmentForm : System.Windows.Forms.Form
	{
		public System.Windows.Forms.TextBox txtBuildingName;
		private System.Windows.Forms.Label lblBuildingName;
		private System.Windows.Forms.Button btnSave;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.GroupBox gbSize;
		public System.Windows.Forms.RadioButton rdbSmall;
		public System.Windows.Forms.RadioButton rdbMedium;
		public System.Windows.Forms.RadioButton rdbLarge;
		private System.Windows.Forms.GroupBox gbWindowArr;
		public System.Windows.Forms.RadioButton rdbUniform;
		public System.Windows.Forms.RadioButton rdbIrregular;
		private System.Windows.Forms.Label lblWindows;
		public System.Windows.Forms.TextBox txtNumWindows;
		private System.Windows.Forms.Label lblObstacles;
		public System.Windows.Forms.TextBox txtNumObstacles;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public NewEnvironmentForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
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
			this.txtBuildingName = new System.Windows.Forms.TextBox();
			this.lblBuildingName = new System.Windows.Forms.Label();
			this.btnSave = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.gbSize = new System.Windows.Forms.GroupBox();
			this.rdbLarge = new System.Windows.Forms.RadioButton();
			this.rdbMedium = new System.Windows.Forms.RadioButton();
			this.rdbSmall = new System.Windows.Forms.RadioButton();
			this.gbWindowArr = new System.Windows.Forms.GroupBox();
			this.rdbIrregular = new System.Windows.Forms.RadioButton();
			this.rdbUniform = new System.Windows.Forms.RadioButton();
			this.lblWindows = new System.Windows.Forms.Label();
			this.lblObstacles = new System.Windows.Forms.Label();
			this.txtNumWindows = new System.Windows.Forms.TextBox();
			this.txtNumObstacles = new System.Windows.Forms.TextBox();
			this.gbSize.SuspendLayout();
			this.gbWindowArr.SuspendLayout();
			this.SuspendLayout();
			// 
			// txtBuildingName
			// 
			this.txtBuildingName.Location = new System.Drawing.Point(112, 8);
			this.txtBuildingName.Name = "txtBuildingName";
			this.txtBuildingName.Size = new System.Drawing.Size(144, 20);
			this.txtBuildingName.TabIndex = 0;
			this.txtBuildingName.Text = "";
			// 
			// lblBuildingName
			// 
			this.lblBuildingName.Location = new System.Drawing.Point(8, 8);
			this.lblBuildingName.Name = "lblBuildingName";
			this.lblBuildingName.TabIndex = 1;
			this.lblBuildingName.Text = "Building Name";
			// 
			// btnSave
			// 
			this.btnSave.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnSave.Location = new System.Drawing.Point(112, 296);
			this.btnSave.Name = "btnSave";
			this.btnSave.Size = new System.Drawing.Size(48, 32);
			this.btnSave.TabIndex = 2;
			this.btnSave.Text = "Save";
			// 
			// btnCancel
			// 
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(192, 296);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(48, 32);
			this.btnCancel.TabIndex = 3;
			this.btnCancel.Text = "Cancel";
			// 
			// gbSize
			// 
			this.gbSize.Controls.Add(this.rdbLarge);
			this.gbSize.Controls.Add(this.rdbMedium);
			this.gbSize.Controls.Add(this.rdbSmall);
			this.gbSize.Location = new System.Drawing.Point(64, 56);
			this.gbSize.Name = "gbSize";
			this.gbSize.Size = new System.Drawing.Size(136, 96);
			this.gbSize.TabIndex = 4;
			this.gbSize.TabStop = false;
			this.gbSize.Text = "Size";
			// 
			// rdbLarge
			// 
			this.rdbLarge.Location = new System.Drawing.Point(16, 64);
			this.rdbLarge.Name = "rdbLarge";
			this.rdbLarge.TabIndex = 2;
			this.rdbLarge.Text = "Large";
			this.rdbLarge.CheckedChanged += new System.EventHandler(this.rdbLarge_CheckedChanged);
			// 
			// rdbMedium
			// 
			this.rdbMedium.Checked = true;
			this.rdbMedium.Location = new System.Drawing.Point(16, 40);
			this.rdbMedium.Name = "rdbMedium";
			this.rdbMedium.TabIndex = 1;
			this.rdbMedium.TabStop = true;
			this.rdbMedium.Text = "Medium";
			this.rdbMedium.CheckedChanged += new System.EventHandler(this.rdbMedium_CheckedChanged);
			// 
			// rdbSmall
			// 
			this.rdbSmall.Location = new System.Drawing.Point(16, 16);
			this.rdbSmall.Name = "rdbSmall";
			this.rdbSmall.TabIndex = 0;
			this.rdbSmall.Text = "Small";
			this.rdbSmall.CheckedChanged += new System.EventHandler(this.rdbSmall_CheckedChanged);
			// 
			// gbWindowArr
			// 
			this.gbWindowArr.Controls.Add(this.rdbIrregular);
			this.gbWindowArr.Controls.Add(this.rdbUniform);
			this.gbWindowArr.Location = new System.Drawing.Point(64, 176);
			this.gbWindowArr.Name = "gbWindowArr";
			this.gbWindowArr.Size = new System.Drawing.Size(136, 80);
			this.gbWindowArr.TabIndex = 5;
			this.gbWindowArr.TabStop = false;
			this.gbWindowArr.Text = "Window Arrangement";
			// 
			// rdbIrregular
			// 
			this.rdbIrregular.Location = new System.Drawing.Point(8, 48);
			this.rdbIrregular.Name = "rdbIrregular";
			this.rdbIrregular.TabIndex = 1;
			this.rdbIrregular.Text = "Irregular";
			// 
			// rdbUniform
			// 
			this.rdbUniform.Checked = true;
			this.rdbUniform.Location = new System.Drawing.Point(8, 24);
			this.rdbUniform.Name = "rdbUniform";
			this.rdbUniform.TabIndex = 0;
			this.rdbUniform.TabStop = true;
			this.rdbUniform.Text = "Uniform";
			// 
			// lblWindows
			// 
			this.lblWindows.Location = new System.Drawing.Point(216, 184);
			this.lblWindows.Name = "lblWindows";
			this.lblWindows.Size = new System.Drawing.Size(72, 24);
			this.lblWindows.TabIndex = 6;
			this.lblWindows.Text = "Windows";
			// 
			// lblObstacles
			// 
			this.lblObstacles.Location = new System.Drawing.Point(216, 224);
			this.lblObstacles.Name = "lblObstacles";
			this.lblObstacles.Size = new System.Drawing.Size(64, 32);
			this.lblObstacles.TabIndex = 7;
			this.lblObstacles.Text = "Obstacles";
			// 
			// txtNumWindows
			// 
			this.txtNumWindows.Location = new System.Drawing.Point(288, 184);
			this.txtNumWindows.MaxLength = 3;
			this.txtNumWindows.Name = "txtNumWindows";
			this.txtNumWindows.Size = new System.Drawing.Size(56, 20);
			this.txtNumWindows.TabIndex = 8;
			this.txtNumWindows.Text = "8";
			// 
			// txtNumObstacles
			// 
			this.txtNumObstacles.Location = new System.Drawing.Point(288, 224);
			this.txtNumObstacles.MaxLength = 3;
			this.txtNumObstacles.Name = "txtNumObstacles";
			this.txtNumObstacles.Size = new System.Drawing.Size(56, 20);
			this.txtNumObstacles.TabIndex = 9;
			this.txtNumObstacles.Text = "3";
			// 
			// NewEnvironmentForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(368, 382);
			this.Controls.Add(this.txtNumObstacles);
			this.Controls.Add(this.txtNumWindows);
			this.Controls.Add(this.lblObstacles);
			this.Controls.Add(this.lblWindows);
			this.Controls.Add(this.gbWindowArr);
			this.Controls.Add(this.gbSize);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnSave);
			this.Controls.Add(this.lblBuildingName);
			this.Controls.Add(this.txtBuildingName);
			this.Name = "NewEnvironmentForm";
			this.Text = "New Environment";
			this.gbSize.ResumeLayout(false);
			this.gbWindowArr.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void rdbMedium_CheckedChanged(object sender, System.EventArgs e)
		{
			if (rdbMedium.Checked)
			{
				txtNumObstacles.Text = "3";
				txtNumWindows.Text = "8";
			}
		}

		private void rdbLarge_CheckedChanged(object sender, System.EventArgs e)
		{
			if (rdbLarge.Checked)
			{
				txtNumObstacles.Text = "5";
				txtNumWindows.Text = "16";
			}
		
		}

		private void rdbSmall_CheckedChanged(object sender, System.EventArgs e)
		{
			if (rdbSmall.Checked)
			{
				txtNumObstacles.Text = "2";
				txtNumWindows.Text = "4";
			}
		
		}

	
	}
}
