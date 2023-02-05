using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace NasaApp1
{
    internal class Neo
    {
        // dictionnaire des objets terrestres proches
        public Dictionary<string, NearEarthObject[]> Near_earth_objects { get; set; }

        // appelle l'api neo avec la date de début et la clé de l'utilisateur en entrée
        public static async Task<Neo> GetNeo(string startDate, string userKey)
        {
            // envoie une requête à l'API pour obtenir les informations sur les objets NEO
            WebRequest resquest = HttpWebRequest.Create("https://api.nasa.gov/neo/rest/v1/feed?start_date="+startDate+"&api_key="+userKey);
            WebResponse response = resquest.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream());

            // lit les données de la réponse et les convertit en objet neo
            string neo_json = reader.ReadToEnd();
            Neo neo = Newtonsoft.Json.JsonConvert.DeserializeObject<Neo>(neo_json);

            return neo;
        }       
    }

    public class NearEarthObject
    {
        // identifiant de l'objet NEO
        public string Id { get; set; }
        // nom de l'objet NEO
        public string Name { get; set; }
        // tableau des approches les plus proches de la terre
        public CloseApproachData[] close_approach_data { get; set; }
    }

    public class CloseApproachData
    {
        // dictionnaire des distances manquées à la terre 
        public Dictionary<string, string> miss_distance { get; set; }
    }

    public class MissDistance
    {
        // distance manquée en kilomètres
        public string kilometers { get; set; }
    }
}
