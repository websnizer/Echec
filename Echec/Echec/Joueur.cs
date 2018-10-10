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
            //La diférence entre les deux classements
            int Difference = Math.Abs(this.m_classement - leGagnant.m_classement);

            if (Math.Abs(Difference) < 500)
            {
                //Si la différence entre les deux classements est plus petite que 500
                //Si le gagnant à un classement plus élevé que le perdant, on ajoute la différence divisé par 4 à son classement
                if (leGagnant.m_classement > this.m_classement)
                    leGagnant.m_classement += Difference / 4;
                else
                    //Si le classement du gagnant est plus petit que le classement du perdant, on lui ajoute la différence divisé par 2
                    leGagnant.m_classement += Difference / 2;
            }
            else
                //Si la différence est plus grande que 500 on ajoute la différence divisé par 2
                if (leGagnant.m_classement > this.m_classement)
                leGagnant.m_classement += Difference / 2;

            this.m_defaites++;

            //Mettre à jour le fichier ?
        }

        public void gagnant ( Joueur lePerdant)
		{
            //La diférence entre les deux classements
            int Difference = Math.Abs(this.m_classement - lePerdant.m_classement);
            
            if (Math.Abs(Difference) < 500)
            {
                //Si la différence entre les deux classements est plus petite que 500
                //Si le perdant à un classement plus élevé que le gagnant, on ôte la différence divisé par 2 à son classement
                if (lePerdant.m_classement > this.m_classement)
                    lePerdant.m_classement -= Difference / 2;
                else
                    //Si son classment est plus petit que le gagnant on divise par 4
                    lePerdant.m_classement -= Difference / 4;
            }
            else
                //Si la différence est plus grande que 500 on ôte la différence divisé par 2
                if (lePerdant.m_classement > this.m_classement)
                lePerdant.m_classement -= Difference / 2;

            this.m_victoires++;

            //Mettre à jour le fichier ?
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


        //Sérialiser l'échiquier
        public override string ToString()
        {
            return (m_nom + "," + m_victoires + "," + m_defaites + "," + m_classement); 
        }
    }
}
