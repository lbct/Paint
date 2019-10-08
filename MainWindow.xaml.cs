using Pinta.Visual;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.MaterialControls;

namespace Pinta
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : RadWindow
	{
        Dibuja d;
        private bool mover;

		public MainWindow()
		{
            mover = false;
			InitializeComponent();
            d = new DibujaLinea(canvas);
        }

        private void canvas_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!d.EstaActivo)
                d.Marcar(e.GetPosition(canvas));
            else
                d.Trazar();
        }

        private void canvas_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (d.EstaActivo)
            {
                mover = true;
                d.Anclar(e.GetPosition(canvas));
            }
        }

        private void canvas_MouseLeave(object sender, MouseEventArgs e)
        {
            d.QuitarMarca();
        }

        private void canvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                d.MoverMarca(e.GetPosition(canvas));
            }
            else if (d.EstaActivo && mover)
                d.MoverMarcaOrigen(e.GetPosition(canvas));
        }

        private void dibuja_linea_boton_Click(object sender, RoutedEventArgs e)
        {
            d = new DibujaLinea(canvas);
        }

        private void dibuja_rec_boton_Click(object sender, RoutedEventArgs e)
        {
            d = new DibujaRectangulo(canvas);
        }

        private void dibuja_trian_boton_Click(object sender, RoutedEventArgs e)
        {
            d = new DibujaTriangulo(canvas);
        }

        private void dibuja_circulo_boton_Click(object sender, RoutedEventArgs e)
        {
            d = new DibujaCirculo(canvas);
        }

        private void canvas_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (d.EstaActivo)
            {
                d.Rotar();
                mover = false;
            }
        }

        private void color_pick_SelectedColorChanged(object sender, EventArgs e)
        {
            if (color_pick.SelectedColor != null)
                Dibuja.current_color = color_pick.SelectedColor;
        }

        private void pintar_boton_Click(object sender, RoutedEventArgs e)
        {
            Dibuja.fill = !Dibuja.fill;
        }
    }
}
