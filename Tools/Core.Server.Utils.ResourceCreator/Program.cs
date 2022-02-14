using System;
using System.IO;
using System.Linq;

namespace Core.Server.ResourceCreator
{
    public class Program
    {
        static readonly string ProjectName = "Retail.Server";
        static readonly string ProjectPath = @$"E:\Workspace\RetailShopping\Retail.Server.New\";
        static readonly string NewResourceName = "Category";
        static readonly string ParentName = "";
        static readonly bool IsChild = false;

        static readonly string NameWildCard = "!NAME!";
        static readonly string ProjectWildCard = "!PROJECT!";
        static readonly string ParentWildCard = "!PARENT!";
        static readonly string boilerPlatesPath = GetBoilerPlatesPath();

        public static void Main(string[] args)
        {
            var files = GetBolierplateFiles();

            foreach (var fileInfo in files.Select(f => new FileInfo(f)))
            {
                var newFileDirectoryPath = GetNewFileDirectoryPath(fileInfo);
                var newFilePath = GetNewFilePath(fileInfo, newFileDirectoryPath);

                if (!File.Exists(newFilePath))
                    CreateFile(fileInfo, newFilePath);
            }
        }

        private static void CreateFile(FileInfo fileInfo, string newFilePath)
        {
            var fileContent = File.ReadAllText(fileInfo.FullName);
            fileContent = ReplaceWildCards(fileContent);
            using var fs = File.CreateText(newFilePath);
            fs.Write(fileContent);
        }

        private static string[] GetBolierplateFiles()
        {
            return Directory.GetFiles(boilerPlatesPath, "*.*", SearchOption.AllDirectories);
        }

        private static string GetNewFilePath(FileInfo fileInfo, string newFileDirectoryPath)
        {
            var fileName = fileInfo.Name.Replace(".txt", string.Empty);
            fileName = ReplaceWildCards(fileName);
            var newFilePath = newFileDirectoryPath + fileName;
            return newFilePath;
        }
        private static string GetNewFileDirectoryPath(FileInfo fileInfo)
        {
            var absPath = fileInfo.Directory.FullName.Replace(boilerPlatesPath, string.Empty);
            absPath = absPath.Substring(1);
            absPath = ReplaceWildCards(absPath);
            var newFileDirectoryPath = ProjectPath + absPath + "\\";
            Directory.CreateDirectory(newFileDirectoryPath);
            return newFileDirectoryPath;
        }

        private static string GetBoilerPlatesPath()
        {
            return Directory.GetCurrentDirectory() + 
                "\\BoilerPlates\\" + 
                (IsChild ? "ChildResource" : "Resource");
        }

        private static string ReplaceWildCards(string str)
        {
            str = str.Replace(ProjectWildCard, ProjectName);
            str = str.Replace(NameWildCard, NewResourceName);
            str = str.Replace(ParentWildCard, ParentName);
            return str;
        }
    }
}
