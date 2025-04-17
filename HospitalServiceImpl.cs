using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Threading.Tasks;
using HM_CodingChallenge.Models;
using static HM_CodingChallenge.Data.IHospitalService;

namespace HM_CodingChallenge.Data
{
    internal class HospitalServiceImpl : IHospitalService
    {
        SqlConnection con = null;
        SqlCommand command = null;

        public int ScheduleAppointment(Appointment appointment)
        {
            int rowsAffected = 0;
            string query = $"insert into Appointments (appointment_id, patient_id, doctor_id, appointment_date, description) values (@aid, @apid, @adid, @adate, @adescription)";
            try
            {
                using (con = DBUtility.GetConnection())
                {
                    command = new SqlCommand(query, con);
                    command.Parameters.Add(new SqlParameter("@aid", appointment.AppointmentId));
                    command.Parameters.Add(new SqlParameter("@apid", appointment.PatientId));
                    command.Parameters.Add(new SqlParameter("@adid", appointment.DoctorId));
                    command.Parameters.Add(new SqlParameter("@adate", appointment.AppointmentDate));
                    command.Parameters.Add(new SqlParameter("@adescription", appointment.Description));
                    rowsAffected = command.ExecuteNonQuery();
                }
                if (rowsAffected <= 0)
                {
                    throw new PatientNumberNotFoundException("Appointment schedule could not be added!!");
                }
            }
            catch (SqlException e)
            {
                throw e;
            }
            catch (Exception e)
            {
                throw new Exception("Error in adding a new appointment schedule");
            }
            return rowsAffected;
        }

        public int CancelAppointment(int id)
        {
            int rowsAffected = 0;
            string query = "delete from Appointments where appointment_id = @aid";
            try
            {
                using (con = DBUtility.GetConnection())
                {
                    command = new SqlCommand(query, con);
                    command.Parameters.Add(new SqlParameter("@aid", id));
                    rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected <= 0)
                    {
                        throw new PatientNumberNotFoundException("Id not found, Couldn't cancel apppointment!!");
                    }
                }
            }
            catch (SqlException e)
            {
                throw e;
            }
            catch (Exception e)
            {
                throw new Exception("Error in cancelling a new appointment");
            }
            return rowsAffected;
        }

        public Appointment GetAppointmentById(int id)
        {
            Appointment appointment = null;
            SqlConnection con = null;
            SqlCommand command = null;
            string query = "select * from Appointments where appointment_id = @aid";

            using (con = DBUtility.GetConnection())
            {
                try
                {
                    command = new SqlCommand(query, con);

                    command.Parameters.AddWithValue("@aid", id);
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        appointment = new Appointment();
                        appointment.AppointmentId = reader.GetInt32(0);
                        appointment.PatientId = reader.GetInt32(1);
                        appointment.DoctorId = reader.GetInt32(2);
                        appointment.AppointmentDate = reader.GetDateTime(3);
                        appointment.Description = reader.GetString(4);
                    }

                    if (appointment == null)
                    {
                        throw new PatientNumberNotFoundException("Appointment Id couldn't be found!!");
                    }
                }
                catch (SqlException e)
                {
                    throw e;
                }
                catch (Exception e)
                {
                    throw new Exception("Error fetching in appointment with the given appointment id: " + e.Message);
                }
            }
            return appointment;
        }

        public List<Appointment> GetAppointmentsForDoctor(int docId)
        {
            List<Appointment> appointments = new List<Appointment>();
            Appointment appointment = null;
            SqlConnection con = null;
            SqlCommand command = null;
            string query = "select * from Appointments where doctor_id = @adid";
            
            using (con = DBUtility.GetConnection())
            {
                try
                {
                    command = new SqlCommand(query, con);
                    
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        appointment = new Appointment();
                        appointment.AppointmentId = reader.GetInt32(0);
                        appointment.PatientId = reader.GetInt32(1);
                        appointment.DoctorId = reader.GetInt32(2);
                        appointment.AppointmentDate = reader.GetDateTime(3);
                        appointment.Description = reader.GetString(4);
                        appointments.Add(appointment);
                    }
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
            return appointments;
        }

        public List<Appointment> GetAppointmentsForPatient(int patId)
        {
            List<Appointment> appointments = new List<Appointment>();
            Appointment appointment = null;
            SqlConnection con = null;
            SqlCommand command = null;
            string query = "select * from Appointments where patient_id = @apid";

            using (con = DBUtility.GetConnection())
            {
                try
                {
                    command = new SqlCommand(query, con);

                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        appointment = new Appointment();
                        appointment.AppointmentId = reader.GetInt32(0);
                        appointment.PatientId = reader.GetInt32(1);
                        appointment.DoctorId = reader.GetInt32(2);
                        appointment.AppointmentDate = reader.GetDateTime(3);
                        appointment.Description = reader.GetString(4);
                        appointments.Add(appointment);
                    }
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
            return appointments;
        }

        public int UpdateAppointment(int id, string new_desc)
        {
            int rowsAffected = 0;
            Appointment ap = GetAppointmentById(id);
            if (ap == null)
            {
                throw new PatientNumberNotFoundException($"Appointment not found for the given appointment {id}");
            }
            else
            {
                string query = "update Appointments set description = @adescription where appointment_id = @aid";
                try
                {
                    using (con = DBUtility.GetConnection())
                    {
                        command = new SqlCommand(query, con);
                        command.Parameters.AddWithValue("@aid", id);
                        command.Parameters.AddWithValue("@adescription", new_desc);
                        rowsAffected = command.ExecuteNonQuery();
                    }
                }
                catch (SqlException e)
                {
                    throw e;
                }
                return rowsAffected;
            }
        }
    }
}
