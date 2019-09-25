package vista;

import java.awt.Color;
import java.awt.Graphics;
import java.awt.Graphics2D;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import java.awt.event.MouseAdapter;
import java.awt.event.MouseEvent;
import java.awt.event.MouseMotionAdapter;
import java.awt.image.BufferStrategy;

import javax.swing.JFrame;
import javax.swing.Timer;
import javax.swing.WindowConstants;

import modelo.Punto;

import java.util.ArrayList;

public class PanelDibujo extends JFrame {
	private static final long serialVersionUID = 1L;
	private ArrayList<Punto> posicionesGuardadas;
	private int tamPixel = 10;
	private BufferStrategy buffer;
	private Punto posMouse;

	public PanelDibujo() {
		super("Paint");
		posMouse = new Punto(0, 0);
		posicionesGuardadas = new ArrayList<Punto>();
		this.setBounds(310,100,640, 480);
		this.setBackground(Color.WHITE);
		this.setVisible(true);
		this.setDefaultCloseOperation(WindowConstants.EXIT_ON_CLOSE);
		this.createBufferStrategy(2);
		this.setResizable(false);
		buffer = getBufferStrategy();
		this.addMouseMotionListener(new MouseMotionAdapter() {
			@Override
			public void mouseMoved(MouseEvent arg0) {
				posMouse.setX(arg0.getX());
				posMouse.setY(arg0.getY());
			}
			public void mouseDragged(MouseEvent arg0) {
				posicionesGuardadas.add(new Punto(arg0.getX(), arg0.getY()));
				//posXMouse = arg0.getX();
				//posYMouse = arg0.getY();
			}
		});
		this.addMouseListener(new MouseAdapter() {
			@Override
			public void mousePressed(MouseEvent arg0) {
				
			}
		});
		iniciar();
	}
	
	public void paint(Graphics g) {
		if(buffer != null) {
			Graphics g3 = (Graphics2D) buffer.getDrawGraphics();
			for(Punto p : posicionesGuardadas) {
				g3.fillRect(((p.getX() -(tamPixel / 2))/tamPixel) * tamPixel, ((p.getY() -(tamPixel / 2))/tamPixel)*tamPixel, tamPixel, tamPixel);
			}
			g3.fillRect(((posMouse.getX() -(tamPixel / 2))/tamPixel) * tamPixel, ((posMouse.getY() -(tamPixel / 2))/tamPixel)*tamPixel, tamPixel, tamPixel);
			buffer.show();
			g3.clearRect(0, 0, 640, 480);
		}
	}

	public void iniciar() {
		Timer timer1 = new Timer(0, new ActionListener() {
			public void actionPerformed(ActionEvent e) {
				repaint();
			}
		});
		
		timer1.start();
	}
}
