using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace OMS_GUI.Views;

public partial class OrderChangerWindowView : UserControl
{
    public OrderChangerWindowView()
    {
        InitializeComponent();
    }
    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}