using LightUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestTelerik.ViewModel
{
    public enum DockingPosition { Left, Right, Bottom, Document }
    public interface IPaneViewModel : IViewModel
    {
        public string Header { get; set; }
        public string PaneName { get; set; }
        public DockingPosition DockingPosition { get; set; }
        public bool IsDocument { get; }
        public bool CanUserClose { get; set; }
    }
}
