using HM_CodingChallenge.Data;
using HM_CodingChallenge.Models;
using HM_CodingChallenge;
using System.Data.SqlClient;

namespace HM_CodingChallenge
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                IHospitalService service = new HospitalServiceImpl();
                Console.WriteLine("1. Schedule Appointment");
                Console.WriteLine("2. Get Appointment by ID");
                Console.WriteLine("3. Update Appointment");
                Console.WriteLine("4. Cancel Appointment");
                Console.WriteLine("5. Exit");
                int choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        Console.WriteLine("Enter Patient ID: ");
                        int patientId = int.Parse(Console.ReadLine());

                        Console.WriteLine("Enter Doctor ID: ");
                        int doctorId = int.Parse(Console.ReadLine());

                        Console.WriteLine("Enter Description: ");
                        string description = Console.ReadLine();

                        Appointment appointment = new Appointment(1, 101, 201, DateTime.Now, "Routine Checkup");
                        int result = service.ScheduleAppointment(appointment);
                        Console.WriteLine(result > 0 ? "Scheduled successfully." : "Scheduling failed.");
                        break;

                    case 2:
                        Console.WriteLine("Enter Appointment ID: ");
                        int appointmentId = int.Parse(Console.ReadLine());

                        Appointment a = service.GetAppointmentById(appointmentId);
                        Console.WriteLine(a != null ? a.ToString() : "Appointment not found.");
                        break;

                    case 3:
                        Console.WriteLine("Enter Appointment ID to update:");
                        int updateAppointmentId = int.Parse(Console.ReadLine());

                        Console.WriteLine("Enter new Description:");
                        string newDescription = Console.ReadLine();

                        int updateResult = service.UpdateAppointment(updateAppointmentId, newDescription);

                        Console.WriteLine(updateResult > 0 ? "Appointment updated successfully." : "Update failed.");
                        break;

                    case 4:
                        Console.WriteLine("Enter Appointment ID to cancel:");
                        int cancelAppointmentId = int.Parse(Console.ReadLine());

                        int cancelResult = service.CancelAppointment(cancelAppointmentId);
                        Console.WriteLine(cancelResult > 0? "Appointment cancelled successfully." : "Cancelation failed.");
                        break;

                    default:
                        Console.WriteLine("Invalid option!!");
                        break;
                }
            }

            catch (PatientNumberNotFoundException ex)
            {
                Console.WriteLine("Custom Exception: " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("System Exception: " + ex.Message);
            }
        }
    }
}
