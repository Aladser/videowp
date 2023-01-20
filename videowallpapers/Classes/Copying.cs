using System.ComponentModel;

namespace videowp.Classes
{
    public class Copying
    {
        readonly BackgroundWorker bw = new BackgroundWorker();
        
        public Copying(DoWorkEventHandler func)
        {
            bw.WorkerSupportsCancellation = true;
            bw.DoWork += func;
        }

        public void Start() { bw.RunWorkerAsync(); }
        public void Stop() { bw.CancelAsync(); }
        public bool IsActive() { return bw.IsBusy; }
    }
}
