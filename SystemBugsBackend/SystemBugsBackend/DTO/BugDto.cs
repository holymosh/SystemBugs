using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SystemBugsBackend.DTO
{
    public class BugDto
    {
        [JsonPropertyName("ID")]
        public int Id { get; set; }

        public string System { get; set; }
        public string Summary { get; set; }

        [JsonPropertyName("Состояние")]
        public string State { get; set; }

    }
}
