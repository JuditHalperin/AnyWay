using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Threading;

namespace temp
{
        public class NotifyingDateTime : INotifyPropertyChanged

        {

            public event PropertyChangedEventHandler PropertyChanged;

            private DateTime now;

            public NotifyingDateTime()

            {

                now = DateTime.Now;

                DispatcherTimer timer = new DispatcherTimer();

                timer.Interval = TimeSpan.FromMilliseconds(100);

                timer.Tick += new EventHandler(timer_Tick);

                timer.Start();

            }

            public DateTime Now

            {

                get { return now; }

                private set

                {

                    now = value;

                    if (PropertyChanged != null)

                        PropertyChanged(this, new PropertyChangedEventArgs("Now"));

                }

            }

            void timer_Tick(object sender, EventArgs e)

            {

                Now = DateTime.Now;

            }

        }

}
