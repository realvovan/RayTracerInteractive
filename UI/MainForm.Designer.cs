namespace RayTracerUI
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

		#region Windows Form Designer generated code

		/// <summary>
		///  Required method for Designer support - do not modify
		///  the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.WelcomeLabel = new Label();
			this.DescriptionLabel = new Label();
			this.BeginRender = new Button();
			this.OutputPathSelection = new FolderBrowserDialog();
			this.OutputDestination = new Label();
			this.OpenSceneEditor = new Button();
			this.ButtonsGroup = new Panel();
			this.ButtonsGroup.SuspendLayout();
			this.SuspendLayout();
			// 
			// WelcomeLabel
			// 
			this.WelcomeLabel.AutoSize = true;
			this.WelcomeLabel.BackColor = Color.Transparent;
			this.WelcomeLabel.Font = new Font("Segoe UI",16F,FontStyle.Bold);
			this.WelcomeLabel.Location = new Point(18,9);
			this.WelcomeLabel.Name = "WelcomeLabel";
			this.WelcomeLabel.Size = new Size(377,30);
			this.WelcomeLabel.TabIndex = 0;
			this.WelcomeLabel.Text = "Welcome to the amazing ray tracer";
			// 
			// DescriptionLabel
			// 
			this.DescriptionLabel.AutoSize = true;
			this.DescriptionLabel.BackColor = Color.Transparent;
			this.DescriptionLabel.Font = new Font("Segoe UI",16F);
			this.DescriptionLabel.Location = new Point(61,39);
			this.DescriptionLabel.Name = "DescriptionLabel";
			this.DescriptionLabel.Size = new Size(293,30);
			this.DescriptionLabel.TabIndex = 1;
			this.DescriptionLabel.Tag = "";
			this.DescriptionLabel.Text = "Select camera settings below";
			// 
			// BeginRender
			// 
			this.BeginRender.Font = new Font("Segoe UI Semibold",18F);
			this.BeginRender.Location = new Point(165,0);
			this.BeginRender.Name = "BeginRender";
			this.BeginRender.Size = new Size(159,74);
			this.BeginRender.TabIndex = 7;
			this.BeginRender.Text = "Render";
			this.BeginRender.UseVisualStyleBackColor = true;
			this.BeginRender.Click += this.BeginRender_Click;
			// 
			// OutputPathSelection
			// 
			this.OutputPathSelection.Description = "Select the output destination";
			// 
			// OutputDestination
			// 
			this.OutputDestination.AutoSize = true;
			this.OutputDestination.BackColor = Color.FromArgb(199,197,193);
			this.OutputDestination.Cursor = Cursors.Hand;
			this.OutputDestination.Font = new Font("Segoe UI",12F,FontStyle.Underline);
			this.OutputDestination.Location = new Point(82,426);
			this.OutputDestination.Name = "OutputDestination";
			this.OutputDestination.Size = new Size(235,21);
			this.OutputDestination.TabIndex = 0;
			this.OutputDestination.Text = "Click to select output destination";
			this.OutputDestination.Click += this.OutputDestination_Click;
			// 
			// OpenSceneEditor
			// 
			this.OpenSceneEditor.Font = new Font("Segoe UI Semibold",18F);
			this.OpenSceneEditor.Location = new Point(0,0);
			this.OpenSceneEditor.Name = "OpenSceneEditor";
			this.OpenSceneEditor.Size = new Size(159,74);
			this.OpenSceneEditor.TabIndex = 8;
			this.OpenSceneEditor.Text = "Open Scene editor";
			this.OpenSceneEditor.UseVisualStyleBackColor = true;
			this.OpenSceneEditor.Click += this.OpenSceneEditor_Click;
			// 
			// ButtonsGroup
			// 
			this.ButtonsGroup.Controls.Add(this.BeginRender);
			this.ButtonsGroup.Controls.Add(this.OpenSceneEditor);
			this.ButtonsGroup.Location = new Point(30,461);
			this.ButtonsGroup.Name = "ButtonsGroup";
			this.ButtonsGroup.Size = new Size(324,74);
			this.ButtonsGroup.TabIndex = 10;
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new SizeF(7F,15F);
			this.AutoScaleMode = AutoScaleMode.Font;
			this.ClientSize = new Size(407,547);
			this.Controls.Add(this.ButtonsGroup);
			this.Controls.Add(this.OutputDestination);
			this.Controls.Add(this.DescriptionLabel);
			this.Controls.Add(this.WelcomeLabel);
			this.Icon = (Icon)resources.GetObject("$this.Icon");
			this.MinimumSize = new Size(423,586);
			this.Name = "MainForm";
			this.Text = "Ray tracer";
			this.Resize += this.onResize;
			this.ButtonsGroup.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();
		}

		#endregion

		private Label WelcomeLabel;
		private Label DescriptionLabel;
		private Button BeginRender;
		private FolderBrowserDialog OutputPathSelection;
		private Label OutputDestination;
		private Button OpenSceneEditor;
		private Panel ButtonsGroup;
	}
}
