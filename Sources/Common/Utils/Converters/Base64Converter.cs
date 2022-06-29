namespace Utils.Converters
{
    public static class Base64Converter
    {
        public static async Task<string> ConvertFileToBase64(string path)
        {
            await using FileStream fsr = new(path, FileMode.Open);

            byte[] imageBytes = ReadFully(fsr);
            //Convert byte[] to Base64 String
            return Convert.ToBase64String(imageBytes);
        }

        public static async Task<byte[]> ConvertFileToBytes(string path)
        {
            await using FileStream fsr = new(path, FileMode.Open);

            return  ReadFully(fsr);
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