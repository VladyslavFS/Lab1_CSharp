using System.Drawing;

namespace Lab1
{
    public class Grid
    {
        private const int gridSize = 20; 
        public void DrawGrid(Graphics g, int width, int height)
        {
            Pen gridPen = new Pen(Color.LightGray);

            for (int x = 0; x <= width; x += gridSize)
            {
                g.DrawLine(gridPen, x, 0, x, height);
            }

            for (int y = 0; y <= height; y += gridSize)
            {
                g.DrawLine(gridPen, 0, y, width, y);
            }
        }
    }
}
