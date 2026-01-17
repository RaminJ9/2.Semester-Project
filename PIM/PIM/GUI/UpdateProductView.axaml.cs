using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Npgsql;
using PIM.Access.Queries;
using PIM.Data;
using PIM.Data.Queries;
using PIM.Models;

namespace PIM;

public partial class UpdateProductView : UserControl
{
    ProductsAPI productsAPI;
    int selectedSKu;
    public UpdateProductView(int sku)
    {
        productsAPI = ProductsAPI.GetProductAPI();
        selectedSKu = sku;
        InitializeComponent();
    }
    
    
    public static async Task<NpgsqlConnection> GetConnectionAsync()
    {
        var connection = new NpgsqlConnection(UserCredentials.ConnectionString);
        await connection.OpenAsync();
        return connection;
        
    }
    // Show product info
    public async Task ShowcaseInformation()
    {
        var sku = $"{selectedSKu}";
        var textBoxMessageDisplayer = this.FindControl<TextBox>("TextBoxMessageDisplayer");
        try
        {
            //Read Only
            this.FindControl<TextBox>("TextBoxShowName").IsReadOnly = true;
            this.FindControl<TextBox>("TextBoxShowManufacturer").IsReadOnly = true;
            this.FindControl<TextBox>("TextBoxShowRetailPrice").IsReadOnly = true;
            this.FindControl<TextBox>("TextBoxShowSalesPriceVAT").IsReadOnly = true;
            this.FindControl<TextBox>("TextBoxShowSalesPrice").IsReadOnly = true;
            this.FindControl<TextBox>("TextBoxShowHeight").IsReadOnly = true;
            this.FindControl<TextBox>("TextBoxShowWidth").IsReadOnly = true;
            this.FindControl<TextBox>("TextBoxShowDepth").IsReadOnly = true;
            this.FindControl<TextBox>("TextBoxShowWeight").IsReadOnly = true;
            this.FindControl<TextBox>("TextBoxShowColor").IsReadOnly = true;
            this.FindControl<TextBox>("TextBoxShowLongDescription").IsReadOnly = true;



            

            if (string.IsNullOrWhiteSpace(sku))
            {
                textBoxMessageDisplayer.Text = "ProductId is required";
                return;
            }

            if (int.TryParse(sku, out int productId))
            {


                DatabaseConnection connection = productsAPI.GetConnection();
                if (connection == null)
                {
                    throw new Exception(
                        "Cannot access method \"FindProducts\" without supplying DatabaseConnection first");
                }

                if (!connection.isConnected())
                {
                    throw new Exception(
                        "Cannot access method \"FindProducts\" without starting DatabaseConnection first \n(Use method StartAsync)");
                }


                Dictionary<string, object?> param = new Dictionary<string, object?>()
                {
                    { "@sku", productId }
                };
                GetEditableQuery getEditableQuery = new GetEditableQuery(param);
                List<ProductEditable> productList =
                    await connection.ExecuteQueryAsync<ProductEditable>(getEditableQuery);
                ProductEditable product = productList[0];



                this.FindControl<TextBox>("TextBoxShowName").Text = product.name;
                this.FindControl<TextBox>("TextBoxShowManufacturer").Text = product.manufacturer;
                this.FindControl<TextBox>("TextBoxShowRetailPrice").Text = product.retailPrice.ToString();
                this.FindControl<TextBox>("TextBoxShowSalesPriceVAT").Text = product.priceInclVAT.ToString();
                this.FindControl<TextBox>("TextBoxShowSalesPrice").Text = product.priceExclVAT.ToString();
                this.FindControl<TextBox>("TextBoxShowHeight").Text = product.size[0].ToString();
                this.FindControl<TextBox>("TextBoxShowWidth").Text = product.size[1].ToString();
                this.FindControl<TextBox>("TextBoxShowDepth").Text = product.size[2].ToString();
                this.FindControl<TextBox>("TextBoxShowWeight").Text = product.weight.ToString();
                this.FindControl<TextBox>("TextBoxShowColor").Text = product.color;
                this.FindControl<TextBox>("TextBoxShowLongDescription").Text = product.desc;



                textBoxMessageDisplayer.Text = "Information shown";
            }
            else
            {
                textBoxMessageDisplayer.Text = $"Incorrect input format,\nProductId is a number.";
            }
        }
        catch
        {

            this.FindControl<TextBox>("TextBoxShowName").Text = null;
            this.FindControl<TextBox>("TextBoxShowManufacturer").Text = null;
            this.FindControl<TextBox>("TextBoxShowRetailPrice").Text = null;
            this.FindControl<TextBox>("TextBoxShowSalesPriceVAT").Text = null;
            this.FindControl<TextBox>("TextBoxShowSalesPrice").Text = null;
            this.FindControl<TextBox>("TextBoxShowWidth").Text = null;
            this.FindControl<TextBox>("TextBoxShowDepth").Text = null;
            this.FindControl<TextBox>("TextBoxShowWeight").Text = null;
            this.FindControl<TextBox>("TextBoxShowColor").Text = null;
            this.FindControl<TextBox>("TextBoxShowHeight").Text = null;
            this.FindControl<TextBox>("TextBoxShowLongDescription").Text = null;
            
            textBoxMessageDisplayer.Text = "Not a product in database";
        }
    }

    // Abling ability to edit info
    public void OnUpdateProductClick()
    {
        var textBoxShowName = this.FindControl<TextBox>("TextBoxShowName");
        var textBoxMessageDisplayer = this.FindControl<TextBox>("TextBoxMessageDisplayer");

        if (textBoxShowName.Text == null)
        {
            textBoxMessageDisplayer.Text = $"No information shown yet, \n cannot update product until there is information!";
        }
        else
        {
            textBoxShowName.IsReadOnly = false;
            this.FindControl<TextBox>("TextBoxShowManufacturer").IsReadOnly = false;
            this.FindControl<TextBox>("TextBoxShowRetailPrice").IsReadOnly = false;
            this.FindControl<TextBox>("TextBoxShowSalesPriceVAT").IsReadOnly = false;
            this.FindControl<TextBox>("TextBoxShowHeight").IsReadOnly = false;
            this.FindControl<TextBox>("TextBoxShowWidth").IsReadOnly = false;
            this.FindControl<TextBox>("TextBoxShowDepth").IsReadOnly = false;
            this.FindControl<TextBox>("TextBoxShowWeight").IsReadOnly = false;
            this.FindControl<TextBox>("TextBoxShowColor").IsReadOnly = false;
            this.FindControl<TextBox>("TextBoxShowLongDescription").IsReadOnly = false;

            textBoxMessageDisplayer.Text = "Product Info is editable";
        }
    }
    
    // Saving and updating the product info
    public async void OnSaveProductInformationClick(object sender, RoutedEventArgs e)
    {
        var name = this.FindControl<TextBox>("TextBoxShowName").Text;
        var manufacturer = this.FindControl<TextBox>("TextBoxShowManufacturer").Text;
        var retailPriceTextBox = this.FindControl<TextBox>("TextBoxShowRetailPrice").Text;
        var salesPriceVATTextBox = this.FindControl<TextBox>("TextBoxShowSalesPriceVAT").Text;
        var heightTextBox = this.FindControl<TextBox>("TextBoxShowHeight").Text;
        var widthTexbox = this.FindControl<TextBox>("TextBoxShowWidth").Text;
        var depthTextBox = this.FindControl<TextBox>("TextBoxShowDepth").Text;
        var weightTextBox = this.FindControl<TextBox>("TextBoxShowWeight").Text;
        var color = this.FindControl<TextBox>("TextBoxShowColor").Text;
        var longDescription = this.FindControl<TextBox>("TextBoxShowLongDescription").Text;

        
        
        
        
        decimal.TryParse(retailPriceTextBox, out decimal retailPrice);
        decimal.TryParse(salesPriceVATTextBox, out decimal salesPriceVAT);
        int.TryParse(heightTextBox, out int height);
        int.TryParse(widthTexbox, out int width);
        int.TryParse(depthTextBox, out int depth);
        decimal.TryParse(weightTextBox, out decimal weight);
        
        DatabaseConnection connection = productsAPI.GetConnection();
        if (connection == null) 
        {
            throw new Exception("Cannot access method \"FindProducts\" without supplying DatabaseConnection first");
        }
        if (!connection.isConnected())
        {
            throw new Exception("Cannot access method \"FindProducts\" without starting DatabaseConnection first \n(Use method StartAsync)");
        }

            
        Dictionary<string, object?> param = new Dictionary<string, object?>(){
            { "@sku", selectedSKu}, 
            { "@name", name}, 
            { "@manufacturer", manufacturer}, 
            { "@retailPrice", retailPrice}, 
            { "@salesPriceVAT", salesPriceVAT}, 
            { "@height", height}, 
            { "@width", width}, 
            { "@depth", depth}, 
            { "@weight", weight}, 
            { "@color", color},
            { "@longDescription", longDescription} };
        UpdateProductQuery UpdateProductQuery = new UpdateProductQuery(param);
        await connection.ExecuteQueryAsync(UpdateProductQuery);
        
        

        this.FindControl<TextBox>("TextBoxShowName").Text = null;
        this.FindControl<TextBox>("TextBoxShowManufacturer").Text = null;
        this.FindControl<TextBox>("TextBoxShowRetailPrice").Text = null;
        this.FindControl<TextBox>("TextBoxShowSalesPriceVAT").Text = null;
        this.FindControl<TextBox>("TextBoxShowSalesPrice").Text = null;
        this.FindControl<TextBox>("TextBoxShowHeight").Text = null;
        this.FindControl<TextBox>("TextBoxShowWidth").Text = null;
        this.FindControl<TextBox>("TextBoxShowDepth").Text = null;
        this.FindControl<TextBox>("TextBoxShowWeight").Text = null;
        this.FindControl<TextBox>("TextBoxShowColor").Text = null;
        this.FindControl<TextBox>("TextBoxShowLongDescription").Text = null;

        var textBoxMessageDisplayer = this.FindControl<TextBox>("TextBoxMessageDisplayer");
        textBoxMessageDisplayer.Text = $"Product:{selectedSKu} is updated.";

        ShowcaseInformation();
        OnUpdateProductClick();
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