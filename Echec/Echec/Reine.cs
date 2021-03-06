﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Echec
{
	public class Reine : Piece
    {
		public Reine(bool p_couleur, string p_nom, bool p_collision) : base(p_couleur, p_nom, p_collision)
		{
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

			// O X O
			// X Q X
			// O X O
			if ((p_posPiece[1] == p_posCase[1]) || (p_posPiece[0] == p_posCase[0]))
				return true;
			// X O X
			// O Q O
			// X O X
			else if (Math.Abs(p_posPiece[0] - p_posCase[0]) == Math.Abs(p_posPiece[1] - p_posCase[1]))
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

				//Trouver le sens
				if (xDebut == xFin)
				{
					//Déterminer qu'elle des deux coordonnées est la plus grande et la plus petite sur l'Axe Y
					grand = (yDebut > yFin) ? yDebut : yFin;
					petit = (yDebut > yFin) ? yFin : yDebut;
					//Déterminer la direction sur l'Axe Y
					sens = (yDebut > yFin) ? "H" : "B";
					//Le nombre de cases par ou elle passe
					nbCases = grand - petit;
				}
				else if (yDebut == yFin)
				{
					//Déterminer qu'elle des deux coordonnées est la plus grande et la plus petite sur l'Axe X
					grand = (xDebut > xFin) ? xDebut : xFin;
					petit = (xDebut > xFin) ? xFin : xDebut;
					//Déterminer la direction sur l'Axe X
					sens = (xDebut > xFin) ? "G" : "D";
					//Le nombre de cases par ou elle passe
					nbCases = grand - petit;
				}
				else
				{
					//Trouver le plus grand et le plus petit
					grand = (yDebut > yFin) ? yDebut : yFin;
					petit = (xDebut > xFin) ? xFin : xDebut;
					//Déterminer la direction sur l'Axe X et Y
					sens = (yDebut > yFin) ? "H" : "B";
					sens += (xDebut > xFin) ? "G" : "D";
					//Le nombre de cases par ou elle passe
					nbCases = nbCases = ((Math.Abs(yDebut - yFin) + Math.Abs(xDebut - xFin)) / 2);
				}

				//Ajouter toutes les positions que la pièce emprunte.
				if (sens == "H")
					for (int i = 1; i < nbCases; i++)
						ajouterPosition(xDebut, yDebut - i, liste);

				else if (sens == "B")
					for (int i = 1; i < nbCases; i++)
						ajouterPosition(xDebut, yDebut + i, liste);

				else if (sens == "G")
					for (int i = 1; i < nbCases; i++)
						ajouterPosition(xDebut - i, yDebut, liste);

				else if (sens == "D")
					for (int i = 1; i < nbCases; i++)
						ajouterPosition(xDebut + i, yDebut, liste);

				if (sens == "HG")
					for (int i = 1; i < nbCases; i++)
						ajouterPosition(xDebut - i, yDebut - i, liste);

				else if (sens == "HD")
					for (int i = 1; i < nbCases; i++)
						ajouterPosition(xDebut + i, yDebut - i, liste);

				else if (sens == "BG")
					for (int i = 1; i < nbCases; i++)
						ajouterPosition(xDebut - i, yDebut + i, liste);

				else if (sens == "BD")
					for (int i = 1; i < nbCases; i++)
						ajouterPosition(xDebut + i, yDebut + i, liste);

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

		public override bool aBougee
		{
			get { return false; }
		}

        public override void bouge()
        {
        }
    }
}
