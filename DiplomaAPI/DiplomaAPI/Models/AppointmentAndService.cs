namespace DiplomaAPI.Models
{
    public class AppointmentAndService
    {
        public int AppointmentId { get; set; }
        
        public Models.Appointment Appointment { get; set; }

        public string ServiceId { get; set; }

        public Models.Service Service { get; set; }
    }
}
