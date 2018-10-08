using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Echec
{
	public class Partie
    {

		Joueur m_joueurBlanc;
		Joueur m_joueurNoir;
		FormPartie m_interface;
		bool m_tour;
		Plateau m_plateau;

		public Partie(Joueur p_joueur1, Joueur p_joueur2)
		{
			m_joueurBlanc = p_joueur1;
			m_joueurNoir = p_joueur2;
		}

		public void jouerCoup( int[] posPiece, int[] posCase)
		{

		}

		private void verifierStatutJeu()
		{

		}

        public Joueur JoueurBlanc
        {
            get {return m_joueurBlanc; }
            set {value = m_joueurBlanc; }
        }


        public Joueur JoueurNoir
        {
            get {return m_joueurNoir; }
            set { m_joueurNoir = value; }
        }
    }
}
