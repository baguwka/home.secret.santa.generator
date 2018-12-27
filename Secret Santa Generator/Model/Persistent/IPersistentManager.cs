using System.Threading.Tasks;

namespace Secret_Santa_Generator.Model.Persistent
{
    public interface IPersistentManager
    {
        Task ResetAsync();
        Task<PersistentModel> ReadAsync();
        Task WriteAsync(PersistentModel model);
    }
}