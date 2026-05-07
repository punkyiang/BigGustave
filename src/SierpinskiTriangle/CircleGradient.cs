namespace SierpinskiTriangle;

using System.Drawing;
using AnimatedGif;
using BigGustave;

public class CircleGradient
{
    public static void Run()
    {
        const int maxCycles = 11;
        int width = (int)Math.Pow(2, maxCycles);

        var black = new Pixel(0);

        using var gif = AnimatedGif.Create("mygif.gif", 200);

        for (int cycle = 0; cycle <= maxCycles; cycle++)
        {
            var builder = PngBuilder.Create(width, width, false);
            for (int i = 0; i < width; i++)
            for (int j = 0; j < width; j++)
                builder.SetPixel(black, i, j);

            DrawRecursively(builder, cycle);
            var filename = $"file{cycle}.png";
            File.WriteAllBytes(filename, builder.Save());
            var img = Image.FromFile(filename);
            gif.AddFrame(img, quality: GifQuality.Grayscale);
        }
        
        return;
        
        void DrawRecursively(PngBuilder builder, int cycle)
        {
            var white = new Pixel(255);

            var xPoints = (int)Math.Pow(2, cycle);
            
            for (int x = 0; x < width; x++)
            for (int y = 0; y < width; y++)
            {
                if (x * x + y * y < width * width
                    && x * x + y * y > width * width / 3)
                {
                    var reach = (Math.Sqrt(x * x + y * y) - width / 2) / (width / 2);
                    builder.SetPixel(new Pixel((byte)(63 + (63 * reach))), x, y);
                }
            }
        }
    }
}