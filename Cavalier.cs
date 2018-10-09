using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Echec
{
	public class Cavalier : Piece
    {
		public Cavalier(bool p_couleur, string p_nom, bool p_collision) : base(p_couleur, p_nom, p_collision)
		{
			m_collision = false;
		}

		//Vérifier si le déplacement fait partie de ses mouvements.
		//p_posPiece = La position initiale
		//p_posCase = La position finale
		//p_casevide = Statut de la case à la position finale
		public override bool validerDeplacement(int[] p_posPiece, int[] p_posCase, bool p_caseVide)
		{
			//x,y
			//Différent que ca propre position
			if ((p_posPiece[0] == p_posCase[0]) && (p_posPiece[1] == p_posCase[1]))
				return false;

			// O X O X O
			// X O O O X
			// O O F O O
			// X O O O X
			// O X O X O
			if ( (Math.Abs(p_posPiece[0] - p_posCase[0]) == 1 && Math.Abs(p_posPiece[1] - p_posCase[1]) == 2) || (Math.Abs(p_posPiece[0] - p_posCase[0]) == 2 && Math.Abs(p_posPiece[1] - p_posCase[1]) == 1) )
				return true;
			else
				return false;
		}

		//Création de la liste des cases occupés lors d'un déplacements à partir de deux coordonnées
		public override List<int[]> routeDeplacement(int[] p_posPiece, int[] p_posCase, bool p_caseVide)
		{
			if (validerDeplacement(p_posPiece, p_posCase, p_caseVide))
			{
				int xDebut = p_posPiece[0]; //La coordonnées x du début
				int yDebut = p_posPiece[1]; //La coordonnées y du début
				int xFin = p_posCase[0]; //La coordonnées x de la fin
				int yFin = p_posCase[1]; //La coordonnées y de la fin
				string sens = ""; //Le direction du mouvement (Haut, Gauche, Bas, Droite)
				int grand; //La coordonnées la plus grande
				int petit; //La coordonnées la plus petite
				int nbCases; //Le nombre de cases par ou elle passe
				List<int[]> liste = new List<int[]>(); //Liste de toutes les positions pour atteindre la case finale

				//Ajouter la position initiale
				ajouterPosition(xDebut, yDebut, liste);

				//Ajouter toutes les positions que la pièce emprunte.
				if ( (yDebut == yFin + 2) && (xDebut == xFin + 1) ) //Sens HHG
					for ( int i = 1; i < 3; i++)
						ajouterPosition(xDebut, yDebut - i, liste);

				else if ( (yDebut == yFin + 1) && (xDebut == xFin + 2)) //Sens HG
					for (int i = 1; i < 3; i++)
						ajouterPosition(xDebut - i, yDebut, liste);

				else if ( (yDebut == yFin + 2) && (xDebut == xFin - 1)) //Sens HHD
					for (int i = 1; i < 3; i++)
						ajouterPosition(xDebut, yDebut - i, liste);

				else if ( (yDebut == yFin + 1) && (xDebut == xFin - 2)) //Sens HD
					for (int i = 1; i < 3; i++)
						ajouterPosition(xDebut + i, yDebut, liste);

				else if ( (yDebut == yFin - 2) && (xDebut == xFin + 1)) //Sens BBG
					for (int i = 1; i < 3; i++)
						ajouterPosition(xDebut, yDebut + i, liste);

				else if ( (yDebut == yFin - 1) && (xDebut == xFin + 2)) //Sens BG
					for (int i = 1; i < 3; i++)
						ajouterPosition(xDebut + i, yDebut, liste);

				else if ( (yDebut == yFin - 2) && (xDebut == xFin - 1)) //Sens BBD
					for (int i = 1; i < 3; i++)
						ajouterPosition(xDebut, yDebut + i, liste);

				else if ( (yDebut == yFin - 1) && (xDebut == xFin - 2)) //Sens BD
					for (int i = 1; i < 3; i++)
						ajouterPosition(xDebut + i, yDebut, liste);


				//Ajouter la position finale
				ajouterPosition(xFin, yFin, liste);

				return liste;
			}
			return null;
		}

		//Ajouter une position à la liste
		private void ajouterPosition(int x, int y, List<int[]> liste)
		{
			int[] position = new int[2]; //Une position
			position[0] = x;
			position[1] = y;
			liste.Add(position);
		}
	}
}
