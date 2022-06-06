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

            // Compruebo si hay cara o no
            PipeSerial condicion = new PipeSerial(cond, negPipe);
            if (cond.Face == false)
            {
                // se aplica filtro negativo si no hay cara
                IPicture negImage = negPipe.Send(picture);
                negImage = save.Filter(negImage);
            }
            else
            {
                // Se sube a twitter la imagen
                upload.Filter($"No Face test {i}", @$"paso{i}");
            }


            // Aplico filtro escala de grises en pipe
            IFilter filterGreyscale = new FilterGreyscale();
            IPipe grayPipe = new PipeSerial(filterGreyscale, condicion);

            // Guardo filtro escala de grises en imagen // Si mando negImage se cancela el filtro neg con el filtro neg y queda escala de grises. Si mando picture se aplican las dos a la vez
            IPicture grayImage = grayPipe.Send(picture);
            grayImage = save.Filter(grayImage);
            i++;
            //upload.Filter($"prueba Luke {i}", @$"paso{i}");
        }
    }
}