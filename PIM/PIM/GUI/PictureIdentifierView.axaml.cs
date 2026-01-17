using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media.Imaging;
using DAM_1;
using Npgsql;
using PIM.Data;
using PIM.Data.Queries;
using PIM.Models;

namespace PIM;

public partial class PictureIdentifierView : UserControl
{
    ProductsAPI productsAPI;
    int selectedSku;
    public PictureIdentifierView(int sku)
    {
        selectedSku = sku;
        productsAPI = ProductsAPI.GetProductAPI();
        InitializeComponent();
    }
    
    public static async Task<NpgsqlConnection> GetConnectionAsync()
    {
        var connection = new NpgsqlConnection(UserCredentials.ConnectionString);
        await connection.OpenAsync();
        return connection;
    }
    
    public async Task ClickHandlerShowPicture()
    {
        
        string usercred = UserCredentials.ConnectionString;
        FileDatabaseService.IDamPictureProvider damPictureProvider1 = new FileDatabaseService.DamPictureProvider(usercred);
        
        IDamPictureProvider damPictureProvider = new ImageProvider();
    
    
    var textBoxMessageDisplayer = this.FindControl<TextBox>("TextBoxMessageDisplayerPicture"); // defining TextBoxMessageDisplayer

    try
    {
        
        // Query that fetches all product information

        DatabaseConnection connection = productsAPI.GetConnection();
        if (connection == null)
        {
            throw new Exception("Cannot access method \"FindProducts\" without supplying DatabaseConnection first");
        }

        if (!connection.isConnected())
        {
            throw new Exception(
                "Cannot access method \"FindProducts\" without starting DatabaseConnection first \n(Use method StartAsync)");
        }


        Dictionary<string, object?> param = new Dictionary<string, object?>()
        {
            { "@sku", selectedSku }
        };
        GetPictureInfoQuery getEditableQuery = new GetPictureInfoQuery(param);
        List<ProductPicture> productList = await connection.ExecuteQueryAsync<ProductPicture>(getEditableQuery);
        ProductPicture product = productList[0];


        this.FindControl<TextBox>("TextBoxName").Text = product.name;
        this.FindControl<TextBox>("TextBoxPrice").Text = product.priceInclVAT.ToString();
        this.FindControl<TextBox>("TextBoxHeight").Text = product.size[0].ToString();
        this.FindControl<TextBox>("TextBoxWidth").Text = product.size[1].ToString();
        this.FindControl<TextBox>("TextBoxDepth").Text = product.size[2].ToString();
        this.FindControl<TextBox>("TextBoxWeight").Text = product.weight.ToString();
        this.FindControl<TextBox>("TextBoxColor").Text = product.color;

        textBoxMessageDisplayer.Text = "Information Shown. Well Done!";



        byte[] productImage =
            damPictureProvider.GetPicture(selectedSku); // uses interface method with parameter productId

        if (productImage != null && productImage.Length > 0)
        {
            using (var ms = new MemoryStream(productImage)) // Runs a memorystream
            {
                ms.Position = 0;

                Bitmap picture = new Avalonia.Media.Imaging.Bitmap(ms);
                this.FindControl<Image>("ProductImage").Source = picture; // Picture is set as the source

                textBoxMessageDisplayer.Text += "\nPicture Shown. Well Done!";

            }

        }
        else
        {
            textBoxMessageDisplayer.Text += "\nNo image found!";
        }
    }
    catch
    {
        this.FindControl<TextBox>("TextBoxName").Text = " ";
        this.FindControl<TextBox>("TextBoxPrice").Text = " ";
        this.FindControl<TextBox>("TextBoxHeight").Text = " ";
        this.FindControl<TextBox>("TextBoxWidth").Text  = " ";
        this.FindControl<TextBox>("TextBoxDepth").Text = " ";
        this.FindControl<TextBox>("TextBoxWeight").Text = " ";
        this.FindControl<TextBox>("TextBoxColor").Text  = " ";

        this.FindControl<Image>("ProductImage").Source = null;
        
        textBoxMessageDisplayer.Text = "Not a product in database";
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