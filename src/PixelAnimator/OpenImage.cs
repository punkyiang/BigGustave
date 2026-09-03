namespace PixelAnimator;

using System.Diagnostics;

public static class OpenImage
{
    public static async Task Open(string path)
    {
        var fullPath = Path.GetFullPath(path);
        
        if (!File.Exists(fullPath))
            throw new FileNotFoundException("Image not found.", fullPath);

        if (OperatingSystem.IsMacOS())
        {
            using var process = Process.Start(new ProcessStartInfo
            {
                FileName = "/usr/bin/qlmanage",
                UseShellExecute = true,
                ArgumentList = { "-p", fullPath }
            });

            await process?.WaitForExitAsync()!;
            Console.WriteLine("Quick Look exit");
            
        }
        else if (OperatingSystem.IsWindows())
        {
            var psi = new ProcessStartInfo
            {
                FileName = path,
                UseShellExecute = true
            };
            Process.Start(psi);
        }
        else
        {
            throw new NotImplementedException();
        }


    }
}