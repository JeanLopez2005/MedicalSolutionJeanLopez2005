using DTO;

namespace AppLogic
{
    public interface IPatientManager
    {
        public List<Patient> getAllPatients();
        string GetPatientByDoctor(int pIdDoctor);
        string getPatients();

    }
    public class PatientManager : IPatientManager
    {
        public List<Patient> getAllPatients()
        {
            var patients = new List<Patient>();
            patients.Add(new Patient() { Name = "Limberth" });
            patients.Add(new Patient() { Name = "jEAN" });
            patients.Add(new Patient() { Name = "sABANA" });

            return patients;
        }

        public string GetPatientByDoctor(int pIdDoctor)
        {
            return "Datos del paciente by doctor " + pIdDoctor;
        }

        public string getPatients()
        {
            return "Datos del paciente";
        }
    }
}
