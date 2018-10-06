using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Echec
{
	public class Echec
	{
		List<Joueur> m_joueurs;
		FormMenu m_menu;
		string m_pathFichier;

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
			
		}

		static void creerListeJoueur()
		{

		}

		static void majFichierJoueurs()
		{

		}
    }
}
