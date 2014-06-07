using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ProjectSpark
{
    public delegate void ScanEvent(string EAN);
    class Scanner
    {
        Timer myTimer = new Timer(50);
        List<Key> input = new List<Key>();
        public event ScanEvent scanEvent;

        public Scanner()
        {
            Switcher.Switcher.pageSwitcher.KeyDown += KeyDown;
            myTimer.Elapsed += Elapsed;
        }

        void Elapsed(object sender, ElapsedEventArgs e)
        {
            //Stop the timer
            myTimer.Stop();

            //Check to see if the input value is long enough to be an EAN-code
            if (input.Count >= 7)
            {
                string text = "";
                foreach (Key item in input)
                {
                    if (item != Key.LeftShift)
                    {
                        text += (char)System.Windows.Input.KeyInterop.VirtualKeyFromKey(item);
                    }
                }
                //text = text.Trim();
                if (scanEvent != null)
                {
                    scanEvent(text.Trim());
                } 
            }
            //Reset the input string
            input = new List<Key>();
        }

        void KeyDown(object sender, KeyEventArgs e)
        {
            if (!myTimer.Enabled)
            {
                myTimer.Start();
            }
            input.Add(e.Key);
        }
    }
}
