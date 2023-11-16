

namespace Project
{
    public interface IAppointmentControllerForAdmin
    {
        List<Appointment> ViewAppointment(User user, string vaccineCenter = "");
    }
}
