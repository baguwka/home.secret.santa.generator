using System.Collections.Generic;
using Newtonsoft.Json;

namespace Secret_Santa_Generator.Model.Persistent
{
    public class PersistentModel
    {
        [JsonProperty("existed-ids")]
        public List<string> ExistentIds { get; set; } = new List<string>();
    }
}