using DiplomaAPI.Data;
using DiplomaAPI.Models;
using DiplomaAPI.Repositories.Interfaces;
using DiplomaAPI.ViewModels.Referral;
using DiplomaAPI.ViewModels.ReferralPackage;
using SendGrid.Helpers.Errors.Model;
using System.Data;

namespace DiplomaAPI.Repositories
{
    public class ReferralRepository : IReferralRepository
    {
        private DataContext _data;
        public ReferralRepository(DataContext data)
        {
            _data = data;
        }

        public List<Referral> getAll()
        {
            var referrals = _data.Referrals.ToList();
            referrals.ForEach(r =>
            {
                _data.Entry(r).Reference("Service").Load();
                _data.Entry(r.Service).Reference("Category").Load();
            });
            return referrals;
        }

        public Referral getById(int id)
        {
            var referral = _data.Referrals.Find(id);
            _data.Entry(referral).Reference("Service").Load();
            _data.Entry(referral.Service).Reference("Category").Load();
            return referral;
        }

        public ReferralViewModel Update(UpdateReferralViewModel data)
        {
            var referral = _data.Referrals.Find(data.ReferralId);

            _data.Entry(referral).Reference("Service").Load();
            _data.Entry(referral).Reference("HospitalizationDepartment").Load();

            if (referral == null)
            {
                throw new NotFoundException();
            }

            if (data.Priority != referral.Priority)
            {
                referral.Priority = data.Priority;
            }

            if (data.ServiceId != referral.Service.ServiceId)
            {
                referral.Service = _data.Services.Find(data.ServiceId);
            }

            if (data.Category != referral.Category)
            {
                referral.Category = data.Category;
            }

            if (referral.HospitalizationDepartment == null && data.HospitalizationDepartmentId != 0)
            {
                referral.HospitalizationDepartment = _data.Departments.Find(data.HospitalizationDepartmentId);
            } 
            else if (data.HospitalizationDepartmentId != referral.HospitalizationDepartment.DepartmentId && data.HospitalizationDepartmentId != 0)
            {
                referral.HospitalizationDepartment = _data.Departments.Find(data.HospitalizationDepartmentId);
            }

            _data.Update(referral);

            _data.SaveChanges();

            return PrepareResponse(referral);
        }

        public ReferralViewModel Delete(int referralId, string referralPackageId)
        {
            var referral = _data.Referrals.Find(referralId);

            if (referral == null)
            {
                throw new NotFoundException();
            }

            _data.Referrals.Remove(referral);
            _data.SaveChanges();

            if (_data.Referrals.Where(x => x.ReferralPackageId == referralPackageId).Count() == 0)
            {
                var referralPackage = _data.ReferralPackages.Find(referralPackageId);

                var ambulatoryEpisodes = _data.AmbulatoryEpisodes.ToList();

                ambulatoryEpisodes.ForEach(x =>
                {
                    _data.Entry(x).Reference("ReferralPackage").Load();
                });

                var tempEpisode = ambulatoryEpisodes.Where(x => x.ReferralPackage?.ReferralPackageId == referralPackageId).ToList();

                AmbulatoryEpisode episode = null;

                var inpatientEpisodes = _data.InpatientEpisodes.ToList();

                inpatientEpisodes.ForEach(x =>
                {
                    _data.Entry(x).Reference("ReferralPackage").Load();
                });

                var tempEpisode2 = inpatientEpisodes.Where(x => x.ReferralPackage?.ReferralPackageId == referralPackageId).ToList();

                InpatientEpisode episode2 = null;

                if (tempEpisode.Count() != 0)
                {
                    episode = tempEpisode.ElementAt(0);
                    episode.ReferralPackage = _data.ReferralPackages.Find("");
                    _data.AmbulatoryEpisodes.Update(episode);
                } else if (tempEpisode2.Count() != 0)
                {
                    episode2 = tempEpisode2.ElementAt(0);
                    episode2.ReferralPackage = _data.ReferralPackages.Find("");
                    _data.InpatientEpisodes.Update(episode2);
                }

                _data.ReferralPackages.Remove(referralPackage);
            }

            _data.SaveChanges();

            return PrepareResponseDelete(referral);
        }

        public ReferralViewModel DeleteInAmbulatory(int referralId, string referralPackageId)
        {
            var referral = _data.Referrals.Find(referralId);

            if(referral == null)
            {
                throw new NotFoundException();
            }

            _data.Referrals.Remove(referral);
            _data.SaveChanges();

            if(_data.Referrals.Where(x => x.ReferralPackageId == referralPackageId).Count() == 0)
            {
                var referralPackage = _data.ReferralPackages.Find(referralPackageId);

                var episodes = _data.AmbulatoryEpisodes.ToList();

                episodes.ForEach(x =>
                {
                    _data.Entry(x).Reference("ReferralPackage").Load();
                });

                var episode = episodes.Where(x => x.ReferralPackage.ReferralPackageId == referralPackageId).ToList().ElementAt(0);

                episode.ReferralPackage = _data.ReferralPackages.Find("");

                _data.ReferralPackages.Remove(referralPackage);

                _data.AmbulatoryEpisodes.Update(episode);
            }

            _data.SaveChanges();

            return PrepareResponseDelete(referral);
        }

        public ReferralViewModel DeleteInInpatient(int referralId, string referralPackageId)
        {
            var referral = _data.Referrals.Find(referralId);

            if (referral == null)
            {
                throw new NotFoundException();
            }

            _data.Referrals.Remove(referral);
            _data.SaveChanges();

            if (_data.Referrals.Where(x => x.ReferralPackageId == referralPackageId).Count() == 0)
            {
                var referralPackage = _data.ReferralPackages.Find(referralPackageId);

                var episodes = _data.InpatientEpisodes.ToList();

                episodes.ForEach(x =>
                {
                    _data.Entry(x).Reference("ReferralPackage").Load();
                });

                var episode = episodes.Where(x => x.ReferralPackage.ReferralPackageId == referralPackageId).ToList().ElementAt(0);

                episode.ReferralPackage = _data.ReferralPackages.Find("");

                _data.ReferralPackages.Remove(referralPackage);

                _data.InpatientEpisodes.Update(episode);
            }

            _data.SaveChanges();

            return PrepareResponseDelete(referral);
        }

        private ReferralViewModel PrepareResponse(Referral referral)
        {
            _data.Entry(referral.Service).Reference("Category").Load();
            return new ReferralViewModel
            {
               ReferralId = referral.ReferralId,
               ReferralPackageId = referral.ReferralPackageId,
               Status = referral.Status,
               Priority = referral.Priority,
               ProcessStatus = referral.ProcessStatus,
               Service = referral.Service,
            };
        }

        private ReferralViewModel PrepareResponseDelete(Referral referral)
        {
            return new ReferralViewModel
            {
                ReferralId = referral.ReferralId,
                ReferralPackageId = referral.ReferralPackageId,
                Status = referral.Status,
                Priority = referral.Priority,
                ProcessStatus = referral.ProcessStatus,
                Service = referral.Service,
            };
        }
    }
}
