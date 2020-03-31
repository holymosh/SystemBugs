using System;
using System.Linq;
using DbAccess;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace SystemBugsBackend.Controllers
{
    [Route("Data")]
    public class DataController : ControllerBase
    {
        private readonly IRepository<Bug> _repository;

        public DataController(IRepository<Bug> repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [Route("get")]
        public IActionResult GetData([FromQuery] int? systemId, [FromQuery] int? levelId,
            [FromQuery] DateTime? from ,[FromQuery] DateTime? to)
        {

            var query = _repository.GetAll();
            if (systemId.HasValue) query = query.Where(b => b.SystemId == systemId);

            if (levelId.HasValue) query = query.Where(b => b.CriticalLevelId == levelId);

            if (from.HasValue && to.HasValue && from < to)
                query = query.Where(b => from <= b.CreationDate && b.CreationDate <= to);

            return Ok(query);
        }
    }
}
