namespace RayTracerUI;

static class Utils {
	public static void ScaleControlPosition(Control control,double?[] scale,double[] anchorPoint) {
		if (control.Parent == null) return;
		control.Location = new Point(
			scale[0] is double x ? (int)Math.Round(control.Parent.ClientSize.Width * x - control.ClientSize.Width * anchorPoint[0]) : control.Location.X,
			scale[1] is double y ? (int)Math.Round(control.Parent.ClientSize.Height * y - control.ClientSize.Height * anchorPoint[1]) : control.Location.Y
		);	
	}
	public static void ScaleControlSize(Control control,double?[] scale) {
		if (control.Parent == null) return;
		control.ClientSize = new Size(
			scale[0] is double x ? (int)Math.Round(control.Parent.ClientSize.Width * x) : control.ClientSize.Width,
			scale[1] is double y ? (int)Math.Round(control.Parent.ClientSize.Height * y) : control.ClientSize.Height
		);
	}
}
