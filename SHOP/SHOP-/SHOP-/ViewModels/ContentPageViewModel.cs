using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using SHOP.Data;
using SHOP.Interfaces;
using SHOP.Models;

namespace SHOP.ViewModels;

public partial class ContentPageViewModel() : PageViewModel(ApplicationPageNames.Content)
{

    private IShopDbService _shopDbService;
    
    public string ContentName { get; set; }
    
    public string ContentText { get; set; }

    

    
    private readonly MainWindowViewModel _main;

    
    public ContentPageViewModel(MainWindowViewModel main, string contentName, IShopDbService shopDbService) : this()
    {
        _main = main;
        ContentName = contentName;
        _shopDbService = shopDbService;
    }
    
    public async Task LoadContentAsync() 
    {
        await _shopDbService.SetConnection();
        var contents = await _shopDbService.GetAllContents();
        
        Console.WriteLine(contents);
        
        this.ContentText = contents[this.ContentName];



    }
    
}
    
