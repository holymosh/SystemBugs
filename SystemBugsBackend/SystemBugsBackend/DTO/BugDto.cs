using System;
using System.Text.Json.Serialization;

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
        [JsonPropertyName("Найдено при")]
        public string Found { get; set; }

        [JsonPropertyName("Критичность")]
        public string CriticalLevel { get; set; }

        [JsonPropertyName("Тип Дефекта")]
        public string DegectType { get; set; }

        [JsonPropertyName("Дата создания")]
        public DateTime CreationDate { get; set; }

        [JsonPropertyName("Дата изменения")]
        public DateTime ChangeDate { get; set; }

        [JsonPropertyName("Дата закрытия")]
        public DateTime? ClosingDate { get; set; }

        [JsonPropertyName("Метод обнаружения")]
        public string DetectionMethod { get; set; }

        [JsonPropertyName("reopens_amount")]
        public int? ReopensAmount { get; set; }


    }
}
