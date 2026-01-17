using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SHOP.Data;
using SHOP.Factories;
using SHOP.Interfaces;
using SHOP.Models;
using SHOP.Services;
using SHOP.Views;

namespace SHOP.ViewModels;

public partial class HomePageViewModel() : PageViewModel(ApplicationPageNames.Home)
{
    private readonly MainWindowViewModel _main;
    
    public CategoryViewModel CategoryVm { get; }
    
    public ContentPagesViewModel ContentVm { get; }

    
    
    [ObservableProperty] private ObservableCollection<ContentModel> _contentsList;

    
    
    public HomePageViewModel(MainWindowViewModel main) : this() 
    {
        _main = main;
        CategoryVm = new CategoryViewModel(main); // todo: check where to run the Async function LoadCategories!
        ContentVm = new ContentPagesViewModel(main);
    }

    /*public async Task LoadContents() 
    {
        var contents = await shopDBService.GetAllContents();
        Console.WriteLine(contents);
        

        _contentsList = new ObservableCollection<ContentModel>();

        ContentModel cm;
        
        foreach (var content in contents)
        {
            cm = new ContentModel(content.Key, content.Value);
            
            _contentsList.Add(cm);
             
        }
        
    }*/

}