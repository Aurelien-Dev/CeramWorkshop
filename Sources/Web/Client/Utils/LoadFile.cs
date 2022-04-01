using Common.Utils.Singletons;
using Microsoft.AspNetCore.Components.Forms;

namespace Client.Utils
{
    public static class LoadFile
    {
        private static long maxFileSize = 1024 * 1024 * 15;

        /// <summary>
        /// Upload file into server
        /// </summary>
        /// <param name="e">InputFile event argument</param>
        /// <returns>Return path of uploaded img</returns>
        public static async Task<string> LoadFileInput(InputFileChangeEventArgs e, string workshopName)
        {
            string path = CreateUriTempFile(workshopName);

            await using FileStream fs = new(path, FileMode.Create);
            await e.File.OpenReadStream(maxFileSize).CopyToAsync(fs);
            fs.Close();

            return path;
        }

        private static string CreateUriTempFile(string workshopName)
        {
            var trustedFileNameForFileStorage = $"{workshopName}-{Path.GetRandomFileName()}";
            var path = Path.Combine(EnvironementSingleton.WebRootPath, "assets", trustedFileNameForFileStorage);
            return path;
        }

    }
}
