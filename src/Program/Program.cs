using System;
using CompAndDel.Pipes;
using CompAndDel.Filters;

namespace CompAndDel
{
    class Program
    {
        static void Main(string[] args)
        {

            PictureProvider provider = new PictureProvider();
            IPicture picture = provider.GetPicture(@"luke.jpg");
            IFilter filterNegative = new FilterNegative();
            IPipe pipeNull = new PipeNull();
            IPipe pipe2 = new PipeSerial(filterNegative, pipeNull);
            IPicture image2 = pipe2.Send(picture);
            provider.SavePicture(image2, @"luke.jpg");
            IFilter filterGreyscale = new FilterGreyscale();
            IPipe pipe = new PipeSerial(filterGreyscale, pipe2);
            IPicture image = pipe.Send(picture);
            provider.SavePicture(image, @"luke.jpg");
        }
    }
}
