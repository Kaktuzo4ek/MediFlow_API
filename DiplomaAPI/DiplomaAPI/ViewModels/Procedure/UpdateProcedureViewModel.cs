namespace DiplomaAPI.ViewModels.Procedure
{
    public class UpdateProcedureViewModel
    {
        public int ProcedureId { get; set; }

        public string ServiceId { get; set; }

        public string PrevServiceId { get; set; }

        public string Status { get; set; }

        public string? Notes { get; set; }
    }
}
