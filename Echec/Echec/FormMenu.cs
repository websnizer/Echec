using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Echec
{
    public partial class FormMenu : Form
    {
		Echec m_echec;

		public FormMenu(Echec p_echec)
        {
            InitializeComponent();
			m_echec = p_echec;
        }

		private void btn_Partie_Click(object sender, EventArgs e)
		{
            //Partie laPartie = new Partie();
			FormPartie m_form = new FormPartie();
			m_form.Show();
		}

		private void jouer()
		{

		}

		private void quitter()
		{
            Close();
		}

		private void afficherJoueurs()
		{
            for (int i = 0; i < m_echec.ListeJoueurs.Count; i++)
            {
                //les informations du joueurs en cours (dans la boucle)
                string LeNom = m_echec.ListeJoueurs[i].NomJoueur;
                int lesVictoires = m_echec.ListeJoueurs[i].VictoiresJoueur;
                int lesDefaites = m_echec.ListeJoueurs[i].DefaitesJoueur;
                int LeClassement = m_echec.ListeJoueurs[i].ClassementJoueur;
                //lst_ListeJoueurs.Items.Add(new ListViewItem(new string[] { LeNom, lesVictoires.ToString(), lesDefaites.ToString(), LeClassement.ToString() }));


                //This shit doesn't work
                lst_ListeJoueurs.Columns.Add("Nom", 100);
                lst_ListeJoueurs.Columns.Add("Victoires", 50);
                lst_ListeJoueurs.Columns.Add("Défaites", 50);
                lst_ListeJoueurs.Columns.Add("Classement", 50);

                ListViewItem row = new ListViewItem(LeNom);
                row.SubItems.Add(new ListViewItem.ListViewSubItem(row, lesVictoires.ToString()));
                row.SubItems.Add(new ListViewItem.ListViewSubItem(row, lesDefaites.ToString()));
                row.SubItems.Add(new ListViewItem.ListViewSubItem(row, LeClassement.ToString()));
                lst_ListeJoueurs.Items.Add(row);
            }
        }

        private void btn_AfficherJoueurs_Click(object sender, EventArgs e)
        {

        }

        private void btn_Quitter_Click(object sender, EventArgs e)
        {

        }

        private void FormMenu_Load(object sender, EventArgs e)
        {
            m_echec.creerListeJoueur();
            afficherJoueurs();
        }
    }
}
