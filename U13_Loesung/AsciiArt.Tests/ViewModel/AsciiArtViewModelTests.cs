using System.ComponentModel;
using U13.Core.Service;
using U13.Core.ViewModel;

using FakeItEasy;
using FluentAssertions;
using FluentAssertions.Execution;

namespace U13.Tests.ViewModel;

public class AsciiArtViewModelTests
{
    private readonly AsciiArtViewModel _testee;
    private readonly IPlatformSupport _fakePlatformSupport;

    public AsciiArtViewModelTests()
    {
        _fakePlatformSupport = A.Fake<IPlatformSupport>();

        _testee = new AsciiArtViewModel(_fakePlatformSupport);
    }

    [Fact]
    public void Constructor_InitializesProperties()
    {
        // Assert
        using (new AssertionScope())
        {
            _testee.MinimumFontSize.Should().Be(2);
            _testee.MaximumFontSize.Should().Be(20);
            _testee.FontSize.Should().Be(12);
            _testee.MinimumLineWidth.Should().Be(80);
            _testee.MaximumLineWidth.Should().Be(320);
            _testee.LineWidth.Should().Be(120);
            _testee.ImagePath.Should().BeEmpty();
            _testee.Result.Should().BeEmpty();
            _testee.IsCalculating.Should().BeFalse();
            _testee.ChooseImageCommand.Should().NotBeNull();
            _testee.CreateAsciiCommand.Should().NotBeNull();
        }
    }

    [Fact]
    public void ChooseImageCommand_WhenExecuted_WhenValidFileWasChosen_UpdatesImagePath()
    {
        // Arrange
        const string imagePath = @"C:\Foo\Bar.jpg";
        A.CallTo(() => _fakePlatformSupport.ChooseFileAsync()).Returns(imagePath);

        // Act
        _testee.ChooseImageCommand.Execute(null);

        // Assert
        _testee.ImagePath.Should().Be(imagePath);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void ChooseImageCommand_WhenExecuted_WhenInvalidFileWasChosen_DoesNotUpdateImagePath(string filePath)
    {
        // Arrange
        A.CallTo(() => _fakePlatformSupport.ChooseFileAsync()).Returns(filePath);

        // Act
        _testee.ChooseImageCommand.Execute(null);

        // Assert
        _testee.ImagePath.Should().BeEmpty();
    }

    [Fact]
    public void CreateAsciiCommand_WhenExecuted_WhenImagePathIsEmpty_ShowsError()
    {
        // Act
        _testee.CreateAsciiCommand.Execute(null);

        // Assert
        A.CallTo(() => _fakePlatformSupport.ShowErrorAsync(
            "Quelldatei fehlt",
            "Kann leider nichts berechnen: Keine Quelldatei angegeben")
        ).MustHaveHappenedOnceExactly();
    }

    [Fact]
    public void CreateAsciiCommand_WhenExecuted_WhenImagePathIsInvalid_ShowsError()
    {
        // Arrange
        _testee.ImagePath = @"C:\Foo\Bar.jpg";

        // Act
        _testee.CreateAsciiCommand.Execute(null);

        // Assert
        A.CallTo(() => _fakePlatformSupport.ShowErrorAsync(
            "Quelldatei nicht verfügbar",
            "Kann leider nichts berechnen: Quelldatei nicht gefunden")
        ).MustHaveHappenedOnceExactly();
    }

    [Theory]
    [InlineData(80)]
    [InlineData(160)]
    public void CreateAsciiCommand_WhenExecuted_WhenImagePathIsValid_UpdatesResult(int lineWidth)
    {
        // Arrange
        var testDataDirectory = Path.Combine(Environment.CurrentDirectory, "TestData");
        var inputFilePath = Path.Combine(testDataDirectory, "OST-Logo.jpg");
        var outputFilePath = Path.Combine(testDataDirectory, $"OST-Logo-{lineWidth}.txt");

        _testee.ImagePath = inputFilePath;
        _testee.LineWidth = lineWidth;

        // Act
        var waiter = CreatePropertyChangedWaiter(nameof(_testee.Result));
        _testee.CreateAsciiCommand.Execute(null);
        waiter.Wait();

        // Assert
        _testee.Result.Should().NotBeEmpty();
        _testee.Result.Should().Be(File.ReadAllText(outputFilePath));
    }

    #region Private Methods

    private SemaphoreSlim CreatePropertyChangedWaiter(string propertyName)
    {
        var semaphoreSlim = new SemaphoreSlim(0);

        void TesteeOnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName != propertyName)
                return;

            _testee.PropertyChanged -= TesteeOnPropertyChanged;
            semaphoreSlim.Release();
        }

        _testee.PropertyChanged += TesteeOnPropertyChanged;

        return semaphoreSlim;
    }

    #endregion
}