using System;
using System.Drawing;
using System.Windows.Forms;

namespace ScreenShots
{
    /// <summary>
    /// A form that covers all screens with a AreaSelect element.
    /// </summary>
    public partial class AreaSelectForm : Form
    {
        // Drawing control to display on the screen.
        private AreaSelect areaSelector;

        /// <summary>
        /// Gets or sets the area selected by the user.
        /// </summary>
        public Rectangle SelectedArea { get; set; }

        /// <summary>
        /// Creates a new intance of AreaSelectForm adding a drawing control.
        /// </summary>
        /// <param name="areaSelector">Drawing control to display on all screens.</param>
        public AreaSelectForm(AreaSelect areaSelector)
        {
            InitializeComponent();

            this.areaSelector = areaSelector;

            this.areaSelector.AreaSelected += areaSelector_AreaSelected;

            this.KeyDown += AreaSelectForm_KeyDown;

            Controls.Add(this.areaSelector);
        }

        /// <summary>
        /// Handles cancelation if user press ESC.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void AreaSelectForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                DialogResult = System.Windows.Forms.DialogResult.Cancel;
                Close();
            }
        }

        /// <summary>
        /// Handles the selection of an area in the drawing control.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void areaSelector_AreaSelected(object sender, EventArgs e)
        {
            SelectedArea = areaSelector.SelectedArea;
            DialogResult = System.Windows.Forms.DialogResult.OK;
            Close();
        }
    }
}
