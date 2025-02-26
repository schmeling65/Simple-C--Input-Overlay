using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace XXL_Default_Input_Overlay
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Start();
        }

        public void ChangeKeyColor(int _key, bool pressed)
        {
            if (pressed)
            {
                switch (_key)
                {
                    case 0x52:
                        this.RButton.BackColor = System.Drawing.Color.FromArgb(179, 179, 179);
                        break;
                    case 0x44:
                        this.DButton.BackColor = System.Drawing.Color.FromArgb(179, 179, 179);
                        break;
                    case 0x46:
                        this.FButton.BackColor = System.Drawing.Color.FromArgb(179, 179, 179);
                        break;
                    case 0x47:
                        this.GButton.BackColor = System.Drawing.Color.FromArgb(179, 179, 179);
                        break;
                    case 0x43:
                        this.CButton.BackColor = System.Drawing.Color.FromArgb(179, 179, 179);
                        break;
                    case 0xA0:
                        this.LShiftButton.BackColor = System.Drawing.Color.FromArgb(179, 179, 179);
                        break;
                    case 0xA1:
                        this.RShiftButton.BackColor = System.Drawing.Color.FromArgb(179, 179, 179);
                        break;
                    case 0xA2:
                        this.LControlButton.BackColor = System.Drawing.Color.FromArgb(179, 179, 179);
                        break;
                    case 0xA3:
                        this.RControlButton.BackColor = System.Drawing.Color.FromArgb(179, 179, 179);
                        break;
                    case 0x25:
                        this.LeftArrowButton.BackColor = System.Drawing.Color.FromArgb(179, 179, 179);
                        break;
                    case 0x26:
                        this.UpArrowButton.BackColor = System.Drawing.Color.FromArgb(179, 179, 179);
                        break;
                    case 0x27:
                        this.RightArrowButton.BackColor = System.Drawing.Color.FromArgb(179, 179, 179);
                        break;
                    case 0x28:
                        this.DownArrowButton.BackColor = System.Drawing.Color.FromArgb(179, 179, 179);
                        break;
                    case 0x20:
                        this.SpaceButton.BackColor = System.Drawing.Color.FromArgb(179, 179, 179);
                        break;
                }
            }
            else if (!pressed)
            {
                switch (_key)
                {
                    case 0x52:
                        this.RButton.BackColor = System.Drawing.Color.Gray;
                        break;
                    case 0x44:
                        this.DButton.BackColor = System.Drawing.Color.Gray;
                        break;
                    case 0x46:
                        this.FButton.BackColor = System.Drawing.Color.Gray;
                        break;
                    case 0x47:
                        this.GButton.BackColor = System.Drawing.Color.Gray;
                        break;
                    case 0x43:
                        this.CButton.BackColor = System.Drawing.Color.Gray;
                        break;
                    case 0xA0:
                        this.LShiftButton.BackColor = System.Drawing.Color.Gray;
                        break;
                    case 0xA1:
                        this.RShiftButton.BackColor = System.Drawing.Color.Gray;
                        break;
                    case 0xA2:
                        this.LControlButton.BackColor = System.Drawing.Color.Gray;
                        break;
                    case 0xA3:
                        this.RControlButton.BackColor = System.Drawing.Color.Gray;
                        break;
                    case 0x25:
                        this.LeftArrowButton.BackColor = System.Drawing.Color.Gray;
                        break;
                    case 0x26:
                        this.UpArrowButton.BackColor = System.Drawing.Color.Gray;
                        break;
                    case 0x27:
                        this.RightArrowButton.BackColor = System.Drawing.Color.Gray;
                        break;
                    case 0x28:
                        this.DownArrowButton.BackColor = System.Drawing.Color.Gray;
                        break;
                    case 0x20:
                        this.SpaceButton.BackColor = System.Drawing.Color.Gray;
                        break;
                }
            }

        }


        private static int WH_KEYBOARD_LL = 13;
        private static int WM_KEYDOWN = 0x0100;
        private static int WM_KEYUP = 0x0101;
        private static int WM_SYSKEYDOWN = 0x0104;
        private static int WM_SYSKEYUP = 0x0105;
        private static IntPtr hook = IntPtr.Zero;
        private LowLevelKeyboardProc llkProcedure;

        // Use this for initialization
        public void Start()
        {
            this.llkProcedure = this.HookCallback;
            hook = SetHook(llkProcedure);
        }

        public delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);

        public IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            //regular keys
            if (nCode >= 0 && wParam == (IntPtr)WM_KEYDOWN)
            {
                int vkCode = Marshal.ReadInt32(lParam);
                ChangeKeyColor(vkCode, true);
           }
            if (nCode >= 0 && wParam == (IntPtr)WM_KEYUP)
            {
                int vkCode = Marshal.ReadInt32(lParam);
                ChangeKeyColor(vkCode, false);
            }
            //alt key
            if (nCode >= 0 && wParam == (IntPtr)WM_SYSKEYDOWN)
            {
                int vkCode = Marshal.ReadInt32(lParam);
                ChangeKeyColor(vkCode, true);
            }
            if (nCode >= 0 && wParam == (IntPtr)WM_SYSKEYUP)
            {
                int vkCode = Marshal.ReadInt32(lParam);
                ChangeKeyColor(vkCode, false);
            }
            return CallNextHookEx(IntPtr.Zero, nCode, wParam, lParam);
        }

        private IntPtr SetHook(LowLevelKeyboardProc proc)
        {
            Process currentProcess = Process.GetCurrentProcess();
            ProcessModule currentModule = currentProcess.MainModule;
            string moduleName = currentModule.ModuleName;
            IntPtr moduleHandle = GetModuleHandle(moduleName);
            return SetWindowsHookEx(WH_KEYBOARD_LL, llkProcedure, moduleHandle, 0);
        }

        [DllImport("user32.dll")]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);
        [DllImport("user32.dll")]
        private static extern IntPtr SetWindowsHookEx(int idHook, LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);
        [DllImport("user32.dll")]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);
        [DllImport("kernel32.dll")]
        private static extern IntPtr GetModuleHandle(String lpModuleName);

        public void OnDestroy()
        {
            Unhook();
        }

        private void Unhook()
        {
            UnhookWindowsHookEx(hook);
        }
    }
}
