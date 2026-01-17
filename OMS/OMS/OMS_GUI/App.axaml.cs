using System.Linq;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;
using OMS_GUI.ViewModels;
using OMS_GUI.Views;
using Avalonia.Media.Imaging;
using Avalonia.Utilities;
using Avalonia.Platform;
using System.IO;
using System.Reflection;
using System;
using System.Threading.Tasks;


namespace OMS_GUI
{
    public partial class App : Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
            DataTemplates.Add(new ViewLocator());
        }

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                // Avoid duplicate validations from both Avalonia and the CommunityToolkit. 
                // More info: https://docs.avaloniaui.net/docs/guides/development-guides/data-validation#manage-validationplugins
                DisableAvaloniaDataAnnotationValidation();
                var loginWindow = new LoginWindow();
                var loginWindowViewModel = new LoginWindowViewModel();

                loginWindow.DataContext = loginWindowViewModel;
                desktop.MainWindow = loginWindow;
            }

            base.OnFrameworkInitializationCompleted();
        }

        private static void DisableAvaloniaDataAnnotationValidation()
        {
            // Get an array of plugins to remove
            var dataValidationPluginsToRemove =
                BindingPlugins.DataValidators.OfType<DataAnnotationsValidationPlugin>().ToArray();

            // remove each entry found
            foreach (var plugin in dataValidationPluginsToRemove)
            {
                BindingPlugins.DataValidators.Remove(plugin);
            }
        }
    }
}