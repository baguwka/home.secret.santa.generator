using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Secret_Santa_Generator.Model;
using Secret_Santa_Generator.View;
using Secret_Santa_Generator.ViewModel;

namespace Secret_Santa_Generator
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void App_OnStartup(object sender, StartupEventArgs e)
        {
            var vm = new MainViewModel();
            var combinationHandler = new KonamiCodeCombinationHandler();

            var view = new MainView(vm, combinationHandler);

            view.ShowDialog();
        }
    }
}
