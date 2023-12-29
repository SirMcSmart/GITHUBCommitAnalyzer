using System.ComponentModel;

namespace GITHUBCommitAnalyzer.ViewModels
{
    public class GITHUBAnalyzerResponse
    {
        [Description ("Commit SHA")]
        public string? CommitSHA { get; set; }
        [Description("File Name")]
        public string? FileName { get; set; }
        [Description("Old Method Signature")]
        public string? OldMethodSignature { get; set; }
        [Description("New Method Signature")]
        public string? NewMethodSignature { get; set; }
    }
}
