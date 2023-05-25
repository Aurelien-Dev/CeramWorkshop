using System.Text;
using Client.Utils;
using Domain.InterfacesWorker;
using Domain.Models.MainDomain;
using ExternalServices.ServicesUploadImage;
using Microsoft.AspNetCore.Mvc;
using Utils.Helpers;
using Utils.Singletons;

namespace Client.Controllers
{
    [Route("api/v1")]
    [ApiController]
    public class FileServiceController : ControllerBase
    {
        private IApiWorker ApiWorker { get; }
        private IImgBbService ImgBbService { get; }


        public FileServiceController(IApiWorker apiWorker, IImgBbService imgBbService)
        {
            ApiWorker = apiWorker;
            ImgBbService = imgBbService;
        }

        /// <summary>
        /// Permet de réduire la taille des images en passant par le service imgBB,
        /// initialement prvu d'être hébergé chez eux directement mais leur API n'est vraiment pas pratique
        /// </summary>
        /// <returns>success = truc if OK</returns>
        [HttpGet]
        [Route("SynchroImageFile")]
        public async Task<ActionResult> SynchroImageFileAsync(CancellationToken cancellationToken)
        {
            try
            {
                IEnumerable<ImageInstruction> imagesNotExported = await ApiWorker.ImageInstructionRepository.GetAllNonExported(cancellationToken);
                StringBuilder errors = new StringBuilder();

                int filesTraited = 0;
                foreach (var item in imagesNotExported)
                {
                    if (cancellationToken.IsCancellationRequested)
                    {
                        errors.AppendLine("Cancellation requested.");
                        break;
                    }

                    try
                    {
                        string path = Path.Combine(EnvironementSingleton.WebRootPath, item.Url);
                        (string full, string thumb, string medium) = await ImgBbService.UploadFile(path);

                        string localMedium = string.Empty;
                        if (!string.IsNullOrEmpty(medium))
                            localMedium = await ImgBbService.DownloadFile(medium, path);
                        else
                            localMedium = await ImgBbService.DownloadFile(full, path);

                        string directory = WebPathHelper.GetDirectoryName(item.Url);

                        if (string.IsNullOrEmpty(directory))
                            throw new InvalidOperationException("Directory path is null or empty.");

                        item.Url = WebPathHelper.Combine(directory, Path.GetFileName(localMedium));
                        item.FileLocation = Location.ImgBb;

                        await ApiWorker.ImageInstructionRepository.Update(item, cancellationToken);
                        await ApiWorker.Completed(cancellationToken);
                        LoadFileFromInputFile.RemoveFileInput(path);

                        ++filesTraited;
                    }
                    catch (Exception ex)
                    {
                        errors.AppendLine($"Error no file : {item.Url}");
                        errors.AppendLine(ex.Message);
                    }
                }

                return new JsonResult(new { sucess = true, numberFiles = filesTraited, errors = errors.ToString() });
            }
            catch (Exception ex)
            {
                return new JsonResult(new { sucess = false, error = ex.Message, stacktrace = ex.StackTrace });
            }
        }
    }
}