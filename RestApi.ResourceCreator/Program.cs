using System;
using System.IO;
using System.Linq;

namespace RestApi.ResourceCreator
{
    class Program
    {
        static readonly string ProjectName = "BestEmployeePoll";
        static readonly string ProjectPath = @"E:\Dropbox\Workspace\BestEmployeePoll";
        static readonly string NewResourceName = "Address";

        static readonly string NameWildCard = "!NAME!";
        static readonly string ProjectWildCard = "!PROJECT!";
        static readonly string boilerPlatesPath = Directory.GetCurrentDirectory() + "\\BoilerPlates";

        public static void Main(string[] args)
        {
            var files = Directory.GetFiles(boilerPlatesPath, "*.*", SearchOption.AllDirectories);

            foreach (var fileInfo in files.Select(f => new FileInfo(f)))
            {
                var newFileDirectoryPath = GetNewFileDirectoryPath(fileInfo);
                var newFilePath = GetNewFilePath(NewResourceName, fileInfo, newFileDirectoryPath);

                if (!File.Exists(newFilePath))
                {
                    var fileContent = File.ReadAllText(fileInfo.FullName);
                    fileContent = fileContent.Replace(NameWildCard, NewResourceName);
                    fileContent = fileContent.Replace(ProjectWildCard, ProjectName);
                    using var fs = File.CreateText(newFilePath);
                    fs.Write(fileContent);
                }
            }
        }

        private static string GetNewFilePath(string name, FileInfo fileInfo, string newFileDirectoryPath)
        {
            var fileName = fileInfo.Name.Replace(".txt", ".cs");
            fileName = fileName.Replace(NameWildCard, name);
            var newFilePath = newFileDirectoryPath + fileName;
            return newFilePath;
        }
        private static string GetNewFileDirectoryPath(FileInfo fileInfo)
        {
            var absPath = fileInfo.Directory.FullName.Replace(boilerPlatesPath, string.Empty);
            absPath = absPath.Substring(1);
            var newFileDirectoryPath = $"{ProjectPath}\\{ProjectName}.{absPath}\\";
            Directory.CreateDirectory(newFileDirectoryPath);
            return newFileDirectoryPath;
        }
    }
}
