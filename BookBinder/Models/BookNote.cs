using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookBinder.Models
{
    public class BookNote
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public List<NoteSection> NoteSections { get; set; } = new List<NoteSection>();

        public string ToExportFormat()
        {
            StringBuilder sB = new StringBuilder();
            foreach (var noteSection in NoteSections)
            {
                sB.AppendLine(noteSection.Title);

                foreach (var element in noteSection.Elements)
                {
                    sB.AppendLine($"    {element.Name} - {element.Description}");
                }

                sB.AppendLine(); // Add an empty line between sections
            }

            return sB.ToString();
        }
    }
}
