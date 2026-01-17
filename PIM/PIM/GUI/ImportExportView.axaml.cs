using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Platform.Storage;
using PIM.Data;
using PIM.Process;

namespace PIM;

public partial class ImportExportView : UserControl {
    private string filePath = null;
    public ImportExportView() {
        InitializeComponent();
    }
    
    private async void OpenFileButton_Click(object sender, RoutedEventArgs args) {
        string? localPath = null;

        //I only want csv files
        FilePickerFileType csvFileType = new FilePickerFileType("Only CSV files") {
            Patterns = new[] { "*.csv" }
        };

        //Get top level from the current control.
        TopLevel? topLevel = TopLevel.GetTopLevel(this);

        //Start async operation to open the dialog.
        var files = await topLevel.StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions {
            Title = "Open CSV File",
            AllowMultiple = false,
            FileTypeFilter = new[] { csvFileType }
        });

        if (files.Count == 1) {
            localPath = files[0].TryGetLocalPath();

            if (localPath != null) {
                displayInformation.Text = $"You have selected this file:\n{localPath}\n\nIf this is correct press confirm, otherwise choose another file.";
                filePath = localPath;

                confirm.IsEnabled = true;
            }
        }
    }
    public async void Confirm_Click(object sender, RoutedEventArgs e) {
        AddData addData = new AddData(filePath);

        if (addData.GetAllInformationAvailable()) {
            await addData.AddDataNow();
            displayInformation.Text = $"{addData.GetProductAmount()} products were added";
            confirm.IsEnabled = false;
        }
        else {
            displayInformation.Text = $"No products were added, fix the mistakes in your file:\n{addData.GetMistakesInFile()}";
        }
    }
    public async void ExportFile_Click(object sender, RoutedEventArgs e) {
         string? categoryTerm = categoryTermBox.Text;
        string? SearchTerm = SearchTermBox.Text;   
        
        ExportData exportData = new ExportData();
        await exportData.ExportNow(categoryTerm, SearchTerm);

        displayInformation.Text = "Find the file in your downloads folder";
    }
}