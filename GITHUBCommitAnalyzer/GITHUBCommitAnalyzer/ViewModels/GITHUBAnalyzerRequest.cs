using System.ComponentModel.DataAnnotations;

namespace GITHUBCommitAnalyzer.ViewModels
{
    public class GITHUBAnalyzerRequest
    {
        [Required]
        public string? Username { get; set; }
        [Required]
        public string? RepoName { get; set; }
    }
}
