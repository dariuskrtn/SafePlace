using SafePlace.Views.MainWindowView;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace SafePlace
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var uiContext = SynchronizationContext.Current;

            Service.ConsoleLogger Logger = new Service.ConsoleLogger();

            var mainWindowViewModel = new MainWindowViewModel();

            var mainWindowPresenter = new MainWindowPresenter(mainWindowViewModel, Logger, uiContext);
            var mainWindow = new MainWindow();

            mainWindow.DataContext = mainWindowViewModel;
            mainWindow.Show();
        }
    }
}
