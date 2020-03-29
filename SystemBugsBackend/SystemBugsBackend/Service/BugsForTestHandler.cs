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
            CreateTitledEntities<DefectType>(items.Select(d => d.DefectType));
            CreateTitledEntities<DetectionMethod>(items.Select(d => d.DetectionMethod));
            CreateTitledEntities<State>(items.Select(d => d.State));

            CreateBugs(items);

        }

        private void CreateBugs(List<BugDto> items)
        {
            var skip = 0;
            var take = 100;
            var systems = _serviceProvider.GetService<IRepository<SberSystem>>().GetAll();
            var levels = _serviceProvider.GetService<IRepository<CriticalLevel>>().GetAll();
            var defectTypes = _serviceProvider.GetService<IRepository<DefectType>>().GetAll();
            var methods = _serviceProvider.GetService<IRepository<DetectionMethod>>().GetAll();
            var states = _serviceProvider.GetService<IRepository<State>>().GetAll();
            var bugRepository = _serviceProvider.GetService<IRepository<Bug>>();

            while (skip < items.Count)
            {
                var range = items.Skip(skip).Take(take);
                var bugs = range.Select(dto =>
                {
                    var system = systems.SingleOrDefault(s => s.Title == dto.System);
                    var level = levels.SingleOrDefault(s => s.Title == dto.CriticalLevel);
                    var state = states.SingleOrDefault(s => s.Title == dto.State);
                    var method = methods.SingleOrDefault(m => m.Title == dto.DetectionMethod);
                    var defectType = defectTypes.SingleOrDefault(df => df.Title == dto.DefectType);

                    return new Bug
                    {
                        SystemId = system.Id,
                        CriticalLevelId = level.Id,
                        StateId = state.Id,
                        DetectionMethodId = method.Id,
                        DefectTypeId = defectType.Id,
                        ChangeDate = dto.ChangeDate,
                        ClosingDate = dto.ClosingDate,
                        CreationDate = dto.CreationDate,
                        Found = dto.Found,
                        ReopensAmount = dto.ReopensAmount,
                        Summary = dto.Summary,
                    };
                });
                bugRepository.CreateRange(bugs);
                skip += take;
            }
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
