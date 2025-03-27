namespace RayTracerUI; 
partial class RenderProgress: Form {
    public RenderProgress() {
        InitializeComponent();
        foreach(Control i in this.Controls) {
            i.Location = new Point(
                (this.ClientSize.Width - i.Width) / 2,
                i.Location.Y
            );
        }
    }
    public void SetProgress(int progress) => this.ProgressBar.Value = progress;
    public void SetMax(int maxProgress) => this.ProgressBar.Maximum = maxProgress;
}
