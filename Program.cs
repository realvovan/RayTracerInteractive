#nullable disable
using RayTracer;
namespace RayTracerUI;

static class Program {
	internal static MainForm mainForm;
	[STAThread]
	static void Main() {
		ApplicationConfiguration.Initialize();
		Application.Run(mainForm = new MainForm());
	}
}