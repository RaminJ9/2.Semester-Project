using Avalonia;
using DAM;
using System;

namespace DAM_1;

/// <summary>
/// Initialization code. Don't use any Avalonia, third-party APIs or any <br/>
/// SynchronizationContext-reliant code before AppMain is called: things aren't initialized <br/>
/// yet and stuff might break.
/// </summary>
class Program
{
    
    [STAThread]
    public static void Main(string[] args)
    {
        InitializeDatabase();

        BuildAvaloniaApp().StartWithClassicDesktopLifetime(args);
    }
    // Avalonia configuration, don't remove; also used by visual designer.
    public static AppBuilder BuildAvaloniaApp()
        => AppBuilder.Configure<App>()
            .UsePlatformDetect()
            .WithInterFont()
            .LogToTrace();
    
    /// <summary>
    /// This creates the database tables and populate it with data
    /// </summary>
    private static void InitializeDatabase()
    {
        try
        {
            FileDatabaseService dbService = new FileDatabaseService("Host=localhost;Port=5432;Database=2nd_sem-2-DAM;Username=2nd_sem_user;Password=Password"); // Fully PostgreSQL-backed
            Console.WriteLine("Database initialized successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Database initialization failed: {ex.Message}");
        }
    }

}