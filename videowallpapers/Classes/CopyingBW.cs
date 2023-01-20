using System.ComponentModel;

namespace videowp.Classes
{
    internal class CopyingBW
    {
        readonly BackgroundWorker bw = new BackgroundWorker();
        
        public CopyingBW(DoWorkEventHandler func)
        {
            bw.WorkerSupportsCancellation = true;
            bw.DoWork += func;
        }

        public void Start() { bw.RunWorkerAsync(); }
        public void Stop() { bw.CancelAsync(); }
        public bool IsActive() { return bw.IsBusy; }
    }
}
