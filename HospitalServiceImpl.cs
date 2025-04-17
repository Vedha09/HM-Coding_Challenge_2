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
            string insertQuery = "insert into Appointments (patient_id, doctor_id, appointment_date, description) " +
                                 "values (@apid, @adid, @adate, @adescription)";

            try
            {
                using (con = DBUtility.GetConnection())
                {
                    using (command = new SqlCommand(insertQuery, con))
                    {
                        //command.Parameters.AddWithValue("@aid", appointment.AppointmentId);
                        command.Parameters.AddWithValue("@apid", appointment.PatientId);
                        command.Parameters.AddWithValue("@adid", appointment.DoctorId);
                        command.Parameters.AddWithValue("@adate", appointment.AppointmentDate);
                        command.Parameters.AddWithValue("@adescription", appointment.Description);

                        rowsAffected = command.ExecuteNonQuery();
                    }
                }

                if (rowsAffected <= 0)
                {
                    throw new Exception("Appointment schedule could not be added!");
                }
            }
            catch (PatientNumberNotFoundException ex)
            {
                Console.WriteLine("Custom Exception: " + ex.Message);
                throw;
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Exception: " + ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                throw;
            }
            return rowsAffected;
        }

        public int PatientExists(int patientId)
        {
            int exists = 0;
            string query = "select patient_id from Patients where patient_id = @apid";

            try
            {
                using (SqlConnection con = DBUtility.GetConnection())
                using (SqlCommand command = new SqlCommand(query, con))
                {
                    command.Parameters.AddWithValue("@apid", patientId);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            exists = 1;
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Exception: " + ex.Message);
            }
            return exists;
        }

        public int DoctorExists(int doctorId)
        {
            int exists = 0;
            string query = "select doctor_id from Doctors where doctor_id = @adid";

            try
            {
                using (SqlConnection con = DBUtility.GetConnection())
                using (SqlCommand command = new SqlCommand(query, con))
                {
                    command.Parameters.AddWithValue("@adid", doctorId);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            exists = 1;
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Exception: " + ex.Message);
            }
            return exists;
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