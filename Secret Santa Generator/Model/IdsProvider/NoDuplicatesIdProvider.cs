using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Secret_Santa_Generator.Model.IdsProvider
{
    public class NoDuplicatesIdProvider : IIdProvider
    {
        public List<string> PossibleIds { get; }

        public NoDuplicatesIdProvider(int exponent = 2)
        {
            var maxId = (int)(Math.Pow(10, exponent)) - 1;

            PossibleIds = Enumerable.Range(0, maxId)
                .Select(id => id.ToString())
                .ToList();
        }

        /// <inheritdoc />
        public async Task<string> GetNextIdAsync()
        {
            if (PossibleIds.Count == 0)
            {
                throw new OutOfIdsException();
            }

            var index = new Random().Next(0, PossibleIds.Count);
            var nextId = PossibleIds[index];
            PossibleIds.RemoveAt(index);

            return await Task.FromResult(nextId);
        }
    }
}