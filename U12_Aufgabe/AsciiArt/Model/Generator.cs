using System.Text;
using SixLabors.ImageSharp.PixelFormats;

namespace U12.Model;

/// <summary>
/// Erzeugt ASCII Art für Bildes. 
/// </summary>
/// <remarks>
/// Dieser Code basiert auf der Arbeit von Sean Sexton, wurde aber durch Thomas Kälin im
/// Jahr 2022 umgeschrieben, damit er mit .NET Core kompatibel ist. Dazu war es nötig,
/// verschiedene Teile des Codes auf die Verwendung von ImageSharp anzupassen.
///
/// Den ursprünglichen Code von Sean Sexton findet sich hier:
/// http://csharp.2000things.com/2012/12/25/743-ascii-art-generator/
///
/// Details zu ImageSharp finden sich hier:
/// https://docs.sixlabors.com/articles/imagesharp/index.html?tabs=tabid-1
///
/// Eine mögliche Alternative könnte das GitHub-Projekt "Asceils" sein:
/// https://github.com/nikvoronin/Asceils
/// </remarks>
public class Generator
{
    // Typical width/height for ASCII characters
    private const double FontAspectRatio = 0.6;

    // Available character set, ordered by decreasing intensity (brightness)
    private static string OutputCharSet => OutputCharSet2;

    // ReSharper disable UnusedMember.Local
    private const string OutputCharSet1 = "@%#*+=-:. ";
    private const string OutputCharSet2 = "MNFVI*:.";
    private const string OutputCharSet3 = "$@B%8&WM#*oahkbdpqwmZO0QLCJUYXzcvunxrjft/\\|()1{}[]?-_+~<>i!lI;:,\"^`'. ";
    // ReSharper restore UnusedMember.Local

    /// <summary>
    /// creates an ascii art image [outputWidth] characters wide
    /// from the given input image 
    /// </summary>
    /// <param name="imagePath"></param>
    /// <param name="outputWidth"></param>
    /// <returns></returns>
    public string GenerateFrom(string imagePath, int outputWidth)
    {
        var imageFileBytes = File.ReadAllBytes(imagePath);

        using var image = SixLabors.ImageSharp.Image.Load<Rgba32>(imageFileBytes);

        // we cannot produce more characters per line than the actual number of pixels
        outputWidth = Math.Min(outputWidth, image.Width);

        // pixelChunkWidth/pixelChunkHeight - size of a chunk of pixels that will
        // map to 1 character.  These are doubles to avoid progressive rounding
        // error.
        var pixelChunkWidth = image.Width / (double)outputWidth;
        var pixelChunkHeight = pixelChunkWidth / FontAspectRatio;

        // Calculate output height to capture entire image
        var outputHeight = (int)Math.Round(image.Height / pixelChunkHeight);

        // Generate output image, row by row
        var pixelOffSetTop = 0.0;
        var sbOutput = new StringBuilder();

        for (var row = 1; row <= outputHeight; row++)
        {
            var pixelOffSetLeft = 0.0;

            for (var col = 1; col <= outputWidth; col++)
            {
                // Calculate brightness for this set of pixels by averaging
                // brightness across all pixels in this pixel chunk
                var brightSum = 0.0;
                var pixelCount = 0;
                for (var pixelLeftInd = 0; pixelLeftInd < (int)pixelChunkWidth; pixelLeftInd++)
                for (var pixelTopInd = 0; pixelTopInd < (int)pixelChunkHeight; pixelTopInd++)
                {
                    // Calculate a brightness-value for each pixel between 0.0 and 1.0
                    var x = (int)pixelOffSetLeft + pixelLeftInd;
                    var y = (int)pixelOffSetTop + pixelTopInd;
                    if (x < image.Width && y < image.Height)
                    {
                        var pixel = image[x, y];
                        var brightness = (pixel.R / 255d + pixel.G / 255d + pixel.B / 255d) / 3;
                        brightSum += brightness;
                        pixelCount++;
                    }
                }

                // Average brightness for this entire pixel chunk, between 0.0 and 1.0
                var pixelChunkBrightness = brightSum / pixelCount;

                // Target character is just relative position in ordered set of output characters
                var outputIndex = (int)Math.Floor(pixelChunkBrightness * OutputCharSet.Length);
                if (outputIndex == OutputCharSet.Length)
                    outputIndex--;

                var targetChar = OutputCharSet[outputIndex];

                sbOutput.Append(targetChar);

                pixelOffSetLeft += pixelChunkWidth;
            }

            sbOutput.AppendLine();
            pixelOffSetTop += pixelChunkHeight;
        }

        return sbOutput.ToString();
    }
}