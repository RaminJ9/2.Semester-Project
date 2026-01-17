using System;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using OMS_GUI.ViewModels;
// Joachim Low
namespace OMS_GUI
{
    public class ViewLocator : IDataTemplate
    {

        public Control? Build(object? param)
        {
            if (param is null)
                return null;

            var name = param.GetType().FullName!.Replace("ViewModel", "View", StringComparison.Ordinal);
            var type = Type.GetType(name);

            if (type is null)
            {
                return null;
            }

            var control = (Control)Activator.CreateInstance(type)!;
            control.DataContext = param;
            return control;
        }

        public bool Match(object? data)
        {
            return data is ViewModelBase;
        }
    }
}
