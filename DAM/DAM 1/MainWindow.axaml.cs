// Importing necessary system libraries

using System;
using System.Collections.Generic; // Allows us to work with lists and collections.
using System.IO;
using Avalonia.Controls; // Gives us access to user interface (UI) components such as buttons, text boxes, and lists.
using Avalonia.Interactivity;
using Avalonia.Media.Imaging;
using Avalonia.Platform.Storage;
using Npgsql; // Allows us to work with the database
using DAM_1;

namespace DAM;

public partial class MainWindow : Window
{
    // The connection to the database
    private readonly string connString = "Host=localhost; Port=5432; Database=2nd_sem-2-DAM; Username=2nd_sem_user; Password=Password"; //The user needs to be a superuser before the code can run

    private Image _imageDisplay;

    // Constructor: This function is called when the application starts.
    public MainWindow()
    {
        InitializeComponent(); // This sets up the user interface (UI) using the XAML file.
        Title = "DAM";
        _imageDisplay = this.FindControl<Image>("ImageDisplay");
        ResultsList.SelectionChanged += OnResultsListSelectionChanged;
        LoadFiles();

    }


    /// <summary>
    /// This function is triggered when the search button is clicked.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void OnSearchClicked(object? sender, RoutedEventArgs e)
    {
        // Get the user input from the search box.
        string query = SearchBox.Text?.Trim() ?? "";

        // If the input is empty, show the original hardcoded items.
        if (string.IsNullOrWhiteSpace(query))
        {
            ResultsList.ItemsSource = new List<Product>();
            return;
        }


        using var conn = new NpgsqlConnection(connString);
        conn.Open();

        var results = new List<Product>();

        // Check if the input is a number. If it is, treat it as a product ID.
        if (int.TryParse(query, out int productId))
        {
            // SQL query to get product details and tags by product ID
            string sql = @"
                SELECT p.id, p.filename, COALESCE(string_agg(t.tagname, ', '), 'No tags') AS tags, p.path
                FROM products p
                LEFT JOIN productTag pt ON p.id = pt.product_id
                LEFT JOIN tags t ON pt.tag_id = t.id
                WHERE p.id = @id
                GROUP BY p.id, p.filename;";

            using var cmd = new NpgsqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@id", productId);

            using var idReader = cmd.ExecuteReader();

            // Read the result and format the output string
            while (idReader.Read())
            {
                int id = idReader.GetInt32(0);
                string filename = idReader.GetString(1);
                string tags = idReader.IsDBNull(2) ? "No tags" : idReader.GetString(2);
                string path = idReader.GetString(3);
                results.Add(new Product
                {
                    Id = id,
                    Filename = filename,
                    Tag = tags,
                    Path = path
                });
            }
            idReader.Close();
        }

        else
        {
            //Check if input is Filename.

            // SQL query to get product details and tags by product filename
            string nameQuery = @"
                SELECT DISTINCT p.id, p.filename, COALESCE(string_agg(t.tagname, ', '), 'No tags') AS tags, p.path
                FROM products p
                LEFT JOIN productTag pt ON p.id = pt.product_id
                LEFT JOIN tags t ON pt.tag_id = t.id
                WHERE LOWER(p.filename) LIKE LOWER(@filename)
                GROUP BY p.id, p.filename; ";

            using var nameCmd = new NpgsqlCommand(nameQuery, conn);
            nameCmd.Parameters.AddWithValue("@filename", $"%{query}%");
            using var nameReader = nameCmd.ExecuteReader();

            while (nameReader.Read())
            {
                int id = nameReader.GetInt32(0);
                string filename = nameReader.GetString(1);
                string tags = nameReader.IsDBNull(2) ? "No tags" : nameReader.GetString(2);
                string path = nameReader.GetString(3);
                results.Add(new Product
                {
                    Id = id,
                    Filename = filename,
                    Tag = tags,
                    Path = path
                });
            }

            nameReader.Close();

            //Check if input is a Tag.

            // SQL query to get product details and tags by product Tag
            string tagQuery = @"
                SELECT DISTINCT p.id, p.filename, COALESCE(string_agg(t.tagname, ', '), 'No tags') AS tags, p.path
                FROM products p
                LEFT JOIN productTag pt ON p.id = pt.product_id
                LEFT JOIN tags t ON pt.tag_id = t.id
                WHERE LOWER(t.tagname) LIKE LOWER(@tag)
                GROUP BY p.id, p.filename; ";

            using var tagCmd = new NpgsqlCommand(tagQuery, conn);
            tagCmd.Parameters.AddWithValue("@tag", $"%{query}%");
            using var tagReader = tagCmd.ExecuteReader();

            while (tagReader.Read())
            {
                int id = tagReader.GetInt32(0);
                string filename = tagReader.GetString(1);
                string tags = tagReader.IsDBNull(2) ? "No tags" : tagReader.GetString(2);
                string path = tagReader.GetString(3);
                results.Add(new Product
                {
                    Id = id,
                    Filename = filename,
                    Tag = tags,
                    Path = path
                });
            }
            tagReader.Close();
        }


        ResultsList.ItemsSource = results;

        conn.Close();
    }

    // for the Tags

    /// <summary>
    /// findes the filename and displays as a dropdown list
    /// </summary>
    private void LoadFiles()
    {
        try
        {
            var fileDropdown = this.FindControl<ComboBox>("FileDropdown");
            var products = new List<ProductItem>();

            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();
                string query = "SELECT id, filename FROM products;";
                using var cmd = new NpgsqlCommand(query, conn);
                using var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    products.Add(new ProductItem
                    {
                        Id = reader.GetInt32(0),
                        Filename = reader.GetString(1)
                    });
                }
            }

            fileDropdown.ItemsSource = products;
        }
        catch (Exception ex)
        {
            Results_List.ItemsSource = new List<string> { $"[ERROR] Failed to load products: {ex.Message}" };
        }
    }

    /// <summary>
    /// Saves the new tag
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void SaveTags(object? sender, RoutedEventArgs e)
    {
        var fileDropdown = this.FindControl<ComboBox>("FileDropdown");
        var tagInput = this.FindControl<TextBox>("TagInput");

        if (fileDropdown.SelectedItem is ProductItem selectedProduct && !string.IsNullOrWhiteSpace(tagInput.Text))
        {
            string[] tags = tagInput.Text.Split(',', StringSplitOptions.TrimEntries);
            AddTagsToMedia(selectedProduct.Filename, tags);
        }

    }

    /// <summary>
    /// Adds the tags to the database
    /// </summary>
    /// <param name="filename"></param>
    /// <param name="newTags"></param>
    private void AddTagsToMedia(string filename, string[] newTags)
    {
        using var conn = new NpgsqlConnection(connString);
        conn.Open();

        // Get the product ID
        int? productId = null;
        string getProductIdQuery = "SELECT id FROM products WHERE filename = @filename;";
        using (var getProductCmd = new NpgsqlCommand(getProductIdQuery, conn))
        {
            getProductCmd.Parameters.AddWithValue("@filename", filename);
            productId = getProductCmd.ExecuteScalar() as int?;

            if (productId == null)
            {
                throw new FileNotFoundException($"Error: No product found with filename '{filename}'");
            }
        }

        foreach (var tag in newTags)
        {
            int tagId;

            // Check if tag already exists
            string checkTagQuery = "SELECT id FROM tags WHERE tagname = @tagname;";
            using (var checkTagCmd = new NpgsqlCommand(checkTagQuery, conn))
            {
                checkTagCmd.Parameters.AddWithValue("@tagname", tag);
                var result = checkTagCmd.ExecuteScalar();

                if (result == null)
                {
                    // Tag does not exist, insert it
                    string insertTagQuery = "INSERT INTO tags (tagname) VALUES (@tagname) RETURNING id;";
                    using var insertTagCmd = new NpgsqlCommand(insertTagQuery, conn);
                    insertTagCmd.Parameters.AddWithValue("@tagname", tag);
                    tagId = (int)insertTagCmd.ExecuteScalar();
                }
                else
                {
                    tagId = (int)result;
                }
            }

            // Now insert into productTag (if not exists)
            string insertProductTagQuery = @"
            INSERT INTO productTag (product_id, tag_id)
            VALUES (@productId, @tagId)
            ON CONFLICT DO NOTHING;";

            using (var insertProductTagCmd = new NpgsqlCommand(insertProductTagQuery, conn))
            {
                insertProductTagCmd.Parameters.AddWithValue("@productId", productId);
                insertProductTagCmd.Parameters.AddWithValue("@tagId", tagId);
                insertProductTagCmd.ExecuteNonQuery();
            }
        }

        Results_List.ItemsSource = new List<string> { "Tags successfully linked to the product." };
    }


    /// <summary>
    /// Handles delete button click
    /// </summary>
    private void OnDeleteAssetClicked(object? sender, RoutedEventArgs e)
    {
        var input = DeleteIdInput.Text?.Trim();

        if (!string.IsNullOrEmpty(input) && int.TryParse(input, out int assetId))
        {
            DeleteAsset(assetId);
        }
        else
        {
            Results_List.ItemsSource = new List<string> { "Please enter a valid ID." };
        }
    }

    /// <summary>
    /// Deletes an asset and its related data
    /// </summary>
    private void DeleteAsset(int assetId)
    {
        using var conn = new NpgsqlConnection(connString);
        conn.Open();

        // Check if product exists
        string checkSql = "SELECT filename FROM products WHERE id = @id;";
        using var checkCmd = new NpgsqlCommand(checkSql, conn);
        checkCmd.Parameters.AddWithValue("@id", assetId);
        var filename = checkCmd.ExecuteScalar() as string;

        if (filename == null)
        {
            Results_List.ItemsSource = new List<string> { $"No product found with ID {assetId}." };
            return;
        }

        // Delete associated tags
        string deleteTagsSql = "DELETE FROM productTag WHERE product_id = @id;";
        using var deleteTagsCmd = new NpgsqlCommand(deleteTagsSql, conn);
        deleteTagsCmd.Parameters.AddWithValue("@id", assetId);
        deleteTagsCmd.ExecuteNonQuery();

        // Delete product itself
        string deleteProductSql = "DELETE FROM products WHERE id = @id;";
        using var deleteProductCmd = new NpgsqlCommand(deleteProductSql, conn);
        deleteProductCmd.Parameters.AddWithValue("@id", assetId);
        deleteProductCmd.ExecuteNonQuery();

        // Delete file from disk
        if (File.Exists(filename))
        {
            File.Delete(filename);
        }

        Results_List.ItemsSource = new List<string> { $"File {filename} deleted." };
    }

    /// <summary>
    /// The hander for batch upload
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void OnUploadBatchCliked(object? sender, RoutedEventArgs e)
    {
        var topLevel = GetTopLevel(this);
        if (topLevel == null)
        {
            return;
        }

        var selectedFolder = await topLevel.StorageProvider.OpenFolderPickerAsync(new FolderPickerOpenOptions
        {
            Title = "Select Directory to Upload Files",
            AllowMultiple = false,
        });

        if (selectedFolder.Count == 0)
        {
            return;
        }

        var service = new FileDatabaseService(connString);
        var folderPath = selectedFolder[0].Path.LocalPath;
        service.InsertFilesFromDirectory(folderPath);

        // Get only the name of the folder (e.g., "Images" from "C:\Projects\Assets\Images")
        var folderName = Path.GetFileName(folderPath.TrimEnd(Path.DirectorySeparatorChar));

        Results_List.ItemsSource = new List<string> { $"Folder '{folderName}' added." };
    }


    /// <summary>
    /// Helping with the filename dropdown
    /// </summary>
    public class ProductItem
    {
        public int Id { get; set; }
        public string Filename { get; set; }

        public override string ToString() => Filename;
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void OnResultsListSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        if (e.AddedItems.Count > 0)
        {
            DisplayImage((Product)e.AddedItems[0]);
        }
    }


    /// <summary>
    /// The method for display image
    /// </summary>
    /// <param name="selectedResult"></param>
    private void DisplayImage(Product selectedResult)
    {
        if (selectedResult == null)
        {
            _imageDisplay.Source = null;
            return;
        }

        string projectRoot = System.IO.Path.GetFullPath(System.IO.Path.Combine(Directory.GetCurrentDirectory(), "..", "..", ".."));
        string imagePath = $"{projectRoot.Replace('\\', '/')}/DAMDatabase{selectedResult.Path}";

        if (!File.Exists(imagePath))
        {
            throw new FileNotFoundException($"File not found: {imagePath}");
        }

        LoadImageFromFile(imagePath);
    }

    /// <summary>
    /// Loads image from file
    /// </summary>
    /// <param name="filePath"></param>
    private void LoadImageFromFile(string filePath)
    {
        Bitmap bitmap = new Bitmap(filePath);
        _imageDisplay.Source = bitmap;
    }
}

