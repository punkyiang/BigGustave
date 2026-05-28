using System.Drawing;
using AnimatedGif;
using BigGustave;

namespace SierpinskiTriangle;

public static class TriangleGif
{
    public static void Run()
    {
        var filenamePattern = "file";
        
        if (!Directory.Exists(filenamePattern)) 
            Directory.CreateDirectory(filenamePattern);

        const int maxCycles = 11;
        int width = (int)Math.Pow(2, maxCycles);
        int height = width;
        
        var builder = PngBuilder.Create(width, height, true);
            
        var white = new Pixel(255);
        var transparent = new Pixel(0);


        // using var gif = AnimatedGif.AnimatedGif.Create("mygif.gif", 200);

        for (int index = 0; index < maxCycles; index++)
        {
            for (int j = 0; j < width; j++)
            for (int k = 0; k < height; k++)
                builder.SetPixel(white, j, k);


            DrawRecursively(0, new Point(width, 0), new Point(0, height), index);
            var filename = $"{filenamePattern}/filenamePattern{index}.png";
            var bytes = builder.Save();
            
            File.WriteAllBytes(filename, bytes);
            builder = PngBuilder.FromPngBytes(bytes);
            // var img = Image.FromFile(filename);
            // gif.AddFrame(img, quality: GifQuality.Grayscale);
        }
        
        return;
        
        void DrawRecursively(int cycle, Point outerCorner, Point innerCorner, int maxCycleCount) {
            if (cycle >= maxCycleCount) return;

            int minX = outerCorner.X;
            int minY = outerCorner.Y;
            int midX = (outerCorner.X + innerCorner.X) / 2;
            int midY = (outerCorner.Y + innerCorner.Y) / 2;
            int maxX = innerCorner.X;// < width ? innerCorner.X : width - 1;
            int maxY = innerCorner.Y;// < height ? innerCorner.Y : height - 1;
            
            int maxXToDraw = maxX < width ? maxX : width - 1;
            int maxYToDraw = maxY < height ? maxY : height - 1;
    
            // builder.DrawVerticalLine(white, midX, minY, midY);
            // builder.DrawHorizontalLine(white, minX, midX, midY);
    
            builder.DrawRectangle(transparent, new Point(midX, midY), new Point(maxXToDraw, maxYToDraw));

            DrawRecursively(cycle + 1, new Point(minX, minY), new Point(midX, midY), maxCycleCount);
            DrawRecursively(cycle + 1, new Point(midX, minY), new Point(maxX, midY), maxCycleCount);
            DrawRecursively(cycle + 1, new Point(minX, midY), new Point(midX, maxY), maxCycleCount);
        }
    }
}

public record Point(int X, int Y);

public static class PngExtensions
{
    public static void DrawHorizontalLine(this PngBuilder builder, Pixel color, int xFrom, int xTo, int y)
    {
        var minX = xFrom < xTo ? xFrom : xTo;
        var maxX = xFrom > xTo ? xFrom : xTo;
        for (var x = minX; x <= maxX; x++) builder.SetPixel(color, x, y);
    }
    
    public static void DrawVerticalLine(this PngBuilder builder, Pixel color, int x, int yFrom, int yTo)
    {
        var minY = yFrom < yTo ? yFrom : yTo;
        var maxY = yFrom > yTo ? yFrom : yTo;
        for (var y = minY; y <= maxY; y++) builder.SetPixel(color, x, y);
    }
    
    public static void DrawRectangle(this PngBuilder builder, Pixel color, Point from, Point to)
    {
        var minX = from.X < to.X ? from.X : to.X;
        var maxX = from.X > to.X ? from.X : to.X;
        var minY = from.Y < to.Y ? from.Y : to.Y;
        var maxY = from.Y > to.Y ? from.Y : to.Y;

        for (var x = minX; x <= maxX; x++)
        for (var y = minY; y <= maxY; y++) 
            builder.SetPixel(color, x, y);
    }
}