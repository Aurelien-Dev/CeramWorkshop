namespace Utils.Converters
{
    public static class Base64Converter
    {

        /// <summary>
        /// Mockable method
        /// Convert a file in Base64
        /// </summary>
        /// <param name="path">Local path of a file</param>
        /// <returns>Return Base64 string</returns>
        public static Func<string, Task<string>> ConvertFileToBase64Func { get; set; } = async (s) => await ConvertFileToBase64(s);


        /// <summary>
        /// Convert a file in Base64
        /// </summary>
        /// <param name="path">Local path of a file</param>
        /// <returns>Return Base64 string</returns>
        private static async Task<string> ConvertFileToBase64(string path)
        {
            await using FileStream fsr = new(path, FileMode.Open);

            byte[] imageBytes = ReadFully(fsr);

            return Convert.ToBase64String(imageBytes);
        }

        /// <summary>
        /// Read all content of file input
        /// </summary>
        /// <param name="input">File input</param>
        /// <returns>Return all content file in Byte[]</returns>
        private static byte[] ReadFully(Stream input)
        {
            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }
    }
}