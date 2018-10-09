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
		Joueur m_joueurTour;
		FormPartie m_interface;
		bool m_tour;
		Plateau m_plateau;

		public Partie(Joueur p_joueur1, Joueur p_joueur2)
		{
			m_interface = new FormPartie(this);
			m_interface.Show();
			m_tour = true;

			m_joueurBlanc = p_joueur1;
			m_joueurNoir = p_joueur2;
			m_plateau = new Plateau();
			m_interface.effacerPiece();
			m_interface.afficherPiece(m_plateau.ToString());

			m_joueurTour = m_joueurBlanc;
			m_interface.afficherTour(m_joueurTour.NomJoueur);
		}

		public void jouerCoup( int[] p_posPiece, int[] p_posCase)
		{
			int test;

			test = m_plateau.validerCoup(p_posPiece, p_posCase, m_tour);

			if (test == 0)
			{
				m_plateau.deplacerPiece(p_posPiece, p_posCase);
				m_interface.afficherStatut(test);
				m_interface.afficherDeplacement(p_posPiece, p_posCase, m_joueurTour.NomJoueur);
				m_interface.effacerPiece();
				m_interface.afficherPiece(m_plateau.ToString());

				//Changer le tour
				m_tour = !m_tour;

				//Actualiser c'est le tour de quel joueur
				m_joueurTour = (m_tour) ? JoueurNoir: JoueurBlanc;
				m_interface.afficherTour(m_joueurTour.NomJoueur);

				//Afficher si le joueur est en echec.
				if (m_plateau.echec(m_tour))
					m_interface.afficherStatut(8);

				if (m_plateau.verifierPromo(p_posCase))
				{
					m_interface.afficherStatut(9);
					m_plateau.promouvoirPion(p_posCase, "Reine");
				}
				

			}
			else
			{
				m_interface.afficherStatut(test);
			}

		}

		private void verifierStatutJeu()
		{
		}

		public bool Tour
		{
			get { return m_tour; }
		}
		

        public Joueur JoueurBlanc
        {
            get {return m_joueurBlanc; }
            set {value = m_joueurBlanc; }
        }

		public Joueur JoueurTour
		{
			get { return m_joueurTour; }
			set { value = m_joueurTour; }
		}

		public Joueur JoueurNoir
        {
            get {return m_joueurNoir; }
            set { m_joueurNoir = value; }
        }
    }
}
