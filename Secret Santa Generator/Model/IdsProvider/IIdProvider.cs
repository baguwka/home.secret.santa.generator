using System.Threading.Tasks;

namespace Secret_Santa_Generator.Model.IdsProvider
{
    public interface IIdProvider
    {
        /// <exception cref="OutOfIdsException"></exception>
        Task<string> GetNextIdAsync();
    }
}