using System;
using System.Diagnostics.Eventing;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using JetBrains.Annotations;
using Prism.Commands;
using Prism.Mvvm;
using Secret_Santa_Generator.Model.IdsProvider;

namespace Secret_Santa_Generator.ViewModel
{
    public class MainViewModel : BindableBase
    {
        private const string DefaultIdLabel = "#ID";

        private readonly IIdProvider _IdProvider;
        private string _NextId = DefaultIdLabel;

        public string NextId
        {
            get => _NextId;
            set => SetProperty(ref _NextId, value);
        }

        public ICommand GetNextIdCommand { get; }
        public ICommand HideCommand { get; }

        public MainViewModel([NotNull] IIdProvider idProvider)
        {
            _IdProvider = idProvider ?? throw new ArgumentNullException(nameof(idProvider));
            GetNextIdCommand = new DelegateCommand(GetNextIdExecute);
            HideCommand = new DelegateCommand(HideExecute);
        }

        private void HideExecute()
        {
            NextId = DefaultIdLabel;
        }

        private async void GetNextIdExecute()
        {
            try
            {
                NextId = await _IdProvider.GetNextIdAsync();
            }
            catch (OutOfIdsException ooiex)
            {
                MessageBox.Show("Out of ids. Please reset the app (use konami code)", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}