using AppLogic;
using DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Rhcontroller : ControllerBase
    {
        private readonly IRHConnector _rHConnector;

        public Rhcontroller(IRHConnector rHConnector)
        {
            _rHConnector = rHConnector;
        }


        [HttpGet("GetAllEmployees")]
        public async Task<List<Employee>> GetAllEmployees()
        {
            return await _rHConnector.RetrieveAllEmployees();

        }


        [HttpGet("GetAllSpecialties")]
        public async Task<List<string>> GetAllSpecialties()
        {
            return await _rHConnector.RetrieveAllSpecialties();
        }

    }

}

