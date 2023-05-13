using DiplomaAPI.Data;
using DiplomaAPI.Models;
using DiplomaAPI.Repositories.Interfaces;
using DiplomaAPI.ViewModels;
using DiplomaAPI.ViewModels.AmbulatoryEpisode.Appointment;
using Microsoft.AspNet.Identity;
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
            _data.Entry(appointment).Reference("Doctor").Load();
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

        public List<Appointment> GetAllAppointmentsFromAmbulatory(int episodeId)
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

        public List<Appointment> GetAllAppointmentsFromInpatient(int episodeId)
        {
            var episode = _data.InpatientEpisodes.Find(episodeId);
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

        public async Task<UserManagerResponse> CreateAppointmentInAmbulatory(CreateAppointmentViewModel model)
        {
            var referrals = _data.Referrals.Where(x => x.ReferralPackageId == model.ReferralId).ToList();

            int referralId = 0;

            if (referrals.Count() != 0)
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
                Doctor = _data.Doctors.Find(model.DoctorId),
                InteractionClass = model.InteractionClass,
                Visiting = model.Visiting,
                InteractionType = model.InteractionType,
                ServiceComment = model.ServiceComment != "" ? model.ServiceComment : null,
                Priority = model.Priority,
                Treatment = model.Treatment != "" ? model.Treatment : null,
                Notes = model.Notes != "" ? model.Notes : null,
            };

            var episode = _data.AmbulatoryEpisodes.Find(model.AmbulatoryEpisodeId);

            if (episode == null)
                throw new NotFoundException();

            _data.Entry(episode).Collection("Appointments").Load();
            _data.Entry(episode).Reference("ReferralPackage").Load();
            if(episode.ReferralPackage != null)
            {
                _data.Entry(episode.ReferralPackage).Collection("Referrals").Load();
            }

            episode.Appointments.Add(appointment);


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

            if (model.Visiting == "Завершення епізоду")
            {
                var tempReferrals = new List<Referral>();

                if (episode.ReferralPackage != null)
                {
                    _data.Entry(episode.ReferralPackage).Collection("Referrals").Load();
                    tempReferrals = episode.ReferralPackage.Referrals.Where(x => x.ProcessStatus == "Не погашене").ToList();
                }

                if (tempReferrals.Count() == 0)
                {
                    episode.Status = "Завершений";
                } else
                {
                    return new UserManagerResponse
                    {
                        Message = "not completed referrals",
                        IsSuccess = false
                    };
                }
            }

            _data.Appointments.Add(appointment);
            _data.AmbulatoryEpisodes.Update(episode);

            _data.SaveChanges();

            return new UserManagerResponse
            {
                Message = "Episode successfully created",
                IsSuccess = true
            };
        }

        public async Task<UserManagerResponse> CreateAppointmentInInpatient(CreateAppointmentViewModel model)
        {
            var referrals = _data.Referrals.Where(x => x.ReferralPackageId == model.ReferralId).ToList();

            int referralId = 0;

            if (referrals.Count() != 0)
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
                Doctor = _data.Doctors.Find(model.DoctorId),
                InteractionClass = model.InteractionClass,
                Visiting = model.Visiting,
                InteractionType = model.InteractionType,
                ServiceComment = model.ServiceComment != "" ? model.ServiceComment : null,
                Priority = model.Priority,
                Treatment = model.Treatment != "" ? model.Treatment : null,
                Notes = model.Notes != "" ? model.Notes : null,
            };

            var episode = _data.InpatientEpisodes.Find(model.AmbulatoryEpisodeId);

            if (episode == null)
                throw new NotFoundException();

            _data.Entry(episode).Collection("Appointments").Load();
            _data.Entry(episode).Reference("ReferralPackage").Load();
            if (episode.ReferralPackage != null)
            {
                _data.Entry(episode.ReferralPackage).Collection("Referrals").Load();
            }

            episode.Appointments.Add(appointment);


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

            if (model.Visiting == "Завершення епізоду")
            {
                var tempReferrals = new List<Referral>();

                if (episode.ReferralPackage != null)
                {
                    _data.Entry(episode.ReferralPackage).Collection("Referrals").Load();
                    tempReferrals = episode.ReferralPackage.Referrals.Where(x => x.ProcessStatus == "Не погашене").ToList();
                }

                if (tempReferrals.Count() == 0)
                {
                    episode.Status = "Завершений";
                }
                else
                {
                    return new UserManagerResponse
                    {
                        Message = "not completed referrals",
                        IsSuccess = false
                    };
                }
            }

            _data.Appointments.Add(appointment);
            _data.InpatientEpisodes.Update(episode);

            _data.SaveChanges();

            return new UserManagerResponse
            {
                Message = "Episode successfully created",
                IsSuccess = true
            };
        }

        public AppointmentViewModel UpdateInAmbulatory(int episodeId, UpdateAppointmentViewModel model)
        {
            var episode = _data.AmbulatoryEpisodes.Find(episodeId);

            _data.Entry(episode).Collection("Appointments").Load();

            if (episode == null)
                throw new NotFoundException();

            var appointment = _data.Appointments.Find(model.AppointmentId);

            if (appointment == null)
                throw new NotFoundException();

            _data.Entry(appointment).Collection("AppointmentsAndServices").Load();
            _data.Entry(appointment).Collection("AppointmentsAndDiagnosesICPC2").Load();
            _data.Entry(appointment).Reference("Referral").Load();
            foreach (var diagnoses in appointment.AppointmentsAndDiagnosesICPC2)
            {
                _data.Entry(diagnoses).Reference("Appointment").Load();
                _data.Entry(diagnoses).Reference("DiagnosisICPC2").Load();
            }
            foreach (var entry in appointment.AppointmentsAndServices)
            {
                _data.Entry(entry).Reference("Appointment").Load();
                _data.Entry(entry).Reference("Service").Load();
            }
           
            if(model.ReferralId != appointment.Referral.ReferralPackageId)
            {
                var prevRef = _data.Referrals.Find(appointment.Referral.ReferralId);

                prevRef.ProcessStatus = "Не погашене";

                _data.Referrals.Update(prevRef);

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
                    appointment.Referral = _data.Referrals.Find(referralId);
                    episode.Appointments.First(x => x.AppointmentId == model.AppointmentId).Referral = _data.Referrals.Find(referralId);
                }
            }

            if (model.DiagnosesICPC2.Length != 0)
            {
                var appointmentAndDiagnosisICPC2 = _data.AppointmentsAndDiagnosesICPC2.Where(x => x.AppointmentId == model.AppointmentId).ToList();

                _data.AppointmentsAndDiagnosesICPC2.RemoveRange(appointmentAndDiagnosisICPC2);

                int appAndDiagnCount = appointmentAndDiagnosisICPC2.Count();

                appointmentAndDiagnosisICPC2.RemoveRange(0, appAndDiagnCount);

                for (int i = 0; i < model.DiagnosesICPC2.Length; i++)
                {
                    var item = new AppointmentAndDiagnosisICPC2
                    {
                        AppointmentId = model.AppointmentId,
                        Appointment = _data.Appointments.Find(model.AppointmentId),
                        DiagnosisId = model.DiagnosesICPC2[i],
                        DiagnosisICPC2 = _data.DiagnosesICPC2.Find(model.DiagnosesICPC2[i])
                    };
                    
                    appointmentAndDiagnosisICPC2.Add(item);
                }

                _data.AppointmentsAndDiagnosesICPC2.AddRange(appointmentAndDiagnosisICPC2);
                appointment.AppointmentsAndDiagnosesICPC2 = appointmentAndDiagnosisICPC2;
                episode.Appointments.First(x => x.AppointmentId == model.AppointmentId).AppointmentsAndDiagnosesICPC2 = appointmentAndDiagnosisICPC2;
            }

            if(model.AppealReasonComment != appointment.AppealReasonComment)
            {
                appointment.AppealReasonComment = model.AppealReasonComment;
                episode.Appointments.First(x => x.AppointmentId == model.AppointmentId).AppealReasonComment = model.AppealReasonComment;
            }

            if(model.InteractionClass != appointment.InteractionClass)
            {
                appointment.InteractionClass = model.InteractionClass;
                episode.Appointments.First(x => x.AppointmentId == model.AppointmentId).InteractionClass = model.InteractionClass;
            }

            if (model.Visiting != appointment.Visiting)
            {
                appointment.Visiting = model.Visiting;
                episode.Appointments.First(x => x.AppointmentId == model.AppointmentId).Visiting = model.Visiting;
            }

            if (model.InteractionType != appointment.InteractionType)
            {
                appointment.InteractionType = model.InteractionType;
                episode.Appointments.First(x => x.AppointmentId == model.AppointmentId).InteractionType = model.InteractionType;
            }

            if (model.Services.Length != 0)
            {
                var appointmentAndServices = _data.AppointmentsAndServices.Where(x => x.AppointmentId == model.AppointmentId).ToList();

                _data.AppointmentsAndServices.RemoveRange(appointmentAndServices);

                int appAndServicesCount = appointmentAndServices.Count();

                appointmentAndServices.RemoveRange(0, appAndServicesCount);

                for (int i = 0; i < model.Services.Length; i++)
                {
                    var item = new AppointmentAndService
                    {
                        AppointmentId = model.AppointmentId,
                        Appointment = _data.Appointments.Find(model.AppointmentId),
                        ServiceId = model.Services[i],
                        Service = _data.Services.Find(model.Services[i])
                    };

                    appointmentAndServices.Add(item);
                }

                _data.AppointmentsAndServices.AddRange(appointmentAndServices);
                appointment.AppointmentsAndServices = appointmentAndServices;
                episode.Appointments.First(x => x.AppointmentId == model.AppointmentId).AppointmentsAndServices = appointmentAndServices;
            }

            if (model.ServiceComment != appointment.ServiceComment)
            {
                appointment.ServiceComment = model.ServiceComment;
                episode.Appointments.First(x => x.AppointmentId == model.AppointmentId).ServiceComment = model.ServiceComment;
            }

            if (model.Priority != appointment.Priority)
            {
                appointment.Priority = model.Priority;
                episode.Appointments.First(x => x.AppointmentId == model.AppointmentId).Priority = model.Priority;
            }

            if (model.Treatment != appointment.Treatment)
            {
                appointment.Treatment = model.Treatment;
                episode.Appointments.First(x => x.AppointmentId == model.AppointmentId).Treatment = model.Treatment;
            }


            if (model.Notes != appointment.Notes)
            {
                appointment.Notes = model.Notes;
                episode.Appointments.First(x => x.AppointmentId == model.AppointmentId).Notes = model.Notes;
            }

            _data.Appointments.Update(appointment);
            _data.AmbulatoryEpisodes.Update(episode);

            _data.SaveChanges();

            return PrepareResponse(appointment);
        }

        public AppointmentViewModel UpdateInInpatient(int episodeId, UpdateAppointmentViewModel model)
        {
            var episode = _data.InpatientEpisodes.Find(episodeId);

            _data.Entry(episode).Collection("Appointments").Load();

            if (episode == null)
                throw new NotFoundException();

            var appointment = _data.Appointments.Find(model.AppointmentId);

            if (appointment == null)
                throw new NotFoundException();

            _data.Entry(appointment).Collection("AppointmentsAndServices").Load();
            _data.Entry(appointment).Collection("AppointmentsAndDiagnosesICPC2").Load();
            _data.Entry(appointment).Reference("Referral").Load();
            foreach (var diagnoses in appointment.AppointmentsAndDiagnosesICPC2)
            {
                _data.Entry(diagnoses).Reference("Appointment").Load();
                _data.Entry(diagnoses).Reference("DiagnosisICPC2").Load();
            }
            foreach (var entry in appointment.AppointmentsAndServices)
            {
                _data.Entry(entry).Reference("Appointment").Load();
                _data.Entry(entry).Reference("Service").Load();
            }

            var tempReferralPackageId = "";

            if(appointment.Referral != null)
            {
                tempReferralPackageId = appointment.Referral.ReferralPackageId;
            }

            if (model.ReferralId != tempReferralPackageId && tempReferralPackageId != "")
            {
                var prevRef = _data.Referrals.Find(appointment.Referral.ReferralId);

                prevRef.ProcessStatus = "Не погашене";

                _data.Referrals.Update(prevRef);

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
                    appointment.Referral = _data.Referrals.Find(referralId);
                    episode.Appointments.First(x => x.AppointmentId == model.AppointmentId).Referral = _data.Referrals.Find(referralId);
                }
            }

            if (model.DiagnosesICPC2.Length != 0)
            {
                var appointmentAndDiagnosisICPC2 = _data.AppointmentsAndDiagnosesICPC2.Where(x => x.AppointmentId == model.AppointmentId).ToList();

                _data.AppointmentsAndDiagnosesICPC2.RemoveRange(appointmentAndDiagnosisICPC2);

                int appAndDiagnCount = appointmentAndDiagnosisICPC2.Count();

                appointmentAndDiagnosisICPC2.RemoveRange(0, appAndDiagnCount);

                for (int i = 0; i < model.DiagnosesICPC2.Length; i++)
                {
                    var item = new AppointmentAndDiagnosisICPC2
                    {
                        AppointmentId = model.AppointmentId,
                        Appointment = _data.Appointments.Find(model.AppointmentId),
                        DiagnosisId = model.DiagnosesICPC2[i],
                        DiagnosisICPC2 = _data.DiagnosesICPC2.Find(model.DiagnosesICPC2[i])
                    };

                    appointmentAndDiagnosisICPC2.Add(item);
                }

                _data.AppointmentsAndDiagnosesICPC2.AddRange(appointmentAndDiagnosisICPC2);
                appointment.AppointmentsAndDiagnosesICPC2 = appointmentAndDiagnosisICPC2;
                episode.Appointments.First(x => x.AppointmentId == model.AppointmentId).AppointmentsAndDiagnosesICPC2 = appointmentAndDiagnosisICPC2;
            }

            if (model.AppealReasonComment != appointment.AppealReasonComment)
            {
                appointment.AppealReasonComment = model.AppealReasonComment;
                episode.Appointments.First(x => x.AppointmentId == model.AppointmentId).AppealReasonComment = model.AppealReasonComment;
            }

            if (model.InteractionClass != appointment.InteractionClass)
            {
                appointment.InteractionClass = model.InteractionClass;
                episode.Appointments.First(x => x.AppointmentId == model.AppointmentId).InteractionClass = model.InteractionClass;
            }

            if (model.Visiting != appointment.Visiting)
            {
                appointment.Visiting = model.Visiting;
                episode.Appointments.First(x => x.AppointmentId == model.AppointmentId).Visiting = model.Visiting;
            }

            if (model.InteractionType != appointment.InteractionType)
            {
                appointment.InteractionType = model.InteractionType;
                episode.Appointments.First(x => x.AppointmentId == model.AppointmentId).InteractionType = model.InteractionType;
            }

            if (model.Services.Length != 0)
            {
                var appointmentAndServices = _data.AppointmentsAndServices.Where(x => x.AppointmentId == model.AppointmentId).ToList();

                _data.AppointmentsAndServices.RemoveRange(appointmentAndServices);

                int appAndServicesCount = appointmentAndServices.Count();

                appointmentAndServices.RemoveRange(0, appAndServicesCount);

                for (int i = 0; i < model.Services.Length; i++)
                {
                    var item = new AppointmentAndService
                    {
                        AppointmentId = model.AppointmentId,
                        Appointment = _data.Appointments.Find(model.AppointmentId),
                        ServiceId = model.Services[i],
                        Service = _data.Services.Find(model.Services[i])
                    };

                    appointmentAndServices.Add(item);
                }

                _data.AppointmentsAndServices.AddRange(appointmentAndServices);
                appointment.AppointmentsAndServices = appointmentAndServices;
                episode.Appointments.First(x => x.AppointmentId == model.AppointmentId).AppointmentsAndServices = appointmentAndServices;
            }

            if (model.ServiceComment != appointment.ServiceComment)
            {
                appointment.ServiceComment = model.ServiceComment;
                episode.Appointments.First(x => x.AppointmentId == model.AppointmentId).ServiceComment = model.ServiceComment;
            }

            if (model.Priority != appointment.Priority)
            {
                appointment.Priority = model.Priority;
                episode.Appointments.First(x => x.AppointmentId == model.AppointmentId).Priority = model.Priority;
            }

            if (model.Treatment != appointment.Treatment)
            {
                appointment.Treatment = model.Treatment;
                episode.Appointments.First(x => x.AppointmentId == model.AppointmentId).Treatment = model.Treatment;
            }


            if (model.Notes != appointment.Notes)
            {
                appointment.Notes = model.Notes;
                episode.Appointments.First(x => x.AppointmentId == model.AppointmentId).Notes = model.Notes;
            }

            _data.Appointments.Update(appointment);
            _data.InpatientEpisodes.Update(episode);

            _data.SaveChanges();

            return PrepareResponse(appointment);
        }

        public AppointmentViewModel DeleteInAmbulatory(int episodeId, int appointmentId)
        {
            var episode = _data.AmbulatoryEpisodes.Find(episodeId);

            _data.Entry(episode).Collection("Appointments").Load();

            if (episode == null)
                throw new NotFoundException();

            var appointment = _data.Appointments.Find(appointmentId);

            if (appointment == null)
                throw new NotFoundException();

            episode.Appointments.Remove(appointment);

            _data.AmbulatoryEpisodes.Update(episode);
            _data.Appointments.Remove(appointment);

            _data.SaveChanges();

            return PrepareResponse(appointment);
        }

        public AppointmentViewModel DeleteInInpatient(int episodeId, int appointmentId)
        {
            var episode = _data.InpatientEpisodes.Find(episodeId);

            _data.Entry(episode).Collection("Appointments").Load();

            if (episode == null)
                throw new NotFoundException();

            var appointment = _data.Appointments.Find(appointmentId);

            if (appointment == null)
                throw new NotFoundException();

            episode.Appointments.Remove(appointment);

            _data.InpatientEpisodes.Update(episode);
            _data.Appointments.Remove(appointment);

            _data.SaveChanges();

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
