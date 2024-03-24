using BuiesnesLogic.Entities;

namespace Trach.Utility
{
    public class Converter
    {
        public static byte[]? IFormFileToImage(IFormFile image)
        {
            byte[]? result = null;
            if (image != null)
            {
                long fileSize = image.Length;
                string type = image.ContentType;
                if (fileSize > 0)
                {
                    using (var stream = new MemoryStream())
                    {

                        image.CopyTo(stream);
                        result = stream.ToArray();
                    }
                }
            }
            return result;
        } 
    }
}
