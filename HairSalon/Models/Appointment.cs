namespace HairSalon.Models
{
    public class Appointment
    {
        public int AppointmentId { get; set; }
        public int ClientId { get; set; }

        public DateTime? Date { get; set; }
        public string Description { get; set; }

        public Client Client { get; set; }
    }
}