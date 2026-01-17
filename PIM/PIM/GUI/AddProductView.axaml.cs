using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Npgsql;
using PIM.Data;
using PIM.Data.Queries;

namespace PIM;

public partial class AddProductView : UserControl
{
    ProductsAPI productsAPI;
    
    private List<string> _availableTags = new();
    
    public AddProductView()
    {
        InitializeComponent();
        MyComboBox.ItemsSource = _availableTags;
        productsAPI = ProductsAPI.GetProductAPI();
        LoadCategoriesFromDatabase();
    }
    
    private async Task LoadCategoriesFromDatabase()
    {   // connection to API
        
        DatabaseConnection connection = productsAPI.GetConnection();
        
        if (connection == null) 
        {
            throw new Exception("Cannot access method without supplying DatabaseConnection first");
        }
        if (!connection.isConnected())
        {
            throw new Exception("Cannot access method without starting DatabaseConnection first \n(Use method StartAsync)");
        }
        
        
        _availableTags.Clear(); // Clear local cache
        
        
        Dictionary<string, object?> param = new Dictionary<string, object?>(){ };

        GetCategoryNamesQuery query = new GetCategoryNamesQuery(param);

        _availableTags = await connection.ExecuteQueryAsync<string>(query); // Executing the query via (DatabaseConnection) connection
        
        MyComboBox.ItemsSource = null;
        MyComboBox.ItemsSource = _availableTags;
    }
    
    public static async Task InsertProductAsync(
        string name, string manufacturer, float retail_price, float sales_price_vat,
        float height, float width, float depth, float weight, string color, string long_d,
        List<string> categories)
    {
        List<object?> param = new List<object?>();
        param.Add(name);
        param.Add(manufacturer);
        param.Add(retail_price);
        param.Add(sales_price_vat);
        param.Add(height);
        param.Add(width);
        param.Add(depth);
        param.Add(weight);
        param.Add(color);
        param.Add(long_d);
        
        AddData data = new AddData(param, categories);

        await data.AddDataNow();

        /*
        using var connection = await GetConnectionAsync();
        */ /*
        int newSKU;
        // Insert product
        using (var cmd = new NpgsqlCommand(@"
        INSERT INTO products (name, manufacturer, retail_price, sales_price_vat, height, width, depth, weight, color, long_descr)
        VALUES (@nam, @manu, @ret_price, @sale_price_var, @hei, @wid, @dep, @wei, @col, @long_d) RETURNING sku;", connection))
        {
            cmd.Parameters.AddWithValue("@nam", name);
            cmd.Parameters.AddWithValue("@manu", manufacturer);
            cmd.Parameters.AddWithValue("@ret_price", retail_price);
            cmd.Parameters.AddWithValue("@sale_price_var", sales_price_vat);
            cmd.Parameters.AddWithValue("@hei", height);
            cmd.Parameters.AddWithValue("@wid", width);
            cmd.Parameters.AddWithValue("@dep", depth);
            cmd.Parameters.AddWithValue("@wei", weight);
            cmd.Parameters.AddWithValue("@col", color);
            cmd.Parameters.AddWithValue("@long_d", long_d);

            newSKU = (int)(await cmd.ExecuteScalarAsync())!;
        }
        */
        // Map product to categories
        /*
        foreach (string cat in categories)
        {
            // Get category ID
            using var catCmd = new NpgsqlCommand("SELECT id FROM categories WHERE category = @name", connection);
            catCmd.Parameters.AddWithValue("@name", cat);
            int catId = (int)(await catCmd.ExecuteScalarAsync())!;

            // Insert into types
            using var typeCmd = new NpgsqlCommand("INSERT INTO types (product_sku, category_id) VALUES (@sku, @catid)", connection);
            typeCmd.Parameters.AddWithValue("@sku", newSKU);
            typeCmd.Parameters.AddWithValue("@catid", catId);
            await typeCmd.ExecuteNonQueryAsync();
        }
        */
    }
    
    public async void Add_Product(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        try
        {
            string insert_name = P_name.Text;
            string inser_manu = P_manu.Text;
            float insert_retail_price = float.Parse(P_retprice.Text);
            float insert_sales_price_var = float.Parse(P_saleprice_vat.Text);
            float insert_hei = float.Parse(P_height.Text);
            float insert_wid = float.Parse(P_width.Text);
            float insert_dep = float.Parse(P_depth.Text);
            float insert_wei = float.Parse(P_weight.Text);
            string insert_col = P_color.Text;
            string insert_long_d = P_long_d.Text;

            // Split and trim categories
            List<string> selectedCategories = SelectedBox.Text?
                .Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                .ToList() ?? new List<string>();

            await InsertProductAsync(insert_name, inser_manu, insert_retail_price, insert_sales_price_var,
                insert_hei, insert_wid, insert_dep, insert_wei, insert_col, insert_long_d, selectedCategories);

            Results.Text = $"Inserted product: {insert_name}";
        }
        catch (Exception ex)
        {
            Results.Text = $"Error: {ex.Message}";
        }
    }
    
    private async void NewCategory(object? sender, RoutedEventArgs e)
    {
        string? newTag = NewTagTextBox?.Text?.Trim();

        if (string.IsNullOrEmpty(newTag))
        {
            NewTagConfirm.Text = "Please enter a valid tag";
            return;
        }
        
        if (!string.IsNullOrEmpty(newTag))
        {
            try
            {
                await productsAPI.AddCategory(newTag);
                await LoadCategoriesFromDatabase(); // Refresh UI
                NewTagConfirm.Text = $"Created tag: {newTag}";
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
    
    

    private void ClearHandler(object? sender, RoutedEventArgs e)
    {
        P_name.Text = "";
        P_manu.Text = "";
        P_retprice.Text = "";
        P_saleprice_vat.Text = "";
        P_height.Text = "";
        P_width.Text = "";
        P_depth.Text = "";
        P_weight.Text = "";
        P_color.Text = "";
        P_long_d.Text = "";
    }

    private void MyComboBox_SelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        if (SelectedBox != null)
        {
            // Split existing values into a list
            var currentValues = string.IsNullOrEmpty(SelectedBox.Text)
                ? new List<string>()
                : SelectedBox.Text
                    .Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                    .ToList();

            // Get the selected tag from the ComboBox
            if (MyComboBox.SelectedItem is string selected && !currentValues.Contains(selected))
            {
                currentValues.Add(selected);
                SelectedBox.Text = string.Join(", ", currentValues);

                AddTagToAvailableTags(selected);
            }
        }
        else
        {
            Console.WriteLine("SelectedBox is null");
        }

    }
    
    private async void ClearTags_Click(object? sender, RoutedEventArgs e)
    {
        SelectedBox.Text = "";
        await LoadCategoriesFromDatabase();
    }

    private void AddTagToAvailableTags(string newTag)
    {
        // Add the new tag to _availableTags if it's not already present
        if (!_availableTags.Contains(newTag))
        {
            _availableTags.Add(newTag);
            // Rebind the ComboBox to the updated _availableTags list
            MyComboBox.ItemsSource = null; // Clear existing binding
            MyComboBox.ItemsSource = _availableTags; // Rebind the updated list
        }
    }

    private void BackButton(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        MainWindow main = (MainWindow)this.VisualRoot;
        TabItem productsTab = main.FindControl<TabItem>("ViewProductsTab");
        if (productsTab.Content is ViewAllProducts viewAllProducts)
        {
            viewAllProducts.ProductsContent.Content = new ViewAllProducts();
        }
    }
}