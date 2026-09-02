using BigGustave;

namespace PixelAnimator;

public static class Enlarger
{
    public static PngBuilder Enlarge(this PngBuilder file, int scale)
    {
        var newFile = PngBuilder.Create(file.Width * scale, file.Height * scale, file.HasAlphaChannel);
        var oldPng = Png.Open(file.Save());
        
        for (var y = 0; y < file.Height * scale; y++)
        for (var x = 0; x < file.Width * scale; x++)
            newFile.SetPixel(oldPng.GetPixel(x / scale, y / scale), x, y);

        return newFile;
    }
}