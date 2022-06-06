using System;
using CompAndDel.Pipes;
using CompAndDel.Filters;
using CognitiveCoreUCU;

namespace CompAndDel
{
    class Program
    {
        static void Main(string[] args)
        {
            int i = 0;
            // Creo filtros
            PictureProvider provider = new PictureProvider();
            IFilter save = new FilterSave();
            FilterTwitter upload = new FilterTwitter();
            FilterConditional cond = new FilterConditional();

            // COnsigo imagenes
            IPicture picture = provider.GetPicture(@"luke.jpg");

            // Aplico filtro negativo al pipe
            IFilter filterNegative = new FilterNegative();
            IPipe pipeNull = new PipeNull();
            IPipe negPipe = new PipeSerial(filterNegative, pipeNull);

            PipeSerial condicion = new PipeSerial(cond, negPipe);

            // Aplico filtro escala de grises en pipe
            IFilter filterGreyscale = new FilterGreyscale();
            IPipe grayPipe = new PipeSerial(filterGreyscale, condicion);

            // Guardo filtro escala de grises en imagen
            IPicture grayImage = grayPipe.Send(picture);
            grayImage = save.Filter(grayImage);

            // Compruebo si hay cara o no
            if (cond.Face == false)
            {
                // se aplica filtro negativo si no hay cara
                IPicture negImage = negPipe.Send(picture);
                negImage = save.Filter(negImage);
                i++;
            }
            else
            {
                // Se sube a twitter la imagen
                upload.Filter($"No Face test {i}", @$"paso{i}");
            }
        }
    }
}