using System;
using System.Drawing;
using System.Windows.Forms;

namespace SUDOKU
{
    public partial class SudokuBt : Button
    {
        /// <summary>
        /// Vodorovná souřadnice tlačítka v mřížce
        /// </summary>
        public int X { get; set; }
        /// <summary>
        /// Svislá souřadnice tlačítka v mřížce
        /// </summary>
        public int Y { get; set; }
        /// <summary>
        /// Základní nastavení
        /// </summary>
        public SudokuBt()
        {
            BackColor = Color.White;
            ForeColor = Color.Black;
        }
        /// <summary>
        /// Základní velikost
        /// </summary>
        protected override Size DefaultSize
        {
            get {return new Size(30, 30); }
        }
        /// <summary>
         /// Základní podoba tlačítka
         /// </summary>
        protected override void OnPaint(PaintEventArgs pevent)
        {
            pevent.Graphics.FillRectangle(new SolidBrush(BackColor), 0, 0, Width, Height);
            pevent.Graphics.DrawString(Text, new Font("Open Sans", Height / 2, FontStyle.Bold), Brushes.Black, Width / 4, Height / 10);
        }
        /// <summary>
        /// Základní vlastnost při najetí kurzorem myši na tlačítko
        /// </summary>
        protected override void OnMouseEnter(EventArgs e)
        {
            if(BackColor != Color.FromArgb(252, 188, 110))
            {
                BackColor = Color.LightBlue;
            }
            base.OnMouseEnter(e);
        }
        /// <summary>
        /// Základní vlastnost při vyjetí kurzorem myši z tlačítka
        /// </summary>
        protected override void OnMouseLeave(EventArgs e)
        {
            if (BackColor != Color.FromArgb(252, 188, 110))
            {
                BackColor = Color.White;
            }
            base.OnMouseLeave(e);
        }
    }
}
