using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Secret_Santa_Generator.Model;
using Secret_Santa_Generator.Model.Combination;
using Secret_Santa_Generator.Model.IdsProvider;
using Secret_Santa_Generator.Model.Persistent;
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
            var idProviderFactory = new NoDuplicatesIdsProviderFactory(2);
            var persistentManager = new JsonPersistentManager();
            var persistentIdsProvider = new PersistentNoDuplicatesIdProvider(persistentManager, idProviderFactory);
            var vm = new MainViewModel(persistentIdsProvider);
            var combinationHandler = new KonamiCodeCombinationHandler();

            var view = new MainView(vm, combinationHandler);

            view.ShowDialog();
        }
    }
}
