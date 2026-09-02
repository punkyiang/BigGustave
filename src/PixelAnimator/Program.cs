using BigGustave;
using PixelAnimator;

Console.WriteLine("Hello, World!");

var ogPng = Png.Open("danger sign.png");

var white = new Pixel(255, 255, 255);
var black = new Pixel(0, 0, 0);

//detect pixel islands
//set pixel type enum
//colour the whole canvas black
//paint in the white bits


var pixelArray = new PixelEdge[ogPng.Width, ogPng.Height];
var fullPixel = PixelEdge.Up | PixelEdge.Down | PixelEdge.Left | PixelEdge.Right;

//do a colour check and copy data
for (var i = 0; i < ogPng.Width; i++)
{
    for (var j = 0; j < ogPng.Height; j++)
    {
        var pixel = ogPng.GetPixel(i, j);
        if (!pixel.Equals(black) && !pixel.Equals(white))
        {
            Console.WriteLine($"Incorrect color found: {pixel} at [{i}, {j}]");
            return;
        }

        if (pixel.Equals(white))
        {
            pixelArray[i, j] = fullPixel;

            Console.Write("#");
        }
        else
        {
            Console.Write(" ");
        }
    }

    Console.WriteLine();
}

Console.WriteLine();


//set already known pixel edges
for (var i = 0; i < ogPng.Width; i++)
{
    for (var j = 0; j < ogPng.Height; j++)
    {
        if (i == 0 || pixelArray[i - 1, j] == 0)
            pixelArray[i, j] &= fullPixel ^ PixelEdge.Left;

        if (i == ogPng.Width - 1 || pixelArray[i + 1, j] == 0)
            pixelArray[i, j] &= fullPixel ^ PixelEdge.Right;

        if (j == 0 || pixelArray[i, j - 1] == 0)
            pixelArray[i, j] &= fullPixel ^ PixelEdge.Up;

        if (j == ogPng.Height - 1 || pixelArray[i, j + 1] == 0)
            pixelArray[i, j] &= fullPixel ^ PixelEdge.Down;

        Console.Write(((int)pixelArray[i, j]).ToString("D2") + ' ');
    }
    Console.WriteLine();
}
Console.WriteLine();

var builder = PngBuilder.Create(ogPng.Width * 2, ogPng.Height * 2, false);

for (var i = 0; i < ogPng.Width; i++)
{
    for (var j = 0; j < ogPng.Height; j++)
    {
        var pixel = pixelArray[i, j];

        if (pixel.HasFlag(PixelEdge.Up) || pixel.HasFlag(PixelEdge.Left))
            SetPixelLocal(builder, white, i * 2, j * 2);
        else
            SetPixelLocal(builder, black, i * 2, j * 2);

        if (pixel.HasFlag(PixelEdge.Up) || pixel.HasFlag(PixelEdge.Right))
            SetPixelLocal(builder, white, i * 2 + 1, j * 2);
        else
            SetPixelLocal(builder, black, i * 2 + 1, j * 2);

        if (pixel.HasFlag(PixelEdge.Down) || pixel.HasFlag(PixelEdge.Left))
            SetPixelLocal(builder, white, i * 2, j * 2 + 1);
        else
            SetPixelLocal(builder, black, i * 2, j * 2 + 1);

        // if (pixel.HasFlag(PixelEdge.Down) || pixel.HasFlag(PixelEdge.Right))
        if (pixel > 0)
            SetPixelLocal(builder, white, i * 2 + 1, j * 2 + 1);
        else
            SetPixelLocal(builder, black, i * 2 + 1, j * 2 + 1);
    }
    
    Console.WriteLine();
}
File.WriteAllBytes("result.png", builder.Save());

void SetPixelLocal(PngBuilder builder, Pixel pixel, int x, int y)
{
    builder.SetPixel(pixel, x, y);
    Console.Write(pixel.Equals(white) ? "#" : " ");
}