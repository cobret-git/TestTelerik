using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LightUI;
using Telerik.Windows.Controls.Docking;

namespace TestTelerik.ViewModel
{
    public class ShellViewModel : PropertyChangedBase, IViewModel
    {
        public ShellViewModel()
        {
            eventAggregator = new EventAggregator();

            Panes = new ObservableCollection<IPaneViewModel>();

            FontViewModel = new FontViewModel(eventAggregator);

            AddNewFile = new RelayCommand<object>(AddNew);
            ChangeFont = new RelayCommand<object>(ChangeFontCommand);
        }

        #region Properties
        public FontViewModel FontViewModel { get; set; }
        public RelayCommand<object> AddNewFile { get; set; }
        public RelayCommand<object> ChangeFont { get; set; }
        public ObservableCollection<IPaneViewModel> Panes { get; set; }
        #endregion

        #region Fields
        private IEventAggregator eventAggregator;
        #endregion

        #region Void
        private void AddNew(object param)
        {
            Panes.Add(new FileViewModel(eventAggregator, new FileViewInfo()));
        }
        private void ChangeFontCommand(object param)
        {
            if (Panes.Contains(FontViewModel)) return;

            Panes.Add(FontViewModel);
        }
        #endregion
    }
}
