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
			m_interface = new FormPartie(this);
			m_interface.Show();
			m_tour = false;

			m_joueurBlanc = p_joueur1;
			m_joueurNoir = p_joueur2;
			m_plateau = new Plateau();
			m_interface.effacerPiece();
			m_interface.afficherPiece(m_plateau.ToString());
		}

		public void jouerCoup( int[] p_posPiece, int[] p_posCase)
		{
			int test;

			test = m_plateau.validerCoup(p_posPiece, p_posCase, m_tour);

			//m_interface.message( "Echec ? " + m_plateau.echec(!m_tour).ToString() );

			//m_interface.message(test.ToString());
			if (test == 0)
			{
				m_interface.afficherDeplacement(p_posPiece, p_posCase);
				m_interface.effacerPiece();
				m_interface.afficherPiece(m_plateau.ToString());
				m_tour = !m_tour;
			}
			else
			{

			}


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
