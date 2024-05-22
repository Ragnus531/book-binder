using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookBinder.Models;

namespace BookBinder.Services.Files
{
    public interface IFileExport
    {
        Task FileExport(BookNote bookNote);
    }
}
