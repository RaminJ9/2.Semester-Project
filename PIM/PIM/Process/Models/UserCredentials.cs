using System;
using System.Collections.Generic;
using System.IO;
using PIM.Data;

public class UserCredentials
{
    public static string filePath = new FindPath("credentials.csv").GetPath(); 
        
    public static UserCredentials credentials; // Declaring variable of type UserCredential class, without initialization
    public static string username = "postgres";
    public static string password = "abodeh";
    public static string ConnectionString; // Declare without initialization
    
    private Dictionary<string, string> _credentials; // Dictionairy der skal indeholde username og passowrd som strings


    public UserCredentials(string filePath)
    {
        _credentials = new Dictionary<string, string>(); // Når et instans af klassen "UserCredentials" er dannet initialiseres Dictionairy
        if (File.Exists(filePath))
        {
            LoadCredentials(filePath); // using the the lower method ("LoadCredentials"), to the filepath given.
        }
        else
        {
            Console.WriteLine("File does not exist");
            _credentials["postgres"] = "postgres"; // if file does not exist, it will create a new file with username and password ["postgres", "postgres"]
        }
    }
    
    
     private void LoadCredentials(string filePath) // metode for file reader
     {
         try
         {
             foreach (var line in File.ReadLines(filePath))
             {
                 var values = line.Split(','); // values er en variabel, som splitter en string op i 2 og bliver lavet om til et array, fordi den indeholder flere værdier.
                 if (values.Length == 2) // Fordi Values nu er et array, kan man tage dets længde. og fordi dens længde er 2 kører den vidre.
                 {
                     _credentials[values[0].Trim()] = values[1].Trim(); // Værdierne gemmes i credentials dictionairy, "Trim" gør at ting som mellemrum(ekstra space) ikke kommer med.
                     // plus Valuess[0] er key og values[1] er value i dictionairy.
                     string username = values[0].Trim();
                     string password = values[1].Trim();
                     _credentials[username] = password;
                 }
                 else
                 {
                     Console.WriteLine("Value amount in credentials file is invalid. \n Please change to two values."); // hvis forkerte mængde af values i "credentials.csv"
                 }

             }
         }
         catch (Exception ex) // hvis der kommer en fejl som at hvis man ikke har navngivet filen ordenligt.
         {
             Console.WriteLine("Error reading file: " + ex.Message);
         }
     }
     
    
       public string GetPassword(string username) // metoden der tager password
       {
           if (_credentials.TryGetValue(username, out string? password)) // den tager username og retunere password
           {
               return password ?? "";
           }
           else
           {
               Console.WriteLine("User not found"); // hvis der ikke er en person med det username
               return "";
           }
       }
      
   
}
