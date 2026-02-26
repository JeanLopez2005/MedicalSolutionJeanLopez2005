using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]/[action]")] //Agregando action hacemos que los metodos se llamen por su nombre, osea: GetByDoctor se llamara por ese nombre y no por el nombre del metodo http, esto es para evitar confusiones al llamar a los metodos
    [ApiController]
    public class DoctorsController : ControllerBase
    {
        [HttpGet] 
        public string getDoctor()
        {
            return "Datos del doctor";
        }
        [HttpGet] 
        public string getAllPatients()
        {
            return "Datos de todos los doctores";
        }
        [HttpGet] 
        public string GetDoctorById(int pIdDoctor)
        {
            return "Datos del doctor by id " + pIdDoctor;
        }
    }
}
