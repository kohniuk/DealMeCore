﻿using DealMeCore.WebApi.Models.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;

namespace DealMeCore.WebApi.Utils
{
    /// <summary>
    /// ImageWriter.
    /// </summary>
    public class ImageWriter : IImageWriter
    {
        /// <summary>
        /// Upload Image 
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public async Task<string> UploadImage(IFormFile file)
        {
            if (CheckIfImageFile(file))
            {
                return await WriteFile(file);
            }

            return null;
        }

        /// <summary>
        /// Method to check if file is image file
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        private bool CheckIfImageFile(IFormFile file)
        {
            byte[] fileBytes;

            using (var ms = new MemoryStream())
            {
                file.CopyTo(ms);
                fileBytes = ms.ToArray();
            }

            return fileBytes.GetImageFormat() != ImageFormat.Unknown;
        }

        /// <summary>
        /// Method to write file onto the disk
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public async Task<string> WriteFile(IFormFile file)
        {
            if (file.Length == 0)
            {
                return null;
            }

            var extension = "." + file.FileName.Split('.')[file.FileName.Split('.').Length - 1];
            var fileName = Guid.NewGuid() + extension;

            //for the file due to security reasons.
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images", fileName);

            using (var bits = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(bits);
            }

            return fileName;
        }
    }
}