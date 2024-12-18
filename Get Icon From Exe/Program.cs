using System;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;

public class Program
{
    [DllImport("Shell32.dll")]
    static extern int ExtractIconEx(string lpszFile, int nIconIndex, IntPtr[] phiconLarge, IntPtr[] phiconSmall, int nIcons);

    [DllImport("user32.dll")]
    static extern bool DestroyIcon(IntPtr hIcon);

    public static void RunMain(string arg)
    {
        Console.WriteLine(arg);
    }
    public static void Main(string[] args)
    {
        Console.WriteLine("Usage: IconsExtractor.exe <filePath> <outputPath>");
        if (args.Length > 0)
        {
            try
            {
                string filePath = Path.GetFullPath(args[0]);
                string outputPath = Path.GetFullPath(args[1]);
                string fileType = ".png";

                if (!Directory.Exists(outputPath))
                {
                    Directory.CreateDirectory(outputPath);
                }

                int iconCount = ExtractIconEx(filePath, -1, null, null, 0);

                IntPtr[] largeIconHandles = new IntPtr[iconCount];
                IntPtr[] smallIconHandles = new IntPtr[iconCount];

                ExtractIconEx(filePath, 0, largeIconHandles, smallIconHandles, iconCount);

                for (int i = 0; i < iconCount; i++)
                {
                    Icon largeIcon = Icon.FromHandle(largeIconHandles[i]);
                    Icon smallIcon = Icon.FromHandle(smallIconHandles[i]);

                    // Save to PNG:
                    if (fileType == ".png")
                    {
                        string largeIconFilePath = Path.Combine(outputPath, $"{i}_l.png");
                        string smallIconFilePath = Path.Combine(outputPath, $"{i}_s.png");
                        largeIcon.ToBitmap().Save(largeIconFilePath);
                        smallIcon.ToBitmap().Save(smallIconFilePath);
                    }

                    //// Save to ICO:                                                           // Colors artifacts 
                    //if (fileType == ".ico")
                    //{
                    //    string largeIconFilePath = Path.Combine(outputPath, $"{i}_large.ico");
                    //    string smallIconFilePath = Path.Combine(outputPath, $"{i}_small.ico");

                    //    using (FileStream fs = new FileStream(largeIconFilePath, FileMode.Create))
                    //    {
                    //        largeIcon.Save(fs);
                    //    }
                    //    using (FileStream fs = new FileStream(smallIconFilePath, FileMode.Create))
                    //    {
                    //        smallIcon.Save(fs);
                    //    }
                    //}

                    // Destroy the icon handles
                    DestroyIcon(largeIconHandles[i]);
                    DestroyIcon(smallIconHandles[i]);
                }
                Console.WriteLine($"Successfully extracted {largeIconHandles.Length} large icons.");
                Console.WriteLine($"Successfully extracted {smallIconHandles.Length} small icons.");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}