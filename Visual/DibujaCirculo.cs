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
    class DibujaCirculo : Dibuja
    {
        private Ellipse ellipse;
        public override bool EstaActivo => ellipse != null;

        public DibujaCirculo(Canvas canvas) : base(canvas)
        {
            ellipse = null;
        }

        public override void Rotar()
        {

        }

        public override void Trazar()
        {
            if (ellipse != null)
            {
                var x = (ellipse.RenderTransform as TranslateTransform).X;
                x += ellipse.Width / 2;
                var y = (ellipse.RenderTransform as TranslateTransform).Y;
                y += ellipse.Height / 2;
                var puntos = Algoritmos.Algoritmos.CirculoBresenham((int)x, (int)y, (int)(ellipse.Width / 2));
                DibujarLinea(puntos);
                if(!fill)
                    canvas.Children.Remove(ellipse);
                else
                {
                    ellipse.StrokeThickness = 0;
                    ellipse.Fill = new SolidColorBrush(current_color);
                }
                ellipse = null;
            }
        }

        public override void Marcar(Point punto)
        {
            ellipse = new Ellipse()
            {
                Stroke = new SolidColorBrush(current_color),
                StrokeThickness = 1,
                Width = 0,
                Height = 0
            };
            ellipse.RenderTransform = new TranslateTransform() 
            {
                X = punto.X,
                Y = punto.Y
            };
            canvas.Children.Add(ellipse);
        }

        public override void QuitarMarca()
        {
            if (ellipse != null)
            {
                canvas.Children.Remove(ellipse);
                ellipse = null;
            }
        }

        public override void MoverMarca(Point punto)
        {
            if (ellipse != null)
            {
                var x1 = (ellipse.RenderTransform as TranslateTransform).X;
                var y1 = (ellipse.RenderTransform as TranslateTransform).Y;
                var x2 = punto.X;
                var y2 = punto.Y;

                var dist = Math.Sqrt(Math.Pow(x2 - x1, 2) + Math.Pow(y2 - y1, 2));

                ellipse.Width = dist;
                ellipse.Height = dist;
            }
        }

        public override void MoverMarcaOrigen(Point punto)
        {
            if(ellipse != null)
            {
                (ellipse.RenderTransform as TranslateTransform).X += punto.X - ancla.X;
                (ellipse.RenderTransform as TranslateTransform).Y += punto.Y - ancla.Y;
                Anclar(punto);
            }
        }
    }
}
