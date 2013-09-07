//////////////////////////////////////////////////////////////////////////////////
//	MultipleWiimoteForm.cs
//	Managed Wiimote Library Tester
//	Written by Brian Peek (http://www.brianpeek.com/)
//  for MSDN's Coding4Fun (http://msdn.microsoft.com/coding4fun/)
//	Visit http://blogs.msdn.com/coding4fun/archive/2007/03/14/1879033.aspx
//  and http://www.codeplex.com/WiimoteLib
//  for more information
//////////////////////////////////////////////////////////////////////////////////

using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using System.Net.Sockets;
using WiimoteLib;

namespace WiimoteTest
{
	public partial class WiimoteInfo : UserControl
	{
		private delegate void UpdateWiimoteStateDelegate(WiimoteChangedEventArgs args);
		private delegate void UpdateExtensionChangedDelegate(WiimoteExtensionChangedEventArgs args);

        private String lastMessageSent;

		private Bitmap b = new Bitmap(256, 192, PixelFormat.Format24bppRgb);
		private Graphics g;
		private Wiimote mWiimote;

		public WiimoteInfo()
		{
			InitializeComponent();
			g = Graphics.FromImage(b);

            lastMessageSent = "512,384,512,384";
		}

		public WiimoteInfo(Wiimote wm) : this()
		{
			mWiimote = wm;
		}

		public void UpdateState(WiimoteChangedEventArgs args)
		{
			BeginInvoke(new UpdateWiimoteStateDelegate(UpdateWiimoteChanged), args);
		}

		public void UpdateExtension(WiimoteExtensionChangedEventArgs args)
		{
			BeginInvoke(new UpdateExtensionChangedDelegate(UpdateExtensionChanged), args);
		}

		private void chkLED_CheckedChanged(object sender, EventArgs e)
		{
			mWiimote.SetLEDs(chkLED1.Checked, chkLED2.Checked, chkLED3.Checked, chkLED4.Checked);
		}

		private void chkRumble_CheckedChanged(object sender, EventArgs e)
		{
			mWiimote.SetRumble(chkRumble.Checked);
		}

		private void UpdateWiimoteChanged(WiimoteChangedEventArgs args)
		{
			WiimoteState ws = args.WiimoteState;

			clbButtons.SetItemChecked(0, ws.ButtonState.A);
			clbButtons.SetItemChecked(1, ws.ButtonState.B);
			clbButtons.SetItemChecked(2, ws.ButtonState.Minus);
			clbButtons.SetItemChecked(3, ws.ButtonState.Home);
			clbButtons.SetItemChecked(4, ws.ButtonState.Plus);
			clbButtons.SetItemChecked(5, ws.ButtonState.One);
			clbButtons.SetItemChecked(6, ws.ButtonState.Two);
			clbButtons.SetItemChecked(7, ws.ButtonState.Up);
			clbButtons.SetItemChecked(8, ws.ButtonState.Down);
			clbButtons.SetItemChecked(9, ws.ButtonState.Left);
			clbButtons.SetItemChecked(10, ws.ButtonState.Right);

			lblAccel.Text = ws.AccelState.Values.ToString();

			chkLED1.Checked = ws.LEDState.LED1;
			chkLED2.Checked = ws.LEDState.LED2;
			chkLED3.Checked = ws.LEDState.LED3;
			chkLED4.Checked = ws.LEDState.LED4;

			switch(ws.ExtensionType)
			{
				case ExtensionType.Nunchuk:
                    lblChuk.Text = ws.NunchukState.AccelState.Values.ToString();
					lblChukJoy.Text = ws.NunchukState.Joystick.ToString();
                    //System.Console.WriteLine(ws.NunchukState.AccelState.Values.ToString()); GET nunchuk accel
                    //System.Console.WriteLine(ws.NunchukState.Joystick.ToString()); GET nunchuk joy
					chkC.Checked = ws.NunchukState.C;
					chkZ.Checked = ws.NunchukState.Z;

                    //AsynchronousClient.Send(StateObject.workSocket, ws.NunchukState.Joystick.ToString() + '\n');
					break;

				case ExtensionType.ClassicController:
					clbCCButtons.SetItemChecked(0, ws.ClassicControllerState.ButtonState.A);
					clbCCButtons.SetItemChecked(1, ws.ClassicControllerState.ButtonState.B);
					clbCCButtons.SetItemChecked(2, ws.ClassicControllerState.ButtonState.X);
					clbCCButtons.SetItemChecked(3, ws.ClassicControllerState.ButtonState.Y);
					clbCCButtons.SetItemChecked(4, ws.ClassicControllerState.ButtonState.Minus);
					clbCCButtons.SetItemChecked(5, ws.ClassicControllerState.ButtonState.Home);
					clbCCButtons.SetItemChecked(6, ws.ClassicControllerState.ButtonState.Plus);
					clbCCButtons.SetItemChecked(7, ws.ClassicControllerState.ButtonState.Up);
					clbCCButtons.SetItemChecked(8, ws.ClassicControllerState.ButtonState.Down);
					clbCCButtons.SetItemChecked(9, ws.ClassicControllerState.ButtonState.Left);
					clbCCButtons.SetItemChecked(10, ws.ClassicControllerState.ButtonState.Right);
					clbCCButtons.SetItemChecked(11, ws.ClassicControllerState.ButtonState.ZL);
					clbCCButtons.SetItemChecked(12, ws.ClassicControllerState.ButtonState.ZR);
					clbCCButtons.SetItemChecked(13, ws.ClassicControllerState.ButtonState.TriggerL);
					clbCCButtons.SetItemChecked(14, ws.ClassicControllerState.ButtonState.TriggerR);

					lblCCJoy1.Text = ws.ClassicControllerState.JoystickL.ToString();
					lblCCJoy2.Text = ws.ClassicControllerState.JoystickR.ToString();

					lblTriggerL.Text = ws.ClassicControllerState.TriggerL.ToString();
					lblTriggerR.Text = ws.ClassicControllerState.TriggerR.ToString();
					break;

				case ExtensionType.Guitar:
				    clbGuitarButtons.SetItemChecked(0, ws.GuitarState.FretButtonState.Green);
				    clbGuitarButtons.SetItemChecked(1, ws.GuitarState.FretButtonState.Red);
				    clbGuitarButtons.SetItemChecked(2, ws.GuitarState.FretButtonState.Yellow);
				    clbGuitarButtons.SetItemChecked(3, ws.GuitarState.FretButtonState.Blue);
				    clbGuitarButtons.SetItemChecked(4, ws.GuitarState.FretButtonState.Orange);
				    clbGuitarButtons.SetItemChecked(5, ws.GuitarState.ButtonState.Minus);
				    clbGuitarButtons.SetItemChecked(6, ws.GuitarState.ButtonState.Plus);
				    clbGuitarButtons.SetItemChecked(7, ws.GuitarState.ButtonState.StrumUp);
				    clbGuitarButtons.SetItemChecked(8, ws.GuitarState.ButtonState.StrumDown);

					clbTouchbar.SetItemChecked(0, ws.GuitarState.TouchbarState.Green);
					clbTouchbar.SetItemChecked(1, ws.GuitarState.TouchbarState.Red);
					clbTouchbar.SetItemChecked(2, ws.GuitarState.TouchbarState.Yellow);
					clbTouchbar.SetItemChecked(3, ws.GuitarState.TouchbarState.Blue);
					clbTouchbar.SetItemChecked(4, ws.GuitarState.TouchbarState.Orange);

					lblGuitarJoy.Text = ws.GuitarState.Joystick.ToString();
					lblGuitarWhammy.Text = ws.GuitarState.WhammyBar.ToString();
					lblGuitarType.Text = ws.GuitarState.GuitarType.ToString();
				    break;

				case ExtensionType.Drums:
					clbDrums.SetItemChecked(0, ws.DrumsState.Red);
					clbDrums.SetItemChecked(1, ws.DrumsState.Blue);
					clbDrums.SetItemChecked(2, ws.DrumsState.Green);
					clbDrums.SetItemChecked(3, ws.DrumsState.Yellow);
					clbDrums.SetItemChecked(4, ws.DrumsState.Orange);
					clbDrums.SetItemChecked(5, ws.DrumsState.Pedal);
					clbDrums.SetItemChecked(6, ws.DrumsState.Minus);
					clbDrums.SetItemChecked(7, ws.DrumsState.Plus);

					lbDrumVelocity.Items.Clear();
					lbDrumVelocity.Items.Add(ws.DrumsState.RedVelocity);
					lbDrumVelocity.Items.Add(ws.DrumsState.BlueVelocity);
					lbDrumVelocity.Items.Add(ws.DrumsState.GreenVelocity);
					lbDrumVelocity.Items.Add(ws.DrumsState.YellowVelocity);
					lbDrumVelocity.Items.Add(ws.DrumsState.OrangeVelocity);
					lbDrumVelocity.Items.Add(ws.DrumsState.PedalVelocity);

					lblDrumJoy.Text = ws.DrumsState.Joystick.ToString();
					break;

				case ExtensionType.BalanceBoard:
					if(chkLbs.Checked)
					{
						lblBBTL.Text = ws.BalanceBoardState.SensorValuesLb.TopLeft.ToString();
						lblBBTR.Text = ws.BalanceBoardState.SensorValuesLb.TopRight.ToString();
						lblBBBL.Text = ws.BalanceBoardState.SensorValuesLb.BottomLeft.ToString();
						lblBBBR.Text = ws.BalanceBoardState.SensorValuesLb.BottomRight.ToString();
						lblBBTotal.Text = ws.BalanceBoardState.WeightLb.ToString();
					}
					else
					{
						lblBBTL.Text = ws.BalanceBoardState.SensorValuesKg.TopLeft.ToString();
						lblBBTR.Text = ws.BalanceBoardState.SensorValuesKg.TopRight.ToString();
						lblBBBL.Text = ws.BalanceBoardState.SensorValuesKg.BottomLeft.ToString();
						lblBBBR.Text = ws.BalanceBoardState.SensorValuesKg.BottomRight.ToString();
						lblBBTotal.Text = ws.BalanceBoardState.WeightKg.ToString();
					}
					lblCOG.Text = ws.BalanceBoardState.CenterOfGravity.ToString();
					break;
			}

			g.Clear(Color.Black);

            /*
            String firstCoordinate = UpdateIR(ws.IRState.IRSensors[0], lblIR1, lblIR1Raw, chkFound1, Color.Red);
            String secondCoordinate = UpdateIR(ws.IRState.IRSensors[1], lblIR1, lblIR1Raw, chkFound2, Color.Blue);
            UpdateIR(ws.IRState.IRSensors[2], lblIR3, lblIR3Raw, chkFound3, Color.Yellow);
            UpdateIR(ws.IRState.IRSensors[3], lblIR4, lblIR4Raw, chkFound4, Color.Orange);
            String message = firstCoordinate + "," + secondCoordinate;
            /*
            String message = firstCoordinate + "," + secondCoordinate;
			//UpdateIR(ws.IRState.IRSensors[1], lblIR2, lblIR2Raw, chkFound2, Color.Blue);
            if (firstCoordinate != null && secondCoordinate != null && Math.Abs(ws.IRState.IRSensors[0].RawPosition.X - ws.IRState.IRSensors[1].RawPosition.X) > 30)
                AsynchronousClient.Send(StateObject.workSocket, message + "\n");
            System.Console.WriteLine(ws.IRState.IRSensors[0].RawPosition.X - ws.IRState.IRSensors[1].RawPosition.X);
			UpdateIR(ws.IRState.IRSensors[2], lblIR3, lblIR3Raw, chkFound3, Color.Yellow);
			UpdateIR(ws.IRState.IRSensors[3], lblIR4, lblIR4Raw, chkFound4, Color.Orange);

			if(ws.IRState.IRSensors[0].Found && ws.IRState.IRSensors[1].Found)
				g.DrawEllipse(new Pen(Color.Green), (int)(ws.IRState.RawMidpoint.X / 4), (int)(ws.IRState.RawMidpoint.Y / 4), 2, 2);
            */

            //if(ws.IRState.IRSensors[0].Found && ws.IRState.IRSensors[1].Found && Math.Abs(ws.IRState.IRSensors[0].RawPosition.X - ws.IRState.IRSensors[1].RawPosition.X) > 100)
            //    AsynchronousClient.Send(StateObject.workSocket, message + "\n");
/*
            if (ws.IRState.IRSensors[0].Found && ws.IRState.IRSensors[1].Found && ws.IRState.IRSensors[2].Found)
                AsynchronousClient.Send(StateObject.workSocket, calculateMaxDist(ws.IRState.IRSensors[0], ws.IRState.IRSensors[1], ws.IRState.IRSensors[2]) + "\n");
            else if (ws.IRState.IRSensors[0].Found && ws.IRState.IRSensors[1].Found && ws.IRState.IRSensors[3].Found)
                AsynchronousClient.Send(StateObject.workSocket, calculateMaxDist(ws.IRState.IRSensors[1], ws.IRState.IRSensors[2], ws.IRState.IRSensors[3]) + "\n");
            else if (ws.IRState.IRSensors[0].Found && ws.IRState.IRSensors[2].Found && ws.IRState.IRSensors[3].Found)
                AsynchronousClient.Send(StateObject.workSocket, calculateMaxDist(ws.IRState.IRSensors[0], ws.IRState.IRSensors[2], ws.IRState.IRSensors[3]) + "\n");
            else if (ws.IRState.IRSensors[1].Found && ws.IRState.IRSensors[2].Found && ws.IRState.IRSensors[3].Found)
                AsynchronousClient.Send(StateObject.workSocket, calculateMaxDist(ws.IRState.IRSensors[1], ws.IRState.IRSensors[2], ws.IRState.IRSensors[3]) + "\n");

            else if(ws.IRState.IRSensors[0].Found && ws.IRState.IRSensors[1].Found && Math.Abs(ws.IRState.IRSensors[0].RawPosition.X - ws.IRState.IRSensors[1].RawPosition.X) > 100)
                 AsynchronousClient.Send(StateObject.workSocket, getTwoPoints(ws.IRState.IRSensors[0], ws.IRState.IRSensors[1]));
            else if (ws.IRState.IRSensors[0].Found && ws.IRState.IRSensors[2].Found && Math.Abs(ws.IRState.IRSensors[0].RawPosition.X - ws.IRState.IRSensors[2].RawPosition.X) > 100)
                AsynchronousClient.Send(StateObject.workSocket, getTwoPoints(ws.IRState.IRSensors[0], ws.IRState.IRSensors[2]));
            else if (ws.IRState.IRSensors[0].Found && ws.IRState.IRSensors[3].Found && Math.Abs(ws.IRState.IRSensors[0].RawPosition.X - ws.IRState.IRSensors[3].RawPosition.X) > 100)
                AsynchronousClient.Send(StateObject.workSocket, getTwoPoints(ws.IRState.IRSensors[0], ws.IRState.IRSensors[3]));
            else if (ws.IRState.IRSensors[1].Found && ws.IRState.IRSensors[2].Found && Math.Abs(ws.IRState.IRSensors[1].RawPosition.X - ws.IRState.IRSensors[2].RawPosition.X) > 100)
                AsynchronousClient.Send(StateObject.workSocket, getTwoPoints(ws.IRState.IRSensors[1], ws.IRState.IRSensors[2]));
            else if (ws.IRState.IRSensors[1].Found && ws.IRState.IRSensors[3].Found && Math.Abs(ws.IRState.IRSensors[1].RawPosition.X - ws.IRState.IRSensors[3].RawPosition.X) > 100)
                AsynchronousClient.Send(StateObject.workSocket, getTwoPoints(ws.IRState.IRSensors[1], ws.IRState.IRSensors[3]));
            else if (ws.IRState.IRSensors[2].Found && ws.IRState.IRSensors[3].Found && Math.Abs(ws.IRState.IRSensors[2].RawPosition.X - ws.IRState.IRSensors[3].RawPosition.X) > 100)
                AsynchronousClient.Send(StateObject.workSocket, getTwoPoints(ws.IRState.IRSensors[2], ws.IRState.IRSensors[3]));
*/
            if (ws.IRState.IRSensors[0].Found && ws.IRState.IRSensors[1].Found && ws.IRState.IRSensors[2].Found)
                Send(StateObject.workSocket, calculateMaxDist(ws.IRState.IRSensors[0], ws.IRState.IRSensors[1], ws.IRState.IRSensors[2]) + "\n");
            else if (ws.IRState.IRSensors[0].Found && ws.IRState.IRSensors[1].Found && ws.IRState.IRSensors[3].Found)
               Send(StateObject.workSocket, calculateMaxDist(ws.IRState.IRSensors[1], ws.IRState.IRSensors[2], ws.IRState.IRSensors[3]) + "\n");
            else if (ws.IRState.IRSensors[0].Found && ws.IRState.IRSensors[2].Found && ws.IRState.IRSensors[3].Found)
                Send(StateObject.workSocket, calculateMaxDist(ws.IRState.IRSensors[0], ws.IRState.IRSensors[2], ws.IRState.IRSensors[3]) + "\n");
            else if (ws.IRState.IRSensors[1].Found && ws.IRState.IRSensors[2].Found && ws.IRState.IRSensors[3].Found)
                Send(StateObject.workSocket, calculateMaxDist(ws.IRState.IRSensors[1], ws.IRState.IRSensors[2], ws.IRState.IRSensors[3]) + "\n");

            else if (ws.IRState.IRSensors[0].Found && ws.IRState.IRSensors[1].Found && Math.Abs(ws.IRState.IRSensors[0].RawPosition.X - ws.IRState.IRSensors[1].RawPosition.X) > 100)
                Send(StateObject.workSocket, getTwoPoints(ws.IRState.IRSensors[0], ws.IRState.IRSensors[1]));
            else if (ws.IRState.IRSensors[0].Found && ws.IRState.IRSensors[2].Found && Math.Abs(ws.IRState.IRSensors[0].RawPosition.X - ws.IRState.IRSensors[2].RawPosition.X) > 100)
                Send(StateObject.workSocket, getTwoPoints(ws.IRState.IRSensors[0], ws.IRState.IRSensors[2]));
            else if (ws.IRState.IRSensors[0].Found && ws.IRState.IRSensors[3].Found && Math.Abs(ws.IRState.IRSensors[0].RawPosition.X - ws.IRState.IRSensors[3].RawPosition.X) > 100)
                Send(StateObject.workSocket, getTwoPoints(ws.IRState.IRSensors[0], ws.IRState.IRSensors[3]));
            else if (ws.IRState.IRSensors[1].Found && ws.IRState.IRSensors[2].Found && Math.Abs(ws.IRState.IRSensors[1].RawPosition.X - ws.IRState.IRSensors[2].RawPosition.X) > 100)
                Send(StateObject.workSocket, getTwoPoints(ws.IRState.IRSensors[1], ws.IRState.IRSensors[2]));
            else if (ws.IRState.IRSensors[1].Found && ws.IRState.IRSensors[3].Found && Math.Abs(ws.IRState.IRSensors[1].RawPosition.X - ws.IRState.IRSensors[3].RawPosition.X) > 100)
                Send(StateObject.workSocket, getTwoPoints(ws.IRState.IRSensors[1], ws.IRState.IRSensors[3]));
            else if (ws.IRState.IRSensors[2].Found && ws.IRState.IRSensors[3].Found && Math.Abs(ws.IRState.IRSensors[2].RawPosition.X - ws.IRState.IRSensors[3].RawPosition.X) > 100)
                Send(StateObject.workSocket, getTwoPoints(ws.IRState.IRSensors[2], ws.IRState.IRSensors[3]));


			pbIR.Image = b;

			pbBattery.Value = (ws.Battery > 0xc8 ? 0xc8 : (int)ws.Battery);
			lblBattery.Text = ws.Battery.ToString();
			lblDevicePath.Text = "Device Path: " + mWiimote.HIDDevicePath;
		}

		private String UpdateIR(IRSensor irSensor, Label lblNorm, Label lblRaw, CheckBox chkFound, Color color)
		{
			chkFound.Checked = irSensor.Found;

			if(irSensor.Found)
			{
                //AsynchronousClient.Send(StateObject.workSocket, irSensor.RawPosition.X.ToString() + ',');
                //AsynchronousClient.Send(StateObject.workSocket, "69" + '\n');
                //System.Console.WriteLine(irSensor + irSensor.RawPosition.ToString());
				lblNorm.Text = irSensor.Position.ToString() + ", " + irSensor.Size;
				lblRaw.Text = irSensor.RawPosition.ToString();
				g.DrawEllipse(new Pen(color), (int)(irSensor.RawPosition.X / 4), (int)(irSensor.RawPosition.Y / 4),
							 irSensor.Size+1, irSensor.Size+1);
                return irSensor.RawPosition.X.ToString() + "," + irSensor.RawPosition.Y.ToString();
			}
            return null;
		}

        private String calculateMaxDist(IRSensor first, IRSensor second, IRSensor third)
        {
            int firstX = first.RawPosition.X;
            int secondX = second.RawPosition.X;
            int thirdX = third.RawPosition.X;

            int firstY = first.RawPosition.Y;
            int secondY = second.RawPosition.Y;
            int thirdY = third.RawPosition.Y;

            g.DrawEllipse(new Pen(Color.Red), (int)(first.RawPosition.X / 4), (int)(first.RawPosition.Y / 4),
                             first.Size + 1, first.Size + 1);

            g.DrawEllipse(new Pen(Color.Blue), (int)(second.RawPosition.X / 4), (int)(second.RawPosition.Y / 4),
                             second.Size + 1, second.Size + 1);

            g.DrawEllipse(new Pen(Color.Yellow), (int)(third.RawPosition.X / 4), (int)(third.RawPosition.Y / 4),
                             third.Size + 1, third.Size + 1);

            double firstSecondDist = Math.Sqrt(Math.Pow((firstX - secondX), 2) + (Math.Pow((firstY - secondY), 2)));
            double secondThirdDist = Math.Sqrt(Math.Pow((secondX - thirdX), 2) + (Math.Pow((secondY - thirdY), 2)));
            double firstThirdDist = Math.Sqrt(Math.Pow((firstX - thirdX), 2) + (Math.Pow((firstY - thirdY), 2)));

            if (firstSecondDist > secondThirdDist && firstSecondDist > firstThirdDist)
                return firstX.ToString() + "," + firstY.ToString() + "," + secondX.ToString() + "," + secondY.ToString();

            if(secondThirdDist > firstSecondDist && secondThirdDist > firstThirdDist)
                return secondX.ToString() + "," + secondY.ToString() + "," + thirdX.ToString() + "," + thirdY.ToString();
            //firstThirdDist is largest
            return firstX.ToString() + "," + firstY.ToString() + "," + thirdX.ToString() + "," + thirdY.ToString();
        }

        private String getTwoPoints(IRSensor first, IRSensor second)
        {
            g.DrawEllipse(new Pen(Color.Red), (int)(first.RawPosition.X / 4), (int)(first.RawPosition.Y / 4),
                            first.Size + 1, first.Size + 1);

            g.DrawEllipse(new Pen(Color.Blue), (int)(second.RawPosition.X / 4), (int)(second.RawPosition.Y / 4),
                            second.Size + 1, second.Size + 1);

            return first.RawPosition.X.ToString() + "," + first.RawPosition.Y.ToString() + "," + second.RawPosition.X.ToString() + "," + second.RawPosition.Y.ToString() + '\n';
        }

        private void Send(Socket client, String message)
        {
            String[] messageArgs = message.Split(',');
            int inX1 = Convert.ToInt16(messageArgs[0]);
            int inY1 = Convert.ToInt16(messageArgs[1]);
            int inX2 = Convert.ToInt16(messageArgs[2]);
            int inY2 = Convert.ToInt16(messageArgs[3]);

            String[]oldArgs = lastMessageSent.Split(',');
            int oldX1 = Convert.ToInt16(oldArgs[0]);
            int oldY1 = Convert.ToInt16(oldArgs[1]);
            int oldX2 = Convert.ToInt16(oldArgs[2]);
            int oldY2 = Convert.ToInt16(oldArgs[3]);

            if (inX1 > inX2)
                message = Swap(inX1, inY1, inX2, inY2);

            if (Math.Abs(inX1 - oldX1) + Math.Abs(inX2 - oldX2) + Math.Abs(inY1 - oldY1) + Math.Abs(inY2 - oldY2) < 1500)
            {
                AsynchronousClient.Send(StateObject.workSocket, message);
                lastMessageSent = message;
            }
            else
            {
                AsynchronousClient.Send(StateObject.workSocket, lastMessageSent);
            }
        }

		private void UpdateExtensionChanged(WiimoteExtensionChangedEventArgs args)
		{
			chkExtension.Text = args.ExtensionType.ToString();
			chkExtension.Checked = args.Inserted;
		}

        private String Swap(int X1, int Y1, int X2, int Y2)
        {
            return X2.ToString() + ',' + Y2.ToString() + ',' + X1.ToString() + ',' + Y1.ToString() + '\n';
        }

		public Wiimote Wiimote
		{
			set { mWiimote = value; }
		}
	}
}
