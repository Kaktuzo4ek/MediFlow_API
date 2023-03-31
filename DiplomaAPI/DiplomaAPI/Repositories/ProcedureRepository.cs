using DiplomaAPI.Data;
using DiplomaAPI.Models;
using DiplomaAPI.Repositories.Interfaces;
using DiplomaAPI.ViewModels.Procedure;
using DiplomaAPI.ViewModels.Referral;
using DiplomaAPI.ViewModels.ReferralPackage;
using SendGrid.Helpers.Errors.Model;

namespace DiplomaAPI.Repositories
{
    public class ProcedureRepository : IProcedureRepository
    {
        private DataContext _data;
        public ProcedureRepository(DataContext data)
        {
            _data = data;
        }

        public List<Procedure> getAll(int patientId)
        {
            var procedures = _data.Procedures.Where(x => x.Patient.PatientId == patientId).ToList();

            if (procedures == null)
            {
                throw new NotFoundException();
            }

            procedures.ForEach(p =>
            {
                _data.Entry(p).Reference("Referral").Load();
                if (p.Referral != null)
                {
                    _data.Entry(p.Referral).Reference("Service").Load();
                    _data.Entry(p.Referral.Service).Reference("Category").Load();
                }

                _data.Entry(p).Reference("Doctor").Load();
                _data.Entry(p.Doctor).Reference("Institution").Load();
                _data.Entry(p.Doctor).Reference("Department").Load();
                _data.Entry(p).Reference("Patient").Load();
            });
            return procedures;
        }

        public Procedure getById(int id)
        {
            var procedure = _data.Procedures.Find(id);

            if (procedure == null)
            {
                throw new NotFoundException();
            }

            _data.Entry(procedure).Reference("Referral").Load();
            _data.Entry(procedure.Referral).Reference("Service").Load();
            _data.Entry(procedure.Referral.Service).Reference("Category").Load();
            _data.Entry(procedure).Reference("Doctor").Load();
            _data.Entry(procedure.Doctor).Reference("Institution").Load();
            _data.Entry(procedure.Doctor).Reference("Department").Load();
            _data.Entry(procedure).Reference("Patient").Load();
            return procedure;
        }

        public ProcedureViewModel Create(CreateProcedureViewModel data)
        {
            var referralTmp = _data.Referrals.Where(x => x.ReferralPackageId == data.ReferralPackageId && x.Service.ServiceId == data.ServiceId).ToList();

            var service = _data.Services.Find(data.ServiceId);

            _data.Entry(service).Reference("Category").Load();

            if (referralTmp == null)
            {
                throw new NotFoundException();
            }

            int referralId = 0;
            referralTmp.ForEach(r =>
            {
                referralId = r.ReferralId;
            });

            var procedure = new Procedure
            {
                Referral = _data.Referrals.Find(referralId),
                Doctor = _data.Doctors.Find(data.DoctorId),
                Patient = _data.Patients.Find(data.PatientId),
                Status = data.Status,
                EventDate = DateTime.Now,
                DateCreated = DateTime.Now,
                ProcedureName = '(' + service.ServiceId + ") " + service.ServiceName,
                Category = service.Category.CategoryName,
            };

            var referral = _data.Referrals.Find(referralId);

            referral.ProcessStatus = "Погашене " + "(від " + DateTime.Now + ")";

            _data.Referrals.Update(referral);
            _data.Procedures.Add(procedure);
            _data.SaveChanges();

            return PrepareResponseCreate(procedure);
        }

        public ProcedureViewModel Update(UpdateProcedureViewModel data)
        {
            var procedure = _data.Procedures.Find(data.ProcedureId);
            _data.Entry(procedure).Reference("Referral").Load();
            _data.Entry(procedure.Referral).Reference("Service").Load();

            var service = _data.Services.Find(data.ServiceId);

            _data.Entry(service).Reference("Category").Load();

            if (procedure == null)
            {
                throw new NotFoundException();
            }

            var prevReferral = _data.Referrals.Where(x => x.Service.ServiceId == data.PrevServiceId).ToList();
            var referral = _data.Referrals.Where(x => x.Service.ServiceId == data.ServiceId).ToList();
            var prevReferralId = 0;
            var referralId = 0;
            prevReferral.ForEach(r =>
            {
                prevReferralId = r.ReferralId;
            });
            referral.ForEach(r =>
            {
                referralId = r.ReferralId;
            });

            var prevReferralUpdate = _data.Referrals.Find(prevReferralId);
            var referralUpdate = _data.Referrals.Find(referralId);

            if (data.ServiceId != "" && data.PrevServiceId != data.ServiceId)
            {
                procedure.Referral.Service = _data.Services.Find(data.ServiceId);
                referralUpdate.ProcessStatus = "Погашене " + "(від " + DateTime.Now + ")";
                prevReferralUpdate.ProcessStatus = "Не погашене";
                _data.Update(referralUpdate);
                _data.Update(prevReferralUpdate);
            }

            if (data.Status != "")
            {
                procedure.Status = data.Status;
            }

            if(data.ServiceId != data.PrevServiceId)
            {
                procedure.ProcedureName = '(' + service.ServiceId + ") " + service.ServiceName;
                procedure.Category = service.Category.CategoryName;
            }

            _data.Procedures.Update(procedure);

            _data.SaveChanges();

            return PrepareResponse(procedure);
        }

        public ProcedureViewModel Delete(int procedureId)
        {
            var procedure = _data.Procedures.Find(procedureId);
            _data.Entry(procedure).Reference("Referral").Load();

            if (procedure == null)
            {
                throw new NotFoundException();
            }

            var referral = _data.Referrals.Find(procedure.Referral.ReferralId);

            referral.ProcessStatus = "Не погашене";

            _data.Procedures.Remove(procedure);
            _data.SaveChanges();

            return PrepareResponse(procedure);
        }

        private ProcedureViewModel PrepareResponseCreate(Procedure procedure)
        {
            _data.Entry(procedure).Reference("Referral").Load();
            _data.Entry(procedure.Referral).Reference("Service").Load();
            _data.Entry(procedure.Referral.Service).Reference("Category").Load();
            _data.Entry(procedure).Reference("Doctor").Load();
            _data.Entry(procedure.Doctor).Reference("Institution").Load();
            _data.Entry(procedure.Doctor).Reference("Department").Load();
            _data.Entry(procedure).Reference("Patient").Load();

            return new ProcedureViewModel
            {
                ProcedureId = procedure.ProcedureId,
                Referral = procedure.Referral,
                Doctor = procedure.Doctor,
                Patient = procedure.Patient,
                Status = procedure.Status,
                EventDate = procedure.EventDate,
                DateCreated = procedure.DateCreated,
            };
        }

        private ProcedureViewModel PrepareResponse(Procedure procedure)
        {
            return new ProcedureViewModel
            {
                ProcedureId = procedure.ProcedureId,
                Referral = procedure.Referral,
                Doctor = procedure.Doctor,
                Patient = procedure.Patient,
                Status = procedure.Status,
                EventDate = procedure.EventDate,
                DateCreated = procedure.DateCreated,
            };
        }
    }
}
