using System;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace SystemBugsBackend.DTO
{
    public class BugDto
    {
        [JsonProperty("ID")]
        public int Id { get; set; }

        public string System { get; set; }
        public string Summary { get; set; }

        [JsonProperty("Состояние")]
        public string State { get; set; }

        [JsonProperty("Найдено при")]
        public string Found { get; set; }

        [JsonProperty("Критичность")]
        public string CriticalLevel { get; set; }

        [JsonProperty("Тип Дефекта")]
        public string DefectType { get; set; }

        [JsonProperty("Дата создания")]
        public DateTime CreationDate { get; set; }

        [JsonProperty("Дата изменения")]
        public DateTime ChangeDate { get; set; }

        [JsonProperty("Дата закрытия")]
        public DateTime? ClosingDate { get; set; }

        [JsonProperty("Метод обнаружения")]
        public string DetectionMethod { get; set; }

        [JsonProperty("reopens_amount")]
        public int? ReopensAmount { get; set; }


    }
}
