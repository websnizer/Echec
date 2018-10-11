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
		List<string> ListJoueurString = new List<string>();
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

			//Pour toutes les lignes du textfile (sauf la première parce qu'elle est vide)
			foreach (var line in File.ReadAllLines(m_pathFichier).Skip(1))
			{
				//Prend la ligne de lecture en cours et la "split" en un tableau 2d qui "split" en premier lieu avec les ','
				//Ensuite avec les ';' (pour indiquer la fin d'un joueur)
				int linecount = 0;
				string leJoueur = line;
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
			sr.Close();
			CreerListeJoueur();
		}

		public void majFichierJoueurs()
		{
			//Crée le StreamWriter
			StreamWriter strm = File.CreateText(m_pathFichier);

			//Vide le fichier texte
			strm.Flush();

			for (int i = 0; i < ListJoueurString.Count; i++)
			{
				//Ajoute les inforamtions du joueur dans le fichier texte
				strm.Write(Environment.NewLine + ListJoueurString[i] + ";");
			}

			//Ferme le StramWriter
			strm.Close();

		}

		private void CreerListeJoueur()
		{
			foreach (Joueur J in m_joueurs)
			{
				ListJoueurString.Add(J.ToString());
			}
		}


		public List<string> ListeJoueurs
		{
			//Sérialise la liste de joueurs
			get { return ListJoueurString; }

			set
			{ ListJoueurString = value; }
		}
	}
}