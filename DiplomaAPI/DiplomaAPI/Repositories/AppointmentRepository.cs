using DiplomaAPI.Data;
using DiplomaAPI.Models;
using DiplomaAPI.Repositories.Interfaces;
using DiplomaAPI.ViewModels.AmbulatoryEpisode.Appointment;
using SendGrid.Helpers.Errors.Model;
using System.Linq;

namespace DiplomaAPI.Repositories
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private DataContext _data;
        public AppointmentRepository(DataContext data)
        {
            _data = data;
        }

        public Appointment GetAppointment(int id) 
        {
            var appointment = _data.Appointments.Find(id);

            _data.Entry(appointment).Reference("Referral").Load();
            _data.Entry(appointment).Collection("AppointmentsAndServices").Load();
            _data.Entry(appointment).Collection("AppointmentsAndDiagnosesICPC2").Load();
            foreach (var diagnoses in appointment.AppointmentsAndDiagnosesICPC2)
            {
                _data.Entry(diagnoses).Reference("Appointment").Load();
                _data.Entry(diagnoses).Reference("DiagnosisICPC2").Load();
                //_data.Entry(diagnoses.DiagnosisICPC2.Category).Reference("Category").Load();
            }
            foreach (var entry in appointment.AppointmentsAndServices)
            {
                _data.Entry(entry).Reference("Appointment").Load();
                _data.Entry(entry).Reference("Service").Load();
            }

            return appointment;
        }

        public List<Appointment> GetAllAppointments(int episodeId)
        {
            var episode = _data.AmbulatoryEpisodes.Find(episodeId);
            _data.Entry(episode).Collection("Appointments").Load();

            var appointments = episode.Appointments.ToList();

            appointments.ForEach(a =>
            {
                _data.Entry(a).Collection("AppointmentsAndDiagnosesICPC2").Load();
                foreach (var diagnoses in a.AppointmentsAndDiagnosesICPC2)
                {
                    _data.Entry(diagnoses).Reference("Appointment").Load();
                    _data.Entry(diagnoses).Reference("DiagnosisICPC2").Load();
                    //_data.Entry(diagnoses.DiagnosisICPC2.Category).Reference("Category").Load();
                }

                _data.Entry(a).Collection("AppointmentsAndServices").Load();
                foreach (var entry in a.AppointmentsAndServices)
                {
                    _data.Entry(entry).Reference("Appointment").Load();
                    _data.Entry(entry).Reference("Service").Load();
                }
            });

            return appointments;
        }

        public AppointmentViewModel CreateAppointment(CreateAppointmentViewModel model)
        {
            var referrals = _data.Referrals.Where(x => x.ReferralPackageId == model.ReferralId);

            int referralId = 0;

            if (referrals != null)
            {
                var referral = new List<Referral>();

                for (int i = 0; i < model.Services.Length; i++)
                {
                    var service = _data.Services.Find(model.Services[i].ToString());

                    referral = referrals.Where(x => x.Service.ServiceId == model.Services[i]).ToList();

                }

                referral.ForEach(x =>
                {
                    referralId = x.ReferralId;
                });

                var updateReferral = _data.Referrals.Find(referralId);

                updateReferral.ProcessStatus = "Погашене " + "(від " + DateTime.Now.Date + ")";

                _data.Referrals.Update(updateReferral);
            }

            var appointment = new Appointment
            {
                Date = DateTime.Now,
                Referral = _data.Referrals.Find(referralId),
                AppealReasonComment = model.AppealReasonComment != "" ? model.AppealReasonComment : null,
                InteractionClass = model.InteractionClass,
                Visiting = model.Visiting,
                InteractionType = model.InteractionType,
                ServiceComment = model.ServiceComment != "" ? model.ServiceComment : null,
                Priority = model.Priority,
                Treatment = model.Treatment != "" ? model.Treatment : null,
                Notes = model.Notes != "" ? model.Notes : null,
            };

            var episode = _data.AmbulatoryEpisodes.Find(model.AmbulatoryEpisodeId);
            _data.Entry(episode).Collection("Appointments").Load();
            episode.Appointments.Add(appointment);

            if (model.Visiting == "Завершення епізоду")
            {
                episode.Status = "Завершений";
            }

            for (int i = 0; i < model.Services.Length; i++)
            {
                var service = _data.Services.Find(model.Services[i].ToString());

                var appointmentAndService = new AppointmentAndService
                {
                    AppointmentId = appointment.AppointmentId,
                    Appointment = appointment,
                    ServiceId = service.ServiceId,
                    Service = service
                };

                _data.AppointmentsAndServices.Add(appointmentAndService);
                appointment.AppointmentsAndServices.Add(appointmentAndService);

            }

            for (int i = 0; i < model.DiagnosesICPC2.Length; i++)
            {
                var diagnosis = _data.DiagnosesICPC2.Find(model.DiagnosesICPC2[i]);

                var appointmentAndDiagnosisICPC2 = new AppointmentAndDiagnosisICPC2
                {
                    AppointmentId = appointment.AppointmentId,
                    Appointment = appointment,
                    DiagnosisId = diagnosis.DiagnosisId,
                    DiagnosisICPC2 = diagnosis
                };

                _data.AppointmentsAndDiagnosesICPC2.Add(appointmentAndDiagnosisICPC2);
                appointment.AppointmentsAndDiagnosesICPC2.Add(appointmentAndDiagnosisICPC2);

            }

            _data.Appointments.Add(appointment);
            _data.AmbulatoryEpisodes.Update(episode);

            _data.SaveChanges();

            return PrepareResponse(appointment);
        }

        public AppointmentViewModel Update(UpdateAppointmentViewModel model)
        {
            var appointment = _data.Appointments.Find(model.AppointmentId);

            if (appointment == null)
            {
                throw new NotFoundException();
            }

            _data.Entry(appointment).Collection("AppointmentsAndServices").Load();
            _data.Entry(appointment).Collection("AppointmentsAndDiagnosesICPC2").Load();
            _data.Entry(appointment).Reference("Referral").Load();
            foreach (var diagnoses in appointment.AppointmentsAndDiagnosesICPC2)
            {
                _data.Entry(diagnoses).Reference("Appointment").Load();
                _data.Entry(diagnoses).Reference("DiagnosisICPC2").Load();
                //_data.Entry(diagnoses.DiagnosisICPC2.Category).Reference("Category").Load();
            }
            foreach (var entry in appointment.AppointmentsAndServices)
            {
                _data.Entry(entry).Reference("Appointment").Load();
                _data.Entry(entry).Reference("Service").Load();
            }

            if (model.ReferralId != "" && appointment.Referral.ReferralPackageId != model.ReferralId)
            {
                var referrals = _data.Referrals.Where(x => x.ReferralPackageId == model.ReferralId);

                int referralId = 0;

                var referral = new List<Referral>();

                for (int i = 0; i < model.Services.Length; i++)
                {
                    var service = _data.Services.Find(model.Services[i].ToString());

                    referral = referrals.Where(x => x.Service.ServiceId == model.Services[i]).ToList();

                }

                referral.ForEach(x =>
                {
                    referralId = x.ReferralId;
                });

                var updateReferral = _data.Referrals.Find(referralId);

                updateReferral.ProcessStatus = "Погашене " + "(від " + DateTime.Now + ")";

                _data.Referrals.Update(updateReferral);
            }

            if(model.DiagnosesICPC2.Length != 0)
            {
                for (int i = 0; i < model.DiagnosesICPC2.Length; i++)
                {
                    var diagnosis = _data.DiagnosesICPC2.Find(model.DiagnosesICPC2[i]);

                    var appointmentAndDiagnosisICPC2 = _data.AppointmentsAndDiagnosesICPC2.Where(x => x.DiagnosisId == model.DiagnosesICPC2[i]);

                }
            }

            return PrepareResponse(appointment);
        }

        private AppointmentViewModel PrepareResponse(Appointment appointment)
        {
            return new AppointmentViewModel
            {
                AppointmentId = appointment.AppointmentId,
                Date = appointment.Date,
                AppealReasonComment = appointment.AppealReasonComment,
                InteractionClass = appointment.InteractionClass,
                Visiting = appointment.Visiting,
                InteractionType = appointment.InteractionType,
                ServiceComment = appointment.ServiceComment,
                Priority = appointment.Priority,
                Treatment = appointment.Treatment,
                Notes = appointment.Notes
            };
        }
    }
}
