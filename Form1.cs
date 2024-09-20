using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Lab1
{
    public partial class Form1 : Form
    {
        private List<Point> points;
        private Point? P1, P2, P3;
        private float radius = 0;
        private bool rectangleDrawn = false;
        private bool circleDrawn = false;
        private Grid grid;
        private RectangleShape rectangleShape;
        private Circle circle;
        private AreaCalculator areaCalculator;

        public Form1()
        {
            InitializeComponent();
            points = new List<Point>();
            grid = new Grid();
            rectangleShape = new RectangleShape();
            circle = new Circle();
            areaCalculator = new AreaCalculator();

            panel1.Paint += DrawGridAndShapes;
            panel1.MouseClick += AddPoint;
            button1.Click += ClearPoints;
        }

        private void DrawGridAndShapes(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            int width = panel1.ClientSize.Width;
            int height = panel1.ClientSize.Height;

            grid.DrawGrid(g, width, height);

            g.DrawLine(Pens.Black, width / 2, 0, width / 2, height);  
            g.DrawLine(Pens.Black, 0, height / 2, width, height / 2); 

            foreach (var point in points)
            {
                g.FillEllipse(Brushes.Red, point.X - 5, point.Y - 5, 10, 10);
            }

            if (P1 != null && P2 != null && rectangleDrawn)
            {
                var rect = rectangleShape.GetRectangleFromPoints(P1.Value, P2.Value);
                rectangleShape.Draw(g, Pens.Blue, rect);
            }

            if (P3 != null && radius > 0 && circleDrawn)
            {
                circle.Center = P3.Value;
                circle.Radius = radius;

                var circleRect = new Rectangle(P3.Value.X - (int)radius, P3.Value.Y - (int)radius, (int)(2 * radius), (int)(2 * radius));
                circle.Draw(g, Pens.Green, circleRect);
            }
        }


        private void AddPoint(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                int x = (e.X / 20) * 20;
                int y = (e.Y / 20) * 20;

                if (P1 == null)
                {
                    P1 = new Point(x, y);
                    points.Add(P1.Value);
                }
                else if (P2 == null)
                {
                    P2 = new Point(x, y);
                    points.Add(P2.Value);
                    rectangleDrawn = true;
                }
                else if (P3 == null)
                {
                    P3 = new Point(x, y);
                    points.Add(P3.Value);
                    circleDrawn = true;
                }

                panel1.Invalidate();
            }
        }

        private void ClearPoints(object sender, EventArgs e)
        {
            points.Clear();
            P1 = P2 = P3 = null;
            rectangleDrawn = false;
            circleDrawn = false;
            panel1.Invalidate();
        }


        private void SetRadius(object sender, EventArgs e)
        {
            if (float.TryParse(textBox1.Text, out float r))
            {
                radius = r;
                circleDrawn = true; 
                panel1.Invalidate(); 
                CalculateIntersectionPercentage();
            }
        }


        private void CalculateIntersectionPercentage()
        {
            if (P1 != null && P2 != null && P3 != null && radius > 0)
            {
                var rect = rectangleShape.GetRectangleFromPoints(P1.Value, P2.Value);
                double rectArea = rect.Width * rect.Height;
                double intersectionPercentage = areaCalculator.CalculateIntersectionPercentage(rect, rectArea, P3.Value, radius);

                label1.Text = $"Площа перетину: {intersectionPercentage:F2}%";
            }
        }

    }
}
