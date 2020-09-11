using System;
using System.IO;
using System.Linq;

namespace RestApi.ResourceCreator
{
    class Program
    {
        static readonly string ProjectName = "BestEmployeePoll";
        static readonly string ProjectPath = @"E:\Dropbox\Workspace\BestEmployeePoll";
        static readonly string[] NewResourcesName = new string[] {  };

        static readonly string NameWildCard = "!NAME!";
        static readonly string ProjectWildCard = "!PROJECT!";
        static readonly string boilerPlatesPath = Directory.GetCurrentDirectory() + "\\BoilerPlates";

        public static void Main(string[] args)
        {
            foreach (var NewResourceName in NewResourcesName)
            {
                var files = GetBolierplateFiles();

                foreach (var fileInfo in files.Select(f => new FileInfo(f)))
                {
                    var newFileDirectoryPath = GetNewFileDirectoryPath(fileInfo);
                    var newFilePath = GetNewFilePath(NewResourceName, fileInfo, newFileDirectoryPath);

                    if (!File.Exists(newFilePath))
                        CreateFile(NewResourceName, fileInfo, newFilePath);
                }
            }
        }

        private static void CreateFile(string NewResourceName, FileInfo fileInfo, string newFilePath)
        {
            var fileContent = File.ReadAllText(fileInfo.FullName);
            fileContent = fileContent.Replace(NameWildCard, NewResourceName);
            fileContent = fileContent.Replace(ProjectWildCard, ProjectName);
            using var fs = File.CreateText(newFilePath);
            fs.Write(fileContent);
        }

        private static string[] GetBolierplateFiles()
        {
            return Directory.GetFiles(boilerPlatesPath, "*.*", SearchOption.AllDirectories);
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
