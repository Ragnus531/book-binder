using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookBinder.Tests.Services.Files;

public static class StreamTestData
{
    public static MemoryStream StreamForFirstTest()
    {
        /*
         section1
            -element1.Title - element1.Descriptn
            -element2.Title - element2.Description

        section2

        section3
          -element1.Title - element1.Description


         */
        var memStream = new MemoryStream();
        var writer = new StreamWriter(memStream);
        writer.WriteLine("section1");
        writer.WriteLine("-element1.Title - element1.Descriptn");
        writer.WriteLine("-element2.Title - element2.Description");
        writer.WriteLine(string.Empty);
        writer.WriteLine("section2");
        writer.WriteLine(string.Empty);
        writer.WriteLine("section3");
        writer.WriteLine("-element1.Title - element1.Descriptn\r\n");
        writer.Flush();
        memStream.Seek(0, SeekOrigin.Begin);
        return memStream;
    }

    public static MemoryStream StreamForSecondTest()
    {
        /*
          .
          section1
         */

        var memStream = new MemoryStream();
        var writer = new StreamWriter(memStream);
        writer.WriteLine(".");
        writer.WriteLine("section1");
        writer.Flush();
        memStream.Seek(0, SeekOrigin.Begin);
        return memStream;
    }

    public static MemoryStream StreamForThirdTest()
    {
        /*
         element1.Title - element1.Descriptn
         element2.Title - element2.Description

        section1
         element1.Title - element1.Description
         element2.Title - element2.Description

         element3.Title - element4.Description

        section2
         */

        var memStream = new MemoryStream();
        var writer = new StreamWriter(memStream);
        writer.WriteLine(" element1.Title - element1.Descriptn");
        writer.WriteLine(" element2.Title - element2.Description");
        writer.WriteLine(string.Empty);
        writer.WriteLine("section1");
        writer.WriteLine(" element1.Title - element1.Descriptn");
        writer.WriteLine(" element2.Title - element2.Description");
        writer.WriteLine(string.Empty);
        writer.WriteLine(" element3.Title - element2.Description");
        writer.WriteLine(string.Empty);
        writer.WriteLine("section2");
        writer.Flush();
        memStream.Seek(0, SeekOrigin.Begin);
        return memStream;
    }

    public static MemoryStream StreamForFourthTest()
    {
        /*
          element1.Title - element1.Descriptn
          element2.Title - element2.Description
         */

        var memStream = new MemoryStream();
        var writer = new StreamWriter(memStream);
        writer.WriteLine(" element1.Title - element1.Descriptn");
        writer.WriteLine(" element2.Title - element2.Description");
        writer.Flush();
        memStream.Seek(0, SeekOrigin.Begin);
        return memStream;
    }

    public static MemoryStream StreamForThirdFifth()
    {
        /*
         element1.Title - element1.Descriptn
         element2.Title - element2.Description

        section1
         element1.Title - element1.Description
         element2.Title - element2.Description

         element3.Title - element4.Description

        section2
         */

        var memStream = new MemoryStream();
        var writer = new StreamWriter(memStream);
        writer.WriteLine(" element1.Title @ element1.Descriptn");
        writer.WriteLine(" element2.Title / element2.Description");
        writer.WriteLine(string.Empty);
        writer.WriteLine("section1");
        writer.WriteLine(" element1.Title  element1.Descriptn");
        writer.WriteLine(" element2.Title  element2.Description");
        writer.WriteLine(string.Empty);
        writer.WriteLine(" element3.Title | element2.Description");
        writer.WriteLine(string.Empty);
        writer.WriteLine("section2");
        writer.Flush();
        memStream.Seek(0, SeekOrigin.Begin);
        return memStream;
    }
}
