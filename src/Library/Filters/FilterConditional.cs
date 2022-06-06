using System;
using System.Drawing;
using CognitiveCoreUCU;

namespace CompAndDel.Filters
{
    /// <summary>
    /// Un filtro que recibe una imagen y retorna su negativo.
    /// </remarks>
    public class FilterConditional : IFilter
    {
        public bool Face;
        /// Un filtro que retorna el negativo de la imagen recibida.
        /// </summary>
        /// <param name="image">La imagen a la cual se le va a aplicar el filtro.</param>
        /// <returns>La imagen recibida pero en negativo.</returns>
        public IPicture Filter(IPicture pic)
        {
            CognitiveFace cog = new CognitiveFace();
            cog.Recognize(@$"paso0.jpg");

            this.Face = cog.FaceFound;
            return pic;
        }
    }
}