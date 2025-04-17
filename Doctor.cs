using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HM_CodingChallenge
{
    internal class Doctor
    {
        public int DoctorId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Specialization { get; set; }
        public string ContactNumber { get; set; }

        public Doctor() { }

        public Doctor(int doctorId, string firstName, string lastName, string specialization, string contact)
        {
            DoctorId = doctorId;
            FirstName = firstName;
            LastName = lastName;
            Specialization = specialization;
            ContactNumber = contact;
        }

        public override string ToString()
        {
           return $"Doctor[{DoctorId}, {FirstName} {LastName}, {Specialization}, Contact: {ContactNumber}]";
        }
    }
}
