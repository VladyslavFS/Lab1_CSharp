using System.Drawing;

namespace Lab1
{
    public class Circle : Shape
    {
        public Point Center { get; set; }
        public float Radius { get; set; }

        public override void Draw(Graphics g, Pen pen, Rectangle rect)
        {
            g.DrawEllipse(pen, rect);
        }

        public void DrawCircle(Graphics g, Pen pen)
        {
            g.DrawEllipse(pen, Center.X - Radius, Center.Y - Radius, Radius * 2, Radius * 2);
        }
    }
}
