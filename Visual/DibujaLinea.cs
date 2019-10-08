using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Pinta.Visual
{
    public class DibujaLinea : Dibuja
    {
        private Line linea_actual;
        public override bool EstaActivo => linea_actual != null;

        public DibujaLinea(Canvas canvas) : base(canvas)
        {
            linea_actual = null;
        }

        public override void Trazar()
        {
            if (linea_actual != null)
            {
                var trans = linea_actual.RenderTransform as RotateTransform;
                var puntos = Algoritmos.Algoritmos.LineaBresenham(trans.Transform(new Point(linea_actual.X1, linea_actual.Y1))
                    , trans.Transform(new Point(linea_actual.X2, linea_actual.Y2)));
                DibujarLinea(puntos);
                canvas.Children.Remove(linea_actual);
                linea_actual = null;
            }
        }

        public override void Rotar()
        {
            if(linea_actual != null)
            {
                (linea_actual.RenderTransform as RotateTransform).Angle = ((linea_actual.RenderTransform as RotateTransform).Angle + 45) % 360;
                (linea_actual.RenderTransform as RotateTransform).CenterX = (linea_actual.X1 + linea_actual.X2) / 2;
                (linea_actual.RenderTransform as RotateTransform).CenterY = (linea_actual.Y1 + linea_actual.Y2) / 2;
            }
        }

        public override void Marcar(Point punto)
        {
            linea_actual = new Line()
            {
                X1 = punto.X,
                Y1 = punto.Y,
                X2 = punto.X,
                Y2 = punto.Y,
                Stroke = new SolidColorBrush(current_color),
                StrokeThickness = 1
            };
            linea_actual.RenderTransform = new RotateTransform()
            {
                Angle = 0
            };
            canvas.Children.Add(linea_actual);
        }

        public override void QuitarMarca()
        {
            if (linea_actual != null)
            {
                canvas.Children.Remove(linea_actual);
                linea_actual = null;
            }
        }

        public override void MoverMarca(Point punto)
        {
            if (linea_actual != null)
            {
                linea_actual.X2 = punto.X;
                linea_actual.Y2 = punto.Y;
            }
        }

        public override void MoverMarcaOrigen(Point punto)
        {
            if (linea_actual != null)
            {
                linea_actual.X1 += punto.X - ancla.X;
                linea_actual.Y1 += punto.Y - ancla.Y;
                linea_actual.X2 += punto.X - ancla.X;
                linea_actual.Y2 += punto.Y - ancla.Y;
                Anclar(punto);
            }
        }
    }
}
