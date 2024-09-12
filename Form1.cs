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
        private List<Point> points;  // Список для збереження точок
        private Point? P1, P2, P3;   // Точки P1, P2 для прямокутника, P3 для центру кола
        private float radius = 0;    // Радіус кола
        private const int gridSize = 20;  // Розмір клітинок сітки
        private bool rectangleDrawn = false;  // Чи був намальований прямокутник
        private bool circleDrawn = false;     // Чи було намальоване коло

        public Form1()
        {
            InitializeComponent();
            points = new List<Point>();

            // Події
            panel1.Paint += new PaintEventHandler(DrawGridAndShapes);  // Малювання сітки та фігур
            panel1.MouseClick += new MouseEventHandler(AddPoint);      // Додавання точок
            button1.Click += new EventHandler(ClearPoints);            // Очищення точок
        }

        // Метод для малювання сітки, фігур та осей координат
        private void DrawGridAndShapes(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Pen gridPen = new Pen(Color.LightGray);
            Pen axisPen = new Pen(Color.Black, 2);  // Осі координат
            int width = panel1.ClientSize.Width;
            int height = panel1.ClientSize.Height;

            // Малювання вертикальних ліній сітки
            for (int x = 0; x <= width; x += gridSize)
            {
                g.DrawLine(gridPen, x, 0, x, height);
            }

            // Малювання горизонтальних ліній сітки
            for (int y = 0; y <= height; y += gridSize)
            {
                g.DrawLine(gridPen, 0, y, width, y);
            }

            // Малювання осей координат
            g.DrawLine(axisPen, width / 2, 0, width / 2, height);  // Вертикальна вісь Y
            g.DrawLine(axisPen, 0, height / 2, width, height / 2); // Горизонтальна вісь X

            // Малювання точок
            foreach (var point in points)
            {
                g.FillEllipse(Brushes.Red, point.X - 5, point.Y - 5, 10, 10);
            }

            // Малювання прямокутника
            if (P1 != null && P2 != null && rectangleDrawn)
            {
                var rect = GetRectangleFromPoints(P1.Value, P2.Value);
                g.DrawRectangle(Pens.Blue, rect);
            }

            // Малювання кола
            if (P3 != null && radius > 0 && circleDrawn)
            {
                g.DrawEllipse(Pens.Green, P3.Value.X - radius, P3.Value.Y - radius, radius * 2, radius * 2);
            }
        }

        // Метод для додавання точок при кліку миші
        private void AddPoint(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                int x = (e.X / gridSize) * gridSize;
                int y = (e.Y / gridSize) * gridSize;

                // Перша точка для прямокутника
                if (P1 == null)
                {
                    P1 = new Point(x, y);
                    points.Add(P1.Value);
                }
                // Друга точка для прямокутника
                else if (P2 == null)
                {
                    P2 = new Point(x, y);
                    points.Add(P2.Value);
                    rectangleDrawn = true;
                }
                // Центр кола
                else if (P3 == null)
                {
                    P3 = new Point(x, y);
                    points.Add(P3.Value);
                    circleDrawn = true;
                }

                panel1.Invalidate();  // Оновлюємо панель для малювання
            }
        }

        // Очищення точок
        private void ClearPoints(object sender, EventArgs e)
        {
            points.Clear();
            P1 = P2 = P3 = null;
            rectangleDrawn = false;
            circleDrawn = false;
            panel1.Invalidate();  // Оновлення панелі після очищення
        }

        // Метод для розрахунку перетину прямокутника і кола
        private double CalculateIntersectionArea()
        {
            if (P1 != null && P2 != null && P3 != null && radius > 0)
            {
                var rect = GetRectangleFromPoints(P1.Value, P2.Value);

                // Центр кола
                float circleX = P3.Value.X;
                float circleY = P3.Value.Y;

                // Точність розрахунку (кількість частинок на одиницю площі)
                int gridResolution = 1000;
                double totalArea = 0;
                double stepX = (rect.Right - rect.Left) / (double)gridResolution;
                double stepY = (rect.Bottom - rect.Top) / (double)gridResolution;

                for (double x = rect.Left; x <= rect.Right; x += stepX)
                {
                    for (double y = rect.Top; y <= rect.Bottom; y += stepY)
                    {
                        // Перевіряємо, чи точка (x, y) знаходиться всередині кола
                        double dx = x - circleX;
                        double dy = y - circleY;
                        double distanceSquared = dx * dx + dy * dy;

                        if (distanceSquared <= radius * radius)
                        {
                            // Якщо точка в колі, додаємо її площу до загальної площі
                            totalArea += stepX * stepY;
                        }
                    }
                }

                return totalArea;
            }
            return 0;
        }

        // Метод для перетворення точок P1 і P2 у прямокутник
        private Rectangle GetRectangleFromPoints(Point p1, Point p2)
        {
            int x = Math.Min(p1.X, p2.X);
            int y = Math.Min(p1.Y, p2.Y);
            int width = Math.Abs(p1.X - p2.X);
            int height = Math.Abs(p1.Y - p2.Y);
            return new Rectangle(x, y, width, height);
        }

        // Метод для встановлення радіуса кола з текстового поля
        private void SetRadius(object sender, EventArgs e)
        {
            if (float.TryParse(textBox1.Text, out float r))
            {
                radius = r;
                panel1.Invalidate();  // Оновлення панелі для малювання кола
                CalculateIntersectionArea();  // Підрахунок площі перетину
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            double intersectionArea = CalculateIntersectionArea();
            label1.Text = $"Площа перетину: {intersectionArea:F2}";
        }
    }
}
