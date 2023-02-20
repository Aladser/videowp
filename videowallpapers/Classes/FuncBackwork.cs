using System.ComponentModel;
using videowp.Formes;

namespace videowp.Classes
{
   /// <summary>
   /// Фоновая функция
   /// </summary>
    internal class FuncBackwork
    {
        readonly BackgroundWorker bw = new BackgroundWorker();
        readonly SettingForm setForm;
        
        public FuncBackwork(DoWorkEventHandler func, SettingForm sf=null)
        {
            setForm = sf;
            bw.WorkerSupportsCancellation = true;
            bw.DoWork += func;
        }

        public void Start() {
            if (setForm != null) 
                bw.RunWorkerAsync(setForm);
            else 
                bw.RunWorkerAsync(); 
        }
        public void Stop() { bw.CancelAsync(); }
        public bool IsActive() { return bw.IsBusy; }
    }
}
