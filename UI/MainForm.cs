using System.Diagnostics;
using System.Media;
using System.Reflection;
using System.Text.Json;
using RayTracer;

namespace RayTracerUI;

public partial class MainForm : Form {
	private Dictionary<Control,double?[]> scalePositions;
	private Dictionary<Control,double?[]> scaleSizes;
	private Dictionary<Control,double[]> anchorPoints;
	private Dictionary<string,TextBox> cameraParamBoxes = [];
	private System.Drawing.Color outputDestDefaultColor;
	private JsonSerializerOptions serializeSaveOptions = new() { WriteIndented = true };
	internal Camera cam = new();
	internal Dictionary<string,Sphere> scene = [];
	internal bool shouldPromptSaveOption = false;

	public MainForm() {
		this.InitializeComponent();
		this.scalePositions = new Dictionary<Control,double?[]> {
			{this.WelcomeLabel, [0.5,null]},
			{this.DescriptionLabel, [0.5,null]},
			{this.OutputDestination, [0.5,null]},
			{this.ButtonsGroup, [0.5,null]},
		};
		this.scaleSizes = new Dictionary<Control,double?[]> {

		};
		this.anchorPoints = new Dictionary<Control,double[]> {
			{this.WelcomeLabel, [0.5,0]},
			{this.DescriptionLabel, [0.5,0]},
			{this.OutputDestination, [0.5,0]},
			{this.ButtonsGroup, [0.5,0]},
		};

		var properties = typeof(Camera).GetProperties();
		for (int i = 0; i < properties.Length; i++) {
			string propertyName = properties[i].Name;
			if (propertyName == "ScanLines" || propertyName == "IsRendering") continue;
			var panel = new Panel() {
				Size = new Size(377,26),
				Location = new Point(12,72 + 30 * (i - 2)), //subtract 2 bcuz of the 2 ignored properties
				Name = propertyName + "Panel"
			};
			var label = new Label() {
				Text = propertyName,
				Name = propertyName + "Label",
				AutoSize = true,
				Location = new Point(0,0),
				Font = new Font("Segoe UI",12)
			};
			var box = new TextBox() {
				Name = propertyName,
				Text = properties[i].GetValue(cam)!.ToString(),
				Size = new Size(226,29),
				Location = new Point(151,0),
				ForeColor = System.Drawing.Color.FromArgb(64,64,64),
				TextAlign = HorizontalAlignment.Center,
				Font = new Font("Segoe UI",12F)
			};
			box.KeyPress += (sender,args) => {
				if (args.KeyChar == (char)Keys.Enter) {
					handleInputs(box);
					args.Handled = true;
				}
				args.Handled =
					!char.IsControl(args.KeyChar)
					&& !char.IsDigit(args.KeyChar)
					&& args.KeyChar != '/'
					&& args.KeyChar != '-'
					&& args.KeyChar != '.'
					&& args.KeyChar != ' ';
			};
			box.Tag = properties[i].PropertyType.Name;
			scalePositions.Add(panel,[0.5,null]);
			anchorPoints.Add(panel,[0.5,0]);
			box.LostFocus += (sender,args) => handleInputs(box);

			this.cameraParamBoxes.Add(propertyName,box);
			panel.Controls.Add(label);
			panel.Controls.Add(box);
			this.Controls.Add(panel);
		}
		onResize(this,new EventArgs());

		this.outputDestDefaultColor = this.OutputDestination.BackColor;
		this.SaveFileDialog.InitialDirectory = Application.StartupPath;
	}

	private void handleInputs(Control element) {
		if (element.Tag is not string tag) return;
		object value;
		if (tag == "Vector3") {
			value = Vector3.FromString(element.Text);
			element.Text = value.ToString();
		} else {
			double[] margins = element.Name switch {
				"AspectRatio" => [0.1,3],
				"ImageHeight" => [10,2160],
				"SamplesPerPixel" => [1,500],
				"MaxDepth" => [1,100],
				"vFOV" => [5,180],
				_ => [1,999]
			};
			string[] split = element.Text.Split('/');
			value = Math.Clamp(
				split.Select(s => double.TryParse(s,out double n) ? n : 0).Aggregate((a,b) => a / b),
				margins[0],
				margins[1]
			);
			if (tag == "Int32") value = Convert.ToInt32(value);
			element.Text = value.ToString();
		}
		typeof(Camera).GetProperty(element.Name)?.SetValue(this.cam,value);
		this.shouldPromptSaveOption = true;
	}

	private async void BeginRender_Click(object sender,EventArgs e) {
		if (this.OutputPathSelection.SelectedPath == "") {
			this.OutputDestination.Text = "Please selected an output destination!";
			this.OutputDestination.BackColor = System.Drawing.Color.Red;
			SystemSounds.Asterisk.Play();
			onResize(this,new EventArgs());
			return;
		}
		if (this.shouldPromptSaveOption) {
			var result = MessageBox.Show(
			"You've made changed to the scene, would you like to save them before rendering the image?",
			"Caution",
			MessageBoxButtons.YesNoCancel,
			MessageBoxIcon.None
			);
			if (result == DialogResult.Yes) {
				this.SaveSceneButton_Click(this.SaveSceneButton,new EventArgs());
			} else if (result == DialogResult.Cancel) return;
		}
		if (this.scene.Count == 0) {
			DialogResult result = MessageBox.Show(
				text: "You haven't added any objects to the scene, are you sure you want to render an empty image?",
				caption: "Warning",
				buttons: MessageBoxButtons.YesNo,
				icon: MessageBoxIcon.Warning
			);
			if (result == DialogResult.No) return;
		} else if (this.scene.Count > 10) {
			DialogResult result = MessageBox.Show(
				text: "You added more than 10 objects to the scene, the rendering process may take some time. Do you wish to continue?",
				caption: "Caution",
				buttons: MessageBoxButtons.YesNo,
				icon: MessageBoxIcon.Warning
			);
			if (result == DialogResult.No) return;
		}
		this.Enabled = false;
		var progressForm = new RenderProgress();
		progressForm.Show();
		var renderTask = Task.Run(() => {
			HittableList hittables = new();
			string directory = this.OutputPathSelection.SelectedPath;
			string fullPath = directory + "\\output.jpeg";
			foreach (KeyValuePair<string,Sphere> i in this.scene) hittables.Add(i.Value);
			using Image? render = this.cam.Render(hittables);
			if (render == null) return;
			int counter = 1;
			while (File.Exists(fullPath)) {
				fullPath = directory + string.Format("\\output({0}).jpeg",counter);
				counter++;
			}
			render.Save(fullPath,System.Drawing.Imaging.ImageFormat.Jpeg);
		});
		//add an event for when you want to abort the render
		progressForm.StopRender.Click += (sender,args) => {
			progressForm.StopRender.Text = "Please wait";
			progressForm.Enabled = false;
			this.cam.IsRendering = false;
		};
		//waits until the rendering process begins
		while (cam.ScanLines == -1) Thread.Sleep(0);
		//updates the progress bar
		progressForm.SetMax(cam.ImageHeight);
		while (this.cam.IsRendering && this.cam.ScanLines < this.cam.ImageHeight) {
			progressForm.SetProgress(this.cam.ScanLines);
			await Task.Delay(100);
		}
		//closes the application
		progressForm.Close();
		Process.Start("explorer.exe",this.OutputPathSelection.SelectedPath);
		await renderTask; //wait for the file to get saved
		Application.Exit();
	}

	private void OutputDestination_Click(object sender,EventArgs e) {
		if (this.OutputPathSelection.ShowDialog() == DialogResult.OK) {
			this.OutputDestination.Text = "Output path: " + this.OutputPathSelection.SelectedPath + "\\output.jpeg";
			this.OutputDestination.BackColor = this.outputDestDefaultColor;
			onResize(this,new EventArgs());
		}
	}
	private void OpenSceneEditor_Click(object sender,EventArgs e) {
		using var sceneEditor = new SceneEditor();
		sceneEditor.ShowDialog();
	}

	private void onResize(object sender,EventArgs e) {
		foreach (KeyValuePair<Control,double?[]> kv in this.scaleSizes) {
			Utils.ScaleControlSize(kv.Key,kv.Value);
		}
		foreach (KeyValuePair<Control,double?[]> kv in this.scalePositions) {
			Utils.ScaleControlPosition(kv.Key,kv.Value,this.anchorPoints.GetValueOrDefault(kv.Key,[0,0]));
		}
	}

	private void SaveSceneButton_Click(object sender,EventArgs e) {
		if (this.SaveFileDialog.ShowDialog() != DialogResult.OK) return;
		this.Enabled = false;
		string path = this.SaveFileDialog.FileName;
		this.SaveFileDialog.FileName = ""; // resets the file name cuz otherwise it would be the full path on re-open
		if (Path.GetExtension(path) != ".rtc") path = Path.ChangeExtension(path,".rtc");
		using var writer = new StreamWriter(path);
		writer.WriteLine(JsonSerializer.Serialize(new { Camera = this.cam,Scene = this.scene },this.serializeSaveOptions));
		MessageBox.Show(
			$"Scene saved successfully to {Path.GetFileName(path)}!",
			"Success",
			MessageBoxButtons.OK,
			MessageBoxIcon.None
		);
		this.shouldPromptSaveOption = false;
		this.Enabled = true;
	}

	private void LoadSceneButton_Click(object sender,EventArgs e) {
		if (this.OpenFileDialog.ShowDialog() != DialogResult.OK) return;
		string path = this.OpenFileDialog.FileName;
		this.OpenFileDialog.FileName = "";
		if (Path.GetExtension(path) != ".rtc") {
			MessageBox.Show(
				"Unsupported file type! Please select an \".rtc\" file",
				"Oops...",
				MessageBoxButtons.OK,
				MessageBoxIcon.Error
			);
			return;
		}
		//deserialize objects and load them
		this.Enabled = false;
		try {
			using var reader = new StreamReader(path);
			var dict = JsonSerializer.Deserialize<Dictionary<string,JsonElement>>(reader.ReadToEnd());
			var errorMessage = $"Could not load the scene from file {Path.GetFileName(path)}";
			if (dict == null) throw new Exception(errorMessage);
			else {
				this.scene.Clear();
				this.cam = dict["Camera"].Deserialize<Camera>() ?? throw new Exception(errorMessage);
				this.scene = dict["Scene"].Deserialize<Dictionary<string,Sphere>>() ?? throw new Exception(errorMessage);
				foreach (PropertyInfo i in typeof(Camera).GetProperties()) {
					if (i.Name == "ScanLines" || i.Name == "IsRendering") continue;
					this.cameraParamBoxes[i.Name].Text = i.GetValue(this.cam)!.ToString();
				}
				dict.Clear();
			}
		} catch (Exception exception) {
			Console.WriteLine($"Source: {exception.Source}");
			Console.WriteLine($"Stack trace: {exception.StackTrace}");
			MessageBox.Show(
				exception.Message,
				"Oops...",
				MessageBoxButtons.OK,
				MessageBoxIcon.Error
			);
			this.Enabled = true;
			return;
		}
		MessageBox.Show(
			"Scene loaded successfully!",
			"Yippie",
			MessageBoxButtons.OK,
			MessageBoxIcon.None
		);
		this.shouldPromptSaveOption = false;
		this.Enabled = true;
	}

	private void OnClosing(object sender,FormClosingEventArgs e) {
		if (!this.shouldPromptSaveOption || e.CloseReason != CloseReason.UserClosing) {
			e.Cancel = false;
			return;
		}
		var result = MessageBox.Show(
			"You've made changed to the scene, would you like to save them?",
			"Caution",
			MessageBoxButtons.YesNoCancel,
			MessageBoxIcon.None
		);
		if (result == DialogResult.Yes) {
			this.SaveSceneButton_Click(this.SaveSceneButton,new EventArgs());
			e.Cancel = false;
		} else if (result == DialogResult.No) e.Cancel = false;
		else e.Cancel = true;
	}
}
