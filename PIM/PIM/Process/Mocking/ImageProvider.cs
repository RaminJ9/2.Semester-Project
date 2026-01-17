using System;
using static System.IO.File;

namespace PIM;


// Mocking a picture into the picture identifier

public class ImageProvider : IDamPictureProvider
{
    public byte[] GetPicture(int productId)
    {
        
        string path = @"C:\TestNag\11.png.png";

        Console.WriteLine("Attempting to load: " + path);
        Console.WriteLine(path);
        if (Exists(path))
        {
            Console.WriteLine(path+" : Exists");
            return ReadAllBytes(path);

        }
        else
        {
            Console.WriteLine(path + " : Not Exists");
        }
        return null;
        
    }
    
    
}
