namespace RayTracerUI {
	partial class SceneEditor {
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
			if (disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SceneEditor));
			this.SceneObjects = new FlowLayoutPanel();
			this.AddObject = new Button();
			this.ObjectControls = new Panel();
			this.RemoveObject = new Button();
			this.ObjectName = new Label();
			this.ObjectNameBox = new TextBox();
			this.LambertianProperties = new Panel();
			this.LambertianColorButton = new Panel();
			this.LambertianColor = new Label();
			this.ColorPicker = new ColorDialog();
			this.MetalProperties = new Panel();
			this.MetalFuzzBox = new TextBox();
			this.MetalFuzz = new Label();
			this.MetalColorButton = new Panel();
			this.MetalColor = new Label();
			this.Material = new Label();
			this.MaterialBox = new ComboBox();
			this.DielectricProperties = new Panel();
			this.DielectricIndexBox = new TextBox();
			this.DielectricIndex = new Label();
			this.Radius = new Label();
			this.RadiusBox = new TextBox();
			this.ObjectPosition = new Label();
			this.PositionBox = new TextBox();
			this.ObjectControls.SuspendLayout();
			this.LambertianProperties.SuspendLayout();
			this.MetalProperties.SuspendLayout();
			this.DielectricProperties.SuspendLayout();
			this.SuspendLayout();
			// 
			// SceneObjects
			// 
			this.SceneObjects.AutoScroll = true;
			this.SceneObjects.BackColor = Color.Silver;
			this.SceneObjects.FlowDirection = FlowDirection.TopDown;
			this.SceneObjects.Location = new Point(12,12);
			this.SceneObjects.Margin = new Padding(10);
			this.SceneObjects.Name = "SceneObjects";
			this.SceneObjects.Size = new Size(305,377);
			this.SceneObjects.TabIndex = 0;
			this.SceneObjects.WrapContents = false;
			// 
			// AddObject
			// 
			this.AddObject.Font = new Font("Segoe UI Semibold",13F);
			this.AddObject.Location = new Point(0,0);
			this.AddObject.Name = "AddObject";
			this.AddObject.Size = new Size(97,43);
			this.AddObject.TabIndex = 1;
			this.AddObject.Text = "Add";
			this.AddObject.UseVisualStyleBackColor = true;
			this.AddObject.Click += this.AddObject_Click;
			// 
			// ObjectControls
			// 
			this.ObjectControls.Controls.Add(this.RemoveObject);
			this.ObjectControls.Controls.Add(this.AddObject);
			this.ObjectControls.Location = new Point(51,402);
			this.ObjectControls.Name = "ObjectControls";
			this.ObjectControls.Size = new Size(225,43);
			this.ObjectControls.TabIndex = 0;
			// 
			// RemoveObject
			// 
			this.RemoveObject.Enabled = false;
			this.RemoveObject.Font = new Font("Segoe UI Semibold",13F);
			this.RemoveObject.Location = new Point(128,0);
			this.RemoveObject.Name = "RemoveObject";
			this.RemoveObject.Size = new Size(97,43);
			this.RemoveObject.TabIndex = 2;
			this.RemoveObject.Text = "Remove";
			this.RemoveObject.UseVisualStyleBackColor = true;
			this.RemoveObject.Click += this.RemoveObject_Click;
			// 
			// ObjectName
			// 
			this.ObjectName.AutoSize = true;
			this.ObjectName.Font = new Font("Segoe UI",14F);
			this.ObjectName.Location = new Point(330,12);
			this.ObjectName.Name = "ObjectName";
			this.ObjectName.Size = new Size(62,25);
			this.ObjectName.TabIndex = 1;
			this.ObjectName.Text = "Name";
			// 
			// ObjectNameBox
			// 
			this.ObjectNameBox.Font = new Font("Segoe UI",12F);
			this.ObjectNameBox.ForeColor = Color.FromArgb(64,64,64);
			this.ObjectNameBox.Location = new Point(418,12);
			this.ObjectNameBox.Name = "ObjectNameBox";
			this.ObjectNameBox.Size = new Size(194,29);
			this.ObjectNameBox.TabIndex = 2;
			this.ObjectNameBox.Tag = "String";
			this.ObjectNameBox.TextAlign = HorizontalAlignment.Center;
			// 
			// LambertianProperties
			// 
			this.LambertianProperties.Controls.Add(this.LambertianColorButton);
			this.LambertianProperties.Controls.Add(this.LambertianColor);
			this.LambertianProperties.Location = new Point(330,176);
			this.LambertianProperties.Name = "LambertianProperties";
			this.LambertianProperties.Size = new Size(282,213);
			this.LambertianProperties.TabIndex = 3;
			this.LambertianProperties.Visible = false;
			// 
			// LambertianColorButton
			// 
			this.LambertianColorButton.BackColor = Color.Red;
			this.LambertianColorButton.Cursor = Cursors.Hand;
			this.LambertianColorButton.Location = new Point(154,3);
			this.LambertianColorButton.Name = "LambertianColorButton";
			this.LambertianColorButton.Size = new Size(41,41);
			this.LambertianColorButton.TabIndex = 5;
			this.LambertianColorButton.Tag = "ColorPicker";
			// 
			// LambertianColor
			// 
			this.LambertianColor.AutoSize = true;
			this.LambertianColor.Font = new Font("Segoe UI",14F);
			this.LambertianColor.Location = new Point(0,10);
			this.LambertianColor.Name = "LambertianColor";
			this.LambertianColor.Size = new Size(58,25);
			this.LambertianColor.TabIndex = 4;
			this.LambertianColor.Text = "Color";
			// 
			// ColorPicker
			// 
			this.ColorPicker.Color = Color.Red;
			// 
			// MetalProperties
			// 
			this.MetalProperties.Controls.Add(this.MetalFuzzBox);
			this.MetalProperties.Controls.Add(this.MetalFuzz);
			this.MetalProperties.Controls.Add(this.MetalColorButton);
			this.MetalProperties.Controls.Add(this.MetalColor);
			this.MetalProperties.Location = new Point(330,176);
			this.MetalProperties.Name = "MetalProperties";
			this.MetalProperties.Size = new Size(282,213);
			this.MetalProperties.TabIndex = 0;
			this.MetalProperties.Visible = false;
			// 
			// MetalFuzzBox
			// 
			this.MetalFuzzBox.Font = new Font("Segoe UI",12F);
			this.MetalFuzzBox.ForeColor = Color.FromArgb(64,64,64);
			this.MetalFuzzBox.Location = new Point(68,0);
			this.MetalFuzzBox.Name = "MetalFuzzBox";
			this.MetalFuzzBox.Size = new Size(214,29);
			this.MetalFuzzBox.TabIndex = 4;
			this.MetalFuzzBox.Tag = "Double,Fuzz";
			this.MetalFuzzBox.Text = "Fuzz";
			this.MetalFuzzBox.TextAlign = HorizontalAlignment.Center;
			// 
			// MetalFuzz
			// 
			this.MetalFuzz.AutoSize = true;
			this.MetalFuzz.Font = new Font("Segoe UI",14F);
			this.MetalFuzz.Location = new Point(0,0);
			this.MetalFuzz.Name = "MetalFuzz";
			this.MetalFuzz.Size = new Size(50,25);
			this.MetalFuzz.TabIndex = 4;
			this.MetalFuzz.Text = "Fuzz";
			// 
			// MetalColorButton
			// 
			this.MetalColorButton.BackColor = Color.Red;
			this.MetalColorButton.Location = new Point(156,35);
			this.MetalColorButton.Name = "MetalColorButton";
			this.MetalColorButton.Size = new Size(41,41);
			this.MetalColorButton.TabIndex = 5;
			this.MetalColorButton.Tag = "ColorPicker";
			// 
			// MetalColor
			// 
			this.MetalColor.AutoSize = true;
			this.MetalColor.Font = new Font("Segoe UI",14F);
			this.MetalColor.Location = new Point(0,46);
			this.MetalColor.Name = "MetalColor";
			this.MetalColor.Size = new Size(58,25);
			this.MetalColor.TabIndex = 4;
			this.MetalColor.Text = "Color";
			// 
			// Material
			// 
			this.Material.AutoSize = true;
			this.Material.Font = new Font("Segoe UI",14F);
			this.Material.Location = new Point(330,130);
			this.Material.Name = "Material";
			this.Material.Size = new Size(82,25);
			this.Material.TabIndex = 4;
			this.Material.Text = "Material";
			// 
			// MaterialBox
			// 
			this.MaterialBox.Font = new Font("Segoe UI",12F);
			this.MaterialBox.ForeColor = Color.FromArgb(64,64,64);
			this.MaterialBox.FormattingEnabled = true;
			this.MaterialBox.Location = new Point(418,130);
			this.MaterialBox.Name = "MaterialBox";
			this.MaterialBox.Size = new Size(194,29);
			this.MaterialBox.TabIndex = 5;
			// 
			// DielectricProperties
			// 
			this.DielectricProperties.Controls.Add(this.DielectricIndexBox);
			this.DielectricProperties.Controls.Add(this.DielectricIndex);
			this.DielectricProperties.Location = new Point(330,176);
			this.DielectricProperties.Name = "DielectricProperties";
			this.DielectricProperties.Size = new Size(282,213);
			this.DielectricProperties.TabIndex = 6;
			this.DielectricProperties.Visible = false;
			// 
			// DielectricIndexBox
			// 
			this.DielectricIndexBox.Font = new Font("Segoe UI",12F);
			this.DielectricIndexBox.ForeColor = Color.FromArgb(64,64,64);
			this.DielectricIndexBox.Location = new Point(154,0);
			this.DielectricIndexBox.Name = "DielectricIndexBox";
			this.DielectricIndexBox.Size = new Size(128,29);
			this.DielectricIndexBox.TabIndex = 7;
			this.DielectricIndexBox.Tag = "Double,RefractionIndex";
			this.DielectricIndexBox.TextAlign = HorizontalAlignment.Center;
			// 
			// DielectricIndex
			// 
			this.DielectricIndex.AutoSize = true;
			this.DielectricIndex.Font = new Font("Segoe UI",14F);
			this.DielectricIndex.Location = new Point(0,0);
			this.DielectricIndex.Name = "DielectricIndex";
			this.DielectricIndex.Size = new Size(148,25);
			this.DielectricIndex.TabIndex = 7;
			this.DielectricIndex.Text = "Refraction index";
			// 
			// Radius
			// 
			this.Radius.AutoSize = true;
			this.Radius.Font = new Font("Segoe UI",14F);
			this.Radius.Location = new Point(330,53);
			this.Radius.Name = "Radius";
			this.Radius.Size = new Size(68,25);
			this.Radius.TabIndex = 7;
			this.Radius.Text = "Radius";
			// 
			// RadiusBox
			// 
			this.RadiusBox.Font = new Font("Segoe UI",12F);
			this.RadiusBox.ForeColor = Color.FromArgb(64,64,64);
			this.RadiusBox.Location = new Point(418,49);
			this.RadiusBox.Name = "RadiusBox";
			this.RadiusBox.Size = new Size(194,29);
			this.RadiusBox.TabIndex = 8;
			this.RadiusBox.Tag = "Double,Radius";
			this.RadiusBox.TextAlign = HorizontalAlignment.Center;
			// 
			// ObjectPosition
			// 
			this.ObjectPosition.AutoSize = true;
			this.ObjectPosition.Font = new Font("Segoe UI",14F);
			this.ObjectPosition.Location = new Point(330,92);
			this.ObjectPosition.Name = "ObjectPosition";
			this.ObjectPosition.Size = new Size(79,25);
			this.ObjectPosition.TabIndex = 9;
			this.ObjectPosition.Text = "Position";
			// 
			// PositionBox
			// 
			this.PositionBox.Font = new Font("Segoe UI",12F);
			this.PositionBox.ForeColor = Color.FromArgb(64,64,64);
			this.PositionBox.Location = new Point(418,88);
			this.PositionBox.Name = "PositionBox";
			this.PositionBox.Size = new Size(194,29);
			this.PositionBox.TabIndex = 10;
			this.PositionBox.Tag = "Vector";
			this.PositionBox.TextAlign = HorizontalAlignment.Center;
			// 
			// SceneEditor
			// 
			this.AutoScaleDimensions = new SizeF(7F,15F);
			this.AutoScaleMode = AutoScaleMode.Font;
			this.ClientSize = new Size(624,459);
			this.Controls.Add(this.PositionBox);
			this.Controls.Add(this.ObjectPosition);
			this.Controls.Add(this.RadiusBox);
			this.Controls.Add(this.Radius);
			this.Controls.Add(this.DielectricProperties);
			this.Controls.Add(this.MaterialBox);
			this.Controls.Add(this.Material);
			this.Controls.Add(this.MetalProperties);
			this.Controls.Add(this.LambertianProperties);
			this.Controls.Add(this.ObjectNameBox);
			this.Controls.Add(this.ObjectName);
			this.Controls.Add(this.ObjectControls);
			this.Controls.Add(this.SceneObjects);
			this.FormBorderStyle = FormBorderStyle.FixedSingle;
			this.Icon = (Icon)resources.GetObject("$this.Icon");
			this.MaximizeBox = false;
			this.MinimumSize = new Size(640,498);
			this.Name = "SceneEditor";
			this.Tag = "";
			this.Text = "Scene editor";
			this.ObjectControls.ResumeLayout(false);
			this.LambertianProperties.ResumeLayout(false);
			this.LambertianProperties.PerformLayout();
			this.MetalProperties.ResumeLayout(false);
			this.MetalProperties.PerformLayout();
			this.DielectricProperties.ResumeLayout(false);
			this.DielectricProperties.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();
		}

		#endregion

		private FlowLayoutPanel SceneObjects;
		private Button AddObject;
		private Panel ObjectControls;
		private Button RemoveObject;
		private Label ObjectName;
		private TextBox ObjectNameBox;
		private Panel LambertianProperties;
		private Label LambertianColor;
		private Panel LambertianColorButton;
		private ColorDialog ColorPicker;
		private Panel MetalProperties;
		private Label MetalFuzz;
		private Panel MetalColorButton;
		private Label MetalColor;
		private TextBox MetalFuzzBox;
		private Label Material;
		private ComboBox MaterialBox;
		private Panel DielectricProperties;
		private Label DielectricIndex;
		private TextBox DielectricIndexBox;
		private Label Radius;
		private TextBox RadiusBox;
		private Label ObjectPosition;
		private TextBox PositionBox;
	}
}