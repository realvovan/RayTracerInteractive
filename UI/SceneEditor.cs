using RayTracer;

namespace RayTracerUI;

enum Materials {
	Lambertian,
	Metal,
	Dielectric
}
public partial class SceneEditor : Form {
	private Label? selectedObject;
	public SceneEditor() {
		InitializeComponent();
		foreach (var i in Enum.GetValues(typeof(Materials))) {
			this.MaterialBox.Items.Add(i);
		}
		setUpTextBoxEvent(parent: this);
		foreach (KeyValuePair<string,Sphere> i in Program.mainForm.scene) {
			var label = new Label() {
				BackColor = System.Drawing.Color.Gray,
				Font = new Font("Segoe UI",10F),
				ForeColor = System.Drawing.Color.White,
				Location = new Point(0,0),
				Size = new Size(299,23),
				AutoSize = false,
				Name = i.Key,
				Text = getObjectLabelText(i.Key,i.Value),
			};
			label.Click += onSceneObjectClick;
			this.SceneObjects.Controls.Add(label);
		}
		Utils.ScaleControlPosition(this.RenderPreview,[0.8235,null],[0.5,0]);
		Utils.ScaleControlPosition(this.ImagePreview,[0.8235,null],[0.5,0]);
	}
	private void onKeyPressTagDouble(object? sender,KeyPressEventArgs args) {
		if (args.KeyChar == (char)Keys.Enter) {
			onFocusLostTagDouble(sender,new EventArgs());
			args.Handled = true;
		}
		args.Handled =
			!char.IsControl(args.KeyChar)
			&& !char.IsDigit(args.KeyChar)
			&& args.KeyChar != '-'
			&& args.KeyChar != '.';
	}
	private void onKeyPressTagVector(object? sender,KeyPressEventArgs args) {
		if (args.KeyChar == (char)Keys.Enter) {
			onFocusLostTagVector(sender,new EventArgs());
			args.Handled = true;
		}
		args.Handled =
			!char.IsControl(args.KeyChar)
			&& !char.IsDigit(args.KeyChar)
			&& args.KeyChar != '-'
			&& args.KeyChar != '.'
			&& args.KeyChar != ' ';
	}
	private void onKeyPressTagString(object? sender,KeyPressEventArgs args) {
		if (args.KeyChar == (char)Keys.Enter) {
			onFocusLostTagString(sender,new EventArgs());
			args.Handled = true;
		}
	}
	private void onFocusLostTagVector(object? sender,EventArgs args) {
		//if its a vector its only position
		if (sender is not TextBox control || this.selectedObject == null) return;
		var newPosition = Vector3.FromString(control.Text);
		var sphere = Program.mainForm.scene[this.selectedObject.Name];
		sphere.Center = newPosition;
		control.Text = newPosition.ToString();
		this.selectedObject.Text = getObjectLabelText(this.selectedObject.Name,sphere);
		Program.mainForm.shouldPromptSaveOption = true;
	}
	private void onFocusLostTagString(object? sender,EventArgs args) {
		//if its a string type it can only be name so yeah
		if (sender is not TextBox control || this.selectedObject == null || control.Text == this.selectedObject.Name) return;
		var sceneDict = Program.mainForm.scene;
		if (sceneDict.ContainsKey(control.Text)) {
			int counter = 1;
			while (sceneDict.ContainsKey(control.Text + counter)) counter++;
			control.Text += counter;
		}
		var temp = sceneDict[this.selectedObject.Name];
		sceneDict.Remove(this.selectedObject.Name);
		sceneDict.Add(control.Text,temp);
		this.selectedObject.Name = control.Text;
		this.selectedObject.Text = getObjectLabelText(control.Text,sceneDict[control.Text]);
		Program.mainForm.shouldPromptSaveOption = true;
	}
	private void onFocusLostTagDouble(object? sender,EventArgs args) {
		if (sender is not TextBox control || this.selectedObject == null) return;
		var sphere = Program.mainForm.scene[this.selectedObject.Name];

		if (control.Parent!.Name == this.Name) {
			//sphere property
			var propertyName = (control.Tag as string[])![1];
			typeof(Sphere).GetProperty(propertyName)!.SetValue(sphere,Convert.ToDouble(control.Text));
		} else {
			//material property
			var propertyName = (control.Tag as string[])![1];
			sphere.Material.GetType().GetProperty(propertyName)?.SetValue(sphere.Material,Convert.ToDouble(control.Text));
		}
		this.selectedObject.Text = getObjectLabelText(this.selectedObject.Name,sphere);
		Program.mainForm.shouldPromptSaveOption = true;
	}
	private void onMaterialBoxChanged(object? sender,EventArgs args) {
		if (sender is not ComboBox control || this.selectedObject == null) return;
		Sphere sphere = Program.mainForm.scene[this.selectedObject.Name];
		if (control.SelectedItem is Materials.Lambertian) {
			this.LambertianProperties.Visible = true;
			this.MetalProperties.Visible = false;
			this.DielectricProperties.Visible = false;
			sphere.Material = new Lambertian(new RayTracer.Color(
				this.LambertianColorButton.BackColor.R / 255d,
				this.LambertianColorButton.BackColor.G / 255d,
				this.LambertianColorButton.BackColor.B / 255d
			));
		} else if (control.SelectedItem is Materials.Metal) {
			this.LambertianProperties.Visible = false;
			this.MetalProperties.Visible = true;
			this.DielectricProperties.Visible = false;
			sphere.Material = new Metal(new RayTracer.Color(
				this.MetalColorButton.BackColor.R / 255d,
				this.MetalColorButton.BackColor.G / 255d,
				this.MetalColorButton.BackColor.B / 255d
			),Convert.ToDouble(this.MetalFuzzBox.Text));
		} else if (control.SelectedItem is Materials.Dielectric) {
			this.LambertianProperties.Visible = false;
			this.MetalProperties.Visible = false;
			this.DielectricProperties.Visible = true;
			sphere.Material = new Dielectric(Convert.ToDouble(this.DielectricIndexBox.Text));
		}
		this.selectedObject.Text = getObjectLabelText(this.selectedObject.Name,sphere);
		Program.mainForm.shouldPromptSaveOption = true;
	}
	private void onFocusLostMaterialBox(object? sender,EventArgs args) {
		//if the user types in a material and it's not real, it defaults to Lambertian
		if (sender is not ComboBox control || this.selectedObject == null) return;
		bool success = Enum.TryParse<Materials>(control.Text,out var result);
		control.SelectedItem = success ? result : Materials.Lambertian;
	}
	private void onColorPickerPressed(object? sender,EventArgs args) {
		if (sender is not Panel control || this.selectedObject == null) return;
		Sphere sphere = Program.mainForm.scene[this.selectedObject.Name];
		this.ColorPicker.ShowDialog();
		control.BackColor = this.ColorPicker.Color;
		RayTracer.Color color = new(control.BackColor.R / 255d,control.BackColor.G / 255d,control.BackColor.B / 255d);
		if (sphere.Material is Lambertian) ((Lambertian)sphere.Material).Albedo = color;
		else if (sphere.Material is Metal) ((Metal)sphere.Material).Albedo = color;
		Program.mainForm.shouldPromptSaveOption = true;
	}
	private void setUpTextBoxEvent(Control parent) {
		foreach (Control i in parent.Controls) {
			if (i is TextBox && i.Tag is string tag) {
				string[] split = tag.Split(','); //first element is type, second element is property name
				i.Tag = split;
				switch (split[0]) {
					case "String":
						i.KeyPress += onKeyPressTagString;
						i.LostFocus += onFocusLostTagString;
						break;
					case "Double":
						i.KeyPress += onKeyPressTagDouble;
						i.LostFocus += onFocusLostTagDouble;
						i.Text = "0";
						break;
					case "Vector":
						i.KeyPress += onKeyPressTagVector;
						i.LostFocus += onFocusLostTagVector;
						break;
				}
			} else if (i is Panel) {
				if (i.Tag is "ColorPicker") {
					i.Click += onColorPickerPressed;
				} else {
					setUpTextBoxEvent(i);
				}
			} else if (i is ComboBox box) {
				box.SelectedIndexChanged += onMaterialBoxChanged;
				box.LostFocus += onFocusLostMaterialBox;
			}
		}
	}
	private void onSceneObjectClick(object? sender,EventArgs args) {
		if (sender is not Label label) return;
		foreach (Label i in this.SceneObjects.Controls) {
			i.BackColor = System.Drawing.Color.Gray;
			i.Font = new Font("Segoe UI",10F);
		}
		this.selectedObject = label;

		var obj = Program.mainForm.scene[label.Name];
		label.BackColor = System.Drawing.Color.FromArgb(0,120,215);
		label.Font = new Font("Segoe UI",10F,FontStyle.Bold);
		this.ObjectNameBox.Text = label.Name;
		this.RadiusBox.Text = obj.Radius.ToString();
		this.PositionBox.Text = obj.Center.ToString();
		this.RemoveObject.Enabled = true;
		if (obj.Material is Lambertian lambertian) {
			this.LambertianProperties.Visible = true;
			this.MetalProperties.Visible = false;
			this.DielectricProperties.Visible = false;
			this.LambertianColorButton.BackColor = System.Drawing.Color.FromArgb(
				(int)(lambertian.Albedo.R * 255),(int)(lambertian.Albedo.G * 255),(int)(lambertian.Albedo.B * 255)
			);
			this.MaterialBox.SelectedItem = Materials.Lambertian;
		} else if (obj.Material is Metal metal) {
			this.LambertianProperties.Visible = false;
			this.MetalProperties.Visible = true;
			this.DielectricProperties.Visible = false;
			this.MetalFuzzBox.Text = metal.Fuzz.ToString();
			this.MetalColorButton.BackColor = System.Drawing.Color.FromArgb(
				(int)(metal.Albedo.R * 255),(int)(metal.Albedo.G * 255),(int)(metal.Albedo.B * 255)
			);
			this.MaterialBox.SelectedItem = Materials.Metal;
		} else if (obj.Material is Dielectric dielectric) {
			this.LambertianProperties.Visible = false;
			this.MetalProperties.Visible = false;
			this.DielectricProperties.Visible = true;
			this.DielectricIndexBox.Text = dielectric.RefractionIndex.ToString();
			this.MaterialBox.SelectedItem = Materials.Dielectric;
		}
	}
	private void AddObject_Click(object sender,EventArgs e) {
		var sceneDict = Program.mainForm.scene;
		int counter = 1;
		while (sceneDict.ContainsKey("Sphere" + counter)) counter++;
		this.ObjectNameBox.Text = "Sphere" + counter;
		this.RadiusBox.Text = "1";
		this.PositionBox.Text = "0 0 0";
		this.MaterialBox.SelectedItem = Materials.Lambertian;
		this.LambertianProperties.Visible = true;
		this.RemoveObject.Enabled = true;
		foreach (Control i in this.SceneObjects.Controls) {
			i.BackColor = System.Drawing.Color.Gray;
			i.Font = new Font("Segoe UI",10F);
		}

		var obj = new Sphere(new Vector3(0,0,0),1,new Lambertian(new RayTracer.Color(1,0,0)));
		sceneDict.Add(this.ObjectNameBox.Text,obj);

		selectedObject = new Label() {
			BackColor = System.Drawing.Color.FromArgb(0,120,215),
			Font = new Font("Segoe UI",10F,FontStyle.Bold),
			ForeColor = System.Drawing.Color.White,
			Location = new Point(0,0),
			Size = new Size(299,23),
			AutoSize = false,
			Name = this.ObjectNameBox.Text,
			Text = getObjectLabelText(this.ObjectNameBox.Text,obj),
		};

		selectedObject.Click += onSceneObjectClick;

		this.SceneObjects.Controls.Add(selectedObject);
		Program.mainForm.shouldPromptSaveOption = true;
	}
	private void RemoveObject_Click(object sender,EventArgs e) {
		if (this.selectedObject == null) return;
		Program.mainForm.scene.Remove(this.selectedObject.Name);
		this.selectedObject.Dispose();
		this.selectedObject = null;

		this.RemoveObject.Enabled = false;
		this.LambertianProperties.Visible = false;
		this.MetalProperties.Visible = false;
		this.DielectricProperties.Visible = false;
		this.ObjectNameBox.Text = "";
		this.RadiusBox.Text = "";
		this.PositionBox.Text = "";
		Program.mainForm.shouldPromptSaveOption = true;
	}

	private async void RenderPreview_Click(object sender,EventArgs e) {
		this.RenderPreview.Enabled = false;
		this.UseWaitCursor = true;
		Camera renderCam = Program.mainForm.cam;
		Camera previewCam = new();
		HittableList previewScene = new();
		foreach (var property in typeof(Camera).GetFields()) {
			property.SetValue(previewCam,property.GetValue(renderCam));
		}
		previewCam.ImageHeight = 240;
		foreach (KeyValuePair<string,Sphere> i in Program.mainForm.scene) {
			previewScene.Add(new Sphere(
				new Vector3(i.Value.Center.X,i.Value.Center.Y,i.Value.Center.Z),
				i.Value.Radius,
				i.Value.Material.Clone()
			));
		}
		Image? preview = await Task.Run(() => previewCam.Render(previewScene));
		if (preview != null) {
			this.ImagePreview.Image = preview;
			this.ImagePreview.Width = (int)(this.ImagePreview.Height * previewCam.AspectRatio);
			Utils.ScaleControlPosition(this.ImagePreview,[0.8235,null],[0.5,0]);
		};
		previewScene.Clear();
		this.RenderPreview.Enabled = true;
		this.UseWaitCursor = false;
	}

	private static string getObjectLabelText(string name,Sphere obj)
		=> $"{name}, radius: {obj.Radius}, position: {obj.Center}, material: {obj.Material.GetType().Name}";
}
