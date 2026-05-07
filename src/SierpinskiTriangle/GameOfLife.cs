namespace SierpinskiTriangle;

using System.Collections;
using System.Drawing;
using System.Drawing.Drawing2D;
using AnimatedGif;
using BigGustave;

public static class GameOfLife
{
    public static void Run()
    {
        int size = 17;
        const bool A = true;
        const bool O = false;
        
        var currentState = new BitArray([
            O, O, O, O, O, O, O, O, O, O, O, O, O, O, O, O, O,  
            O, O, O, O, O, O, O, O, O, O, O, O, O, O, O, O, O,  
            O, O, O, O, A, A, A, O, O, O, A, A, A, O, O, O, O,  
            O, O, O, O, O, O, O, O, O, O, O, O, O, O, O, O, O,  
            O, O, A, O, O, O, O, A, O, A, O, O, O, O, A, O, O,  
            O, O, A, O, O, O, O, A, O, A, O, O, O, O, A, O, O,  
            O, O, A, O, O, O, O, A, O, A, O, O, O, O, A, O, O,  
            O, O, O, O, A, A, A, O, O, O, A, A, A, O, O, O, O,  
            O, O, O, O, O, O, O, O, O, O, O, O, O, O, O, O, O,  
            O, O, O, O, A, A, A, O, O, O, A, A, A, O, O, O, O,  
            O, O, A, O, O, O, O, A, O, A, O, O, O, O, A, O, O,  
            O, O, A, O, O, O, O, A, O, A, O, O, O, O, A, O, O,  
            O, O, A, O, O, O, O, A, O, A, O, O, O, O, A, O, O,  
            O, O, O, O, O, O, O, O, O, O, O, O, O, O, O, O, O,  
            O, O, O, O, A, A, A, O, O, O, A, A, A, O, O, O, O,  
            O, O, O, O, O, O, O, O, O, O, O, O, O, O, O, O, O,  
            O, O, O, O, O, O, O, O, O, O, O, O, O, O, O, O, O
        ]);

        using var gif = AnimatedGif.Create("mygif.gif", 200);
        var black = new Pixel(0);
        var white = new Pixel(255);
        
        for (int cycle = 0; cycle < 3; cycle++)
        {
            var builder = PngBuilder.Create(18, 18, false);
            for (var i = 0; i < size; i++)
            {
                for (var j = 0; j < size; j++)
                {
                    builder.SetPixel(currentState[Coord(i, j, size)] ? white : black, i, j);
                    Console.Write(currentState[Coord(i, j, size)] ? '█' : ' ');
                }
                Console.WriteLine();
            }
            Console.WriteLine();

            var filename = $"file{cycle}.png";
            File.WriteAllBytes(filename, builder.Save());
            var img = Image.FromFile(filename);
            gif.AddFrame(img, quality: GifQuality.Grayscale);
            
            currentState = RunGameOfLife(size, currentState);
        }

    }

    public static BitArray RunGameOfLife(int size, BitArray currentState)
    {
        var nextState = new BitArray(currentState.Length, false);

        for (int x = 0; x < size; x++)
        for (int y = 0; y < size; y++)
        {
            var coord = Coord(x, y, size);
            nextState[coord] = NeighborSum(x, y) switch
            {
                3 => true,
                4 => currentState[coord],
                _ => false
            };
        }
        
        return nextState;

        int NeighborSum(int x, int y)
        {
            int sum = 0;

            for (var i = -1; i <= 1; i++)
            for (var j = -1; j <= 1; j++)
            {
                sum += currentState[Coord(x + i, y + j, size)] ? 1 : 0;
            }

            return sum;
        }

    }
    
    public static int Coord(int x, int y, int size)
    {
        x = (x + size) % size;
        y = (y + size) % size;
        return x + y * size;
    }
}