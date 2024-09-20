using System.Drawing;

namespace Lab1
{
    public class RectangleShape : Shape
    {
        public Rectangle GetRectangleFromPoints(Point p1, Point p2)
        {
            int x = Math.Min(p1.X, p2.X);
            int y = Math.Min(p1.Y, p2.Y);
            int width = Math.Abs(p1.X - p2.X);
            int height = Math.Abs(p1.Y - p2.Y);
            return new Rectangle(x, y, width, height);
        }

        public override void Draw(Graphics g, Pen pen, Rectangle rect)
        {
            g.DrawRectangle(pen, rect);
        }
    }
}
