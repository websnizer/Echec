using System;
using System.Collections.Generic;
using System.Drawing;
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
		Echec m_echec; //Référence à Echec

		//Constructeur
        public Partie(Joueur p_joueur1, Joueur p_joueur2, Echec p_echec)
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
			m_echec = p_echec;
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
					m_interface.afficherStatut(14);
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

		private void verifierStatutJeu() //Vérifier les échecs, échecs et mat, échecs et pat, nulles
		{
            bool echec = m_plateau.echec(m_tour);
            bool deplacementsImpossibles = m_plateau.deplacementsImpossibles(m_tour);

			if (m_plateau.nbCoups() == 50)
			{
				m_interface.afficherStatut(13);
				finPartie();
			}

			//Le joueur est en échec, mais peut se déplacer
			if (echec && !deplacementsImpossibles)
			{
                m_interface.afficherStatut(8);
			}

			//Le joueur n'a aucun déplacement possible (échec et mat)
			if (echec && deplacementsImpossibles)
			{
				m_interface.afficherStatut(10);
                finPartie();
            }

			//Les joueurs ne sont pas en échec et le joueur n'a aucun déplacement possible (échec et pat)
			if (!echec && !m_plateau.echec(!m_tour) && deplacementsImpossibles)
			{
				m_interface.afficherStatut(12);
				finPartie();
			}

			//La partie est nulle
			if (m_plateau.nulle())
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
			if (m_plateau.deplacementsImpossibles(m_tour))
			{
				if (JoueurTour == m_joueurBlanc)
				{
					m_joueurBlanc.perdant(m_joueurNoir);
					m_joueurNoir.gagnant(m_joueurBlanc);
				}
				else if (JoueurTour == m_joueurNoir)
				{
					m_joueurNoir.perdant(m_joueurBlanc);
					m_joueurBlanc.gagnant(m_joueurNoir);
				}
				m_interface.AfficherClassementJoueur();

				//Mettre la liste de joueur à jour
				MettreAJourClassement();
			}
		}

		private void MettreAJourClassement()
		{
			//Les noms des joueurs
			string NomJoueurBlanc;
			string NomJoueurNoir;
			string NomJoueurListe;

			//La liste avec les valeurs mises à jours
			List<string> ListJoueurUpdate = new List<string>();

			//Pour tous les éléments dans la liste de joueurs
			for (int i = 0; i < m_echec.ListeJoueurs.Count; i++)
			{
				//Met le nom du joueur blanc dans une variable
				string leJoueurBlanc = m_joueurBlanc.ToString();
				string[] InfosJoueurBlanc = leJoueurBlanc.Split(',');
				NomJoueurBlanc = InfosJoueurBlanc[0];

				//Met le nom du joueur noir dans une variable
				string leJoueurNoir = m_joueurNoir.ToString();
				string[] InfosJoueurNoir = leJoueurNoir.Split(',');
				NomJoueurNoir = InfosJoueurNoir[0];

				//Le nom du joueur dans la liste (à l'index de la boucle)
				string leJoueurListe = m_echec.ListeJoueurs[i].ToString();
				string[] InfosJoueurListe = leJoueurListe.Split(',');
				NomJoueurListe = InfosJoueurListe[0];

				if (NomJoueurBlanc == NomJoueurListe)
				{
					//Si le nom du joueur blanc est le joueur dans la liste on l'ajoute dans la liste mis à jour
					//Avec ses stats mis à jours
					string LeJoueur = m_joueurBlanc.ToString();

					ListJoueurUpdate.Add(LeJoueur);
					//m_echec.ListeJoueurs[m_echec.ListeJoueurs.FindIndex(ind => ind.Equals(leJoueurListe))] = m_joueurBlanc.ToString();

				}
				else if (NomJoueurNoir == NomJoueurListe)
				{
					//Si le nom du joueur noir est le joueur dans la liste on l'ajoute dans la liste mis à jour
					//Avec ses stats mis à jour
					m_echec.ListeJoueurs[i] = m_joueurNoir.ToString();

					string LeJoueur = m_joueurNoir.ToString();

					ListJoueurUpdate.Add(LeJoueur);

					//m_echec.ListeJoueurs[m_echec.ListeJoueurs.FindIndex(ind => ind.Equals(leJoueurListe))] = m_joueurNoir.ToString();
				}
				else
				{
					//Sinon on parcour tous les éléments dans la liste de joueurs
					foreach (string J in m_echec.ListeJoueurs)
					{
						//Stock le nom du joueur en cours (dans la boucle)
						string[] InfosJoueur = J.Split(',');
						string NomJoueur = InfosJoueur[0];

						//Si le nom du joueur est égale au nom du joueur dans la liste (non mis à jour)
						//On l'ajoute à la liste (mis à jour)
						if (NomJoueur == NomJoueurListe)
						{
							string LeJoueur = J;

							ListJoueurUpdate.Add(LeJoueur);

						}
					}
				}
			}

			//Assigne la liste mis à jour à l'ancienne liste
			m_echec.ListeJoueurs.Clear();
			m_echec.ListeJoueurs = ListJoueurUpdate;
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
