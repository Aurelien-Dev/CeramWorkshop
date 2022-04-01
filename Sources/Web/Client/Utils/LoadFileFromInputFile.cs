using Common.Utils.Singletons;
using Microsoft.AspNetCore.Components.Forms;

namespace Client.Utils
{
    public static class LoadFileFromInputFile
    {
        private static long maxFileSize = 1024 * 1024 * 15;

        /// <summary>
        /// Upload file into server
        /// </summary>
        /// <param name="e">InputFile event argument</param>
        /// <returns>Return path of uploaded img</returns>
        public static async Task<string> LoadFileInput(InputFileChangeEventArgs e, string workshopName)
        {
            string path = CreateUriTempFile(workshopName, Path.GetExtension(e.File.Name));
            
            await using FileStream fs = new(path, FileMode.Create);
            await e.File.OpenReadStream(maxFileSize).CopyToAsync(fs);
            fs.Close();

            return path;
        }

        /// <summary>
        /// Create URI for temporary file in wwwroot/assets
        /// </summary>
        /// <param name="workshopName">Name of workshop</param>
        /// <param name="extension">Original extension file</param>
        /// <returns>Return completed path for asset folder on server</returns>
        private static string CreateUriTempFile(string workshopName, string extension)
        {
            var trustedFileNameForFileStorage = $"{workshopName}-{Path.GetRandomFileName()}";
            var path = Path.Combine(EnvironementSingleton.WebRootPath, "assets", trustedFileNameForFileStorage);
            path = Path.ChangeExtension(path, extension);
            return path;
        }
    }
}