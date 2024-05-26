using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookBinder.Services.Files.FileRequests;

public class TextFilePickerRequest : IFilePickerRequest
{
    private readonly IFilePicker _filePicker;

    public TextFilePickerRequest() { }

    public TextFilePickerRequest(IFilePicker filePicker)
    {
        _filePicker = filePicker;
    }

    public async ValueTask<FileInfo?> PickFileAsync(PickOptions options)
    {
        var result = await _filePicker.PickAsync(options);
        if (result != null)
        {
            if (result.FileName.EndsWith("txt", StringComparison.OrdinalIgnoreCase))
            {
                return new FileInfo(result.FileName, await result.OpenReadAsync());
            }
        }

        return null;
    }
}
