using System;
using System.Net;

namespace ForgeModBuilder.Managers
{
    public static class WebManager
    {
        public static string GithubUrlPath { get; private set; } = @"https://raw.githubusercontent.com/CJMinecraft01/ForgeModBuilder/rewrite/";

        public static void DownloadFileFromGithub(string githubPath, string destinationPath)
        {
            WebClient client = new WebClient();
            client.DownloadFileAsync(new Uri(GithubUrlPath + githubPath), destinationPath);
        }
    }
}
