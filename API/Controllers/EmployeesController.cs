using AppLogic;
using DTO;
using Microsoft.AspNetCore.Mvc;


namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IRHConnector _rHConnector;

        public EmployeeController(IRHConnector rHConnector)
        {
            _rHConnector = rHConnector;
        }

        #region Gets de Employees 

        [HttpGet("GetAllEmployees")]
        public async Task<List<Employee>> GetAllEmployees()
        {
            return await _rHConnector.RetrieveAllEmployees();
        }

        [HttpGet("GetAllEmployeesRest")]
        public async Task<List<Employee>> GetAllEmployeesRestSharp()
        {
            return await _rHConnector.RetrieveAllEmployeesRest();
        }

        [HttpGet("GetAllEmployeesFlurl")]
        public async Task<List<Employee>> GetAllEmployeesFlurl()
        {
            return await _rHConnector.RetrieveAllEmployeesFlurl();
        }
        #endregion


        #region Metodos Employees

        [HttpGet("GetEmployeeManager")]
        public async Task<Employee> GetEmployeeManager(int id)
        {
            var employees = await _rHConnector.RetrieveAllEmployees();
            var employee = employees.FirstOrDefault(e => e.Id == id);

            if (employee == null || employee.ManagerId == null)
            {
                return null;
            }

            return employees.FirstOrDefault(e => e.Id == employee.ManagerId.Value);
        }

        [HttpGet("GetOldestEmployee")]
        public async Task<List<Employee>> GetOldestEmployee()
        {
            var employees = await _rHConnector.RetrieveAllEmployees();
            var valid = employees
                .Where(e => TryGetHiringDate(e, out _))
                .ToList();

            if (valid.Count == 0)
            {
                return new List<Employee>();
            }

            DateTime minDate = valid
                .Select(e => GetHiringDateOrMin(e))
                .Min();

            return valid
                .Where(e => GetHiringDateOrMin(e) == minDate)
                .ToList();
        }

        [HttpGet("GetNewestEmployee")]
        public async Task<List<Employee>> GetNewestEmployee()
        {
            var employees = await _rHConnector.RetrieveAllEmployees();
            var valid = employees
                .Where(e => TryGetHiringDate(e, out _))
                .ToList();

            if (valid.Count == 0)
            {
                return new List<Employee>();
            }

            DateTime maxDate = valid
                .Select(e => GetHiringDateOrMin(e))
                .Max();

            return valid
                .Where(e => GetHiringDateOrMin(e) == maxDate)
                .ToList();
        }

        [HttpGet("GetEmployeeByID")]
        public async Task<Employee> GetEmployeeById(int id)
        {
            var employees = await _rHConnector.RetrieveAllEmployees();
            return employees.FirstOrDefault(e => e.Id == id);
        }



        [HttpGet("GetEmployeesWithMoreThan")]
        public async Task<List<Employee>> GetEmployeesWithMoreThan(int years)
        {
            var employees = await _rHConnector.RetrieveAllEmployees();
            return employees
                .Where(e => GetYearsCompany(e) >= years).ToList();
        }

        [HttpGet("GetEmployeesWithLessThan")]
        public async Task<List<Employee>> GetEmployeesWithLessThan(int years)
        {
            var employees = await _rHConnector.RetrieveAllEmployees();
            return employees
                .Where(e => GetYearsCompany(e) <= years)
                .ToList();
        }

        #endregion

        #region Helpers

        private int GetYearsCompany(Employee e)
        {

            if (e == null)
            {
                return -1;
            }

            if (e.HiringDate == null || e.HiringDate.Trim() == "")
            {
                return -1;
            }

            DateTime hiring = DateTime.MinValue;
            bool vDate = DateTime.TryParse(e.HiringDate, out hiring);

            if (vDate == false)
            {
                return -1;
            }

            DateTime today = DateTime.Today;

            int years = today.Year - hiring.Year;

            if (today.Month < hiring.Month)
            {
                years = years - 1;
            }
            else if (today.Month == hiring.Month && today.Day < hiring.Day)
            {
                years = years - 1;
            }

            return years;
        }

        private bool TryGetHiringDate(Employee e, out DateTime hiringDate)
        {
            hiringDate = DateTime.MinValue;

            if (e == null)
            {
                return false;
            }

            if (e.HiringDate == null)
            {
                return false;
            }

            if (e.HiringDate.Trim() == "")
            {
                return false;
            }

            DateTime temp = DateTime.MinValue;
            bool vDate = DateTime.TryParse(e.HiringDate, out temp);

            if (vDate == false)
            {
                return false;
            }

            hiringDate = temp;
            return true;
        }

        private DateTime GetHiringDateOrMin(Employee e)
        {
            DateTime d = DateTime.MinValue;

            bool vDate = TryGetHiringDate(e, out d);

            if (vDate == true)
            {
                return d.Date;
            }

            return DateTime.MinValue.Date;
        }
    }
}

        #endregion