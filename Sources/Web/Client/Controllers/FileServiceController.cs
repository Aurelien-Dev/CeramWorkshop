using Client.Utils;
using Common.Utils.Singletons;
using Domain.InterfacesWorker;
using Domain.Models.MainDomain;
using ExternalServices.ServicesUploadImage;
using ExternalServices.ServicesUploadImage.Model;
using Microsoft.AspNetCore.Mvc;
using System.Drawing;
using System.Security.Policy;

namespace Client.Controllers
{
    [Route("api/v1")]
    [ApiController]
    public class FileServiceController : ControllerBase
    {
        private IApiWorker _apiWorker { get; } = default!;
        private IImgBBService _imgBBService { get; } = default!;


        public FileServiceController(IApiWorker apiWorker, IImgBBService imgBBService)
        {
            _apiWorker = apiWorker;
            _imgBBService = imgBBService;
        }

        [HttpGet]
        [Route("SynchroImageFile")]
        public async Task<ActionResult> SynchroImageFile()
        {
            try
            {
                IEnumerable<ImageInstruction> imagesNotExported = await _apiWorker.ImageInstructionRepository.GetAllNonExported();

                foreach (var item in imagesNotExported)
                {
                    try
                    {
                        string path = Path.Combine(EnvironementSingleton.WebRootPath, item.Url.Replace("/", @"\"));
                        (string url, string thumb, string medium) = await _imgBBService.UploadFile(path);
                        string localMedium = await _imgBBService.DownloadFile(medium, path);


                        item.Url = Path.Combine(Path.GetDirectoryName(item.Url), Path.GetFileName(localMedium));
                        item.FileLocation = Location.ImgBB;

                        _apiWorker.ImageInstructionRepository.Update(item);
                        _apiWorker.Completed();

                        LoadFileFromInputFile.RemoveFileInput(path);
                    }
                    catch (Exception)
                    {
                        //On passe au suivant
                    }
                }

                return new JsonResult(new { sucess = true });
            }
            catch (Exception ex)
            {
                _apiWorker.Rollback();
                return new JsonResult(new { sucess = false, error = ex.Message, stacktrace = ex.StackTrace });
            }
        }
    }
}