namespace RayTracerUI; 
partial class RenderProgress {
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
		this.Title = new Label();
		this.ProgressBar = new ProgressBar();
		this.StopRender = new Button();
		this.SuspendLayout();
		// 
		// Title
		// 
		this.Title.AutoSize = true;
		this.Title.Font = new Font("Segoe UI",14F);
		this.Title.Location = new Point(22,9);
		this.Title.Name = "Title";
		this.Title.Size = new Size(302,25);
		this.Title.TabIndex = 0;
		this.Title.Text = "Rendering the image, please wait...";
		this.Title.UseWaitCursor = true;
		// 
		// ProgressBar
		// 
		this.ProgressBar.Location = new Point(22,49);
		this.ProgressBar.Name = "ProgressBar";
		this.ProgressBar.Size = new Size(302,23);
		this.ProgressBar.Step = 1;
		this.ProgressBar.TabIndex = 0;
		this.ProgressBar.UseWaitCursor = true;
		// 
		// StopRender
		// 
		this.StopRender.Font = new Font("Segoe UI",10F);
		this.StopRender.Location = new Point(112,83);
		this.StopRender.Name = "StopRender";
		this.StopRender.Size = new Size(117,28);
		this.StopRender.TabIndex = 1;
		this.StopRender.Text = "Abort rendering";
		this.StopRender.UseVisualStyleBackColor = true;
		this.StopRender.UseWaitCursor = true;
		// 
		// RenderProgress
		// 
		this.AutoScaleDimensions = new SizeF(7F,15F);
		this.AutoScaleMode = AutoScaleMode.Font;
		this.ClientSize = new Size(346,122);
		this.Controls.Add(this.StopRender);
		this.Controls.Add(this.ProgressBar);
		this.Controls.Add(this.Title);
		this.FormBorderStyle = FormBorderStyle.FixedDialog;
		this.MaximizeBox = false;
		this.Name = "RenderProgress";
		this.Text = "Rendering...";
		this.UseWaitCursor = true;
		this.ResumeLayout(false);
		this.PerformLayout();
	}

	#endregion

	private Label Title;
	private ProgressBar ProgressBar;
	public Button StopRender;
}