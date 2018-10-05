using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Echec
{
	public class Tour : Mouvement
    {
		public Tour(bool p_couleur, string p_nom, bool p_collision, bool p_aBougee) : base(p_couleur, p_nom, p_collision, p_aBougee)
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
			// X T X
			// O X O
			if ( (p_posPiece[1] == p_posCase[1]) || (p_posPiece[0] == p_posCase[0]) )
				return true;
			else
				return false;
		}
	}

}
