using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Echec
{
	public class Partie
    {
		Joueur m_joueurBlanc; //Le joueur blanc
		Joueur m_joueurNoir; //Le joueur noir
		Joueur m_joueurTour; //Le tour de quel joueur?
		FormPartie m_interface; //La form de la partie
		bool m_tour; //Le tour en boolean
		Plateau m_plateau;  //Référence au plateau de la partie
        string m_piece = ""; //Le choix de l'utilisateur

		//Constructeur
        public Partie(Joueur p_joueur1, Joueur p_joueur2)
		{
            m_joueurBlanc = p_joueur1;
            m_joueurNoir = p_joueur2;

            m_interface = new FormPartie(this);
			m_interface.Show();

			m_tour = true;

			m_plateau = new Plateau();
			refreshForm();

			m_joueurTour = m_joueurBlanc;
			m_interface.afficherTour(m_joueurTour.NomJoueur);
		}

		public void jouerCoup( int[] p_posPiece, int[] p_posCase)
		{
			int code; //Le code que le plateau retourne

			//Valider le coup
			code = m_plateau.validerCoup(p_posPiece, p_posCase, m_tour);

			//Si le coup est valide
            if (code == 0)
			{
				m_plateau.deplacerPiece(p_posPiece, p_posCase);
				m_interface.afficherStatut(code);
				m_interface.afficherDeplacement(p_posPiece, p_posCase, m_joueurTour.NomJoueur);
				refreshForm();

				//Si promotion
				if (m_plateau.verifierPromo(p_posCase))
				{
					m_interface.afficherStatut(9);

					//Demander le choix à l'utilisateur avec une form en dialog
					FormChoixPiece m_choixpiece = new FormChoixPiece(this, m_tour);
					m_choixpiece.ShowDialog();	

					m_plateau.promouvoirPion(p_posCase, m_piece);
					refreshForm();
				}

				//Changer le tour
				m_tour = !m_tour;

				//Actualiser c'est le tour de quel joueur
				m_joueurTour = (m_tour) ? JoueurBlanc : JoueurNoir;
				m_interface.afficherTour(m_joueurTour.NomJoueur);

				//Vérifier le statut du jeu
				verifierStatutJeu();
			}
			else
			{
				//Afficher l'erreur approprié à l'utilisateur selon le code
				m_interface.afficherStatut(code);
			}
        }

		//Avertir la form qu'elle doit refresh
		private void refreshForm()
		{
			m_interface.effacerPiece(); //Effacer l'ancienne configuration
			m_interface.afficherPiece(m_plateau.ToString()); //Afficher la nouvelle configuration
		}

		private void verifierStatutJeu()
		{
			//Le joueur est en echec et au minimum un deplacement est possible.
			if ( m_plateau.echec(m_tour) && !m_plateau.deplacementsImpossibles(m_tour) )
			{
                m_interface.afficherStatut(8);
			}

			//Le joueur n'a aucun deplacement possible (echec et mat)
			if (m_plateau.echec(m_tour) && m_plateau.deplacementsImpossibles(m_tour) )
			{
				m_interface.afficherStatut(10);
                finPartie();
            }

			//Les joueurs ne sont pas en échec et il n'y a aucun deplacement possible. (echec et pat)
			if ( !m_plateau.echec(m_tour) && !m_plateau.echec(!m_tour) && m_plateau.deplacementsImpossibles(m_tour) )
			{
				m_interface.afficherStatut(12);
				finPartie();
			}

			//La partie est nulle
			if ( m_plateau.nulle() )
            {
                m_interface.afficherStatut(11);
                finPartie();
            }
		}


        public void promouvoir(string p_piece)
        {
            m_piece = p_piece;
        }

        public void finPartie() {
            m_interface.freezeInterface();

            //Ajuster score si échec et mat
            if (m_plateau.deplacementsImpossibles(m_tour)) {
                if (JoueurTour == m_joueurBlanc) {
                    m_joueurBlanc.perdant(m_joueurNoir);
                    m_joueurNoir.gagnant(m_joueurBlanc);
                }
                else if (JoueurTour == m_joueurNoir) {
                    m_joueurNoir.perdant(m_joueurBlanc);
                    m_joueurBlanc.gagnant(m_joueurNoir);
                }

                m_interface.AfficherClassementJoueur();
            }
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
