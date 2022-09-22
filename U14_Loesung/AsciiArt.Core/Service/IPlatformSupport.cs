namespace U14.Core.Service;

public interface IPlatformSupport
{
    Task<string> ChooseFileAsync();

    Task ShowErrorAsync(string title, string msg);
}