using System;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Secret_Santa_Generator.Model.Persistent
{
    public class JsonPersistentManager : IPersistentManager
    {
        private readonly string _DirectoryLocation;
        private readonly string _StorageLocation;
        private readonly object _FileSync = new object();

        public JsonPersistentManager()
        {
            var appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            var folder = Path.Combine(appData, "Secret.Santa.Generator");
            _DirectoryLocation = folder;
            _StorageLocation = Path.Combine(folder, "_GeneratedIds.db");
        }

        public async Task ResetAsync()
        {
            await CheckFileForExistIfNoFileCreateNew();
            await CreateNewStorageFile();
        }

        public async Task<PersistentModel> ReadAsync()
        {
            await CheckFileForExistIfNoFileCreateNew();
            return await InternalReadAsync();
        }

        private async Task<PersistentModel> InternalReadAsync()
        {
            return await Task.Run(() =>
            {
                string serialized;

                lock (_FileSync)
                {
                    serialized = File.ReadAllText(_StorageLocation);
                }

                var model = JsonConvert.DeserializeObject<PersistentModel>(serialized);
                return model;
            });
        }

        public async Task WriteAsync(PersistentModel model)
        {
            await CheckFileForExistIfNoFileCreateNew();
            await InternalWriteAsync(model);
        }

        private async Task InternalWriteAsync(PersistentModel model)
        {
            await Task.Run(() => {
                var serialized = JsonConvert.SerializeObject(model);
                lock (_FileSync)
                {
                    File.WriteAllText(_StorageLocation, serialized);
                }
            });
        }

        private async Task CheckFileForExistIfNoFileCreateNew()
        {
            await Task.Run(() =>
            {
                if (!File.Exists(_StorageLocation))
                {
                    if (!Directory.Exists(_DirectoryLocation))
                    {
                        Directory.CreateDirectory(_DirectoryLocation);
                    }

                    var model = new PersistentModel();
                    var serialized = JsonConvert.SerializeObject(model);
                    lock (_FileSync)
                    {
                        File.WriteAllText(_StorageLocation, serialized);
                    }
                }
            });
        }
        private async Task CreateNewStorageFile()
        {
            await Task.Run(() =>
            {
                var model = new PersistentModel();
                var serialized = JsonConvert.SerializeObject(model);
                lock (_FileSync)
                {
                    File.WriteAllText(_StorageLocation, serialized);
                }
            });
        }
    }
}