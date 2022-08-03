using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LightUI;
using System.Drawing;

namespace TestTelerik.ViewModel
{
    public class FileViewModel : PaneViewModel
    {
        public FileViewModel(IEventAggregator eventAggregator, FileViewInfo fvi)
        {
            this.eventAggregator = eventAggregator;
            fileViewInfo = fvi;
            Header = fvi.FileName;
            FontSize = fvi.FontSize;
            PaneName = "FileViewModel";
            DockingPosition = DockingPosition.Document;
            IsDocument = true;
            CanUserClose = true;

            GotFocus = new RelayCommand<object>(GotFocusCommand);

            eventAggregator.SubscribeAction<FileViewInfo>(FVIChanged);
        }


        #region Properties
        public string UserText
        {
            get => userText.ToString();
            set 
            {
                userText = new StringBuilder(value);
                RowNumber = (value.Split('\n').Length - 1).ToString();
                OnPropertyChanged(); 
            }
        }
        public string RowNumber
        {
            get => rowNumber;
            set { rowNumber = value; OnPropertyChanged(); }
        }
        public int FontSize
        {
            get => fileViewInfo.FontSize;
            set { fileViewInfo.FontSize = value; OnPropertyChanged(); }
        }
        public System.Windows.Media.FontFamily FontFamily
        {
            get => new System.Windows.Media.FontFamily(fileViewInfo.FontFamily.Name);
            set { fileViewInfo.FontFamily = new System.Drawing.FontFamily(value.Source); OnPropertyChanged(); }
        }
        public RelayCommand<object> GotFocus { get; set; }
        #endregion

        #region Fields
        private IEventAggregator eventAggregator;
        private StringBuilder userText = new StringBuilder();
        private FileViewInfo fileViewInfo;
        private string rowNumber = 0.ToString();
        #endregion

        #region Void
        private void FVIChanged(FileViewInfo fvi)
        {
            if (fileViewInfo.Id != fvi.Id) return;
            fileViewInfo = fvi;
            OnPropertyChanged(nameof(FontSize));
            OnPropertyChanged(nameof(FontFamily));
        }
        private void GotFocusCommand(object param)
        {
            eventAggregator.Publish<FileViewSelected>(new FileViewSelected(fileViewInfo));
        }
        #endregion
    }

    public class FileViewInfo
    {
        public string FileName { get; set; }
        public int FontSize { get; set; }
        public FontFamily FontFamily { get; set; }
        public string Id
        {
            get
            {
                if (id != null) return id;
                id = $"ID{InstanceNumber}";
                InstanceNumber++;
                return id;
            }
        }
        private string id;
        public static int InstanceNumber { get; set; }

        public FileViewInfo()
        {
            FileName = "Unnamed";
            FontSize = 14;
            FontFamily = new FontFamily("Segoe UI Symbol");
        }
    }
    public class FileViewSelected
    {
        public FileViewInfo FileInfo { get; set; }
        public FileViewSelected(FileViewInfo fileInfo)
        {
            FileInfo = fileInfo;
        }
    }

}
