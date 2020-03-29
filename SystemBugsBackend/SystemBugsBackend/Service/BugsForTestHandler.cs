using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using SystemBugsBackend.DTO;
using DbAccess;
using Domain;
using Domain.Entities;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace SystemBugsBackend.Service
{
    public class BugsForTestHandler : IBugsForTestHandler
    {
        private readonly IServiceProvider _serviceProvider;

        public BugsForTestHandler(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void Handle()
        {
            List<BugDto> items;
            using (var streamReader = new StreamReader("bugs_for_test.json"))
            {
                var json = streamReader.ReadToEnd();
                items = JsonConvert.DeserializeObject<List<BugDto>>(json).OrderBy(dto => dto.Id).ToList();
            }

            CreateTitledEntities<SberSystem>(items.Select(d => d.System));
            CreateTitledEntities<CriticalLevel>(items.Select(d => d.CriticalLevel));
            CreateTitledEntities<DefectType>(items.Select(d => d.DegectType));
            CreateTitledEntities<DetectionMethod>(items.Select(d => d.DetectionMethod));
            CreateTitledEntities<State>(items.Select(d => d.State));
            
        }

        private void CreateTitledEntities<TEntity>(IEnumerable<string> titles) where TEntity:BaseEntityWithTitle, new()
        {
            var entities = titles.Distinct().Select(s => new TEntity
            {
                Title = s
            });
            var repository = _serviceProvider.GetService<IRepository<TEntity>>();
            repository.CreateRange(entities);
        }

    }

}
