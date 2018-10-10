using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Echec
{
	public class Piece
	{
		protected bool m_couleur; //La couleur de la piece. true=blanc false=noir
		protected string m_nom; //Le nom de la pièce 
		protected bool m_collision; //Permet les collision ou non

		//Constructeur de piece
		public Piece(bool p_couleur, string p_nom, bool p_collision)
		{
			m_couleur = p_couleur;
			m_nom = p_nom;
			m_collision = true;
		}

		//Vérifier si le déplacement fait partie de ses mouvements.
		//p_posPiece = La position initiale
		//p_posCase = La position finale
		//p_casevide = Statut de la case à la position finale
		public virtual bool validerDeplacement(int[] p_posPiece, int[] p_posCase, bool p_caseVide)
		{
			//Aucune valeur puisque tous les enfants override?
			return false;
		}

        public virtual void bouge()
        {
        }

		public virtual List<int[]> routeDeplacement(int[] p_posPiece, int[] p_posCase, bool p_caseVide)
		{
			return null;
		}

		public override string ToString()
		{
			return (m_couleur) ? m_nom.ToLower() : m_nom.ToUpper();
		}

		//Accesseur de la donnée membre collision
		public bool Collision
		{
			get{ return m_collision;}
		}

		public virtual bool aBougee
		{
			get { return false; }
		}

		//Accesseur de la donnée membre Couleur
		public bool Couleur
		{
			get { return m_couleur; }
		}

		//Accesseur de la donnée membre Nom
		public string Nom
		{
			get { return m_nom; }
		}
	}
}
