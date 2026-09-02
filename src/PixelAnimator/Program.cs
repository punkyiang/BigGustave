using System.Diagnostics;
using AnimatedGif;
using BigGustave;
using PixelAnimator;

var ogPng = Png.Open("danger sign.png");
var ogPngBuilder = PngBuilder.FromPng(ogPng);

var emptyFrame = PngBuilder.Create(ogPng.Width, ogPng.Height, ogPng.HasAlphaChannel);
var firstFrame = ogPngBuilder.GetFirstFrame();
var lastFrame = ogPngBuilder.GetLastFrame();

File.WriteAllBytes("1.png", emptyFrame.Save());
File.WriteAllBytes("2.png", firstFrame.Save());
File.WriteAllBytes("4.png", lastFrame.Save());


var gif = AnimatedGif.AnimatedGif.Create("result.gif", 250);
gif.AddFrame("1.png");
gif.AddFrame("2.png");
gif.AddFrame("danger sign.png");
gif.AddFrame("4.png");


    

//
// //open image viewer
// var psi = new ProcessStartInfo
// {
//     FileName = @"result.png",
//     UseShellExecute = true
// };
// Process.Start(psi);
