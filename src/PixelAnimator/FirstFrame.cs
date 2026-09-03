using BigGustave;

namespace PixelAnimator;

public static class FirstFrame
{
    public static PngBuilder GetFirstFrame(this Png file)
    {
        var white = new Pixel(255, 255, 255);
        var black = new Pixel(0, 0, 0);

        //detect pixel islands
        //set pixel type enum
        //colour the whole canvas black
        //paint in the white bits


        var pixelArray = new PixelEdge[file.Width, file.Height];
        const PixelEdge fullPixel = PixelEdge.Up | PixelEdge.Down | PixelEdge.Left | PixelEdge.Right
                                    | PixelEdge.UpLeft | PixelEdge.UpRight | PixelEdge.DownLeft | PixelEdge.DownRight;

        //do a colour check and copy data
        for (var y = 0; y < file.Height; y++)
        for (var x = 0; x < file.Width; x++)
        {
            var pixel = file.GetPixel(x, y);
            if (!pixel.Equals(black) && !pixel.Equals(white))
                throw new Exception($"Incorrect color found: {pixel} at [{y}, {x}]");

            if (pixel.Equals(white)) pixelArray[x, y] = fullPixel;
        }

        //set already known pixel edges
        for (var y = 0; y < file.Height; y++)
        {
            for (var x = 0; x < file.Width; x++)
            {
                //sides
                if (x == 0 || pixelArray[x - 1, y] == 0)
                    pixelArray[x, y] &= fullPixel ^ PixelEdge.Left;

                if (x == file.Width - 1 || pixelArray[x + 1, y] == 0)
                    pixelArray[x, y] &= fullPixel ^ PixelEdge.Right;

                if (y == 0 || pixelArray[x, y - 1] == 0)
                    pixelArray[x, y] &= fullPixel ^ PixelEdge.Up;

                if (y == file.Height - 1 || pixelArray[x, y + 1] == 0)
                    pixelArray[x, y] &= fullPixel ^ PixelEdge.Down;
                
                //diagonals
                if (x == 0 || y == 0 || pixelArray[x - 1, y - 1] == 0)
                    pixelArray[x, y] &= fullPixel ^ PixelEdge.UpLeft;

                if (x == file.Width - 1 || y == 0 || pixelArray[x + 1, y - 1] == 0)
                    pixelArray[x, y] &= fullPixel ^ PixelEdge.UpRight;

                if (x == 0 || y == file.Height - 1 || pixelArray[x - 1, y + 1] == 0)
                    pixelArray[x, y] &= fullPixel ^ PixelEdge.DownLeft;

                if (x == file.Width - 1 || y == file.Height - 1 || pixelArray[x + 1, y + 1] == 0)
                    pixelArray[x, y] &= fullPixel ^ PixelEdge.DownRight;
                

                if (pixelArray[x, y] == 0)
                    Console.Write("    ");
                else
                    Console.Write(((int)pixelArray[x, y]).ToString("D3") + ' ');
            }

            Console.WriteLine();
        }

        Console.WriteLine();

        var builder = PngBuilder.Create(file.Width * 2, file.Height * 2, false);

        //paint in the new pixels based on edge data
        for (var y = 0; y < file.Height; y++)
        for (var x = 0; x < file.Width; x++)
        {
            var pixel = pixelArray[x, y];

            builder.SetPixel(pixel.HasFlag(PixelEdge.Up | PixelEdge.Left | PixelEdge.UpLeft) ? white : black, x * 2, y * 2); //TODO: check the diagonals too
            builder.SetPixel(pixel.HasFlag(PixelEdge.Up) ? white : black, x * 2 + 1, y * 2);
            builder.SetPixel(pixel.HasFlag(PixelEdge.Left) ? white : black, x * 2, y * 2 + 1);
            // builder.SetPixel(pixel.HasFlag(PixelEdge.Down | PixelEdge.Right) ? white : black, x * 2 + 1, y * 2 + 1);
            builder.SetPixel(pixel > 0 ? white : black, x * 2 + 1, y * 2 + 1);
        }

        return builder;
    }
}