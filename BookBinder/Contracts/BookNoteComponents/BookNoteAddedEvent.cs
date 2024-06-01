using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookBinder.Models;

namespace BookBinder.Contracts.BookNoteComponents;

public class BookNoteAddedEvent
{
    public BookNote BookNote { get; set; }
}
