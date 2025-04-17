using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HM_CodingChallenge
{
    internal class Appointment
    {
        public int AppointmentId { get; set; }
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string Description { get; set; }
        public SqlDbType AppointmentID { get; internal set; }

        public Appointment() { }

        public Appointment(int id, int patientId, int doctorId, DateTime date, string desc)
        {
            AppointmentId = id;
            PatientId = patientId;
            DoctorId = doctorId;
            AppointmentDate = date;
            Description = desc;
        }

        public override string ToString()
        {
            return $"Appointment[{AppointmentId}, Patient: {PatientId}, Doctor: {DoctorId}, Date: {AppointmentDate}, Description: {Description}]";
        }
    }
}