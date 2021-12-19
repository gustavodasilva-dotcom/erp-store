using System;
using System.IO;
using System.Drawing;

namespace ERP.Store.Desktop.Services
{
    public class ImageService
    {
        public string ConvertImageToBase64(Image image)
        {
            try
            {
                using var memoryStream = new MemoryStream();
                
                image.Save(memoryStream, image.RawFormat);

                byte[] imageBytes = memoryStream.ToArray();

                return Convert.ToBase64String(imageBytes);
            }
            catch (Exception) { throw; }
        }
    }
}
