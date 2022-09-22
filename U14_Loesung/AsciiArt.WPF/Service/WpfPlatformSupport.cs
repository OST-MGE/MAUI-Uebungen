using Microsoft.Win32;
using System;
using System.Threading.Tasks;
using System.Windows;
using U14.Core.Service;

namespace U14.WPF.Service;

public sealed class WpfPlatformSupport : IPlatformSupport
{
    public Task<string> ChooseFileAsync()
    {
        const string imagesFilter = "Image Files | *.jpg; *.jpeg; *.png; *.gif;";
        var userDirectory = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

        var dlg = new OpenFileDialog
        {
            Filter = imagesFilter,
            InitialDirectory = userDirectory
        };

        var dialogResult = dlg.ShowDialog();
        if (dialogResult != true)
            return null;

        var chosenFile = string.IsNullOrEmpty(dlg.FileName) ? null : dlg.FileName;

        return Task.FromResult(chosenFile);
    }

    public Task ShowErrorAsync(string title, string msg)
    {
        MessageBox.Show(msg, title, MessageBoxButton.OK, MessageBoxImage.Error);

        return Task.CompletedTask;
    }
}