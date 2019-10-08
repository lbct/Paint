using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Pinta.Algoritmos
{
    public static class Algoritmos
    {
        public static List<Point> LineaDDA(Point p, Point p2)
        {
            List<Point> puntos = new List<Point>();
            int dx = (int)p2.X - (int)p.X; int dxAbs = Math.Abs(dx);
            int dy = (int)p2.Y - (int)p.Y; int dyAbs = Math.Abs(dy);
            int xx, yy;
            int paso;
            if (dxAbs > dyAbs)
            {
                paso = dxAbs;
            }
            else
            {
                paso = dyAbs;
            }
            double xincremento = (double)dx / paso;
            double yincremento = (double)dy / paso;
            double x = p.X;
            double y = p.Y;
            for (int i = 1; i <= paso; i++)
            {
                x += xincremento;
                y += yincremento;
                xx = (int)Math.Round(x);
                yy = (int)Math.Round(y);
                puntos.Add(new Point(xx, yy));
            }
            return puntos;
        }

        private static void CirculoBresenham(int xc, int yc, int x, int y, ref List<Point> points)
        {
            points.Add(new Point(xc + x, yc + y));
            points.Add(new Point(xc - x, yc + y));
            points.Add(new Point(xc + x, yc - y));
            points.Add(new Point(xc - x, yc - y));
            points.Add(new Point(xc + y, yc + x));
            points.Add(new Point(xc - y, yc + x));
            points.Add(new Point(xc + y, yc - x));
            points.Add(new Point(xc - y, yc - x));
        }

        public static List<Point> CirculoBresenham(int xc, int yc, int r)
        {
            List<Point> puntos = new List<Point>();
            int x = 0, y = r;
            int d = 3 - 2 * r;
            CirculoBresenham(xc, yc, x, y, ref puntos);
            while (y >= x)
            {
                x++;
                if (d > 0)
                {
                    y--;
                    d = d + 4 * (x - y) + 10;
                }
                else
                    d = d + 4 * x + 6;
                CirculoBresenham(xc, yc, x, y, ref puntos);
            }
            return puntos;
        }

        public static List<Point> LineaBresenham(Point inicio, Point fin)
        {
            List<Point> puntos = new List<Point>();
            int w = (int)fin.X - (int)inicio.X;
            int h = (int)fin.Y - (int)inicio.Y;
            int dx1 = 0, dy1 = 0, dx2 = 0, dy2 = 0;
            if (w < 0) dx1 = -1; else if (w > 0) dx1 = 1;
            if (h < 0) dy1 = -1; else if (h > 0) dy1 = 1;
            if (w < 0) dx2 = -1; else if (w > 0) dx2 = 1;
            int longest = Math.Abs(w);
            int shortest = Math.Abs(h);
            if (!(longest > shortest))
            {
                longest = Math.Abs(h);
                shortest = Math.Abs(w);
                if (h < 0) dy2 = -1; else if (h > 0) dy2 = 1;
                dx2 = 0;
            }
            int numerator = longest >> 1;
            for (int i = 0; i <= longest; i++)
            {
                puntos.Add(new Point(inicio.X, inicio.Y));
                numerator += shortest;
                if (!(numerator < longest))
                {
                    numerator -= longest;
                    inicio.X += dx1;
                    inicio.Y += dy1;
                }
                else
                {
                    inicio.X += dx2;
                    inicio.Y += dy2;
                }
            }
            return puntos;
        }
    }
}
