using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LightUI;

namespace TestTelerik.ViewModel
{
    public class FontViewModel : PaneViewModel
    {
        public FontViewModel(IEventAggregator eventAggregator)
        {
            this.eventAggregator = eventAggregator;
            Header = "Font";
            PaneName = "FontModel";
            DockingPosition = DockingPosition.Left;
            IsDocument = false;

            eventAggregator.SubscribeAction<FileViewSelected>(GetFocus);
        }

        #region Properties
        public ObservableCollection<string> Fonts
        {
            get
            {
                ObservableCollection<string> result = new ObservableCollection<string>();
                InstalledFontCollection fontCollection = new InstalledFontCollection();
                var fontFamilies = fontCollection.Families;
                foreach (var fontFamily in fontFamilies)
                {
                    result.Add(fontFamily.Name);
                }
                return result;
            }
        }
        public ObservableCollection<string> Sizes
        {
            get
            {
                ObservableCollection<string> result = new ObservableCollection<string>()
                {
                    "4",  "5",  "6",  "7",  "8", "9", "10", "11", "12", "13", "14",
                     "16", "18", "20", "22", "24", "26", "28", "36", "48", "72"
                };
                return result;
            }
        }
        public int FontSelectedIndex
        {
            get => fontSelectedIndex;
            set
            {
                fontSelectedIndex = value;
                OnPropertyChanged();
                fileViewInfo.FontFamily = new FontFamily(Fonts[fontSelectedIndex]);
                eventAggregator.Publish<FileViewInfo>(fileViewInfo);
            }
        }
        public string FontSize
        {
            get => fontSize.ToString();
            set
            {
                int outParse = 0;
                bool sParse = int.TryParse(value, out outParse);
                if (!sParse || outParse <= 0) return;
                fontSize = outParse;
                OnPropertyChanged();
                fileViewInfo.FontSize = fontSize;
                eventAggregator.Publish<FileViewInfo>(fileViewInfo);
            }
        }
        #endregion

        #region Fields
        private IEventAggregator eventAggregator;
        private int fontSelectedIndex = 0;
        private int fontSize;
        private FileViewInfo fileViewInfo;
        #endregion

        #region Void
        public void GetFocus(FileViewSelected fvs)
        {
            fileViewInfo = fvs.FileInfo;
            FontSize = fileViewInfo.FontSize.ToString();
            int index = Fonts.IndexOf(fileViewInfo.FontFamily.Name);
            if (index <= 0) return;
            FontSelectedIndex = index;
        }
        #endregion
    }
}
