using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using SimplePhotos;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.Foundation;
using Windows.Foundation.Collections;
using static System.Net.Mime.MediaTypeNames;
using Windows.Storage.Search;
using Windows.Storage;

using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.Storage;
using Windows.Storage.Search;

using System.Collections.ObjectModel;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.


private async Task GetItemsAsync()
{
    StorageFolder appInstalledFolder = Package.Current.InstalledLocation;
    StorageFolder picturesFolder = await appInstalledFolder.GetFolderAsync("Assets\\Samples");

    var result = picturesFolder.CreateFileQueryWithOptions(new QueryOptions());

    IReadOnlyList<StorageFile> imageFiles = await result.GetFilesAsync();
    foreach (StorageFile file in imageFiles)
    {
        Images.Add(await LoadImageInfoAsync(file));
    }

    ImageGridView.ItemsSource = Images;
}

public async static Task<ImageFileInfo> LoadImageInfoAsync(StorageFile file)
{
    var properties = await file.Properties.GetImagePropertiesAsync();
    ImageFileInfo info = new(properties,
                             file, file.DisplayName, file.DisplayType);

    return info;
}


namespace SimplePhotos
{
    public sealed partial class MainWindow : Window
    {
        public ObservableCollection<ImageFileInfo> Images { get; } =
            new ObservableCollection<ImageFileInfo>();
       
    }
}

