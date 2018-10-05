using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Echec
{
    public class Pion : Mouvement
    {
		public Pion(bool p_couleur, string p_nom, bool p_collision, bool p_aBougee) : base(p_couleur, p_nom, p_collision, p_aBougee)
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

			//Si couleur blanc
			if (m_couleur)
			{
				// O O O
				// O X O
				// O P O
				if ((p_posPiece[1] - 1 == p_posCase[1]) && (p_posPiece[0] == p_posCase[0]) && (p_caseVide == true))
					return true;
				// O O O
				// O O X
				// O P O
				else if ((p_posPiece[1] - 1 == p_posCase[1]) && (p_posPiece[0] + 1 == p_posCase[0]) && (p_caseVide == false))
					return true;
				// O O O
				// X O O
				// O P O
				else if ((p_posPiece[1] - 1 == p_posCase[1]) && (p_posPiece[0] - 1 == p_posCase[0]) && (p_caseVide == false))
					return true;
				// O X O
				// O O O
				// O P O
				else if ((p_posPiece[1] - 2 == p_posCase[1]) && (p_posPiece[0] == p_posCase[0])  && (p_caseVide == true) && ( p_caseVide == true ) && (m_aBougee == false))
					return true;
				else
					return false;
			}
			else
			{
				if ((p_posPiece[1] + 1 == p_posCase[1]) && (p_posPiece[0] == p_posCase[0]) && (p_caseVide == true))
					return true;
				else if ((p_posPiece[1] + 1 == p_posCase[1]) && (p_posPiece[0] - 1 == p_posCase[0]) && (p_caseVide == false))
					return true;
				else if ((p_posPiece[1] + 1 == p_posCase[1]) && (p_posPiece[0] + 1 == p_posCase[0]) && (p_caseVide == false))
					return true;
				else if ((p_posPiece[1] + 2 == p_posCase[1]) && (p_posPiece[0] == p_posCase[0]) && (p_caseVide == true) && (p_caseVide == true) && (m_aBougee == false))
					return true;
				else
					return false;
			}
		}

		public int[]
	}
}
