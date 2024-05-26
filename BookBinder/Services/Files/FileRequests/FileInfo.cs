using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookBinder.Services.Files.FileRequests;

public record FileInfo(string FileName, Stream FileStream);
