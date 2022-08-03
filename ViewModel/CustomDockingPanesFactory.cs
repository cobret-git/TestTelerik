using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using LightUI.Wpf;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.Docking;

namespace TestTelerik.ViewModel
{
    public class CustomDockingPanesFactory : DockingPanesFactory
    {
        protected override RadPane CreatePaneForItem(object item)
        {
            if (item is IPaneViewModel viewModel)
            {
                RadPane radPane = viewModel.IsDocument ? new RadDocumentPane() : new RadPane();
                radPane.DataContext = item;
                radPane.Header = viewModel.Header;
                radPane.Tag = viewModel.DockingPosition.ToString();
                radPane.CanUserClose = viewModel.CanUserClose;
                RadDocking.SetSerializationTag(radPane, viewModel.Header);
                UIElement shellView = ViewLocator.Locate(viewModel);
                radPane.Content = shellView;

                return radPane;
            }
            return base.CreatePaneForItem(item);
        }

        protected override void AddPane(Telerik.Windows.Controls.RadDocking radDocking, Telerik.Windows.Controls.RadPane pane)
        {
            var tag = pane.Tag.ToString();

            if (tag == null)
                return;

            if (radDocking.SplitItems.ToList().FirstOrDefault(i => i.Control.Name.Contains(tag)) is RadPaneGroup paneGroup)
            {
                paneGroup.Items.Add(pane);
            }
            else
            {
                base.AddPane(radDocking, pane);
            }
        }

    }
}
