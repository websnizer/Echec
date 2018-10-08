using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Echec
{
	public class Joueur
    {
		string m_nom;
		int m_victoires;
		int m_defaites;
		int m_classement;

		public Joueur(string p_nom, int p_victoires, int p_defaites, int p_classement)
		{
			m_nom = p_nom;
			m_victoires = p_victoires;
			m_defaites = p_defaites;
			m_classement = p_classement;
		}

		public void perdant ( Joueur leGagnant)
		{
            //Ajuster le score en conséquence?
            //à tester
            int Difference = Math.Abs(this.m_classement - leGagnant.m_classement);
            if (Math.Abs(this.m_classement - leGagnant.m_classement) < 500)
            {
                if (this.m_classement > leGagnant.m_classement)
                    this.m_classement += Difference / 4;
                else
                    this.m_classement += Difference / 2;
            }
            else
                if (this.m_classement > leGagnant.m_classement)
                this.m_classement += Difference / 2;
        }

		public void gagnant ( Joueur lePerdant)
		{
			//Ajuster le score en conséquence?
            //à tester
            int Difference = Math.Abs(this.m_classement - lePerdant.m_classement);
            if (Math.Abs(this.m_classement - lePerdant.m_classement) < 500)
            {
                if (lePerdant.m_classement > this.m_classement)
                    lePerdant.m_classement -= Difference / 2;
                else
                    lePerdant.m_classement -= Difference / 4;
            }
                if (lePerdant.m_classement > this.m_classement)
                lePerdant.m_classement -= Difference / 2;
		}

        public string NomJoueur
        {
            get {return m_nom; }
        }

        public int VictoiresJoueur
        {
            get {return m_victoires; }
            set { m_victoires = value; }
        }

        public int DefaitesJoueur
        {
            get {return m_defaites; }
            set {m_defaites = value; }
        }

        public int ClassementJoueur
        {
            get {return m_classement; }
            set { m_classement = value;}
        }
    }
}
