namespace Utils.Converters
{
    public static class Base64Converter
    {

        public static Func<string, Task<string>> ConvertFileToBase64Func { get; set; } = async (s) => await ConvertFileToBase64(s);


        private static async Task<string> ConvertFileToBase64(string path)
        {
            await using FileStream fsr = new(path, FileMode.Open);

            byte[] imageBytes = ReadFully(fsr);
            //Convert byte[] to Base64 String
            return Convert.ToBase64String(imageBytes);
        }

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