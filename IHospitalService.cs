using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HM_CodingChallenge.Models;
using static HM_CodingChallenge.Data.HospitalServiceImpl;

namespace HM_CodingChallenge.Data
{
    internal interface IHospitalService
    {
        Appointment GetAppointmentById(int id);
        List<Appointment> GetAppointmentsForPatient(int patId);
        List<Appointment> GetAppointmentsForDoctor(int docId);
        int ScheduleAppointment(Appointment appointment);
        int UpdateAppointment(int id, string new_desc);
        int CancelAppointment(int id);
        int PatientExists(int patientId);
        int DoctorExists(int doctorId);
    }
}
