using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using LightUI.Wpf;

namespace TestTelerik.ViewModel
{
    public class AppBootstrapper : BootstrapperBase
    {
        public AppBootstrapper()
        {
            Initialize();
        }
        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            DisplayShellView<ShellViewModel>(new Dictionary<string, object>()
            {
                { "Title", "LightUI Testing App" },
                { "WindowSize", "Manual" },
                { "MinWidth", 1000 },
                { "MinHeight", 600 }
            });
        }
    }
}
