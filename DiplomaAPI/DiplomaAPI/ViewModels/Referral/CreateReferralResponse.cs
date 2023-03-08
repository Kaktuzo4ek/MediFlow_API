using DiplomaAPI.ViewModels;

namespace DiplomaAPI.ViewModels.Referral
{
    public class CreateReferralResponse
    {
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
        public IEnumerable<string> Errors { get; set; }
        public ViewModels.Referral.ReferralViewModel ReferralViewModel { get; set; }
    }
}
