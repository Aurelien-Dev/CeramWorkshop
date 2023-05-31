using Domain.InterfacesWorker;
using ExternalServices.ServicesUploadImage;
using Microsoft.AspNetCore.Mvc;

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
            // try
            // {
            //     IEnumerable<ImageInstruction> imagesNotExported = await ApiWorker.ImageInstructionRepository.GetAllNonExported(cancellationToken);
            //     StringBuilder errors = new StringBuilder();
            //
            //     int filesTraited = 0;
            //     foreach (ImageInstruction item in imagesNotExported)
            //     {
            //         if (cancellationToken.IsCancellationRequested)
            //         {
            //             errors.AppendLine("Cancellation requested.");
            //             break;
            //         }
            //
            //         try
            //         {
            //             string path = Path.Combine(EnvironementSingleton.WebRootPath, item.UrlSmall);
            //             string webDirectory = WebPathHelper.GetDirectoryName(item.UrlSmall);
            //
            //             // SKBitmap original = SKBitmap.Decode(path);
            //             // SKBitmap resized = new SKBitmap(912, 912);
            //             // var dd = original.ScalePixels(resized.PeekPixels(), SKFilterQuality.Low);
            //             //
            //             // // Encodez l'image en un format spécifique (par exemple, en format PNG)
            //             // SKData encoded = resized.Encode(SKEncodedImageFormat.Png, 5);
            //             //
            //             // string newPathLarge = Path.Combine(Path.GetDirectoryName(path), $"{Path.GetFileNameWithoutExtension(path)}_large{Path.GetExtension(path)}");
            //             //
            //             // using (FileStream stream = new FileStream(newPathLarge, FileMode.OpenOrCreate))
            //             // {
            //             //     encoded.SaveTo(stream);
            //             // }
            //
            //
            //             SKBitmap bitmap = SKBitmap.Decode("file.png");
            //             var scaled = bitmap.Resize(new SKImageInfo(912, 912), SKBitmapResizeMethod.Lanczos3);
            //             
            //             SKImage image = SKImage.FromBitmap(bitmap);
            //             SKData png = image.Encode(SKImageEncodeFormat.Png, 100);
            //             using (var filestream = File.OpenWrite("image.png")) {
            //                 png.SaveTo(filestream);
            //             }
            //             
            //
            //             // await using (FileStream stream = new FileStream(path, FileMode.Open))
            //             // {
            //             //     using (Image image = await Image.LoadAsync(stream, cancellationToken))
            //             //     {
            //             //         // string newPathLarge = Path.Combine(Path.GetDirectoryName(path), $"{Path.GetFileNameWithoutExtension(path)}_large{Path.GetExtension(path)}");
            //             //         // image.Mutate(x => x.Resize(912, 912));
            //             //         // image.Save(newPathLarge, new PngEncoder()); //Remplacer l'encodeur Png par le format de fichier de votre choix
            //             //         // item.UrlLarge = WebPathHelper.Combine(webDirectory, Path.GetFileName(newPathLarge));
            //             //         //
            //             //         // string newPathMedium = Path.Combine(Path.GetDirectoryName(path), $"{Path.GetFileNameWithoutExtension(path)}_medium{Path.GetExtension(path)}");
            //             //         // image.Mutate(x => x.Resize(640, 640));
            //             //         // image.Save(newPathMedium, new PngEncoder()); //Remplacer l'encodeur Png par le format de fichier de votre choix
            //             //         // item.UrlMedium = WebPathHelper.Combine(webDirectory, Path.GetFileName(newPathMedium));
            //             //         //
            //             //         // string newPath = Path.Combine(Path.GetDirectoryName(path), $"{Path.GetFileNameWithoutExtension(path)}_small{Path.GetExtension(path)}");
            //             //         // image.Mutate(x => x.Resize(180, 180));
            //             //         // image.Save(newPath, new PngEncoder()); //Remplacer l'encodeur Png par le format de fichier de votre choix
            //             //         // item.UrlSmall = WebPathHelper.Combine(webDirectory, Path.GetFileName(newPath));
            //             //     }
            //             // }
            //
            //             // item.FileLocation = Location.ServerResized;
            //
            //             // await ApiWorker.ImageInstructionRepository.Update(item, cancellationToken);
            //             // await ApiWorker.Completed(cancellationToken);
            //             // LoadFileFromInputFile.RemoveFileInput(path);
            //
            //             ++filesTraited;
            //         }
            //         catch (Exception ex)
            //         {
            //             errors.AppendLine($"Error no file : {item.UrlSmall}");
            //             errors.AppendLine(ex.Message);
            //         }
            //     }
            //
            //     return new JsonResult(new { sucess = true, numberFiles = filesTraited, errors = errors.ToString() });
            // }
            // catch (Exception ex)
            // {
            //     return new JsonResult(new { sucess = false, error = ex.Message, stacktrace = ex.StackTrace });
            // }

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