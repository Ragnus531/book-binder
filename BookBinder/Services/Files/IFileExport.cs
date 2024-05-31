using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookBinder.Models;
using BookBinder.Utils;

namespace BookBinder.Services.Files
{
    public interface IFileExport
    {
        Task<bool> FileExport(BookNote bookNote, ExportOptions exportOptions);
    }
}
