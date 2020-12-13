using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Threading;
using System.Threading;

namespace temp
{
    public class NotifyingDateTime : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private DateTime now; public DateTime Now
        {
            get { return now; }
            private set
            {
                now = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Now"));
            }
        }
        public BackgroundWorker worker;

        public NotifyingDateTime()
        {
            now = DateTime.Now;
            worker = new BackgroundWorker();
            worker.DoWork += startTimer;
            worker.ProgressChanged += showTimer;
            worker.WorkerReportsProgress = true;
            
        }

        private void startTimer(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;

            while (true)
            {
                worker.ReportProgress(0);
                Thread.Sleep(1000);
            }
        }


        private void showTimer(object sender, ProgressChangedEventArgs e)
        {
            Now = DateTime.Now;
        }

    }
}
