﻿using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace FlightSimulatorApp.View
{
	/// <summary>
	/// Interaction logic for Joystick.xaml
	/// </summary>
	public partial class Joystick : UserControl
	{
		public const double UP_BOARDER = 40;
		public const double DOWN_BOARDER = -40;
		public const double LEFT_BOARDER = -40;
		public const double RIGHT_BOARDER = 40;
		public const double RANGE = 40;
		private bool mousePressed = false;
		private double startX, startY, currentX, currentY, positionY = 0, positionX = 0;
		private readonly Storyboard centerKnob;
		// Properties.
		public double PositionX { get { return positionX; } set { if (value > UP_BOARDER) { positionX = UP_BOARDER; } else if (value < DOWN_BOARDER) { positionX = DOWN_BOARDER; } else { positionX = value; } } }
		public double PositionY { get { return positionY; } set { if (value < LEFT_BOARDER) { positionY = LEFT_BOARDER; } else if (value > RIGHT_BOARDER) { positionY = RIGHT_BOARDER; } else { positionY = value; } } }
	    public double ValueX
		{
			get { return (double)GetValue(ValueXProperty); }
			set { SetValue(ValueXProperty, value); }
		}

		// Using a DependencyProperty as the backing store for RudderX.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty ValueXProperty =
			DependencyProperty.Register("ValueX", typeof(double), typeof(Joystick));
		public double ValueY
		{
			get { return (double)GetValue(ValueYProperty); }
			set { SetValue(ValueYProperty, value); }
		}

		// Using a DependencyProperty as the backing store for ElevatorY.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty ValueYProperty =
			DependencyProperty.Register("ValueY", typeof(double), typeof(Joystick));

		public Joystick()
		{
			InitializeComponent();
			centerKnob = Knob.Resources["CenterKnob"] as Storyboard;
		}

		private void CenterKnob_Completed(object sender, EventArgs e)
		{
		}

		private void Joystick_MouseDown(object sender, MouseButtonEventArgs e)
		{
			if (!mousePressed)
			{
				mousePressed = true;
				Knob.CaptureMouse();
				// Initilaze the begining point.
				startX = e.GetPosition(this).X;
				startY = e.GetPosition(this).Y;
				centerKnob.Stop();
			}
		}

		private void Joystick_MouseUp(object sender, MouseButtonEventArgs e)
		{
			mousePressed = false;
			UIElement element = (UIElement)Knob;
			element.ReleaseMouseCapture();

			// Reset the knob to the center (0,0).
			PositionY = 0;
			PositionX = 0;
			ValueX = 0;
			ValueY = 0;
			centerKnob.Begin();
		}

		private void Joystick_MouseMove(object sender, MouseEventArgs e)
		{
			if (mousePressed)
			{
				// The place now.
				currentX = e.GetPosition(this).X;
				currentY = e.GetPosition(this).Y;
				PositionX = currentX - startX;
				PositionY = currentY - startY;
				// The point limited between -1 to 1.
				ValueX = PositionX / RANGE;
				ValueY= -PositionY / RANGE;
				// Update the knob.
				knobPosition.X = PositionX;
				knobPosition.Y = PositionY;
			}
		}
	}
}
