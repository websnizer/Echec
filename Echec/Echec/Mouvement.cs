using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Echec
{
	public abstract class Mouvement : Piece
	{
		protected bool m_aBougee;

		public Mouvement(bool p_couleur, string p_nom, bool p_collision, bool p_aBougee) : base(p_couleur, p_nom, p_collision)
		{
			m_aBougee = p_aBougee;
		}

		//Accesseur de la donnée membre aBougee
		public override bool aBougee
		{
			get { return m_aBougee; }
		}

        public override void bouge()
        {
        }
    }
}
