using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HM_CodingChallenge
{
    internal class Patient
    {
        public int PatientId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string ContactNumber { get; set; }
        public string Address { get; set; }

        public Patient() { }

        public Patient(int patientId, string firstName, string lastName, DateTime dob, string gender, string contact, string address)
        {
            PatientId = patientId;
            FirstName = firstName;
            LastName = lastName;
            DateOfBirth = dob;
            Gender = gender;
            ContactNumber = contact;
            Address = address;
        }

        public override string ToString()
        {
            return $"Patient[{PatientId}, {FirstName} {LastName}, DOB: {DateOfBirth.ToShortDateString()}, Gender: {Gender}, Contact: {ContactNumber}, Address: {Address}]";
        }
    }
}
