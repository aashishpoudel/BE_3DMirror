using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Collections;
using System.Windows.Forms;

namespace Graphic_Objects
{
	/// <summary>
	/// Main function.
	/// </summary>
	public class fclsMain : System.Windows.Forms.Form
	{
		background myBkg=new background();
		Cube myCube=new Cube(250,320,180,80);
		Cube myCubeM=new Cube(330,200,-180,80);
		Sphere mySphere=new Sphere(400,315,200,200);
		Sphere mySphereM=new Sphere(475,180,-460,200);
		Cylinder myCyl=new Cylinder(250,320,180,100,50);
		Cylinder myCylM=new Cylinder(330,200,-180,100,50);
		Cone myCone=new Cone(250,320,180,100,80,1);
		Cone myConeM=new Cone(330,200,-180,100,80,1);
		Vessel myVessel=new Vessel(250,320,180,120,30);
		Vessel myVesselM=new Vessel(330,200,-180,120,30);

		bool shouldScale=false;
		float ScaleFactor=1;
		int eXLast, eYLast;
		bool flag=false;

		#region
		
		private Bitmap bmpDrawingSurface;
		private Graphics objGraphics;
		private System.Windows.Forms.PictureBox pbxDisplay;
		private System.Windows.Forms.Timer tmrClock;
		private System.Windows.Forms.Button btnStartStop;
		private System.Windows.Forms.TrackBar trbScale;
		private System.Windows.Forms.TrackBar trbXrot;
		private System.Windows.Forms.TrackBar trbYrot;
		private System.Windows.Forms.TrackBar trbZrot;
		private System.Windows.Forms.Label lblXrot;
		private System.Windows.Forms.Label lblYrot;
		private System.Windows.Forms.Label lblZrot;
		private System.Windows.Forms.Button btnRenderMode;
		private System.Windows.Forms.Button btnQuit;
		private System.Windows.Forms.RadioButton rbtnCube;
		private System.Windows.Forms.RadioButton rbtnSphere;
		private System.Windows.Forms.RadioButton rbtnCylinder;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Button btnReset;
		private System.Windows.Forms.Label lblLightX;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.NumericUpDown numUD_lightY;
		private System.Windows.Forms.NumericUpDown numUD_lightZ;
		private System.Windows.Forms.Label lblLightZ;
		private System.Windows.Forms.RadioButton rbtnDiffuse;
		private System.Windows.Forms.RadioButton rbtnSpec;
		private System.Windows.Forms.Label lblLightY;
		private System.Windows.Forms.NumericUpDown numUD_lightX;
		private System.Windows.Forms.Button btnAbout;
		private System.Windows.Forms.RadioButton rbtnVessel;
		private System.Windows.Forms.RadioButton rbtnCone;
		private System.Windows.Forms.RadioButton rbtnBoth;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.Label lblXrotvalue;
		private System.Windows.Forms.Label lblYrotvalue;
		private System.Windows.Forms.Label lblZrotvalue;
		private System.Windows.Forms.GroupBox groupBox4;
		private System.Windows.Forms.Label lblScaleFactor;
		private System.ComponentModel.IContainer components;
		#endregion
		public fclsMain()
		{
			InitializeComponent();
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(fclsMain));
            this.pbxDisplay = new System.Windows.Forms.PictureBox();
            this.tmrClock = new System.Windows.Forms.Timer(this.components);
            this.btnStartStop = new System.Windows.Forms.Button();
            this.trbScale = new System.Windows.Forms.TrackBar();
            this.trbXrot = new System.Windows.Forms.TrackBar();
            this.trbYrot = new System.Windows.Forms.TrackBar();
            this.trbZrot = new System.Windows.Forms.TrackBar();
            this.lblXrot = new System.Windows.Forms.Label();
            this.lblYrot = new System.Windows.Forms.Label();
            this.lblZrot = new System.Windows.Forms.Label();
            this.btnRenderMode = new System.Windows.Forms.Button();
            this.btnQuit = new System.Windows.Forms.Button();
            this.rbtnCube = new System.Windows.Forms.RadioButton();
            this.rbtnSphere = new System.Windows.Forms.RadioButton();
            this.rbtnCylinder = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbtnVessel = new System.Windows.Forms.RadioButton();
            this.rbtnCone = new System.Windows.Forms.RadioButton();
            this.btnReset = new System.Windows.Forms.Button();
            this.lblLightX = new System.Windows.Forms.Label();
            this.lblLightZ = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.numUD_lightX = new System.Windows.Forms.NumericUpDown();
            this.lblLightY = new System.Windows.Forms.Label();
            this.numUD_lightZ = new System.Windows.Forms.NumericUpDown();
            this.rbtnDiffuse = new System.Windows.Forms.RadioButton();
            this.numUD_lightY = new System.Windows.Forms.NumericUpDown();
            this.rbtnSpec = new System.Windows.Forms.RadioButton();
            this.rbtnBoth = new System.Windows.Forms.RadioButton();
            this.btnAbout = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lblXrotvalue = new System.Windows.Forms.Label();
            this.lblYrotvalue = new System.Windows.Forms.Label();
            this.lblZrotvalue = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.lblScaleFactor = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pbxDisplay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trbScale)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trbXrot)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trbYrot)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trbZrot)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numUD_lightX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUD_lightZ)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUD_lightY)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // pbxDisplay
            // 
            this.pbxDisplay.BackColor = System.Drawing.SystemColors.Control;
            this.pbxDisplay.Location = new System.Drawing.Point(0, 0);
            this.pbxDisplay.Name = "pbxDisplay";
            this.pbxDisplay.Size = new System.Drawing.Size(1024, 768);
            this.pbxDisplay.TabIndex = 0;
            this.pbxDisplay.TabStop = false;
            this.pbxDisplay.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pbxDisplay_MouseMove);
            this.pbxDisplay.Click += new System.EventHandler(this.pbxDisplay_Click);
            // 
            // tmrClock
            // 
            this.tmrClock.Tick += new System.EventHandler(this.tmrClock_Tick);
            // 
            // btnStartStop
            // 
            this.btnStartStop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStartStop.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnStartStop.Location = new System.Drawing.Point(84, 52);
            this.btnStartStop.Name = "btnStartStop";
            this.btnStartStop.Size = new System.Drawing.Size(72, 20);
            this.btnStartStop.TabIndex = 6;
            this.btnStartStop.Text = "Start";
            this.btnStartStop.Click += new System.EventHandler(this.btnStartStop_Click);
            // 
            // trbScale
            // 
            this.trbScale.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.trbScale.LargeChange = 1;
            this.trbScale.Location = new System.Drawing.Point(4, 16);
            this.trbScale.Maximum = 20;
            this.trbScale.Name = "trbScale";
            this.trbScale.Size = new System.Drawing.Size(128, 45);
            this.trbScale.TabIndex = 5;
            this.trbScale.Value = 10;
            this.trbScale.ValueChanged += new System.EventHandler(this.trbScale_ValueChanged);
            this.trbScale.Scroll += new System.EventHandler(this.trbScale_Scroll);
            // 
            // trbXrot
            // 
            this.trbXrot.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.trbXrot.LargeChange = 1;
            this.trbXrot.Location = new System.Drawing.Point(20, 12);
            this.trbXrot.Minimum = -10;
            this.trbXrot.Name = "trbXrot";
            this.trbXrot.Size = new System.Drawing.Size(112, 45);
            this.trbXrot.TabIndex = 2;
            // 
            // trbYrot
            // 
            this.trbYrot.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.trbYrot.LargeChange = 1;
            this.trbYrot.Location = new System.Drawing.Point(20, 40);
            this.trbYrot.Minimum = -10;
            this.trbYrot.Name = "trbYrot";
            this.trbYrot.Size = new System.Drawing.Size(112, 45);
            this.trbYrot.TabIndex = 3;
            this.trbYrot.Value = -4;
            // 
            // trbZrot
            // 
            this.trbZrot.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.trbZrot.LargeChange = 1;
            this.trbZrot.Location = new System.Drawing.Point(20, 68);
            this.trbZrot.Minimum = -10;
            this.trbZrot.Name = "trbZrot";
            this.trbZrot.Size = new System.Drawing.Size(112, 45);
            this.trbZrot.TabIndex = 4;
            // 
            // lblXrot
            // 
            this.lblXrot.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblXrot.AutoSize = true;
            this.lblXrot.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblXrot.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.lblXrot.Location = new System.Drawing.Point(4, 20);
            this.lblXrot.Name = "lblXrot";
            this.lblXrot.Size = new System.Drawing.Size(15, 13);
            this.lblXrot.TabIndex = 8;
            this.lblXrot.Text = "X";
            this.lblXrot.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblYrot
            // 
            this.lblYrot.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblYrot.AutoSize = true;
            this.lblYrot.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblYrot.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.lblYrot.Location = new System.Drawing.Point(4, 48);
            this.lblYrot.Name = "lblYrot";
            this.lblYrot.Size = new System.Drawing.Size(15, 13);
            this.lblYrot.TabIndex = 8;
            this.lblYrot.Text = "Y";
            this.lblYrot.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblZrot
            // 
            this.lblZrot.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblZrot.AutoSize = true;
            this.lblZrot.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblZrot.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.lblZrot.Location = new System.Drawing.Point(4, 76);
            this.lblZrot.Name = "lblZrot";
            this.lblZrot.Size = new System.Drawing.Size(15, 13);
            this.lblZrot.TabIndex = 8;
            this.lblZrot.Text = "Z";
            this.lblZrot.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnRenderMode
            // 
            this.btnRenderMode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRenderMode.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnRenderMode.Location = new System.Drawing.Point(84, 20);
            this.btnRenderMode.Name = "btnRenderMode";
            this.btnRenderMode.Size = new System.Drawing.Size(72, 20);
            this.btnRenderMode.TabIndex = 5;
            this.btnRenderMode.Text = "Wireframe";
            this.btnRenderMode.Click += new System.EventHandler(this.btnRenderMode_Click);
            // 
            // btnQuit
            // 
            this.btnQuit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnQuit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnQuit.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnQuit.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnQuit.Location = new System.Drawing.Point(1242, 431);
            this.btnQuit.Name = "btnQuit";
            this.btnQuit.Size = new System.Drawing.Size(74, 20);
            this.btnQuit.TabIndex = 9;
            this.btnQuit.Text = "Quit";
            this.btnQuit.Click += new System.EventHandler(this.btnQuit_Click);
            // 
            // rbtnCube
            // 
            this.rbtnCube.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.rbtnCube.Location = new System.Drawing.Point(8, 16);
            this.rbtnCube.Name = "rbtnCube";
            this.rbtnCube.Size = new System.Drawing.Size(68, 16);
            this.rbtnCube.TabIndex = 0;
            this.rbtnCube.Text = "Cube";
            this.rbtnCube.CheckedChanged += new System.EventHandler(this.rbtnCube_CheckedChanged);
            // 
            // rbtnSphere
            // 
            this.rbtnSphere.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.rbtnSphere.Location = new System.Drawing.Point(8, 36);
            this.rbtnSphere.Name = "rbtnSphere";
            this.rbtnSphere.Size = new System.Drawing.Size(68, 16);
            this.rbtnSphere.TabIndex = 1;
            this.rbtnSphere.Text = "Sphere";
            this.rbtnSphere.CheckedChanged += new System.EventHandler(this.rbtnSphere_CheckedChanged);
            // 
            // rbtnCylinder
            // 
            this.rbtnCylinder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.rbtnCylinder.Location = new System.Drawing.Point(8, 56);
            this.rbtnCylinder.Name = "rbtnCylinder";
            this.rbtnCylinder.Size = new System.Drawing.Size(68, 16);
            this.rbtnCylinder.TabIndex = 2;
            this.rbtnCylinder.Text = "Cylinder";
            this.rbtnCylinder.CheckedChanged += new System.EventHandler(this.rbtnCylinder_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.rbtnCube);
            this.groupBox1.Controls.Add(this.rbtnSphere);
            this.groupBox1.Controls.Add(this.rbtnCylinder);
            this.groupBox1.Controls.Add(this.btnRenderMode);
            this.groupBox1.Controls.Add(this.rbtnVessel);
            this.groupBox1.Controls.Add(this.rbtnCone);
            this.groupBox1.Controls.Add(this.btnStartStop);
            this.groupBox1.Controls.Add(this.btnReset);
            this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(1160, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(164, 116);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Select a shape";
            // 
            // rbtnVessel
            // 
            this.rbtnVessel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.rbtnVessel.Location = new System.Drawing.Point(8, 96);
            this.rbtnVessel.Name = "rbtnVessel";
            this.rbtnVessel.Size = new System.Drawing.Size(68, 16);
            this.rbtnVessel.TabIndex = 4;
            this.rbtnVessel.Text = "Vessel";
            this.rbtnVessel.CheckedChanged += new System.EventHandler(this.rbtnVessel_CheckedChanged);
            // 
            // rbtnCone
            // 
            this.rbtnCone.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.rbtnCone.Location = new System.Drawing.Point(8, 76);
            this.rbtnCone.Name = "rbtnCone";
            this.rbtnCone.Size = new System.Drawing.Size(68, 16);
            this.rbtnCone.TabIndex = 3;
            this.rbtnCone.Text = "Cone";
            this.rbtnCone.CheckedChanged += new System.EventHandler(this.rbtnCone_CheckedChanged);
            // 
            // btnReset
            // 
            this.btnReset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnReset.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnReset.Location = new System.Drawing.Point(84, 88);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(72, 20);
            this.btnReset.TabIndex = 7;
            this.btnReset.Text = "Reset";
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // lblLightX
            // 
            this.lblLightX.AutoSize = true;
            this.lblLightX.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLightX.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.lblLightX.Location = new System.Drawing.Point(8, 18);
            this.lblLightX.Name = "lblLightX";
            this.lblLightX.Size = new System.Drawing.Size(15, 13);
            this.lblLightX.TabIndex = 17;
            this.lblLightX.Text = "X";
            this.lblLightX.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblLightZ
            // 
            this.lblLightZ.AutoSize = true;
            this.lblLightZ.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLightZ.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.lblLightZ.Location = new System.Drawing.Point(8, 60);
            this.lblLightZ.Name = "lblLightZ";
            this.lblLightZ.Size = new System.Drawing.Size(15, 13);
            this.lblLightZ.TabIndex = 17;
            this.lblLightZ.Text = "Z";
            this.lblLightZ.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.numUD_lightX);
            this.groupBox2.Controls.Add(this.lblLightY);
            this.groupBox2.Controls.Add(this.numUD_lightZ);
            this.groupBox2.Controls.Add(this.rbtnDiffuse);
            this.groupBox2.Controls.Add(this.numUD_lightY);
            this.groupBox2.Controls.Add(this.lblLightX);
            this.groupBox2.Controls.Add(this.lblLightZ);
            this.groupBox2.Controls.Add(this.rbtnSpec);
            this.groupBox2.Controls.Add(this.rbtnBoth);
            this.groupBox2.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(1160, 256);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(164, 84);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Lighting";
            // 
            // numUD_lightX
            // 
            this.numUD_lightX.DecimalPlaces = 1;
            this.numUD_lightX.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numUD_lightX.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numUD_lightX.Location = new System.Drawing.Point(24, 16);
            this.numUD_lightX.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numUD_lightX.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.numUD_lightX.Name = "numUD_lightX";
            this.numUD_lightX.Size = new System.Drawing.Size(44, 20);
            this.numUD_lightX.TabIndex = 0;
            this.numUD_lightX.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // lblLightY
            // 
            this.lblLightY.AutoSize = true;
            this.lblLightY.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLightY.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.lblLightY.Location = new System.Drawing.Point(8, 40);
            this.lblLightY.Name = "lblLightY";
            this.lblLightY.Size = new System.Drawing.Size(15, 13);
            this.lblLightY.TabIndex = 20;
            this.lblLightY.Text = "Y";
            this.lblLightY.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // numUD_lightZ
            // 
            this.numUD_lightZ.DecimalPlaces = 1;
            this.numUD_lightZ.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numUD_lightZ.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numUD_lightZ.Location = new System.Drawing.Point(24, 56);
            this.numUD_lightZ.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numUD_lightZ.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.numUD_lightZ.Name = "numUD_lightZ";
            this.numUD_lightZ.Size = new System.Drawing.Size(44, 20);
            this.numUD_lightZ.TabIndex = 2;
            this.numUD_lightZ.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // rbtnDiffuse
            // 
            this.rbtnDiffuse.Checked = true;
            this.rbtnDiffuse.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtnDiffuse.Location = new System.Drawing.Point(72, 20);
            this.rbtnDiffuse.Name = "rbtnDiffuse";
            this.rbtnDiffuse.Size = new System.Drawing.Size(88, 16);
            this.rbtnDiffuse.TabIndex = 3;
            this.rbtnDiffuse.TabStop = true;
            this.rbtnDiffuse.Text = "Diffuse";
            this.rbtnDiffuse.CheckedChanged += new System.EventHandler(this.rbtnDiffuse_CheckedChanged);
            // 
            // numUD_lightY
            // 
            this.numUD_lightY.DecimalPlaces = 1;
            this.numUD_lightY.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numUD_lightY.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numUD_lightY.Location = new System.Drawing.Point(24, 36);
            this.numUD_lightY.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numUD_lightY.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.numUD_lightY.Name = "numUD_lightY";
            this.numUD_lightY.Size = new System.Drawing.Size(44, 20);
            this.numUD_lightY.TabIndex = 1;
            this.numUD_lightY.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // rbtnSpec
            // 
            this.rbtnSpec.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtnSpec.Location = new System.Drawing.Point(72, 40);
            this.rbtnSpec.Name = "rbtnSpec";
            this.rbtnSpec.Size = new System.Drawing.Size(88, 16);
            this.rbtnSpec.TabIndex = 4;
            this.rbtnSpec.Text = "Specular";
            this.rbtnSpec.CheckedChanged += new System.EventHandler(this.rbtnSpec_CheckedChanged);
            // 
            // rbtnBoth
            // 
            this.rbtnBoth.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtnBoth.Location = new System.Drawing.Point(72, 60);
            this.rbtnBoth.Name = "rbtnBoth";
            this.rbtnBoth.Size = new System.Drawing.Size(88, 16);
            this.rbtnBoth.TabIndex = 4;
            this.rbtnBoth.Text = "Both";
            this.rbtnBoth.CheckedChanged += new System.EventHandler(this.rbtnBoth_CheckedChanged);
            // 
            // btnAbout
            // 
            this.btnAbout.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAbout.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnAbout.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAbout.Location = new System.Drawing.Point(1160, 431);
            this.btnAbout.Name = "btnAbout";
            this.btnAbout.Size = new System.Drawing.Size(76, 20);
            this.btnAbout.TabIndex = 8;
            this.btnAbout.Text = "About";
            this.btnAbout.Click += new System.EventHandler(this.btnAbout_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.SystemColors.ControlText;
            this.groupBox3.Controls.Add(this.lblXrotvalue);
            this.groupBox3.Controls.Add(this.trbZrot);
            this.groupBox3.Controls.Add(this.trbYrot);
            this.groupBox3.Controls.Add(this.trbXrot);
            this.groupBox3.Controls.Add(this.lblYrot);
            this.groupBox3.Controls.Add(this.lblZrot);
            this.groupBox3.Controls.Add(this.lblXrot);
            this.groupBox3.Controls.Add(this.lblYrotvalue);
            this.groupBox3.Controls.Add(this.lblZrotvalue);
            this.groupBox3.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.Location = new System.Drawing.Point(1160, 130);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(164, 120);
            this.groupBox3.TabIndex = 20;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Rotation";
            // 
            // lblXrotvalue
            // 
            this.lblXrotvalue.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblXrotvalue.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.lblXrotvalue.Location = new System.Drawing.Point(132, 16);
            this.lblXrotvalue.Name = "lblXrotvalue";
            this.lblXrotvalue.Size = new System.Drawing.Size(24, 20);
            this.lblXrotvalue.TabIndex = 9;
            this.lblXrotvalue.Text = "0";
            this.lblXrotvalue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblYrotvalue
            // 
            this.lblYrotvalue.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblYrotvalue.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.lblYrotvalue.Location = new System.Drawing.Point(132, 44);
            this.lblYrotvalue.Name = "lblYrotvalue";
            this.lblYrotvalue.Size = new System.Drawing.Size(24, 20);
            this.lblYrotvalue.TabIndex = 9;
            this.lblYrotvalue.Text = "-4";
            this.lblYrotvalue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblZrotvalue
            // 
            this.lblZrotvalue.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblZrotvalue.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.lblZrotvalue.Location = new System.Drawing.Point(132, 76);
            this.lblZrotvalue.Name = "lblZrotvalue";
            this.lblZrotvalue.Size = new System.Drawing.Size(24, 20);
            this.lblZrotvalue.TabIndex = 9;
            this.lblZrotvalue.Text = "0";
            this.lblZrotvalue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.lblScaleFactor);
            this.groupBox4.Controls.Add(this.trbScale);
            this.groupBox4.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.groupBox4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox4.Location = new System.Drawing.Point(1160, 346);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(164, 64);
            this.groupBox4.TabIndex = 21;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Scaling";
            // 
            // lblScaleFactor
            // 
            this.lblScaleFactor.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblScaleFactor.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.lblScaleFactor.Location = new System.Drawing.Point(132, 20);
            this.lblScaleFactor.Name = "lblScaleFactor";
            this.lblScaleFactor.Size = new System.Drawing.Size(24, 20);
            this.lblScaleFactor.TabIndex = 9;
            this.lblScaleFactor.Text = "1";
            this.lblScaleFactor.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // fclsMain
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.BackColor = System.Drawing.SystemColors.ControlText;
            this.CancelButton = this.btnQuit;
            this.ClientSize = new System.Drawing.Size(1376, 753);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.btnAbout);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btnQuit);
            this.Controls.Add(this.pbxDisplay);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox3);
            this.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "fclsMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Aashish Poudel Productions\'";
            this.Load += new System.EventHandler(this.fclsMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbxDisplay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trbScale)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trbXrot)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trbYrot)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trbZrot)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numUD_lightX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUD_lightZ)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUD_lightY)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new fclsMain());
		}

		private void fclsMain_Load(object sender, System.EventArgs e)
		{
            this.pbxDisplay.Width = 1024;
            this.pbxDisplay.Height = 768;
			this.bmpDrawingSurface =new Bitmap(this.pbxDisplay.Width,this.pbxDisplay.Height,System.Drawing.Imaging.PixelFormat.Format24bppRgb);
			objGraphics=Graphics.FromImage(bmpDrawingSurface);
			InitializeSurface();
		}
		
		private void InitializeSurface()
		{
			objGraphics.Clear(SystemColors.MenuHighlight);
			mySphereM.Mirror();
			
			mySphere.Rotation(20,'X');	mySphere.Rotation(20,'x');
			mySphere.Rotation(20,'Y');	mySphere.Rotation(20,'y');
			
			myBkg.Draw(this.bmpDrawingSurface);
			myBkg.DrawMirror(this.bmpDrawingSurface);
			this.pbxDisplay.Image=this.bmpDrawingSurface;
            
		}
		
		private void tmrClock_Tick(object sender, System.EventArgs e)
		{
			RotateAll();
			if(shouldScale==true)	{	ScaleAll();		shouldScale=false;	}
			DrawAll();
		}
		
		private void DrawAll()
		{
			myBkg.Draw(this.bmpDrawingSurface);
			myBkg.DrawMirror(this.bmpDrawingSurface);
			if(this.rbtnSphere.Checked==true)
			{	
				this.mySphere.LightSrc.X=(float) this.numUD_lightX.Value;
				this.mySphere.LightSrc.Y=(float) this.numUD_lightY.Value;
				this.mySphere.LightSrc.Z=(float) this.numUD_lightZ.Value;
				this.mySphereM.LightSrc.X=(float) this.numUD_lightX.Value;
				this.mySphereM.LightSrc.Y=(float) this.numUD_lightY.Value;
				this.mySphereM.LightSrc.Z=(float) this.numUD_lightZ.Value;
				
				myBkg.DrawMirror(this.bmpDrawingSurface);
				if(mySphereM.Is_Within())
                { }
                //mySphereM.Draw(this.bmpDrawingSurface);
				else
				{
					//mySphereM.Draw(this.bmpDrawingSurface);
					//myBkg.DrawSides(this.bmpDrawingSurface);
					//myBkg.Draw(this.bmpDrawingSurface);
				}
				mySphere.Draw(this.bmpDrawingSurface);
				//txtBxPos.Text = this.mySphere.position.X.ToString()+','+this.mySphere.position.Y.ToString()+','+this.mySphere.position.Z.ToString();
				//txtBxPosM.Text = this.mySphereM.position.X.ToString()+','+this.mySphereM.position.Y.ToString()+','+this.mySphereM.position.Z.ToString();
			}
			
			else
				myBkg.DrawMirror(this.bmpDrawingSurface);
			
			this.pbxDisplay.Image=this.bmpDrawingSurface;
		}

		private void RotateAll()
		{
			int i=this.trbXrot.Value;
			int j=this.trbYrot.Value;
			int k=this.trbZrot.Value;
			this.lblXrotvalue.Text=i.ToString();
			this.lblYrotvalue.Text=j.ToString();
			this.lblZrotvalue.Text=k.ToString();
			
			if(i!=0)
			{	if(this.rbtnCube.Checked==true)
				{	myCube.Rotation(i,'x');		myCubeM.Rotation(i,'X');	}
				if(this.rbtnSphere.Checked==true)
				{	mySphere.Rotation(i,'x');	mySphereM.Rotation(i,'X');	}
				if(this.rbtnCylinder.Checked==true)
				{	myCyl.Rotation(i,'x');		myCylM.Rotation(i,'X');		}
				if(this.rbtnCone.Checked==true)
				{	myCone.Rotation(i,'x');		myConeM.Rotation(i,'X');	}
				if(this.rbtnVessel.Checked==true)
				{	myVessel.Rotation(i,'x');	myVesselM.Rotation(i,'X');	}
			}
			if(j!=0)
			{	if(this.rbtnCube.Checked==true)
				{	myCube.Rotation(j,'y');		myCubeM.Rotation(j,'Y');	}
				if(this.rbtnSphere.Checked==true)
				{	mySphere.Rotation(j,'y');	mySphereM.Rotation(j,'Y');	}
				if(this.rbtnCylinder.Checked==true)
				{	myCyl.Rotation(j,'y');		myCylM.Rotation(j,'Y');		}
				if(this.rbtnCone.Checked==true)
				{	myCone.Rotation(j,'y');		myConeM.Rotation(j,'Y');	}
				if(this.rbtnVessel.Checked==true)
				{	myVessel.Rotation(j,'y');	myVesselM.Rotation(j,'Y');	}
			}
			if(k!=0)
			{	if(this.rbtnCube.Checked==true)
				{	myCube.Rotation(k,'z');		myCubeM.Rotation(k,'z');	}
				if(this.rbtnSphere.Checked==true)
				{	mySphere.Rotation(k,'z');	mySphereM.Rotation(k,'z');	}
				if(this.rbtnCylinder.Checked==true)
				{	myCyl.Rotation(k,'z');		myCylM.Rotation(k,'z');		}
				if(this.rbtnCone.Checked==true)
				{	myCone.Rotation(k,'z');		myConeM.Rotation(k,'z');	}
				if(this.rbtnVessel.Checked==true)
				{	myVessel.Rotation(k,'z');	myVesselM.Rotation(k,'z');	}
			}
		}
		
		private void ScaleAll()
		{
			float s=new float();
			if(this.trbScale.Value!=0)
			{	s= (float)(this.trbScale.Value/10.0) ;	}
			
			this.lblScaleFactor.Text=s.ToString();

			if(this.rbtnCube.Checked==true)
			{	this.myCube.Scale(s);	this.myCubeM.Scale(s);	}
			if(this.rbtnSphere.Checked==true)
			{	this.mySphere.Scale(s);	this.mySphereM.Scale(s);}
			if(this.rbtnCylinder.Checked==true)
			{	this.myCyl.Scale(s);	this.myCylM.Scale(s);}
			if(this.rbtnCone.Checked==true)
			{	this.myCone.Scale(s);	this.myConeM.Scale(s);}
			if(this.rbtnVessel.Checked==true)
			{	this.myVessel.Scale(s);	this.myVesselM.Scale(s);}
		}

		private void trbScale_ValueChanged(object sender, System.EventArgs e)
		{	shouldScale=true;	}
				
		
		private void btnStartStop_Click(object sender, System.EventArgs e)
		{
			if(this.tmrClock.Enabled==false)
			{
				this.tmrClock.Enabled=true;
				this.btnStartStop.Text="Stop";
			}
			else
			{
				this.tmrClock.Enabled=false;
				this.btnStartStop.Text="Start";
			}
		}

		private void btnQuit_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void btnRenderMode_Click(object sender, System.EventArgs e)
		{
			if(this.myCube.RenderMode=='S')
			{
				this.myCube.RenderMode='W';
				this.myCubeM.RenderMode='W';
				this.mySphere.RenderMode='W';
				this.mySphereM.RenderMode='W';
				this.myCyl.RenderMode='W';
				this.myCylM.RenderMode='W';
				this.myCone.RenderMode='W';
				this.myConeM.RenderMode='W';
				this.myVessel.RenderMode='W';
				this.myVesselM.RenderMode='W';
				this.btnRenderMode.Text="Shaded";
			}
			else
			{
				this.myCube.RenderMode='S';
				this.myCubeM.RenderMode='S';
				this.mySphere.RenderMode='S';
				this.mySphereM.RenderMode='S';
				this.myCyl.RenderMode='S';
				this.myCylM.RenderMode='S';
				this.myCone.RenderMode='S';
				this.myConeM.RenderMode='S';
				this.myVessel.RenderMode='S';
				this.myVesselM.RenderMode='S';
				this.btnRenderMode.Text="Wireframe";
			}
			DrawAll();
		}

		private void btnReset_Click(object sender, System.EventArgs e)
		{
			myCube=new Cube(250,320,180,80);
			myCubeM=new Cube(330,200,-180,80);
			mySphere=new Sphere(250,320,180,60);
			mySphereM=new Sphere(330,200,-180,60);
			myCyl=new Cylinder(250,320,180,100,50);
			myCylM=new Cylinder(330,200,-180,100,50);
			myCone=new Cone(250,320,180,100,80,1);
			myConeM=new Cone(330,200,-180,100,80,1);
			myVessel=new Vessel(250,320,180,120,30);
			myVesselM=new Vessel(330,200,-180,120,30);
											
			this.tmrClock.Enabled=false;
			this.btnStartStop.Text="Start";
			this.numUD_lightX.Value=1;
			this.numUD_lightY.Value=1;
			this.numUD_lightZ.Value=1;
			this.trbXrot.Value=1;
			this.trbYrot.Value=1;
			this.trbZrot.Value=1;
			this.trbScale.Value=10;
			this.rbtnDiffuse.Checked=true;
			
			InitializeSurface();
			DrawAll();
		}

        private void rbtnCube_CheckedChanged(object sender, System.EventArgs e) {}
        private void rbtnSphere_CheckedChanged(object sender, System.EventArgs e){}

		private void rbtnCylinder_CheckedChanged(object sender, System.EventArgs e)
		{
			
		}		
		private void rbtnCone_CheckedChanged(object sender, System.EventArgs e)
		{
			
		}
		private void rbtnVessel_CheckedChanged(object sender, System.EventArgs e)
		{
		
		}
		
		private void pbxDisplay_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
		{	
			
			if(this.rbtnSphere.Checked) 
			{
				
			}
			
		}

		
		private void rbtnDiffuse_CheckedChanged(object sender, System.EventArgs e)
		{
			this.myCube.diffuse=1;
			this.myCubeM.diffuse=1;
			this.mySphere.diffuse=1;
			this.mySphereM.diffuse=1;
			this.myCyl.diffuse=1;
			this.myCylM.diffuse=1;
			this.myCone.diffuse=1;
			this.myConeM.diffuse=1;
			this.myVessel.diffuse=1;
			this.myVesselM.diffuse=1;
			DrawAll();	
		}

		private void rbtnSpec_CheckedChanged(object sender, System.EventArgs e)
		{
		
        }

		private void rbtnBoth_CheckedChanged(object sender, System.EventArgs e)
		{
			this.myCube.diffuse=2;
			this.myCubeM.diffuse=2;
			this.mySphere.diffuse=2;
			this.mySphereM.diffuse=2;
			this.myCyl.diffuse=2;
			this.myCylM.diffuse=2;
			this.myCone.diffuse=2;
			this.myConeM.diffuse=2;
			this.myVessel.diffuse=2;
			this.myVesselM.diffuse=2;
			DrawAll();	
		}

		private void btnAbout_Click(object sender, System.EventArgs e)
		{
			fclsAbout AboutUs=new fclsAbout();
			AboutUs.ShowDialog();
		}

		private void trbScale_Scroll(object sender, System.EventArgs e)
		{
		
		}

		private void pbxDisplay_Click(object sender, System.EventArgs e)
		{
		
		}


	}
}
