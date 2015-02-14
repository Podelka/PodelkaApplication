using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;

namespace Podelka.Core.Service
{
    public class ImageService
    {
        public bool IsImage(HttpPostedFileBase file)
        {
            if (file.ContentType.Contains("image"))
            {
                return true;
            }

            var extensions = new string[] { ".jpg", ".png", ".gif", ".jpeg" };// ToDo - add more if you like...

            // linq from Henrik Stenbæk
            return extensions.Any(item => file.FileName.EndsWith(item, StringComparison.OrdinalIgnoreCase));
        }

        public string SaveTemporaryFile(HttpPostedFileBase file, HttpContext server) // string server)
        {
            // Define destination
            var folderName = "/Temp";
            var serverPath = server.Server.MapPath("/Temp");
            if (Directory.Exists(serverPath) == false)
            {
                Directory.CreateDirectory(serverPath);
            }

            // Generate unique file name
            var fileName = Path.GetFileName(file.FileName);
            fileName = SaveTemporaryAvatarFileImage(file, serverPath, fileName);

            // Clean up old files after every save
            CleanUpTempFolder(1, server);

            return Path.Combine(folderName, fileName);
        }

        private string SaveTemporaryAvatarFileImage(HttpPostedFileBase file, string serverPath, string fileName)
        {
            var img = new WebImage(file.InputStream);

            var width = img.Width;
            var height = img.Height;

            if (height > 500 || width > 800)
            {
                double ratio = (double)img.Width / (double)img.Height;

                int newHeight = 500;
                int newWidth = (int)(500 * ratio);
                if (newWidth > 800)
                {
                    newWidth = 800;
                    double ratio2 = (double)img.Height / (double)img.Width;

                    newHeight = (int)(800 * ratio2);
                }

                img.Resize(newWidth, newHeight); // ToDo - Change the value of the width of the image on the screen
            }

            string fullFileName = Path.Combine(serverPath, fileName);

            if (System.IO.File.Exists(fullFileName))
            {
                System.IO.File.Delete(fullFileName);
            }

            img.Save(fullFileName);

            return Path.GetFileName(img.FileName);
        }

        private void CleanUpTempFolder(int hoursOld, HttpContext server) //string server)
        {
            try
            {
                DateTime fileCreationTime;
                DateTime currentUtcNow = DateTime.UtcNow;

                var serverPath = server.Server.MapPath("/Temp");
                if (Directory.Exists(serverPath))
                {
                    string[] fileEntries = Directory.GetFiles(serverPath);
                    foreach (var fileEntry in fileEntries)
                    {
                        fileCreationTime = System.IO.File.GetCreationTimeUtc(fileEntry);
                        var res = currentUtcNow - fileCreationTime;
                        if (res.TotalHours > hoursOld)
                        {
                            System.IO.File.Delete(fileEntry);
                        }
                    }
                }
            }
            catch
            {
            }
        }
    }
}