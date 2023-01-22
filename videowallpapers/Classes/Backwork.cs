﻿using System.ComponentModel;

namespace videowp.Classes
{
    internal class Backwork
    {
        readonly BackgroundWorker bw = new BackgroundWorker();
        
        public Backwork(DoWorkEventHandler func)
        {
            bw.WorkerSupportsCancellation = true;
            bw.DoWork += func;
        }

        public void Start() { bw.RunWorkerAsync(); }
        public void Stop() { bw.CancelAsync(); }
        public bool IsActive() { return bw.IsBusy; }
    }
}