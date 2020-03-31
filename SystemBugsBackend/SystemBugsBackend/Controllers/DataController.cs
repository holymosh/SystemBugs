using System;
using System.Linq;
using System.Threading.Tasks;
using DbAccess;
using Domain.Entities;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace SystemBugsBackend.Controllers
{
    [Route("data")]
    public class DataController : ControllerBase
    {
        private readonly IRepository<Bug> _repository;

        public DataController(IRepository<Bug> repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [Route("get")]
        public async Task<IActionResult> GetData([FromQuery] int? systemId, [FromQuery] int? levelId,
            [FromQuery] DateTime? from ,[FromQuery] DateTime? to)
        {

            var query = _repository.GetAll();
            if (systemId.HasValue) query = query.Where(b => b.SystemId == systemId);

            if (levelId.HasValue) query = query.Where(b => b.CriticalLevelId == levelId);

            if (from.HasValue && to.HasValue && from < to)
                query = query.Where(b => from <= b.CreationDate && b.CreationDate <= to);

            var result = await query.Include(b => b.System)
                .Include(b => b.CriticalLevel)
                .Include(b => b.DefectType)
                .Include(b => b.State)
                .Select(b => new
                {
                    b.Id,
                    System = b.System.Title,
                    Method = b.DetectionMethod.Title,
                    State = b.State.Title,
                    DefectType = b.DefectType.Title,
                    Level = b.CriticalLevel.Title,
                    b.Summary,
                    b.ChangeDate,
                    b.CreationDate,
                    b.ClosingDate,
                    b.ReopensAmount,
                    b.Found,
                }).ToArrayAsync();

            return Ok(result);
        }
    }
}
