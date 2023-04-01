using System.Drawing;
using System.Drawing.Imaging;

namespace OnPixelColorChange_Clicker
{
    class Program
    {
        private Point pixelLocation;
        private Color pixelColor;
        private readonly Bitmap bitmap = new(1, 1, PixelFormat.Format32bppArgb);
        private bool isClicking;
        private const int KeyboardEventNumpad1 = 0x61;
        private const int KeyboardEventNumpad2 = 0x62;
        private const int KeyboardEventNumpad3 = 0x63;
        private const int WaitLoopDelay = 125;
        private const int WaitLoopIterations = 200;
        private const int WaitDelayAfterClick = 2000;

        private void InitializeApp()
        {
            Console.Title = "OnPixelColorChange Clicker by Kamil Hibszer";
            Console.WriteLine("======================================================");
            Console.WriteLine("[ HOTKEYS ]\n");
            Console.WriteLine("NumPad1 = save pixel position");
            Console.WriteLine("NumPad2 = START");
            Console.WriteLine("NumPad3 = STOP\n\n");
            Console.WriteLine("======================================================");
            Console.WriteLine("\n\n[ LOG ]\n");

            while (true)
            {
                if (DeviceManipulator.GetAsyncKeyState(KeyboardEventNumpad1) != 0)
                {
                    DeviceManipulator.GetCursorPos(out pixelLocation);
                    pixelColor = GetPixelColor(pixelLocation);
                    Console.WriteLine("Mouse X = " + pixelLocation.X);
                    Console.WriteLine("Mouse Y = " + pixelLocation.Y);
                    Thread.Sleep(100);
                }

                if (DeviceManipulator.GetAsyncKeyState(KeyboardEventNumpad2) != 0)
                {
                    Console.WriteLine("Activated !");
                    Thread.Sleep(100);
                    isClicking = true;

                    while (isClicking)
                    {
                        var newPixelColor = GetPixelColor(pixelLocation);
                        if (newPixelColor == pixelColor)
                        {
                            DeviceManipulator.SimulateMouseActions();
                            Thread.Sleep(WaitDelayAfterClick);
                        }

                        var waitLoopIterations = WaitLoopIterations;
                        while (waitLoopIterations > 0)
                        {
                            Thread.Sleep(WaitLoopDelay);
                            waitLoopIterations -= WaitLoopDelay;
                            if (DeviceManipulator.GetAsyncKeyState(KeyboardEventNumpad3) != 0)
                            {
                                isClicking = false;
                                Console.WriteLine("Stopping the clicker..");
                                break;
                            }
                        }
                    }
                }
            }
        }

        private Color GetPixelColor(Point point)
        {
            using (var graphics = Graphics.FromImage(bitmap))
            {
                graphics.CopyFromScreen(point, new Point(0, 0), new Size(1, 1));
            }
            return bitmap.GetPixel(0, 0);
        }

        public static void Main()
        {
            new Program().InitializeApp();
        }
    }
}
