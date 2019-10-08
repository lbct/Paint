using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Pinta.Visual
{
    public abstract class Dibuja
    {
        public static Color current_color = Colors.Black;
        public static bool fill = false;
        protected Canvas canvas;
        protected Point ancla;

        public Dibuja(Canvas canvas)
        {
            this.canvas = canvas;
        }

        public abstract bool EstaActivo { get; }
        public abstract void MoverMarca(Point punto);
        public abstract void MoverMarcaOrigen(Point punto);
        
        public abstract void QuitarMarca();
        public abstract void Marcar(Point punto);
        public abstract void Trazar();

        public abstract void Rotar();

        public void Anclar(Point punto)
        {
            ancla = punto;
        }

        protected void DibujarLinea(List<Point> puntos, double angle = 0)
        {
            foreach (var punto in puntos)
                DibujarPixel(RotatePoint(angle, punto));
        }

        public Point RotatePoint(double angle, Point pt)
        {
            var a = angle * System.Math.PI / 180.0;
            double cosa = Math.Cos(a), sina = Math.Sin(a);
            return new Point(pt.X * cosa - pt.Y * sina, pt.X * sina + pt.Y * cosa);
        }

        protected void DibujarPixel(Point punto)
        {
            Rectangle rec = new Rectangle();
            Canvas.SetTop(rec, punto.Y);
            Canvas.SetLeft(rec, punto.X);
            rec.Width = 1;
            rec.Height = 1;
            rec.Fill = new SolidColorBrush(current_color);
            canvas.Children.Add(rec);
        }
    }
}
