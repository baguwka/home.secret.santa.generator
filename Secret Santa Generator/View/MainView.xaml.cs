using System;
using System.Windows;
using System.Windows.Input;
using JetBrains.Annotations;
using Secret_Santa_Generator.Model;
using Secret_Santa_Generator.Model.Combination;
using Secret_Santa_Generator.Model.Persistent;
using Secret_Santa_Generator.ViewModel;

namespace Secret_Santa_Generator.View
{
    /// <summary>
    /// Interaction logic for MainView.xaml
    /// </summary>
    public partial class MainView : Window
    {
        [NotNull] private readonly MainViewModel _ViewModel;
        [NotNull] private readonly CombinationHandler _CombinationHandler;

        public MainView([NotNull] MainViewModel viewModel, [NotNull] CombinationHandler combinationHandler)
        {
            _ViewModel = viewModel ?? throw new ArgumentNullException(nameof(viewModel));
            _CombinationHandler = combinationHandler ?? throw new ArgumentNullException(nameof(combinationHandler));

            InitializeComponent();

            DataContext = _ViewModel;
        }

        private async void UIElement_OnKeyDown(object sender, KeyEventArgs e)
        {
            var result = _CombinationHandler.ProvideNextInput(e.Key);
            if (result.IsSequienceCompleted)
            {
                var persistentManager = new JsonPersistentManager();
                var adminVm = await AdminViewModel.NewAsync(persistentManager);
                var adminView = new AdminView(adminVm);
                adminView.ShowDialog();
            }
        }
    }
}
