using System.Diagnostics;
using BigGustave;
using PixelAnimator;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Gif;
using SixLabors.ImageSharp.PixelFormats;

foreach (var value in args)
{
    Console.WriteLine($"value: {value}");
}

var ogPng = Png.Open("brackets.png");

const int scale = 2;

var emptyFrame = PngBuilder.Create(ogPng.Width * 2 * scale, ogPng.Height * 2 * scale, ogPng.HasAlphaChannel);
var firstFrame = ogPng.GetFirstFrame().Enlarge(scale);
var lastFrame = ogPng.GetLastFrame().Enlarge(scale);
var bigSign = ogPng.Enlarge(2 * scale);

File.WriteAllBytes("1.png", emptyFrame.Save());
File.WriteAllBytes("2.png", firstFrame.Save());
File.WriteAllBytes("3.png", bigSign.Save());
File.WriteAllBytes("4.png", lastFrame.Save());


var frames = new[]
{
    "1.png",
    "2.png",
    "3.png",
    "4.png",
};

using var gif = Image.Load<Rgba32>(frames[0]);
gif.Frames.RootFrame.Metadata
    .GetGifMetadata()
    .FrameDelay = 25;

foreach (var path in frames.Skip(1))
{
    var source = Image.Load<Rgba32>(path);
    source.Frames.RootFrame.Metadata.GetGifMetadata().FrameDelay = 25;
    gif.Frames.AddFrame(source.Frames.RootFrame);
}

var encoder = new GifEncoder();

const string resultPath = "result.gif";
gif.Save(resultPath, encoder);


// //open image viewer
// var psi = new ProcessStartInfo
// {
//     FileName = resultPath,
//     UseShellExecute = true
// };
// Process.Start(psi);
await OpenImage.Open(resultPath);