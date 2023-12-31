using CsvHelper.Configuration;
using CsvHelper;
using GITHUBCommitAnalyzer.BaseUtilities;
using GITHUBCommitAnalyzer.Interfaces;
using GITHUBCommitAnalyzer.ViewModels;
using Microsoft.AspNetCore.Identity;
using Octokit;
using System.Globalization;
using System.Net;
using System.Net.Http.Headers;

namespace GITHUBCommitAnalyzer.Services
{
    public class CommitAnalyzerService : ICommitAnalyzerService
    {
        private readonly ILogger<CommitAnalyzerService> _logger;
        private readonly IBaseResponse<object> _baseResponse;
        private readonly IWebHostEnvironment _env;
        private const string FILE_UPLOAD = "FILE_UPLOAD_FOLDER";

        public CommitAnalyzerService(ILogger<CommitAnalyzerService> logger, IBaseResponse<object> baseResponse, IWebHostEnvironment env)
        {
            _logger = logger;
            _baseResponse = baseResponse;
            _env = env;
        }
        public async Task<BaseResponseVM<object>> GITHUBAnalyzer(GITHUBAnalyzerRequest request)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(request.Username) || string.IsNullOrWhiteSpace(request.RepoName))
                    return await _baseResponse.CustomErrorMessage("Username and RepoName cannot be Null or empty.", HttpStatusCode.BadRequest);

                var check = await CheckIfUsernameAndRepoExist(request);
                if(check.Status == false)//Not Found
                 return await _baseResponse.CustomErrorMessage($"Username or RepoName cannot be not be found. Kindly re-check the details : {check.Message} ", HttpStatusCode.BadRequest);


                var github = new GitHubClient(new Octokit.ProductHeaderValue(request.RepoName));
                var repository = await github.Repository.Get(request.Username, request.RepoName);                
                var commits = await github.Repository.Commit.GetAll(repository.Id);
                var commitList = new List<GITHUBAnalyzerResponse>();
                //List<GitHubCommit> commitList = new List<GitHubCommit>();

                foreach (GitHubCommit commit in commits)
                {
                    var files = github.Repository.Commit.Get(request.Username, request.RepoName, commit.Sha).Result.Files;
                    foreach (var file in files)
                    {
                        if (file.Status == "modified" && file.Filename.EndsWith(".cs"))
                        {
                            var cl = new GITHUBAnalyzerResponse();
                            cl.CommitSHA = commit.Sha;
                            cl.FileName = file.Filename;
                            var lines = file.Patch.Split('\n');
                            foreach (var line in lines)
                            {
                                if (line.StartsWith("- ") && (line.Contains("public") || line.Contains("private") || line.Contains("protected") || line.Contains("internal") || line.Contains("static")))
                                    cl.OldMethodSignature = line.Replace("- ", "");

                                if (line.StartsWith("+ ") && (line.Contains("public") || line.Contains("private") || line.Contains("protected") || line.Contains("internal") || line.Contains("static")))
                                    cl.NewMethodSignature = line.Replace("+ ", ""); 

                                 if(cl.OldMethodSignature != null && cl.NewMethodSignature != null && cl.OldMethodSignature != cl.NewMethodSignature) commitList.Add(cl);
                            }

                        }
                    }
                }

                CultureInfo ci = CultureInfo.InvariantCulture;
                string datetimeStamp = DateTime.UtcNow.ToString("ddMMyyyyHHmmssf", ci);
                string filename = $"GitHubCommits-{request.Username}-{request.RepoName}-{datetimeStamp}.csv";
                var filePath = Path.Combine(_env.WebRootPath, FILE_UPLOAD);
                WriteToCsv(commitList, $"{filePath}\\{filename}");


                return await _baseResponse.Success(commitList, "00", "Analyzed all the commits in the GitHub repository successfully");

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occured.");
                return await _baseResponse.InternalServerError(ex);
            }

        }

        public static void WriteToCsv<T>(IEnumerable<T> records, string filePath)
        {
            var config = new CsvConfiguration(CultureInfo.InvariantCulture);

            using (var writer = new StreamWriter(filePath))
            using (var csv = new CsvWriter(writer, config))
            {
                csv.WriteRecords(records);
            }
        }

        public async Task<CheckIfUsernameResponse> CheckIfUsernameAndRepoExist(GITHUBAnalyzerRequest request)
        {
            string apiUrl = $"https://api.github.com/repos/{request.Username}/{request.RepoName}/commits";

            var httpClient = new HttpClient();

            httpClient.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue(request.RepoName, "1"));

            var response = await httpClient.GetAsync(apiUrl);
            var ciu = new CheckIfUsernameResponse();

            if (response.IsSuccessStatusCode)
            {
                ciu.Status = true;
                return ciu; ;
            }
            ciu.Message = response.ReasonPhrase;
            ciu.Status = false;
            return ciu;
        }
    }
}
