using Avalonia.Controls;
using PIM.Data;
using PIM.Data.Queries;
using PIM.Models;
using PIM.Process.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace PIM;

public partial class ViewAllProducts : UserControl
{
    ProductsAPI productsAPI;
    ObservableCollection<ProductInternalDisplay> displayedProducts = new(); // The collection that holds all products to be shown
    ItemsControl productsItemControl; // The actual AXAML obejct

    HashSet<ProductInternalDisplay> selectedProducts = new();

    static int selectedFilterIndex = 0;
    static int selectedSortIndex = 0;
    static int selectedSortOrderIndex = 0;

    bool manualSelectionDisabled = false;
    public ViewAllProducts()
    {
        InitializeComponent();
        productsAPI = ProductsAPI.GetProductAPI();
        InitializeWindow();
    }

    private async void InitializeWindow()
    {
        manualSelectionDisabled = true;
        productsItemControl = this.FindControl<ItemsControl>("ProductsItemControl");
        productsItemControl.ItemsSource = displayedProducts; // Bind product collection to XAML

        List<Category> categories = Category.FromDictionary(await productsAPI.GetCategories());
        categories.Insert(0, new Category(-1, "None"));
        CategoryCombo.ItemsSource = categories;
        CategoryCombo.SelectedIndex = selectedFilterIndex;

        List<string> sortByOptions = new List<string>()
        {
            "Sku",
            "Name",
            "Price",
        };
        SortCombo.ItemsSource = sortByOptions;
        SortCombo.SelectedIndex = selectedSortIndex;

        SortOrderCombo.ItemsSource = new List<string>()
        {
            "Ascending",
            "Descending"
        };
        SortOrderCombo.SelectedIndex = selectedSortOrderIndex;
        manualSelectionDisabled = false;
        SearchButtonClick(null, null);
    }
    private void SearchOptionsChanged(object? sender, Avalonia.Controls.SelectionChangedEventArgs e) 
    {
        if (manualSelectionDisabled)
        {
            return;
        }
        SearchButtonClick(sender, e);
    }

    private async void SearchButtonClick(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        displayedProducts.Clear();
        string? selectedCategory = CategoryCombo.SelectedItem.ToString();
        string? filterTerm = selectedCategory == "None" ? null : selectedCategory; // if  selectedItem is "None" then filterTerm = null
        
        string? searchTerm = SearchBar.Text;

        string? sortTerm = SortCombo.SelectedItem.ToString().ToLower();
        string? sortBy = SortOrderCombo.SelectedItem.ToString() == "Ascending" ? "ASC" : "DESC";
        List<ProductDisplay> products = await productsAPI.FindProducts(filterTerm, searchTerm, sortTerm, sortBy); // Fetch products        
        RefillCollection(products);
    }


    private async void DeleteSelected(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {

        List<DeleteSingleQuery> queries = new();

        foreach(ProductInternalDisplay product in selectedProducts)
        {
            Dictionary<string, object?> param = new Dictionary<string, object?>();
            param.Add("@id", product.Sku);
            queries.Add(new DeleteSingleQuery(param));

        }
       
        DatabaseConnection connection = productsAPI.GetConnection();
        bool querySucces = await connection.ExecuteQueriesAsync(queries);

        if (!querySucces)
        {
            return;
        }

        foreach(ProductInternalDisplay product in selectedProducts)
        {
            displayedProducts.Remove(product);
        }
        selectedProducts.Clear();
    }


    private void CheckboxClicked(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        CheckBox checkbox = (CheckBox)sender!;
        ProductInternalDisplay product = (ProductInternalDisplay)(checkbox).DataContext!;
        bool? isChecked = checkbox.IsChecked;

        if (isChecked == null)
        {
            return;
        } 
        else if (isChecked == true)
        {
            selectedProducts.Add(product);
        } else
        {
            selectedProducts.Remove(product);
        }
    }
    private void RefillCollection(List<ProductDisplay> list)
    {
        foreach (ProductDisplay p in list)
        {
            displayedProducts.Add( (ProductInternalDisplay) p);
        }

    }

    private async void EditClick(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        ProductInternalDisplay product = (ProductInternalDisplay)((Button)sender!).DataContext!; // Casting to get actual product
        UpdateProductView updateView =  new UpdateProductView(product.Sku);
        ProductsContent.Content = updateView;
        await updateView.ShowcaseInformation();
        updateView.OnUpdateProductClick();
    }

    private async void ImageClick(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        ProductInternalDisplay product = (ProductInternalDisplay)((Button)sender!).DataContext!; // Casting to get actual product
        PictureIdentifierView pictureIdentifierView = new PictureIdentifierView(product.Sku);
        ProductsContent.Content = pictureIdentifierView;
        await pictureIdentifierView.ClickHandlerShowPicture();
    }

    private void NewProductButton(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        AddProductView addProductView = new AddProductView();
        ProductsContent.Content = addProductView;
    }

    private void FilterChanged(object? sender, Avalonia.Controls.SelectionChangedEventArgs e)
    {
        selectedFilterIndex = ((ComboBox)sender!).SelectedIndex;
        SearchOptionsChanged(sender, e);
    }

    private void SortChanged(object? sender, Avalonia.Controls.SelectionChangedEventArgs e)
    {
        selectedSortIndex = ((ComboBox)sender!).SelectedIndex;
        SearchOptionsChanged(sender, e);
    }

    private void SortOrderChanged(object? sender, Avalonia.Controls.SelectionChangedEventArgs e)
    {
        selectedSortOrderIndex = ((ComboBox)sender!).SelectedIndex;
        SearchOptionsChanged(sender, e);
    }
}