using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Echec
{
	public class Joueur
    {
		string m_nom;
		int m_victoires;
		int m_defaites;
		int m_classement;

		public Joueur(string p_nom, int p_victoires, int p_defaites, int p_classement)
		{
			m_nom = p_nom;
			m_victoires = p_victoires;
			m_defaites = p_defaites;
			m_classement = p_classement;
		}

		public void perdant ( Joueur leGagnant )
		{
			//Ajuster le score en conséquence?
		}

		public void gagnant ( Joueur lePerdant )
		{
			//Ajuster le score en conséquence?
		}
    }
}
