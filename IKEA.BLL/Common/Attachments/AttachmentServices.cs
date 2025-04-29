using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.BLL.Common.Attachments
{
    public class AttachmentServices : IAttachmentServices
    {
        private readonly List<string> AllowedExtensions = new List<string>() { ".Jpg",".Png","Jpeg" };
        private const int FileMaxSize = 2_097_152;

        public string UploadImage(IFormFile file, string folderName)
        {
            var fileExtension = Path.GetExtension(file.FileName);
            if (!AllowedExtensions.Contains(fileExtension))
                throw new Exception("Invalid File Extension!!!!!!!");
            if (fileExtension.Length > FileMaxSize) throw new Exception("invalid file size!!!");
            var FolderPath = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot","files",folderName);
            if(!Directory.Exists(FolderPath)) Directory.CreateDirectory(FolderPath);
            var FileName = $"{Guid.NewGuid()}_{file.FileName}";
            var FilePath = Path.Combine(FolderPath,FileName);
            using var fs = new FileStream(FilePath, FileMode.Create);

            file.CopyTo(fs);
            return FileName;

        }
        public bool DeleteImage(string filePath)
        {
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
                return true;
            }
            return false;

        }

        
    }
}
