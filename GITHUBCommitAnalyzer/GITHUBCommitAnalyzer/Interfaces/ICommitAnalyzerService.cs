using GITHUBCommitAnalyzer.BaseUtilities;
using GITHUBCommitAnalyzer.ViewModels;

namespace GITHUBCommitAnalyzer.Interfaces
{
    public interface ICommitAnalyzerService
    {
        Task<BaseResponseVM<object>> GITHUBAnalyzer(GITHUBAnalyzerRequest request);
    }
}
