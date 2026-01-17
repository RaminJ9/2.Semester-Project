using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;

namespace PIM
{
    public partial class App : Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                var foo = App.Current.Resources;
                var icon = new WindowIcon("../../../Assets/icon.ico"); 
                desktop.MainWindow = new MainWindow
                {
                    Icon = icon
                };
            }

            base.OnFrameworkInitializationCompleted();
        }

    }
}

