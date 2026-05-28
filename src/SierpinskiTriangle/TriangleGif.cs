using System.Drawing;
using AnimatedGif;
using BigGustave;

namespace SierpinskiTriangle;

public static class TriangleGif
{
    public static void Run()
    {
        const int maxCycles = 11;
        int width = (int)Math.Pow(2, maxCycles);
        int height = width;


        // using var gif = AnimatedGif.AnimatedGif.Create("mygif.gif", 200);

        for (int index = 0; index < 5; index++)
        {
            var builder = PngBuilder.Create(width, height, false);

            var black = new Pixel(255);
            var white = new Pixel(0);

            for (int j = 0; j < width; j++)
            for (int k = 0; k < height; k++)
                builder.SetPixel(black, j, k);

            DrawRecursively(0, new Point(0, 0), new Point(width, height), 4, index);
            var filename = $"file{index}.png";
            File.WriteAllBytes(filename, builder.Save());
            // var img = Image.FromFile(filename);
            // gif.AddFrame(img, quality: GifQuality.Grayscale);
            
            void DrawRecursively(int cycle, Point outerCorner, Point innerCorner, int squareToIgnore, int maxCycleCount) {
                if (cycle >= maxCycleCount) return;

                int minX = outerCorner.X;
                int minY = outerCorner.Y;
                int midX = (outerCorner.X + innerCorner.X) / 2;
                int midY = (outerCorner.Y + innerCorner.Y) / 2;
                int maxX = innerCorner.X < width ? innerCorner.X : width - 1;
                int maxY = innerCorner.Y < height ? innerCorner.Y : height - 1;
    
                // builder.DrawVerticalLine(white, midX, minY, midY);
                // builder.DrawHorizontalLine(white, minX, midX, midY);
    
                builder.DrawRectangle(white, new Point(midX, midY), new Point(maxX, maxY));

                if (squareToIgnore != 1) DrawRecursively(cycle + 1, new Point(minX, minY), new Point(midX, midY), squareToIgnore, maxCycleCount);
                if (squareToIgnore != 2) DrawRecursively(cycle + 1, new Point(midX, minY), new Point(maxX, midY), squareToIgnore, maxCycleCount);
                if (squareToIgnore != 3) DrawRecursively(cycle + 1, new Point(minX, midY), new Point(midX, maxY), squareToIgnore, maxCycleCount);
                if (squareToIgnore != 4) DrawRecursively(cycle + 1, new Point(midX, midY), new Point(maxX, maxY), squareToIgnore, maxCycleCount);
            }
        }
        
        return;
    }

}

public record Point(int X, int Y);

public static class PngExtensions
{
    public static void DrawHorizontalLine(this PngBuilder builder, Pixel color, int xFrom, int xTo, int y)
    {
        for (var x = xFrom; x <= xTo; x++) builder.SetPixel(color, x, y);
    }
    
    public static void DrawVerticalLine(this PngBuilder builder, Pixel color, int x, int yFrom, int yTo)
    {
        for (var y = yFrom; y <= yTo; y++) builder.SetPixel(color, x, y);
    }
    
    public static void DrawRectangle(this PngBuilder builder, Pixel color, Point from, Point to)
    {
        for (var x = from.X; x <= to.X; x++)
        for (var y = from.Y; y <= to.Y; y++) 
            builder.SetPixel(color, x, y);
    }
}