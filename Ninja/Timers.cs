using System;
using System.Timers;

using System;
namespace Mic_2
{
    public class Timers
    {
        internal Timer workStatusTimer;//сигнализирует, если за определенный срок не звук на микрофон так и не поступил

        public Timers()
        {
            TimersCreate();
        }


        private void TimersCreate()
        {
            workStatusTimer = new Timer();
            workStatusTimer.Interval = 2000;
            workStatusTimer.AutoReset = true;
            workStatusTimer.Elapsed += new ElapsedEventHandler(delegate (object sender, ElapsedEventArgs e)
                          {

                          });


        }
    }
}
