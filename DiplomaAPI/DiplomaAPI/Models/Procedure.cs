namespace DiplomaAPI.Models
{
    public class Procedure
    {
        public int ProcedureId { get; set; }

        public Models.Referral? Referral { get; set; }

        public Models.Doctor Doctor { get; set; }

        public Models.Patient Patient { get; set; }

        public string Status { get; set; }

        public string Category { get; set; }

        public string ProcedureName { get; set; }

        public DateTime EventDate { get; set; }

        public DateTime DateCreated { get; set; }
    }
}
