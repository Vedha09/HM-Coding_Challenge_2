using HM_CodingChallenge.Data;
using HM_CodingChallenge.Models;
using HM_CodingChallenge;

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
                Console.WriteLine("3. Exit");
                int choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        Appointment appointment = new Appointment(3, 103, 203, DateTime.Now, "Normal checkup");
                        int result = service.ScheduleAppointment(appointment);
                        Console.WriteLine(result > 0? "Scheduled successfully." : "Scheduling failed.");
                        break;

                    case 2:
                        Appointment a = service.GetAppointmentById(3);
                        Console.WriteLine(a != null ? a.ToString() : "Not found.");
                        break;

                    default:
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