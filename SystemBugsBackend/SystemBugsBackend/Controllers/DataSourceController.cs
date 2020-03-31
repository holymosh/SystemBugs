using System.Linq;
using DbAccess;
using Domain.Entities;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace SystemBugsBackend.Controllers
{
    [Route("datasource")]
    public class DataSourceController : ControllerBase
    {
        private readonly IRepository<SberSystem> _systemRepository;
        private readonly IRepository<CriticalLevel> _levelRepository;

        public DataSourceController(IRepository<CriticalLevel> levelRepository, IRepository<SberSystem> systemRepository)
        {
            _levelRepository = levelRepository;
            _systemRepository = systemRepository;
        }

        [HttpGet]
        [Route("system")]
        public IActionResult GetSystemDataSource()
        {
            var result = _systemRepository.GetAll().Select(s => new {s.Id,s.Title});
            return Ok(result);
        }

        [HttpGet]
        [Route("level")]
        public IActionResult GetLevelDataSource()
        {
            var result = _levelRepository.GetAll().Select(l => new {l.Id, l.Title});
            return Ok(result);
        }
    }
}
