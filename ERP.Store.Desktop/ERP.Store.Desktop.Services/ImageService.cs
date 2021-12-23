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

                #pragma warning disable CA1416 // Validar a compatibilidade da plataforma
                
                var bmp = new Bitmap(image);

                bmp.Save(memoryStream, image.RawFormat);

                #pragma warning restore CA1416 // Validar a compatibilidade da plataforma

                byte[] imageBytes = memoryStream.ToArray();

                return Convert.ToBase64String(imageBytes);
            }
            catch (Exception) { throw; }
        }
    }
}
