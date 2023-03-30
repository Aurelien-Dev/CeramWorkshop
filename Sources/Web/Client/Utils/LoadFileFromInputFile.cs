using Common.Utils.Singletons;
using Microsoft.AspNetCore.Components.Forms;
using System.Diagnostics;

namespace Client.Utils
{
    public static class LoadFileFromInputFile
    {
        private static readonly long maxFileSize = 1024 * 1024 * 15;
        private static readonly string RootPath = EnvironementSingleton.WebRootPath;
        private static readonly string ProductFolderFullPath = Path.Combine(RootPath, "ProductImages");
        private static readonly string ProductFolderShort = Path.Combine("ProductImages");

        public static void RemoveFileInput(string path)
        {
            if (string.IsNullOrEmpty(path)) return;

            try
            {
                File.Delete(Path.Combine(RootPath, path));
            }
            catch (Exception)
            {
                //File does not exist
            }
        }

        /// <summary>
        /// Upload file into server
        /// </summary>
        /// <param name="e">InputFile event argument</param>
        /// <returns>Return path of uploaded img</returns>
        public static async Task<string> LoadFileInput(InputFileChangeEventArgs e, string workshopName)
        {
            string fileName = CreateTempFileName(workshopName, Path.GetExtension(e.File.Name));
            string workshopFolder = Path.Combine(ProductFolderFullPath, workshopName, fileName);

            if (!Directory.Exists(Path.Combine(ProductFolderFullPath, workshopName)))
                Directory.CreateDirectory(Path.Combine(ProductFolderFullPath, workshopName));

            await using FileStream fs = new(workshopFolder, FileMode.Create);
            await e.File.OpenReadStream(maxFileSize).CopyToAsync(fs);
            fs.Close();

            return Path.Combine(ProductFolderShort, workshopName, fileName);
        }

        /// <summary>
        /// Create URI for temporary file in wwwroot/assets
        /// </summary>
        /// <param name="workshopName">Name of workshop</param>
        /// <param name="extension">Original extension file</param>
        /// <returns>Return completed path for asset folder on server</returns>
        private static string CreateTempFileName(string workshopName, string extension)
        {
            var trustedFileNameForFileStorage = $"{workshopName}-{Path.GetRandomFileName()}";
            trustedFileNameForFileStorage = Path.ChangeExtension(trustedFileNameForFileStorage, extension);
            return trustedFileNameForFileStorage;
        }
    }
}