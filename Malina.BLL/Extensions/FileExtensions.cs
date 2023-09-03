using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Malina.BLL.Extensions
{
    public static class FileExtensions
    {
        public static bool IsImage(this IFormFile file)
        {
            return file.ContentType.Contains("image");
        }

       
        public static bool IsAllowedSize(this IFormFile file, int mb)
        {
            if (file.Length > mb * 1024 * 1024) return false;
            return true;
        }


        public async static Task<string> GenerateFile(this IFormFile file, string rootPath)
        {
            if (!Directory.Exists(rootPath))
                Directory.CreateDirectory(rootPath);

            var unicalName = $"{Guid.NewGuid()}-{file.FileName}";

            using FileStream fs = new(Path.Combine(rootPath, unicalName), FileMode.Create);

            await file.CopyToAsync(fs);

            return unicalName;
        }
    }
}
