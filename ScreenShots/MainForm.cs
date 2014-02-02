using System;
using System.Linq;
using System.Windows.Forms;

namespace ScreenShots
{
    public partial class MainForm : Form
    {
        KeyboardHook hook = new KeyboardHook();

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            hook.KeyPressed += new EventHandler<KeyPressedEventArgs>(hook_KeyPressed);
            hook.RegisterHotKey(ScreenShots.ModifierKeys.Control, Keys.PrintScreen);
        }

        private void hook_KeyPressed(object sender, KeyPressedEventArgs e)
        {
            if (e.Modifier == ScreenShots.ModifierKeys.Control)
            {
                ScreenShotTakeFromArea();
            }
        }

        /// <summary>
        /// Take a screenshot from all monitors and display in a form that conver all screens.
        /// Then allows the user to draw a rectangle to select the area to copy, returning a crop image and
        /// setting on the clipboard.
        /// </summary>
        private void ScreenShotTakeFromArea()
        {
            var img = ScreenManager.TakeScreenshot();
            var frm = new AreaSelectForm(new AreaSelect { Dock = DockStyle.Fill, BackgroundImage = img });

            var firstScreen = Screen.AllScreens.OrderBy(o => o.Bounds.X).First();
            var totalWidth = Screen.AllScreens.Sum(o => o.Bounds.Width);
            var totalHeight = Screen.AllScreens.Sum(o => o.Bounds.Height);

            frm.SetBounds(firstScreen.Bounds.X, 0, totalWidth, totalHeight);

            if (frm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                var croppedImage = img.Crop(frm.SelectedArea);

                Clipboard.Clear();
                Clipboard.SetImage(croppedImage);

                picPreview.Image = croppedImage;

                img.Dispose();
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (picPreview.Image != null)
            {
                DialogSaveImage.FileName = String.Format("{0}_Screenshot.png", DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss"));
                if (DialogSaveImage.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    picPreview.Image.Save(DialogSaveImage.FileName);
                }
            }
        }

        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
