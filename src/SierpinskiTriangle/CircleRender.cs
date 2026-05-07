using System.Drawing;
using AnimatedGif;
using BigGustave;

namespace SierpinskiTriangle;

public static class CircleRender
{
    public static void Run()
    {
        const int maxCycles = 11;
        int width = (int)Math.Pow(2, maxCycles);
        int height = width;
        
        var black = new Pixel(0);

        using var gif = AnimatedGif.AnimatedGif.Create("mygif.gif", 200);

        for (int cycle = 0; cycle <= maxCycles; cycle++)
        {
            var builder = PngBuilder.Create(width, height, false);
            for (int i = 0; i < width; i++)
            for (int j = 0; j < height; j++)
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
            
            for (int x = 0; x < xPoints; x++)
            for (int y = 0; y < xPoints; y++)
            {
                var blockWidth = width / xPoints;
                
                var xCenter = x * blockWidth + (float)blockWidth / 2;
                var yCenter = y * blockWidth + (float)blockWidth / 2;

                if (xCenter * xCenter + yCenter * yCenter < width * width
                    && xCenter * xCenter + yCenter * yCenter > width * width / 4)
                {
                    //paint the square white
                    builder.DrawRectangle(white, 
                        new Point(x * blockWidth, y * blockWidth),
                        new Point((x + 1) * blockWidth - 1, (y + 1) * blockWidth - 1));
                }
            }
        }
    }
}