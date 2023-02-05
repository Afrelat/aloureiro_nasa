using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace NasaApp1
{
    internal class Apod
    {
        //Stocke les informations de droits d'auteur provenant de l'API
        public string copyright { get; set; }

        //Stocke les explications provenant de l'API
        public string explanation { get; set; }

        //Stocke l'URL en haute définition de l'image provenant de l'API
        public string hdurl { get; set; }

        //Stocke le type de média provenant de l'API
        public string media_type { get; set; }

        //Stocke le titre provenant de l'API
        public string title { get; set; }

        //Stocke l'URL de l'image provenant de l'API
        public string url { get; set; }


        //Appelle l'API NASA pour récupérer les informations sur l'image du jour.
        public Apod GetApod()
        {
            //Utilise une requête web pour accéder à l'API, puis décode le résultat en utilisant la bibliothèque JSON Newtonsoft.
            WebRequest resquest = HttpWebRequest.Create("https://api.nasa.gov/planetary/apod?api_key=pIXpUTCgiY5NRTS2nA9GQnuqC8Ip7aK13nVvLr86");
            WebResponse response = resquest.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream());

            string apod_json = reader.ReadToEnd();
            Apod apod = Newtonsoft.Json.JsonConvert.DeserializeObject<Apod>(apod_json);

            return apod;
        }
    }

}


