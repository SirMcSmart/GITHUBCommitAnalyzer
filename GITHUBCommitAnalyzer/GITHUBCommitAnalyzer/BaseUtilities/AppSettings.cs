namespace GITHUBCommitAnalyzer.BaseUtilities
{
    public class AppSettings
    {

        //Response Codes
        public string? RcFailed { get; set; }
        public string? RcBadRequest { get; set; }
        public string? RcSuccess { get; set; }
        public string? CouldNotFetchDetails { get; set; }
        public string? CouldNotFetchSingle { get; set; }
        public string? CouldNotFetchAll { get; set; }
        public string? CouldNotCreate { get; set; }
        public string? CouldNotUpdate { get; set; }
        public string? UserAlreadyExist { get; set; }
        public string? CouldNotDelete { get; set; }
        public string? InvalidParameter { get; set; }
        public string? ErrorOccured { get; set; }
        public string? NoContent { get; set; }
        public string? PermissionDenied { get; set; }
        public string? Successful { get; set; }
        public string? ReferralNotExist { get; set; }
        public string? BaseURL { get; set; }
        public string? DynamicsVAccount { get; set; }
    }
}
