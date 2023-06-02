using SkiaSharp;

namespace Client.Services;

public class ImageService
{
    public static string CreateImage(string appFolder, string fullFileName, string newName, int desiredWidth, SKEncodedImageFormat format)
    {
        using (FileStream stream = new FileStream(Path.Combine(appFolder, fullFileName), FileMode.OpenOrCreate))
        {
            // create the codec
            SKCodec codec = SKCodec.Create(stream);
            SKImageInfo info = codec.Info;

            // get the scale that is nearest to what we want (eg: jpg returned 512)
            SKSizeI supportedScale = codec.GetScaledDimensions((float)desiredWidth / info.Width);

            // decode the bitmap at the nearest size
            SKImageInfo nearest = new SKImageInfo(supportedScale.Width, supportedScale.Height);
            SKBitmap bmp = SKBitmap.Decode(codec, nearest);

            // now scale that to the size that we want
            float realScale = (float)info.Height / info.Width;
            SKImageInfo desired = new SKImageInfo(desiredWidth, (int)(realScale * desiredWidth));
            bmp = bmp.Resize(desired, SKBitmapResizeMethod.Lanczos3);

            SKImage image = SKImage.FromBitmap(bmp);
            SKData png = image.Encode(format, 100);

            // save
            using (var filestream = File.OpenWrite(Path.Combine(appFolder, $"{newName}.{format.ToString().ToLower()}")))
            {
                png.SaveTo(filestream);
            }

            return $"{newName}.{format.ToString().ToLower()}";
        }
    }
}