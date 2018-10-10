using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Linq;

namespace Echec
{
	public class Echec
	{
		List<Joueur> m_joueurs;
		FormMenu m_menu;
        string m_pathFichier = Path.GetFullPath("ListeJoueurs.txt");

        [STAThread]

		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Echec m_echec = new Echec();
		}

		public Echec()
		{
			m_menu = new FormMenu(this);
			Application.Run(m_menu);
		}

		static void demarrerPartie()
		{
            //Partie laPartie = new Partie();
		}

		public void creerListeJoueur()
		{
            m_joueurs = new List<Joueur>();
            StreamReader sr = new StreamReader(m_pathFichier);
            string[][] lesJoueurs;

            //Tant que le fichier n'est pas à la dernière ligne
            while (!sr.EndOfStream)
            {
                //Prend la ligne de lecture en cours et la "split" en un tableau 2d qui "split" en premier lieu avec les ','
                //Ensuite avec les ';' (pour indiquer la fin d'un joueur)
                int linecount = 0;
                string leJoueur = sr.ReadLine();
                lesJoueurs = leJoueur.Split(';').Select(x => x.Split(',')).ToArray();

                //Range les informations du joueur dans des variables
                string leNom = lesJoueurs[linecount][0];
                int lesVictoires = Int32.Parse(lesJoueurs[linecount][1]);
                int lesDefaites = Int32.Parse(lesJoueurs[linecount][2]);
                int leClassement = Int32.Parse(lesJoueurs[linecount][3]);

                //Crée un joueur avec les données du fichier texte et ajoute ce personnage à la liste de personnages
                Joueur JoueurAjoute = new Joueur(leNom, lesVictoires, lesDefaites, leClassement);

                m_joueurs.Add(JoueurAjoute);
                linecount++;
            }
        }

		private void majFichierJoueurs()
		{

		}

        public List<string> ListeJoueurs
        {
            get
            {
                List<string> ListJoueurString = new List<string>();
                foreach (Joueur J in m_joueurs)
                {
                    ListJoueurString.Add(J.ToString());
                }
                return ListJoueurString;
            }
        }
    }
}
