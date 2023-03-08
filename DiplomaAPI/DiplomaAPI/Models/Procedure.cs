namespace DiplomaAPI.Models
{
    public class Procedure
    {
        public int ProcedureId { get; set; }

        public Models.Patient Patient { get; set; }

        public Models.Employee Doctor { get; set; }

        public string Category { get; set; }

        public string Name { get; set; }

        public string Status { get; set; }
    }
}
