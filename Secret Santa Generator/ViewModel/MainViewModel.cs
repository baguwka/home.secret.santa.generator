using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using JetBrains.Annotations;
using Prism.Commands;
using Prism.Mvvm;
using Secret_Santa_Generator.Model.IdsProvider;

namespace Secret_Santa_Generator.ViewModel
{
    public class MainViewModel : BindableBase
    {
        private const string DefaultIdLabel = "...";

        private readonly IIdProvider _IdProvider;
        private string _NextId = DefaultIdLabel;

        private bool _IsNextIdDelayIsActive;
        private float _NextIdDelayProgressValue;

        public bool IsNextIdDelayIsActive
        {
            get => _IsNextIdDelayIsActive;
            set => SetProperty(ref _IsNextIdDelayIsActive, value);
        }

        public string NextId
        {
            get => _NextId;
            set => SetProperty(ref _NextId, value);
        }

        public float NextIdDelayProgressValue {
            get => _NextIdDelayProgressValue;
            set => SetProperty(ref _NextIdDelayProgressValue, value);
        }


        public ICommand GetNextIdCommand { get; }
        public ICommand HideCommand { get; }

        public MainViewModel([NotNull] IIdProvider idProvider)
        {
            _IdProvider = idProvider ?? throw new ArgumentNullException(nameof(idProvider));
            GetNextIdCommand = new DelegateCommand(GetNextIdExecute, CanExecuteGetNextIdCommand)
                .ObservesProperty(() => IsNextIdDelayIsActive);

            HideCommand = new DelegateCommand(HideExecute);
        }

        private bool CanExecuteGetNextIdCommand()
        {
            return IsNextIdDelayIsActive == false;
        }

        private void HideExecute()
        {
            NextId = DefaultIdLabel;
        }

        private async void GetNextIdExecute()
        {
#pragma warning disable 4014
            RunDelayAsync().ContinueWith(OnRunDelayFailed, TaskContinuationOptions.OnlyOnFaulted);
#pragma warning restore 4014

            try
            {
                NextId = await _IdProvider.GetNextIdAsync();
            }
            catch (OutOfIdsException ooiex)
            {
                MessageBox.Show("Out of ids. Please reset the app (use konami code)", "Info", MessageBoxButton.OK,
                    MessageBoxImage.Information);
            }
            finally
            {
            }
        }

        private async Task RunDelayAsync()
        {
            IsNextIdDelayIsActive = true;
            var maxDelay = TimeSpan.FromSeconds(5);
            var currentDelay = TimeSpan.FromSeconds(0);
            const int deltaMs = 10;

            while (IsNextIdDelayIsActive)
            {
                currentDelay += TimeSpan.FromMilliseconds(deltaMs);
                if (currentDelay >= maxDelay)
                    break;

                NextIdDelayProgressValue = 1 - (float) ((currentDelay.TotalMilliseconds / maxDelay.TotalMilliseconds * 100) / 100);

                await Task.Delay(deltaMs);
            }

            IsNextIdDelayIsActive = false;
            NextIdDelayProgressValue = 0;
        }

        private void OnRunDelayFailed(Task task)
        {
            Exception ex = task.Exception;
            //todo log
        }
    }
}