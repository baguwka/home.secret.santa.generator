using System;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Secret_Santa_Generator.Model.Persistent;

namespace Secret_Santa_Generator.Model.IdsProvider
{
    public class PersistentNoDuplicatesIdProvider : IIdProvider
    {
        private readonly IPersistentManager _PersistentManager;
        [NotNull] private readonly NoDuplicatesIdsProviderFactory _ProviderFactory;

        public PersistentNoDuplicatesIdProvider(
            [NotNull] IPersistentManager persistentManager,
            [NotNull] NoDuplicatesIdsProviderFactory providerFactory)
        {
            _PersistentManager = persistentManager ?? throw new ArgumentNullException(nameof(persistentManager));
            _ProviderFactory = providerFactory ?? throw new ArgumentNullException(nameof(providerFactory));
        }

        public async Task<string> GetNextIdAsync()
        {
            var idProvider = (NoDuplicatesIdProvider)_ProviderFactory.Create();
            await ClearIntersectsFromProvider(idProvider);

            var nextId = await idProvider.GetNextIdAsync();

            var existentModel = await _PersistentManager.ReadAsync();
            if (existentModel.ExistentIds.Contains(nextId))
            {
                //why?
            }

            existentModel.ExistentIds.Add(nextId);
            await _PersistentManager.WriteAsync(existentModel);

            return nextId;
        }

        private async Task ClearIntersectsFromProvider(NoDuplicatesIdProvider idProvider)
        {
            var existentModel = await _PersistentManager.ReadAsync();
            if (existentModel.ExistentIds == null)
                return;

            var intersects = idProvider.PossibleIds.Intersect(existentModel.ExistentIds);
            idProvider.PossibleIds.RemoveAll(s => intersects.Contains(s));
        }
    }
}