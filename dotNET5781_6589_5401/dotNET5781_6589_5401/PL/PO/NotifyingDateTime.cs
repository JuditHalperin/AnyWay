using System;
using System.ComponentModel;
using System.Threading;


namespace PO
{
    class NotifyingDateTime : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public BackgroundWorker worker;

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

        public NotifyingDateTime()
        {
            now = DateTime.Now;
            worker = new BackgroundWorker();
            worker.DoWork += startTime;
            worker.ProgressChanged += showTime;
            worker.WorkerReportsProgress = true;
        }

        private void startTime(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;

            while (true)
            {
                worker.ReportProgress(0);
                Thread.Sleep(1000);
            }
        }

        private void showTime(object sender, ProgressChangedEventArgs e)
        {
            Now = DateTime.Now;
        }
    }

}
