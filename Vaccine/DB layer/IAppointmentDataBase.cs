namespace Project
{
    public interface IAppointmentDataBase
    {
        bool AddAppointment(Appointment appointment);
        bool DeleteItem(Appointment appointment);
    }
}