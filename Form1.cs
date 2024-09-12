using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Lab1
{
    public partial class Form1 : Form
    {
        private List<Point> points;  // Список для збереження точок
        private const int gridSize = 20;  // Розмір клітинок сітки

        public Form1()
        {
            InitializeComponent();
            points = new List<Point>();

            // Додаємо подію для малювання сітки та точок на панелі
            panel1.Paint += new PaintEventHandler(DrawGrid);

            // Додаємо обробку кліків миші на панелі для додавання точок
            panel1.MouseClick += new MouseEventHandler(AddPoint);

            // Обробка кліку на кнопку для очищення точок
            button1.Click += new EventHandler(ClearPoints);
        }

        // Метод для малювання сітки
        private void DrawGrid(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Pen gridPen = new Pen(Color.LightGray);
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

            // Малювання точок
            foreach (var point in points)
            {
                g.FillEllipse(Brushes.Red, point.X - 5, point.Y - 5, 10, 10); // Точки малюються у вигляді маленьких кружків
            }
        }

        // Метод для додавання точки при кліку миші
        private void AddPoint(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                // Округлення координат кліку до найближчої точки сітки
                int x = (e.X / gridSize) * gridSize;
                int y = (e.Y / gridSize) * gridSize;

                points.Add(new Point(x, y));
                panel1.Invalidate(); // Оновлення панелі для перемалювання сітки з новою точкою
            }
        }

        // Очищення точок при натисканні на кнопку
        private void ClearPoints(object sender, EventArgs e)
        {
            points.Clear();
            panel1.Invalidate(); // Оновлення панелі після очищення точок
        }
    }
}
