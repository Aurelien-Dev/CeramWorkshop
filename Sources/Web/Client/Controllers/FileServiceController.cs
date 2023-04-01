using Client.Utils;
using Common.Utils.Singletons;
using Domain.InterfacesWorker;
using Domain.Models.MainDomain;
using ExternalServices.ServicesUploadImage;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace Client.Controllers
{
    [Route("api/v1")]
    [ApiController]
    public class FileServiceController : ControllerBase
    {
        private IApiWorker _apiWorker { get; }
        private IImgBBService _imgBBService { get; }


        public FileServiceController(IApiWorker apiWorker, IImgBBService imgBBService)
        {
            _apiWorker = apiWorker;
            _imgBBService = imgBBService;
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
                IEnumerable<ImageInstruction> imagesNotExported = await _apiWorker.ImageInstructionRepository.GetAllNonExported();
                StringBuilder errors = new StringBuilder();

                foreach (var item in imagesNotExported)
                {
                    try
                    {

                        string path = Path.Combine(EnvironementSingleton.WebRootPath,  item.Url);
                        (_, string medium, _) = await _imgBBService.UploadFile(path);
                        string localMedium = await _imgBBService.DownloadFile(medium, path);

                        string directory = Path.GetDirectoryName(item.Url);

                        if (string.IsNullOrEmpty(directory))
                            throw new InvalidOperationException("Directory path is null or empty.");

                        item.Url = Path.Combine(directory, Path.GetFileName(localMedium));
                        item.FileLocation = Location.ImgBB;

                        _apiWorker.ImageInstructionRepository.Update(item);
                        _apiWorker.Completed();

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
                _apiWorker.Rollback();
                return new JsonResult(new { sucess = false, error = ex.Message, stacktrace = ex.StackTrace });
            }
        }
    }
}