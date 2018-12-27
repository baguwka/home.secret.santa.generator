using System;
using System.Threading.Tasks;
using System.Windows.Input;
using JetBrains.Annotations;
using Prism.Commands;
using Prism.Mvvm;
using Secret_Santa_Generator.Model.Persistent;

namespace Secret_Santa_Generator.ViewModel
{
    public class AdminViewModel : BindableBase
    {
        private readonly JsonPersistentManager _PersistentManager;
        private bool _RaiseExponentOnOverflow;
        private int _GeneratedItems;
        private int _ExponentCount;

        public bool RaiseExponentOnOverflow
        {
            get => _RaiseExponentOnOverflow;
            set => SetProperty(ref _RaiseExponentOnOverflow, value);
        }

        public int GeneratedItems
        {
            get => _GeneratedItems;
            set => SetProperty(ref _GeneratedItems, value);
        }

        public int ExponentCount
        {
            get => _ExponentCount;
            set => SetProperty(ref _ExponentCount, value);
        }

        public ICommand HardResetCommand { get; }

        public static async Task<AdminViewModel> NewAsync([NotNull] JsonPersistentManager persistentManager)
        {
            if (persistentManager == null) throw new ArgumentNullException(nameof(persistentManager));
            var vm = new AdminViewModel(persistentManager);
            await vm.UpdateGeneratedItems();
            return vm;
        }

        private AdminViewModel([NotNull] JsonPersistentManager persistentManager)
        {
            _PersistentManager = persistentManager ?? throw new ArgumentNullException(nameof(persistentManager));
            HardResetCommand = new DelegateCommand(HardResetExecute);
        }

        private async void HardResetExecute()
        {
            await _PersistentManager.ResetAsync();
            await UpdateGeneratedItems();
        }

        private async Task UpdateGeneratedItems()
        {
            var model = await _PersistentManager.ReadAsync();
            var count = model.ExistentIds?.Count;

            if (count.HasValue == false)
                GeneratedItems = 0;
            else
                GeneratedItems = count.Value;
        }
    }
}