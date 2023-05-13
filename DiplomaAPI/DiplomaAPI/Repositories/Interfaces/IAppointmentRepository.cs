using DiplomaAPI.Models;
using DiplomaAPI.ViewModels;
using DiplomaAPI.ViewModels.AmbulatoryEpisode.Appointment;

namespace DiplomaAPI.Repositories.Interfaces
{
    public interface IAppointmentRepository
    {
        public Appointment GetAppointment(int id);

        public List<Appointment> GetAllAppointmentsFromAmbulatory(int episodeId);

        Task<UserManagerResponse> CreateAppointmentInAmbulatory(CreateAppointmentViewModel model);

        public AppointmentViewModel UpdateInAmbulatory(int episodeId, UpdateAppointmentViewModel model);

        public AppointmentViewModel DeleteInAmbulatory(int episodeId, int appointmentId);

        public List<Appointment> GetAllAppointmentsFromInpatient(int episodeId);

        Task<UserManagerResponse> CreateAppointmentInInpatient(CreateAppointmentViewModel model);

        public AppointmentViewModel UpdateInInpatient(int episodeId, UpdateAppointmentViewModel model);

        public AppointmentViewModel DeleteInInpatient(int episodeId, int appointmentId);
    }
}
