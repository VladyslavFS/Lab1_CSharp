using System;
using System.Drawing;

namespace Lab1
{
    public class AreaCalculator
    {
        public double CalculateIntersectionArea(RectangleF rect, PointF circleCenter, float radius)
        {
            double totalArea = 0;
            int gridResolution = 1000;
            double stepX = (rect.Right - rect.Left) / gridResolution;
            double stepY = (rect.Bottom - rect.Top) / gridResolution;

            for (double x = rect.Left; x <= rect.Right; x += stepX)
            {
                for (double y = rect.Top; y <= rect.Bottom; y += stepY)
                {
                    double dx = x - circleCenter.X;
                    double dy = y - circleCenter.Y;
                    double distanceSquared = dx * dx + dy * dy;

                    if (distanceSquared <= radius * radius)
                    {
                        totalArea += stepX * stepY;
                    }
                }
            }

            return totalArea;
        }

        public double CalculateIntersectionPercentage(Rectangle rect, double rectArea, Point circleCenter, float radius)
        {
            if (rectArea > 0)
            {
                var intersectionArea = CalculateIntersectionArea(rect, circleCenter, radius);
                return (intersectionArea / rectArea) * 100;
            }

            return 0;
        }
    }
}
