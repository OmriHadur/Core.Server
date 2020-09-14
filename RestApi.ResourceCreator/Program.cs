using System;
using System.IO;
using System.Linq;

namespace RestApi.ResourceCreator
{
    class Program
    {
        static readonly string ProjectName = "BestEmployeePoll";
        static readonly string ProjectPath = @"E:\Dropbox\Workspace\BestEmployeePoll";
        static string NewResourceName = "employee";
        static string NewResourcePluralName = NewResourceName + "s";

        static readonly string ParentName =  "" ;
        static readonly bool IsInnerRest = false;

        static readonly string NameWildCard = "!NAME!";
        static readonly string NamePluralWildCard = "!NAMES!";
        static readonly string ProjectWildCard = "!PROJECT!";
        static readonly string ParentWildCard = "!PARENT!";
        static readonly string boilerPlatesPath = Directory.GetCurrentDirectory() + "\\BoilerPlates\\" + (IsInnerRest ? "InnerRest" : "Rest");

        public static void Main(string[] args)
        {
            NewResourceName = GetStarsWithUpper(NewResourceName);
            NewResourcePluralName = GetStarsWithUpper(NewResourcePluralName);
            var files = GetBolierplateFiles();

            foreach (var fileInfo in files.Select(f => new FileInfo(f)))
            {
                var newFileDirectoryPath = GetNewFileDirectoryPath(fileInfo);
                var newFilePath = GetNewFilePath(fileInfo, newFileDirectoryPath);

                if (!File.Exists(newFilePath))
                    CreateFile(fileInfo, newFilePath);
            }
        }

        private static string GetStarsWithUpper(string newResourceName)
        {
            newResourceName = newResourceName.ToLower();
            newResourceName = newResourceName[0].ToString().ToUpper() + newResourceName.Substring(1);
            return newResourceName;
        }

        private static void CreateFile(FileInfo fileInfo, string newFilePath)
        {
            var fileContent = File.ReadAllText(fileInfo.FullName);
            fileContent = ReplateWildCards(fileContent);
            using var fs = File.CreateText(newFilePath);
            fs.Write(fileContent);
        }

        private static string ReplateWildCards(string fileContent)
        {
            fileContent = fileContent.Replace(NameWildCard, NewResourceName);
            fileContent = fileContent.Replace(NamePluralWildCard, NewResourcePluralName);
            fileContent = fileContent.Replace(ProjectWildCard, ProjectName);
            return fileContent.Replace(ParentWildCard, ParentName);
        }

        private static string[] GetBolierplateFiles()
        {
            return Directory.GetFiles(boilerPlatesPath, "*.*", SearchOption.AllDirectories);
        }

        private static string GetNewFilePath(FileInfo fileInfo, string newFileDirectoryPath)
        {
            var fileName = fileInfo.Name.Replace(".txt", ".cs");
            fileName = fileName.Replace(NameWildCard, NewResourceName);
            fileName = fileName.Replace(NamePluralWildCard, NewResourcePluralName);
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
