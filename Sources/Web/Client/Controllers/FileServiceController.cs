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
        public async Task<ActionResult> SynchroImageFile()
        {
            try
            {
                IEnumerable<ImageInstruction> imagesNotExported = await ApiWorker.ImageInstructionRepository.GetAllNonExported();
                StringBuilder errors = new StringBuilder();

                foreach (var item in imagesNotExported)
                {
                    try
                    {
                        string path = Path.Combine(EnvironementSingleton.WebRootPath, item.Url);
                        (_, string medium, _) = await ImgBbService.UploadFile(path);
                        string localMedium = await ImgBbService.DownloadFile(medium, path);

                        string directory = WebPathHelper.GetDirectoryName(item.Url);

                        if (string.IsNullOrEmpty(directory))
                            throw new InvalidOperationException("Directory path is null or empty.");

                        item.Url = WebPathHelper.Combine(directory, Path.GetFileName(localMedium));
                        item.FileLocation = Location.ImgBb;

                        await ApiWorker.ImageInstructionRepository.Update(item);

                        LoadFileFromInputFile.RemoveFileInput(path);
                    }
                    catch (Exception ex)
                    {
                        errors.AppendLine($"Error no file : {item.Url}");
                        errors.AppendLine(ex.Message);
                    }
                }

                return new JsonResult(new { sucess = true, errors = errors.ToString() });
            }
            catch (Exception ex)
            {
                return new JsonResult(new { sucess = false, error = ex.Message, stacktrace = ex.StackTrace });
            }
        }
    }
}