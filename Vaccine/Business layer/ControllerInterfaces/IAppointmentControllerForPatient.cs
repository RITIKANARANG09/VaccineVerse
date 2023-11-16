namespace Project
{
    public interface IAppointmentControllerForPatient
    {
        List<Appointment> ViewAppointment(User user, string vaccineCenter = "");
        bool CancelAppointment(Appointment appointmentObject);
        bool BookAppointment(Appointment appointmentObject);


    }
}