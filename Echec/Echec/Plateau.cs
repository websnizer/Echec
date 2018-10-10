using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Echec
{
	public class Plateau
	{
		private Piece[,] m_echiquier; //Tableau qui représente l'échiquier avec ses pièces
		private Stack<Piece[,]> m_historique; //Historique des échiquiers

		public Plateau() //Constructeur
		{
			m_echiquier = new Piece[8, 8]; //Créer l'échiquier
			m_historique = new Stack<Piece[,]>(); //Créer l'historique
			preparerEchiquier();
		}

		public override string ToString() //Sérialiser l'échiquier
		{
			string serial = ""; //Chaine à retourner

			for (int x = 0; x < 8; x++)
			{
				for (int y = 0; y < 8; y++)
				{
					if (m_echiquier[x, y] == null) //Si la case est vide
					{
						serial += "x,";
					}
					else //Sinon sérialiser la pièce
					{
						if ((x == 7) && (y == 7))
						{
							serial += m_echiquier[x, y].ToString();
						}
						else
						{
							serial += m_echiquier[x, y].ToString() + ",";
						}
					}
				}
			}

			return serial;
		}

		private void preparerEchiquier() //Préparer l'échiquier pour une partie
		{
			//Placer les pièces noires
			m_echiquier[0, 0] = new Tour(false, "Tour", true, false); //Noir, nom, possibilité de collisions, n'a pas bougé
			m_echiquier[1, 0] = new Cavalier(false, "Cavalier", false); //Noir, nom, pas de possibilité de collisions
			m_echiquier[2, 0] = new Fou(false, "Fou", true); //Noir, nom, possibilité de collisions
			m_echiquier[3, 0] = new Reine(false, "Reine", true); //Noir, nom, possibilité de collisions
			m_echiquier[4, 0] = new Roi(false, "Roi", true, false); //Noir, nom, possibilité de collisions, n'a pas bougé
			m_echiquier[5, 0] = new Fou(false, "Fou", true); //Noir, nom, possibilité de collisions
			m_echiquier[6, 0] = new Cavalier(false, "Cavalier", false); //Noir, nom, pas de possibilité de collisions
			m_echiquier[7, 0] = new Tour(false, "Tour", true, false); //Noir, nom, possibilité de collisions, n'a pas bougé					
			//Placer les pions noirs
			for (int x = 0; x < 8; x++)
			{
				m_echiquier[x, 1] = new Pion(false, "Pion", true, false); //Noir, nom, possibilité de collisions, n'a pas bougé
			}

			//Placer les pions blancs
			for (int x = 0; x < 8; x++)
			{
				m_echiquier[x, 6] = new Pion(true, "Pion", true, false); //Blanc, nom, possibilité de collisions, n'a pas bougé
			}
			//Placer les pièces blanches
			m_echiquier[0, 7] = new Tour(true, "Tour", true, false); //Blanc, nom, possibilité de collisions, n'a pas bougé
			m_echiquier[1, 7] = new Cavalier(true, "Cavalier", false); //Blanc, nom, pas de possibilité de collisions
			m_echiquier[2, 7] = new Fou(true, "Fou", true); //Blanc, nom, possibilité de collisions
			m_echiquier[3, 7] = new Reine(true, "Reine", true); //Blanc, nom, possibilité de collisions
			m_echiquier[4, 7] = new Roi(true, "Roi", true, false); //Blanc, nom, possibilité de collisions, n'a pas bougé
			m_echiquier[5, 7] = new Fou(true, "Fou", true); //Blanc, nom, possibilité de collisions
			m_echiquier[6, 7] = new Cavalier(true, "Cavalier", false); //Blanc, nom, pas de possibilité de collisions
			m_echiquier[7, 7] = new Tour(true, "Tour", true, false); //Blanc, nom, possibilité de collisions, n'a pas bougé
		}

		private void ajouterHistorique() //Ajouter l'échiquier à l'historique avant de le modifier
		{
			m_historique.Push(m_echiquier);
		}

		private void effacerHistorique() //Effacer l'historique
		{
			m_historique.Clear();
		}

		public int validerCoup(int[] p_posPiece, int[] p_posCase, bool p_tour) //Valider si le coup est légal et sinon renvoyer un code d'erreur
		{
			int xPiece = p_posPiece[0]; //x de la pièce
			int yPiece = p_posPiece[1]; //y de la pièce
			int xCase = p_posCase[0]; //x de la case
			int yCase = p_posCase[1]; //y de la case

			//Cas erreur 1: Il n'y a pas de pièce à jouer sur cette case 
			if (m_echiquier[xPiece, yPiece] == null)
			{
				return 1;
			}

			bool couleurPiece = m_echiquier[xPiece, yPiece].Couleur; //Couleur de la pièce

			//Cas erreur 2: La pièce à jouer n'est pas une pièce du joueur auquel c'est le tour
			if (!verifierTour(couleurPiece, p_tour))
			{
				return 2;
			}

			//Cas erreur 3: La case de départ et la case d'arrivée sont la même case
			if ((xPiece == xCase) && (yPiece == yCase))
			{
				return 3;
			}

			//Cas erreur 4: Il y a une pièce alliée sur la case visée
			if ((m_echiquier[xCase, yCase] != null) && (m_echiquier[xCase, yCase].Couleur == couleurPiece))
			{
				return 4;
			}

			if (!verifierRoque(p_posPiece, p_posCase)) //Vérifier si le joueur a voulu faire un roque et si ce roque est possible
			{
				//Cas erreur 5: La pièce ne peut pas se déplacer de cette façon
				if (!m_echiquier[xPiece, yPiece].validerDeplacement(p_posPiece, p_posCase, m_echiquier[xCase, yCase] == null))
				{
					return 5;
				}

				//Cas erreur 6: Une pièce est dans le chemin et empêche le déplacement
				if (m_echiquier[xPiece, yPiece].Collision) //Si la pièce n'a pas la permission de passer par dessus d'autres pièces
				{
					if (collision(p_posPiece, p_posCase))
					{
						return 6;
					}
				}

				//Cas erreur 7: Le joueur se met lui-même en échec avec ce coup
				if (miseEnEchec(p_posPiece, p_posCase, couleurPiece))
				{
					return 7;
				}
			}

			return 0; //Le coup est accepté
		}

		private bool verifierTour(bool p_couleur, bool p_tour) //Vérifier si la pièce jouée est celle du joueur auquel c'est le tour
		{
			if (p_couleur == p_tour)
			{
				return true;
			}
			return false;
		}

		private bool collision(int[] p_posPiece, int[] p_posCase) //Vérifier s'il y a une collision lors du déplacement
		{
			int x = p_posPiece[0]; //x de la pièce
			int y = p_posPiece[1]; //y de la pièce
			int xCase = p_posCase[0]; //x de la case
			int yCase = p_posCase[1]; //y de la case

			//Stocker les cases par lesquelles la pièce devra se déplacer
			List<int[]> route = m_echiquier[x, y].routeDeplacement(p_posPiece, p_posCase, m_echiquier[xCase, yCase] == null);

			int xTest; //x à tester
			int yTest; //y à tester

			for (int i = 1; i < route.Count - 1; i++) //Vérifier si chaque case est vide
			{
				xTest = (route[i])[0];
				yTest = (route[i])[1];

				if (m_echiquier[xTest, yTest] != null) //La case n'est pas vide
				{
					return true;
				}
			}

			return false;
		}

		public void deplacerPiece(int[] p_posPiece, int[] p_posCase) //Déplacer la pièce sur l'échiquier
		{
			if (verifierRoque(p_posPiece, p_posCase)) //Si le joueur a voulu faire un roque
			{
				effectuerRoque(p_posPiece, p_posCase);
			}
			else //Sinon faire un déplacement normal
			{
				effectuerDeplacement(p_posPiece, p_posCase);
			}
		}

		private void effectuerDeplacement(int[] p_posPiece, int[] p_posCase) //Effectuer un déplacement normal d'une pièce
		{
			int xPiece = p_posPiece[0]; //x de la pièce
			int yPiece = p_posPiece[1]; //y de la pièce
			int xCase = p_posCase[0]; //x de la case
			int yCase = p_posCase[1]; //y de la case

			if ((m_echiquier[xPiece, yPiece] is Pion) || (m_echiquier[xCase, yCase] != null)) //Gérer l'historique
			{
				effacerHistorique(); //Effacer si pièce est pion ou si pièce mangée
			}
			else
			{
				ajouterHistorique(); //Ajouter l'échiquier à l'historique avant de le changer
			}

			m_echiquier[xCase, yCase] = m_echiquier[xPiece, yPiece];
			m_echiquier[xPiece, yPiece] = null;
		}

		public bool echec(bool p_couleur) //Vérifier si le joueur passé en paramètre est en état d'échec
		{
			int[] posRoi = new int[2]; //La case du roi du joueur

			for (int x = 0; x < 8; x++) //Trouver le roi
			{
				for (int y = 0; y < 8; y++)
				{
					if ((m_echiquier[x, y] is Roi) && (m_echiquier[x, y].Couleur == p_couleur)) //Si c'est un roi de la même couleur
					{
						posRoi[0] = x;
						posRoi[1] = y;
					}
				}
			}

			int[] posDepart = new int[2]; //La case à tester

			for (int x = 0; x < 8; x++) //Tester si les pièces peuvent se rendre jusqu'au roi
			{
				for (int y = 0; y < 8; y++)
				{
					posDepart[0] = x;
					posDepart[1] = y;
					if (validerCoup(posDepart, posRoi, !p_couleur) == 0 || validerCoup(posDepart, posRoi, !p_couleur) == 7) //Jouer les coups comme si c'est le tour du joueur opposé
					{
						return true;
					}
				}
			}

			return false;
		}

		private List<int[]> emplacementsPieces(bool p_couleur) //Retourner toutes les pièces de cette couleur
		{
			List<int[]> pieces = new List<int[]>(); //Les pièces de la couleur

			for (int x = 0; x < 8; x++) //Trouver toutes les pièces
			{
				for (int y = 0; y < 8; y++)
				{
					if ((m_echiquier[x, y] != null) && (m_echiquier[x, y].Couleur == p_couleur)) //Si la couleur est la même
					{
						int[] pos = new int[2]; //Les coordonnées à entrer
						pos[0] = x;
						pos[1] = y;
						pieces.Add(pos);
					}
				}
			}

			return pieces;
		}

		public bool echecMat(bool p_couleur) //Vérifier si le joueur passé en paramètre est en état d'échec et mat
		{
			List<int[]> pieces = emplacementsPieces(p_couleur); //Les pièces de la couleur
			int[] posCase = new int[2]; //Les coordonnées des cases

			for (int x = 0; x < 8; x++) //Boucler sur toutes les cases
			{
				for (int y = 0; y < 8; y++)
				{
					posCase[0] = x;
					posCase[1] = y;

					for (int i = 0; i < pieces.Count; i++) //Pour chaque pièce, regarder si un déplacement (du roi?) est possible
					{
						if (validerCoup(pieces[i], posCase, p_couleur) == 0) //Si au moins un déplacement est possible sans échec, ce n'est pas mat
						{
							return false;
						}
					}
				}
			}

			return true; //Si tous les déplacements de pièces sont impossibles, c'est mat
		}

		public bool nulle() //Vérifier si le jeu se termine par une nulle
		{
			return false;
		}

        private bool miseEnEchec(int[] p_posPiece, int[] p_posCase, bool p_couleur) //Vérifier si le joueur se met en échec avec le déplacement voulu
        {
            bool miseEnEchec; //Si le joueur se met en échec
            Piece anciennePiece = null; //Conserver la pièce qui sera écrasée
            int xPiece = p_posPiece[0]; //x de la pièce
            int yPiece = p_posPiece[1]; //y de la pièce
            int xCase = p_posCase[0]; //x de la case
            int yCase = p_posCase[1]; //y de la case

            if (m_echiquier[xCase, yCase] != null) //Vérifier si une pièce sera écrasée
            {
                anciennePiece = m_echiquier[xCase, yCase];
            }

            m_echiquier[xCase, yCase] = m_echiquier[xPiece, yPiece]; //Déplacer la pièce temporairement pour le test
            m_echiquier[xPiece, yPiece] = null;

            miseEnEchec = echec(p_couleur); //Tester la mise en échec

            m_echiquier[xPiece, yPiece] = m_echiquier[xCase, yCase]; //Remettre les pièces à leur place

            if (anciennePiece != null) //Remettre la pièce écrasée si c'est le cas
            {
                m_echiquier[xCase, yCase] = anciennePiece;
            }
            else //Sinon remettre à null
            {
                m_echiquier[xCase, yCase] = null;
            }

            return miseEnEchec;
        }

		public bool verifierPromo(int[] p_posCase) //Vérifier si un pion du joueur a atteint le fond de l'échiquier
		{
			int x = p_posCase[0]; //x de la case
			int y = p_posCase[1]; //y de la case
			bool couleurPiece = m_echiquier[x, y].Couleur; //Couleur de la pièce

			if (m_echiquier[x, y] is Pion) //Vérifier si la pièce est un pion
			{
				if (couleurPiece) //Si joueur blanc, le fond est la rangée 0
				{
					if (y == 0)
					{
						return true;
					}
				}
				else //Si joueur noir, le fond est la rangée 7
				{
					if (y == 7)
					{
						return true;
					}
				}
			}

			return false;
		}

		public void promouvoirPion(int[] p_posCase, string p_pieceChoisie) //Changer le pion pour une pièce choisie
		{
			int x = p_posCase[0]; //x de la pièce
			int y = p_posCase[1]; //y de la pièce
			bool couleurPiece = m_echiquier[x, y].Couleur; //Couleur de la pièce

			effacerHistorique(); //Effacer l'historique 

			switch (p_pieceChoisie) //Changer selon la pièce désirée
			{
				case "Reine":
					m_echiquier[x, y] = new Reine(couleurPiece, "Reine", true);
					break;
				case "Tour":
					m_echiquier[x, y] = new Tour(couleurPiece, "Tour", true, true);
					break;
				case "Cavalier":
					m_echiquier[x, y] = new Fou(couleurPiece, "Fou", true);
					break;
				case "Fou":
					m_echiquier[x, y] = new Cavalier(couleurPiece, "Cavalier", false);
					break;
			}
		}

		private bool verifierRoque(int[] p_posPiece, int[] p_posCase) //Vérifier si le joueur a voulu faire un roque et si ce roque est possible
		{
			int xPiece = p_posPiece[0]; //x de la pièce
			int yPiece = p_posPiece[1]; //y de la pièce
			int xCase = p_posCase[0]; //x de la case
			int yCase = p_posCase[1]; //y de la case
			bool couleur = m_echiquier[xPiece, yPiece].Couleur; //Couleur de la pièce
			List<int> xCasesEntre = new List<int>(); //Liste des coordonnées en x entre le roi et la tour

			//La pièce est son roi et n'a jamais bougé du jeu
			if ((m_echiquier[xPiece, yPiece] is Roi) && !m_echiquier[xPiece, yPiece].aBougee)
			{
				Piece tour; //La tour visée par le roque

				if (xCase > xPiece) //Viser la tour supérieure
				{
					if ((xCase + 1) < 8)
					{
						tour = m_echiquier[xCase + 1, yCase];

						//La tour vers laquelle s'est déplacée le roi est bien une tour de sa couleur qui n'a jamais bougée
						if ((tour is Tour) && (tour.Couleur == couleur) && !tour.aBougee)
						{
							xCasesEntre.Add(xPiece + 1);
							xCasesEntre.Add(xPiece + 2);
						}
					}
				}
				else if (xCase < xPiece) //Viser la tour inférieure
				{
					if ((xCase - 2) >= 0)
					{
						tour = m_echiquier[xCase - 2, yCase];

						//La tour vers laquelle s'est déplacée le roi est bien une tour de sa couleur qui n'a jamais bougée
						if ((tour is Tour) && (tour.Couleur == couleur) && !tour.aBougee)
						{
							xCasesEntre.Add(xPiece - 1);
							xCasesEntre.Add(xPiece - 2);
							xCasesEntre.Add(xPiece - 3);
						}
					}
				}

				//Vérifier que le roque désiré est bien légal avec les pièces désirées
				if (xCasesEntre.Count > 0)
				{
					return verifierRoqueLegal(xCasesEntre, p_posPiece, couleur);
				}
			}

			return false;
		}

		private bool verifierRoqueLegal(List<int> p_xCasesEntre, int[] p_posPiece, bool p_couleur) //Vérifier que le roque est aussi légal
		{
			int xPiece = p_posPiece[0]; //x de la pièce
			int yPiece = p_posPiece[1]; //y de la pièce

			if (!echec(p_couleur)) //Ne pas accepter le roque si le roi est présentement en échec
			{
				for (int i = 0; i < p_xCasesEntre.Count; i++) //Vérifier si chaque case est vide
				{
					if (m_echiquier[p_xCasesEntre[i], yPiece] != null) //La case n'est pas vide
					{
						return false;
					}
				}

				int[] posTest = new int[2]; //La case à tester
				posTest[1] = yPiece;

				for (int i = 0; i < 2; i++) //Vérifier si les cases par lesquelles passe le roi sont menacées d'un échec
				{
					posTest[0] = p_xCasesEntre[i];

					if (miseEnEchec(p_posPiece, posTest, p_couleur)) //Cette case est menacée d'un échec
					{
						return false;
					}
				}
			}

			return true;
		}

		private void effectuerRoque(int[] p_posPiece, int[] p_posCase) //Effectuer un roque
		{
			int xPiece = p_posPiece[0]; //x de la pièce
			int yPiece = p_posPiece[1]; //y de la pièce
			int xCase = p_posCase[0]; //x de la case
			int yCase = p_posCase[1]; //y de la case

			effectuerDeplacement(p_posPiece, p_posCase); //Déplacer le roi vers sa case

			int[] tour = new int[2]; //La tour visée par le roque
			int[] tourCase = new int[2]; //La case visée par la tour

			tour[1] = yCase;
			tourCase[1] = yCase;

			if (xCase > xPiece) //Viser la tour supérieure
			{
				tour[0] = xCase + 1;
				tourCase[0] = xCase - 1;
			}
			else if (xCase < xPiece) //Viser la tour inférieure
			{
				tour[0] = xCase - 2;
				tourCase[0] = xCase + 1;
			}

			effectuerDeplacement(tour, tourCase); //Déplacer la tour également
		}
	}
}