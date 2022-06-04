using System;
using CompAndDel.Pipes;
using CompAndDel.Filters;

namespace CompAndDel
{
    class Program
    {
        static void Main(string[] args)
        {
            int i = 0;
            PictureProvider provider = new PictureProvider();
            IFilter save = new FilterSave();
            FilterTwitter upload = new FilterTwitter();

            IPicture picture = provider.GetPicture(@"luke.jpg");

            IFilter filterNegative = new FilterNegative();
            IPipe pipeNull = new PipeNull();
            IPipe pipeNeg = new PipeSerial(filterNegative, pipeNull);

            IPicture imageNeg = pipeNeg.Send(picture);
            imageNeg = save.Filter(imageNeg);

            upload.Filter($"prueba Luke {i}", @$"paso{i}");

            IFilter filterGreyscale = new FilterGreyscale();
            IPipe pipe = new PipeSerial(filterGreyscale, pipeNeg);

            IPicture image = pipe.Send(imageNeg);
            image = save.Filter(image);
            i++;

            upload.Filter($"prueba Luke {i}", @$"paso{i}");
        }
    }
}