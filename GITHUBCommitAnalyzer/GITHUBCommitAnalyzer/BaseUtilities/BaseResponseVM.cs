namespace GITHUBCommitAnalyzer.BaseUtilities
{
    public class BaseResponseVM<T>
    {
        public int TotalCount { get; set; }
        public int StatusCode { get; set; }
        public string Code { get; set; }
        public bool Success { get; set; }
        public string Description { get; set; }
        public T data { get; set; }
    }
}
