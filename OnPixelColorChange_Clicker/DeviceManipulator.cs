using System.Drawing;
using System.Runtime.InteropServices;

namespace OnPixelColorChange_Clicker
{
    /// <summary>
    /// Provides methods for simulating mouse and keyboard actions using Windows API.
    /// </summary>
    internal class DeviceManipulator
    {
        private const uint MouseEventRightDown = 0x0008;
        private const uint MouseEventRightUp = 0x0010;
        private const uint KeyEventKeyDown = 0x0000;
        private const uint KeyEventKeyUp = 0x0002;
        private const ushort KeyCodeD1 = 0x31;
        private const ushort KeyCodeD2 = 0x32;

        [DllImport("user32.dll", SetLastError = true)]
        private static extern uint SendInput(uint nInputs, INPUT[] pInputs, int cbSize);

        [DllImport("user32.dll")]
        public static extern short GetAsyncKeyState(int vKey);

        [DllImport("user32.dll")]
        public static extern bool GetCursorPos(out Point lpPoint);

        private enum EventType
        {
            MouseEvent = 0,
            KeyEvent = 1,
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct INPUT
        {
            public int Type;
            public INPUTUNION Union;
        }

        [StructLayout(LayoutKind.Explicit)]
        private struct INPUTUNION
        {
            [FieldOffset(0)]
            public MOUSEINPUT MouseInput;
            [FieldOffset(0)]
            public KEYBDINPUT KeyboardInput;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct MOUSEINPUT
        {
            public int X;
            public int Y;
            public uint MouseData;
            public uint Flags;
            public uint Time;
            public IntPtr ExtraInfo;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct KEYBDINPUT
        {
            public ushort VirtualKey;
            public ushort ScanCode;
            public uint Flags;
            public uint Time;
            public IntPtr ExtraInfo;
        }

        private static readonly INPUT mouseRightDownInput = new()
        {
            Type = (int)EventType.MouseEvent,
            Union = new INPUTUNION { MouseInput = new MOUSEINPUT { Flags = MouseEventRightDown } }
        };

        private static readonly INPUT mouseRightUpInput = new()
        {
            Type = (int)EventType.MouseEvent,
            Union = new INPUTUNION { MouseInput = new MOUSEINPUT { Flags = MouseEventRightUp } }
        };

        private static readonly INPUT keyboardD1KeyDownInput = new()
        {
            Type = (int)EventType.KeyEvent,
            Union = new INPUTUNION
            {
                KeyboardInput = new KEYBDINPUT
                {
                    VirtualKey = KeyCodeD1,
                    Flags = KeyEventKeyDown
                }
            }
        };

        private static readonly INPUT keyboardD1KeyUpInput = new()
        {
            Type = (int)EventType.KeyEvent,
            Union = new INPUTUNION
            {
                KeyboardInput = new KEYBDINPUT
                {
                    VirtualKey = KeyCodeD1,
                    Flags = KeyEventKeyUp
                }
            }
        };

        private static readonly INPUT keyboardD2KeyDownInput = new()
        {
            Type = (int)EventType.KeyEvent,
            Union = new INPUTUNION
            {
                KeyboardInput = new KEYBDINPUT
                {
                    VirtualKey = KeyCodeD2,
                    Flags = KeyEventKeyDown
                }
            }
        };

        private static readonly INPUT keyboardD2KeyUpInput = new()
        {
            Type = (int)EventType.KeyEvent,
            Union = new INPUTUNION
            {
                KeyboardInput = new KEYBDINPUT
                {
                    VirtualKey = KeyCodeD2,
                    Flags = KeyEventKeyUp
                }
            }
        };

        public static void SimulateMouseActions()
        {
            var inputs = new[] { mouseRightDownInput, mouseRightUpInput };

            // Simulate a right mouse button click
            _ = SendInput((uint)inputs.Length, inputs, Marshal.SizeOf(typeof(INPUT)));

            Thread.Sleep(800);

            inputs = new[] { keyboardD2KeyDownInput, keyboardD2KeyUpInput };

            // Simulate a keyboard D1 key press
            _ = SendInput((uint)inputs.Length, inputs, Marshal.SizeOf(typeof(INPUT)));

            Thread.Sleep(100);

            inputs = new[] { keyboardD1KeyDownInput, keyboardD1KeyUpInput };

            // Simulate a keyboard D2 key press
            _ = SendInput((uint)inputs.Length, inputs, Marshal.SizeOf(typeof(INPUT)));

            Thread.Sleep(100);

            inputs = new[] { mouseRightDownInput };

            // Simulate a right mouse button click
            _ = SendInput((uint)inputs.Length, inputs, Marshal.SizeOf(typeof(INPUT)));

            Thread.Sleep(500);

            inputs = new[] { mouseRightUpInput };

            // Simulate a right mouse button click
            _ = SendInput((uint)inputs.Length, inputs, Marshal.SizeOf(typeof(INPUT)));
        }
    }
}
