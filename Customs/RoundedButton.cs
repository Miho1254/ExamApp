using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExamApp.Customs
{
    public partial class RoundedButton : Button
    {
        private Color borderColor = Color.Transparent;
        private Color hoverColor = Color.LightGray;
        private Color originalBackColor;
        private int borderRadius = 20;

        public Color BorderColor
        {
            get { return borderColor; }
            set { borderColor = value; Invalidate(); }
        }

        public Color HoverColor
        {
            get { return hoverColor; }
            set { hoverColor = value; Invalidate(); }
        }

        public int BorderRadius
        {
            get { return borderRadius; }
            set { borderRadius = value; Invalidate(); }
        }

        public RoundedButton()
        {
            FlatStyle = FlatStyle.Flat;
            FlatAppearance.BorderSize = 0;
            FlatAppearance.MouseDownBackColor = Color.Transparent;
            FlatAppearance.MouseOverBackColor = Color.Transparent;
            Cursor = Cursors.Hand;
            originalBackColor = BackColor; // Lưu trữ màu nền ban đầu
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            GraphicsPath path = new GraphicsPath();
            path.AddArc(0, 0, BorderRadius * 2, BorderRadius * 2, 180, 90);
            path.AddArc(Width - BorderRadius * 2, 0, BorderRadius * 2, BorderRadius * 2, 270, 90);
            path.AddArc(Width - BorderRadius * 2, Height - BorderRadius * 2, BorderRadius * 2, BorderRadius * 2, 0, 90);
            path.AddArc(0, Height - BorderRadius * 2, BorderRadius * 2, BorderRadius * 2, 90, 90);
            path.CloseFigure();

            Region = new Region(path);

            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            // Fill background with appropriate color
            using (SolidBrush brush = new SolidBrush(BackColor))
            {
                e.Graphics.FillPath(brush, path);
            }

            // Draw border
            using (Pen pen = new Pen(BorderColor, 1.5f))
            {
                e.Graphics.DrawPath(pen, path);
            }

            // Draw text centered within the button
            SizeF textSize = e.Graphics.MeasureString(Text, Font);
            PointF textLocation = new PointF((Width - textSize.Width) / 2, (Height - textSize.Height) / 2);
            using (SolidBrush textBrush = new SolidBrush(ForeColor))
            {
                e.Graphics.DrawString(Text, Font, textBrush, textLocation);
            }
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            originalBackColor = BackColor; // Lưu trữ màu nền ban đầu
            BackColor = HoverColor;
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            BackColor = originalBackColor; // Khôi phục màu nền ban đầu
        }
    }


}
