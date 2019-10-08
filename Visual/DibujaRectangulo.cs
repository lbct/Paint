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
    public class DibujaRectangulo : Dibuja
    {
        Rectangulo rec;
        public override bool EstaActivo => rec != null;

        public DibujaRectangulo(Canvas canvas) : base(canvas)
        {
            rec = null;
        }

        public override void Marcar(Point punto)
        {
            rec = new Rectangulo();
            rec.X1 = punto.X;
            rec.Y1 = punto.Y;
            rec.X2 = punto.X;
            rec.Y2 = punto.Y;
            foreach (var linea in rec.ObtenerLineas())
                canvas.Children.Add(linea);
        }

        public override void MoverMarca(Point punto)
        {
            if (rec != null)
            {
                rec.X2 = punto.X;
                rec.Y2 = punto.Y;
            }
        }

        public override void MoverMarcaOrigen(Point punto)
        {
            if (rec != null)
            {
                rec.X1 += punto.X - ancla.X;
                rec.Y1 += punto.Y - ancla.Y;
                rec.X2 += punto.X - ancla.X;
                rec.Y2 += punto.Y - ancla.Y;
                Anclar(punto);
            }
        }

        public override void QuitarMarca()
        {
            if (rec != null)
            {
                foreach(var linea in rec.ObtenerLineas())
                canvas.Children.Remove(linea);
                rec = null;
            }
        }

        public override void Rotar()
        {
            if (rec != null)
            {
                foreach (var linea_actual in rec.ObtenerLineas())
                {
                    var p = (linea_actual.RenderTransform as RotateTransform).Transform(rec.ObtenerCentro());
                    (linea_actual.RenderTransform as RotateTransform).Angle = ((linea_actual.RenderTransform as RotateTransform).Angle + 45) % 360;
                    (linea_actual.RenderTransform as RotateTransform).CenterX = p.X;
                    (linea_actual.RenderTransform as RotateTransform).CenterY = p.Y;
                }
            }
        }

        public override void Trazar()
        {
            if (rec != null)
            {
                foreach (var linea_actual in rec.ObtenerLineas())
                {
                    var trans = linea_actual.RenderTransform as RotateTransform;
                    var puntos = Algoritmos.Algoritmos.LineaDDA(trans.Transform(new Point(linea_actual.X1, linea_actual.Y1))
                        , trans.Transform(new Point(linea_actual.X2, linea_actual.Y2)));
                    DibujarLinea(puntos);
                    canvas.Children.Remove(linea_actual);
                }
                var rot = (rec.ObtenerLineas().First().RenderTransform as RotateTransform);
                if (fill && rot.Angle == 0)
                {
                    Rectangle rectangle = new Rectangle()
                    {
                        Width = rec.Ancho,
                        Height = rec.Alto
                    };
                    
                    Canvas.SetLeft(rectangle, rec.X1);
                    Canvas.SetTop(rectangle, rec.Y1);
                    rectangle.Fill = new SolidColorBrush(current_color);
               
                    canvas.Children.Add(rectangle);
                }
                rec = null;
            }
        }
    }

    public class Rectangulo
    {
        public double X1
        {
            get => x1;
            set
            {
                x1 = value;
                ActualizarLineas();
            }
        }

        public double Y1
        {
            get => y1;
            set
            {
                y1 = value;
                ActualizarLineas();
            }
        }

        public double X2
        {
            get => x2;
            set
            {
                x2 = value;
                ActualizarLineas();
            }
        }

        public double Y2
        {
            get => y2;
            set
            {
                y2 = value;
                ActualizarLineas();
            }
        }

        private double x1;
        private double y1;
        private double x2;
        private double y2;

        private Line izquierda;
        private Line arriba;
        private Line derecha;
        private Line abajo;

        public Rectangulo()
        {
            izquierda = new Line() { Stroke = new SolidColorBrush(Dibuja.current_color), StrokeThickness = 1 };
            izquierda.RenderTransform = new RotateTransform();
            arriba = new Line() { Stroke = new SolidColorBrush(Dibuja.current_color), StrokeThickness = 1 };
            arriba.RenderTransform = new RotateTransform();
            derecha = new Line() { Stroke = new SolidColorBrush(Dibuja.current_color), StrokeThickness = 1 };
            derecha.RenderTransform = new RotateTransform();
            abajo = new Line() { Stroke = new SolidColorBrush(Dibuja.current_color), StrokeThickness = 1 };
            abajo.RenderTransform = new RotateTransform();
        }

        public Point ObtenerCentro()
        {
            return new Point(X1 + ((X2 - X1) / 2), Y1 + ((Y2 - Y1) / 2));
        }

        public double Ancho
        {
            get
            {
                return Math.Abs(X2 - X1);

            }
        }

        public double Alto
        {
            get
            {
                return Math.Abs(Y2 - Y1);
            }
        }

        public void ActualizarLineas()
        {
            izquierda.X1 = x1; 
            izquierda.Y1 = y1; 
            izquierda.X2 = x1; 
            izquierda.Y2 = y2;
            arriba.X1 = x1; 
            arriba.Y1 = y1; 
            arriba.X2 = x2; 
            arriba.Y2 = y1;
            derecha.X1 = x2; 
            derecha.Y1 = y1; 
            derecha.X2 = x2; 
            derecha.Y2 = y2;
            abajo.X1 = x1; 
            abajo.Y1 = y2; 
            abajo.X2 = x2; 
            abajo.Y2 = y2;
        }

        public Line[] ObtenerLineas()
        {
            return new Line[] { izquierda, arriba, derecha, abajo };
        }
    }
}
