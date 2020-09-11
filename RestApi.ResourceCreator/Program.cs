using System;
using System.IO;
using System.Linq;

namespace Retail.ResourceCreator
{
    class Program
    {
        static string WildCard = "!NAME!";
        static string OutputPath = @"..\..\..\..\";
        static string boilerPlatesPath = Directory.GetCurrentDirectory() + "\\BoilerPlates";
        static void Main(string[] args)
        {
            var name = "Address";
            var files = Directory.GetFiles(boilerPlatesPath, "*.*", SearchOption.AllDirectories);

            foreach (var fileInfo in files.Select(f => new FileInfo(f)))
            {
                var newFileDirectoryPath = GetNewFileDirectoryPath(fileInfo);
                var newFilePath = GetNewFilePath(name, fileInfo, newFileDirectoryPath);

                if (!File.Exists(newFilePath))
                {
                    var fileContent = File.ReadAllText(fileInfo.FullName);
                    fileContent = fileContent.Replace(WildCard, name);
                    using var fs = File.CreateText(newFilePath);
                    fs.Write(fileContent);
                }
            }
        }

        private static string GetNewFilePath(string name, FileInfo fileInfo, string newFileDirectoryPath)
        {
            var fileName = fileInfo.Name.Replace(".txt", ".cs");
            fileName = fileName.Replace(WildCard, name);
            var newFilePath = newFileDirectoryPath + fileName;
            return newFilePath;
        }
        private static string GetNewFileDirectoryPath(FileInfo fileInfo)
        {
            var absPath = fileInfo.Directory.FullName.Replace(boilerPlatesPath, string.Empty);
            var newFileDirectoryPath = $"{OutputPath}{absPath}\\";
            Directory.CreateDirectory(newFileDirectoryPath);
            return newFileDirectoryPath;
        }
    }
}
