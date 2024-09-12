using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection.Emit;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Lab1
{
    public partial class Form1 : Form
    {
        private List<Point> points;  // ������ ��� ���������� �����
        private Point? P1, P2, P3;   // ����� P1, P2 ��� ������������, P3 ��� ������ ����
        private float radius = 0;    // ����� ����
        private const int gridSize = 20;  // ����� ������� ����
        private bool rectangleDrawn = false;  // �� ��� ������������ �����������
        private bool circleDrawn = false;     // �� ���� ����������� ����

        public Form1()
        {
            InitializeComponent();
            points = new List<Point>();

            // ��䳿
            panel1.Paint += new PaintEventHandler(DrawGridAndShapes);  // ��������� ���� �� �����
            panel1.MouseClick += new MouseEventHandler(AddPoint);      // ��������� �����
            button1.Click += new EventHandler(ClearPoints);            // �������� �����
        }

        // ����� ��� ��������� ����, ����� �� ���� ���������
        private void DrawGridAndShapes(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Pen gridPen = new Pen(Color.LightGray);
            Pen axisPen = new Pen(Color.Black, 2);  // �� ���������
            int width = panel1.ClientSize.Width;
            int height = panel1.ClientSize.Height;

            // ��������� ������������ ��� ����
            for (int x = 0; x <= width; x += gridSize)
            {
                g.DrawLine(gridPen, x, 0, x, height);
            }

            // ��������� �������������� ��� ����
            for (int y = 0; y <= height; y += gridSize)
            {
                g.DrawLine(gridPen, 0, y, width, y);
            }

            // ��������� ���� ���������
            g.DrawLine(axisPen, width / 2, 0, width / 2, height);  // ����������� ��� Y
            g.DrawLine(axisPen, 0, height / 2, width, height / 2); // ������������� ��� X

            // ��������� �����
            foreach (var point in points)
            {
                g.FillEllipse(Brushes.Red, point.X - 5, point.Y - 5, 10, 10);
            }

            // ��������� ������������
            if (P1 != null && P2 != null && rectangleDrawn)
            {
                var rect = GetRectangleFromPoints(P1.Value, P2.Value);
                g.DrawRectangle(Pens.Blue, rect);
            }

            // ��������� ����
            if (P3 != null && radius > 0 && circleDrawn)
            {
                g.DrawEllipse(Pens.Green, P3.Value.X - radius, P3.Value.Y - radius, radius * 2, radius * 2);
            }
        }

        // ����� ��� ��������� ����� ��� ���� ����
        private void AddPoint(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                int x = (e.X / gridSize) * gridSize;
                int y = (e.Y / gridSize) * gridSize;

                // ����� ����� ��� ������������
                if (P1 == null)
                {
                    P1 = new Point(x, y);
                    points.Add(P1.Value);
                }
                // ����� ����� ��� ������������
                else if (P2 == null)
                {
                    P2 = new Point(x, y);
                    points.Add(P2.Value);
                    rectangleDrawn = true;
                }
                // ����� ����
                else if (P3 == null)
                {
                    P3 = new Point(x, y);
                    points.Add(P3.Value);
                    circleDrawn = true;
                }

                panel1.Invalidate();  // ��������� ������ ��� ���������
            }
        }

        // �������� �����
        private void ClearPoints(object sender, EventArgs e)
        {
            points.Clear();
            P1 = P2 = P3 = null;
            rectangleDrawn = false;
            circleDrawn = false;
            panel1.Invalidate();  // ��������� ����� ���� ��������
        }

        // ����� ��� ���������� �������� ������������ � ����
        private double CalculateIntersectionArea()
        {
            if (P1 != null && P2 != null && P3 != null && radius > 0)
            {
                var rect = GetRectangleFromPoints(P1.Value, P2.Value);

                // ����� ����
                float circleX = P3.Value.X;
                float circleY = P3.Value.Y;

                // ������� ���������� (������� �������� �� ������� �����)
                int gridResolution = 1000;
                double totalArea = 0;
                double stepX = (rect.Right - rect.Left) / (double)gridResolution;
                double stepY = (rect.Bottom - rect.Top) / (double)gridResolution;

                for (double x = rect.Left; x <= rect.Right; x += stepX)
                {
                    for (double y = rect.Top; y <= rect.Bottom; y += stepY)
                    {
                        // ����������, �� ����� (x, y) ����������� �������� ����
                        double dx = x - circleX;
                        double dy = y - circleY;
                        double distanceSquared = dx * dx + dy * dy;

                        if (distanceSquared <= radius * radius)
                        {
                            // ���� ����� � ���, ������ �� ����� �� �������� �����
                            totalArea += stepX * stepY;
                        }
                    }
                }

                return totalArea;
            }
            return 0;
        }

        // ����� ��� ������������ ����� P1 � P2 � �����������
        private Rectangle GetRectangleFromPoints(Point p1, Point p2)
        {
            int x = Math.Min(p1.X, p2.X);
            int y = Math.Min(p1.Y, p2.Y);
            int width = Math.Abs(p1.X - p2.X);
            int height = Math.Abs(p1.Y - p2.Y);
            return new Rectangle(x, y, width, height);
        }

        // ����� ��� ������������ ������ ���� � ���������� ����
        private void SetRadius(object sender, EventArgs e)
        {
            if (float.TryParse(textBox1.Text, out float r))
            {
                radius = r;
                panel1.Invalidate();  // ��������� ����� ��� ��������� ����
                CalculateIntersectionArea();  // ϳ�������� ����� ��������
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            double intersectionArea = CalculateIntersectionArea();
            label1.Text = $"����� ��������: {intersectionArea:F2}";
        }
    }
}
