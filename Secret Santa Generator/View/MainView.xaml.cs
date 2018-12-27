using System;
using System.Windows;
using System.Windows.Input;
using JetBrains.Annotations;
using Secret_Santa_Generator.Model;
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
            if (viewModel == null) throw new ArgumentNullException(nameof(viewModel));
            if (combinationHandler == null) throw new ArgumentNullException(nameof(combinationHandler));

            _ViewModel = viewModel;
            _CombinationHandler = combinationHandler;

            InitializeComponent();

            DataContext = _ViewModel;
        }

        private void UIElement_OnKeyDown(object sender, KeyEventArgs e)
        {
            var result = _CombinationHandler.ProvideNextInput(e.Key);
            if (result.IsSequienceCompleted)
            {
                MessageBox.Show("Yay!");
            }
        }
    }
}
