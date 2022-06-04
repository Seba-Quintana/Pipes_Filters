using System;
using System.Drawing;
using TwitterUCU;

namespace CompAndDel.Filters
{
    /// <summary>
    /// Un filtro que recibe una imagen y retorna su negativo.
    /// </remarks>
    public class FilterTwitter
    {
        /// Un filtro que retorna el negativo de la imagen recibida.
        /// </summary>
        /// <param name="image">La imagen a la cual se le va a aplicar el filtro.</param>
        /// <returns>La imagen recibida pero en negativo.</returns>
        public void Filter(string text, string path)
        {
            PictureProvider provider = new PictureProvider();
            var twitter = new TwitterImage();
            Console.WriteLine(twitter.PublishToTwitter($"{text}", @$"{path}.jpg"));
        }
    }
}