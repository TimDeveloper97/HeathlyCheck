using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HealthDeclaration.Helpers
{
    public class TimerHelper
    {
        static Timer timer = new Timer();
        public TimerHelper(int time, Action @callback)
        {
            timer.Enabled = true;
            timer.Interval = time * 1000;
            timer.Tick += (s, e) => @callback.Invoke();
        }

        public void Start()
        {
            timer.Start();
        }

        public void Stop()
        {
            timer.Stop();
        }
    }
}
