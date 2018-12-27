using System;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace Secret_Santa_Generator.Model.Persistent
{
    public class ErrorHandlerPersistentManagerDecorator : IPersistentManager
    {
        private readonly IPersistentManager _Decorated;

        public ErrorHandlerPersistentManagerDecorator([NotNull] IPersistentManager decorated)
        {
            _Decorated = decorated ?? throw new ArgumentNullException(nameof(decorated));
        }

        public async Task ResetAsync()
        {
            try
            {
                await _Decorated.ResetAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        public async Task<PersistentModel> ReadAsync()
        {
            try
            {
                return await _Decorated.ReadAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        public async Task WriteAsync(PersistentModel model)
        {
            try
            {
                await _Decorated.WriteAsync(model);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }
    }
}