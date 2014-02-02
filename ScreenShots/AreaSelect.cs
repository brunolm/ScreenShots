using System;
using System.Drawing;
using System.Windows.Forms;

namespace ScreenShots
{
    /// <summary>
    /// Creates a panel that allows the user to draw a rectangle on it.
    /// </summary>
    public class AreaSelect : Panel
    {
        /// <summary>
        /// Triggers when an area is selected.
        /// </summary>
        public event EventHandler AreaSelected;

        /// <summary>
        /// Gets whether the user is drawing or not.
        /// </summary>
        public bool Drawing { get; private set; }

        /// <summary>
        /// Gets the start point of the drawing.
        /// </summary>
        public Point StartPoint { get; private set; }

        /// <summary>
        /// Gets the current point of the drawing.
        /// </summary>
        public Point CurrentPoint { get; private set; }

        /// <summary>
        /// Gets the rectangle area selected by the user.
        /// </summary>
        public Rectangle SelectedArea
        {
            get
            {
                if (StartPoint == default(Point)
                    || CurrentPoint == default(Point))
                {
                    return default(Rectangle);
                }

                return new Rectangle
                (
                    Math.Min(StartPoint.X, CurrentPoint.X),
                    Math.Min(StartPoint.Y, CurrentPoint.Y),
                    Math.Abs(StartPoint.X - CurrentPoint.X),
                    Math.Abs(StartPoint.Y - CurrentPoint.Y)
                );
            }
        }

        /// <summary>
        /// Creates a new intance of AreaSelect.
        /// </summary>
        /// <remarks>
        /// This control redraws on resize and uses double buffer to avoid flickering.
        /// </remarks>
        public AreaSelect()
        {
            this.DoubleBuffered = true;
            this.SetStyle(ControlStyles.ResizeRedraw, true);

            this.Cursor = Cursors.Cross;

            this.MouseDown += areaSelect_MouseDown;
            this.MouseMove += areaSelect_MouseMove;
            this.MouseUp += areaSelect_MouseUp;
            this.Paint += areaSelect_Paint;
        }

        /// <summary>
        /// Handles drawing start.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void areaSelect_MouseDown(object sender, MouseEventArgs e)
        {
            this.Drawing = true;
            this.StartPoint = this.CurrentPoint = e.Location;
        }

        /// <summary>
        /// Handles resize of the rectangle being drawn.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void areaSelect_MouseMove(object sender, MouseEventArgs e)
        {
            if (this.Drawing)
            {
                this.CurrentPoint = e.Location;
                this.Invalidate();
            }
        }

        /// <summary>
        /// Handles the completion of the drawing.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void areaSelect_MouseUp(object sender, MouseEventArgs e)
        {
            if (this.Drawing)
            {
                this.Drawing = false;
                this.Invalidate();
                
                if (AreaSelected != null)
                {
                    AreaSelected(this, EventArgs.Empty);
                }
            }
        }

        /// <summary>
        /// Handles control painting.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void areaSelect_Paint(object sender, PaintEventArgs e)
        {
            if (this.Drawing)
            {
                e.Graphics.DrawRectangle(Pens.Red, this.SelectedArea);
            }
        }
    }
}
