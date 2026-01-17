using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PIM.Data;
using SHOP.Data;
using SHOP.Models;

namespace SHOP.ViewModels;

public partial class CategoryViewModel() : PageViewModel(ApplicationPageNames.Category)
{

    private CategoryModel _categoryModel;

    // Reference to the main VM or a navigation service (injected)
    // This is needed if the CategoryViewModel itself handles navigation
    private readonly MainWindowViewModel _mainViewModel; // Or a dedicated INavigationService


    [ObservableProperty]
    private ObservableCollection<string> _categories; // todo: change the type <string> --> too <CategoryModel>
    
    // Constructor
    public CategoryViewModel(MainWindowViewModel mainViewModel) : this()
    {

        _mainViewModel = mainViewModel;
        _categoryModel = new CategoryModel();
        Categories =
            new ObservableCollection<string>(_categoryModel
                .GetSampleCategories()); //todo: remove after integrating with PIM

    }


    // --- Command for Navigation --- //
    // This command will be triggered when the category card is clicked
    [RelayCommand]
    public void NavigateToProducts(string categoryName)
    {
        // Tell the MainWindowViewModel (or navigation service) to navigate
        // to the Products page, passing this category's ID.
        _mainViewModel.NavigateToProductsCommand.Execute(categoryName);
    }

    // todo: this method should be uncommented when PIM implements API call for the categories.
    
   /* public async Task LoadCategoriesAsync()
    {
        var categories = await CategoriesAPI.GetCategories(); // get all categories from the table!

        Categories = new ObservableCollection<CategoryModel>();

        CategoryModel cm;

        foreach (var category in Categories)
        {
            cm = new CategoryModel(category.name);

            Categories.Add(cm);

        }
    }
    */
}
