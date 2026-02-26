using System.Security.Cryptography.X509Certificates;
using System;

namespace DTO
{
    public class Patient
    {
        public string socialSecurityId { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public DateTime BirthDay { get; set; }

    }

}

