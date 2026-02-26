using AppLogic;
using Microsoft.AspNetCore.Mvc;
using DTO;


namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        private readonly IPatientManager patientManager;

        public PatientsController(IPatientManager ipmanager)
        {
            patientManager = ipmanager;
        }

        [HttpGet("GetPatient")]
        public string getPatient()
        {
            return patientManager.getPatients();

        }
        [HttpGet("GetAllPatients")]
        public List<Patient> getAllPatients()
        {
            return patientManager.getAllPatients();
        }
        [HttpGet("GetByDoctor")]
        public string GetPatientByDoctor(int pIdDoctor)
        {
            return patientManager.GetPatientByDoctor(pIdDoctor);
        }
    }
}
