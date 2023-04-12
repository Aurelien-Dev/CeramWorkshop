namespace ExternalServices.ServicesUploadImage
{
    public interface IImgBbService
    {
        /// <summary>
        /// Upload image on imgBB services
        /// </summary>
        /// <param name="filePath">Path of original image</param>
        /// <exception cref="UploadFileException">Exception when not able to convert image to base64 string</exception>
        /// <exception cref="ApiCallErrorException">Exception after calling API, depending status</exception>
        /// <returns>return three URL : image, image thumb, image medium</returns>
        Task<(string, string, string)> UploadFile(string filePath);

        /// <summary>
        /// Download file from imgBB
        /// </summary>
        /// <param name="urlMedium">Url of imgBB image</param>
        /// <param name="path">Path of local file</param>
        /// <returns>New medium file path</returns>
        Task<string> DownloadFile(string urlMedium, string path);
    }
}