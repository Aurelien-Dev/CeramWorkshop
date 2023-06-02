using System.Text;
using Client.Services;
using Client.Utils;
using Domain.InterfacesWorker;
using Domain.Models.MainDomain;
using ExternalServices.ServicesUploadImage;
using Microsoft.AspNetCore.Mvc;
using SkiaSharp;
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

        private ImageSizingConfiguration[] filesTypeSize = new[]
        {
            new ImageSizingConfiguration("small", 180, 180),
            new ImageSizingConfiguration("medium", 640, 640),
            new ImageSizingConfiguration("large", 912, 912)
        };


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
                foreach (ImageInstruction item in imagesNotExported)
                {
                    if (cancellationToken.IsCancellationRequested)
                    {
                        errors.AppendLine("Cancellation requested.");
                        break;
                    }

                    try
                    {
                        string webFolder = WebPathHelper.GetDirectoryName(item.Url);
                        string appFolder = Path.Combine(EnvironementSingleton.WebRootPath, webFolder);
                        string originalFileName = Path.GetFileName(item.Url);

                        string newFileNameMedium = $"{Path.GetFileNameWithoutExtension(originalFileName)}_medium";
                        string newFileNameCompressMedium = ImageService.CreateImage(appFolder, originalFileName, newFileNameMedium, 640, SKEncodedImageFormat.Webp);
                        item.UrlMedium = $"{webFolder}/{newFileNameCompressMedium}";

                        string newFileName = $"{Path.GetFileNameWithoutExtension(originalFileName)}_large";
                        string newFileNameCompress = ImageService.CreateImage(appFolder, originalFileName, newFileName, 912, SKEncodedImageFormat.Webp);
                        item.Url = $"{webFolder}/{newFileNameCompress}";

                        item.FileLocation = Location.ServerResized;

                        await ApiWorker.ImageInstructionRepository.Update(item, cancellationToken);
                        await ApiWorker.Completed(cancellationToken);
                        
                        LoadFileFromInputFile.RemoveFileInput(Path.Combine(appFolder, originalFileName));

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

            return new JsonResult(new { sucess = false });
        }
    }

    public class ImageSizingConfiguration
    {
        public string Name { get; set; }
        public int MaxWidth { get; set; }
        public int MaxHeight { get; set; }
        public bool RespectRatio { get; set; }

        public ImageSizingConfiguration(string name, int maxWidth, int maxHeight)
        {
            Name = name;
            MaxWidth = maxWidth;
            MaxHeight = maxHeight;
            RespectRatio = true;
        }
    }
}