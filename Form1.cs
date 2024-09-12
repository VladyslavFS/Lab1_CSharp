using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Lab1
{
    public partial class Form1 : Form
    {
        private List<Point> points;  // ������ ��� ���������� �����
        private const int gridSize = 20;  // ����� ������� ����

        public Form1()
        {
            InitializeComponent();
            points = new List<Point>();

            // ������ ���� ��� ��������� ���� �� ����� �� �����
            panel1.Paint += new PaintEventHandler(DrawGrid);

            // ������ ������� ���� ���� �� ����� ��� ��������� �����
            panel1.MouseClick += new MouseEventHandler(AddPoint);

            // ������� ���� �� ������ ��� �������� �����
            button1.Click += new EventHandler(ClearPoints);
        }

        // ����� ��� ��������� ����
        private void DrawGrid(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Pen gridPen = new Pen(Color.LightGray);
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

            // ��������� �����
            foreach (var point in points)
            {
                g.FillEllipse(Brushes.Red, point.X - 5, point.Y - 5, 10, 10); // ����� ��������� � ������ ��������� ������
            }
        }

        // ����� ��� ��������� ����� ��� ���� ����
        private void AddPoint(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                // ���������� ��������� ���� �� ��������� ����� ����
                int x = (e.X / gridSize) * gridSize;
                int y = (e.Y / gridSize) * gridSize;

                points.Add(new Point(x, y));
                panel1.Invalidate(); // ��������� ����� ��� ������������� ���� � ����� ������
            }
        }

        // �������� ����� ��� ��������� �� ������
        private void ClearPoints(object sender, EventArgs e)
        {
            points.Clear();
            panel1.Invalidate(); // ��������� ����� ���� �������� �����
        }
    }
}
