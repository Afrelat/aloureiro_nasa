using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static System.Net.Mime.MediaTypeNames;

namespace NasaApp1
{
    // Déclaration de la classe MainWindow
    public partial class MainWindow : Window
    {
        // Constructeur de la classe MainWindow.
        public MainWindow()
        {
            // Initialise les composants visuels pour la fenêtre
            InitializeComponent();
            // Appelle la méthode asynchrone pour afficher l'image du jour.
            apodImageAsync();
            // Appelle la méthode asynchrone pour afficher les objets proches de la Terre.
            neoAsync();
        }


        // Méthode asynchrone pour afficher l'image du jour en utilisant l'API APOD.
        private async void apodImageAsync()
        {
            // Instance de la classe Apod.
            Apod newApod = new Apod();
            // Récupère l'image du jour de l'API APOD
            Apod apod = newApod.GetApod();

            // Conversion de l'URL de l'image en chaîne de caractères.
            string URLimage = String.Format(apod.hdurl);
            // Affichage de l'image dans le composant image.
            image.Source = new BitmapImage(new Uri(URLimage));
        }

        // Méthode asynchrone pour afficher les objets proches de la Terre en utilisant l'API NeoWs.
        private async void neoAsync()
        {
            // Récupère la date d'aujourd'hui au format yyyy-dd-MM.
            string startDate = DateTime.Today.ToString("yyyy-dd-MM");
            // Clé d'accès à l'API NeoWs.
            string userKey = "pIXpUTCgiY5NRTS2nA9GQnuqC8Ip7aK13nVvLr86";

            // Récupère les objets proches de la Terre à partir de l'API NeoWs.
            Neo result = await Neo.GetNeo(startDate, userKey);

            // Boucle sur les objets proches de la Terre pour les afficher dans le composant ListBoxAsteroid.
            foreach (KeyValuePair<string, NearEarthObject[]> nearEarthObjects in result.Near_earth_objects)
            {
                foreach (NearEarthObject nearEarthObject in result.Near_earth_objects[nearEarthObjects.Key])
                {
                    // Ajout du nom de l'objet proche de la Terre dans la liste.
                    ListBoxAsteroid.Items.Add("Nom : " + nearEarthObject.Name);
                    // Ajout de la distance de l'objet proche de la Terre dans la liste.
                    ListBoxAsteroid.Items.Add("Disstance : " + nearEarthObject.close_approach_data[0].miss_distance["kilometers"]+" Km");
                }
            }
        }

        // Permet de bouger la fenêtre en maintenant le bouton gauche de la souris enfoncé
        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton== MouseButtonState.Pressed)
            {
                DragMove();
            }
        }


        // Variable pour suivre l'état de l'ouverture/fermeture du menu popup
        private bool isPopupOpen = false;

        // Ouvre/ferme un menu popup pour naviguer en cliquant sur le textBlock
        private void textBlock_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (isPopupOpen)
            {
                popup.IsOpen = false;
                isPopupOpen = false;
            }
            else
            {
                popup.IsOpen = true;
                isPopupOpen = true;
            }
        }

        // Ferme l'executable en cliquant sur le bouton "Close"
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Instance de la classe Apod.
            Apod newApod = new Apod();
            // Récupère l'image du jour de l'API APOD
            Apod apod = newApod.GetApod();

            MessageBox.Show(apod.explanation);
        }
    }
}
