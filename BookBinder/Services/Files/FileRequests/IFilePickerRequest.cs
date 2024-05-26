namespace BookBinder.Services.Files.FileRequests;

public interface IFilePickerRequest
{
    ValueTask<FileInfo?> PickFileAsync(PickOptions options);
}
