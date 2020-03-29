using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using SystemBugsBackend.DTO;
using Newtonsoft.Json;

namespace SystemBugsBackend.Service
{
    public class BugsForTestReader
    {
        public void Read()
        {
            using (var streamReader = new StreamReader("bugs_for_test.json"))
            {
                var json = streamReader.ReadToEnd();
                var items = JsonConvert.DeserializeObject<List<BugDto>>(json).OrderBy(dto => dto.Id).ToList();
                
            }

        }
    }
}
