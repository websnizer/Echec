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
        Joueur joueurBlanc;
        Joueur joueurNoir;


        public FormMenu(Echec p_echec)
        {
            InitializeComponent();
			m_echec = p_echec;
        }

		private void btn_Partie_Click(object sender, EventArgs e)
		{
            jouer();
		}

		private void jouer()
		{
            //Si deux joueurs sont sélectionner
            if (lst_ListeJoueurs.SelectedItems.Count == 2)
            {
                //Assigne les deux joueurs sélectionner
                SelectionJoueurs();
                //Commence la partie avec les deux joueurs sélectionnés
                Partie laPartie = new Partie(joueurBlanc, joueurNoir);
            }
            else
            {
                MessageBox.Show("Veuillez choisir deux joueurs.");
            }
        }

		private void quitter()
		{
            Close();
		}

		private void afficherJoueurs()
		{
            //Ajoute les colonnes au listview (avec leur largeur)
            lst_ListeJoueurs.View = View.Details;
            lst_ListeJoueurs.Columns.Add("Nom", 100);
            lst_ListeJoueurs.Columns.Add("Victoires", 75);
            lst_ListeJoueurs.Columns.Add("Défaites", 75);
            lst_ListeJoueurs.Columns.Add("Classement", 75);

            for (int i = 0; i < m_echec.ListeJoueurs.Count; i++)
            {
                //les informations du joueurs en cours (dans la boucle)
                string LeNom = m_echec.ListeJoueurs[i].NomJoueur;
                int lesVictoires = m_echec.ListeJoueurs[i].VictoiresJoueur;
                int lesDefaites = m_echec.ListeJoueurs[i].DefaitesJoueur;
                int LeClassement = m_echec.ListeJoueurs[i].ClassementJoueur;

                //Crée les informations du joueurs dans le listview
                ListViewItem row = new ListViewItem(LeNom);
                row.Name = LeNom;
                row.SubItems.Add(new ListViewItem.ListViewSubItem(row, lesVictoires.ToString()));
                row.SubItems.Add(new ListViewItem.ListViewSubItem(row, lesDefaites.ToString()));
                row.SubItems.Add(new ListViewItem.ListViewSubItem(row, LeClassement.ToString()));
                lst_ListeJoueurs.Items.Add(row);
            }
        }

        private void btn_Quitter_Click(object sender, EventArgs e)
        {
            //Ferme l'applcation
            Close();
        }

        private void FormMenu_Load(object sender, EventArgs e)
        {
            //Crée la liste des joueurs (à partir du fichier .txt)
            m_echec.creerListeJoueur();
            //Affiche les joueurs dans le listview
            afficherJoueurs();
        }

        private void SelectionJoueurs()
        {
            if (lst_ListeJoueurs.SelectedItems.Count == 2)
            {
                //Le name de l'item dans le listview pour le premier joueur
                string Name1 = lst_ListeJoueurs.SelectedItems[0].Name.ToString();

                //Le name de l'item dans le listview pour le deuxième joueur
                string Name2 = lst_ListeJoueurs.SelectedItems[1].Name.ToString();

                //Pour tous les items dans le listview
                foreach (ListViewItem item in lst_ListeJoueurs.Items)
                {
                    if (item.Name == Name1)
                    {
                        //Crée le joueur blanc
                        string Nom = item.SubItems[0].Text;
                        int Victoires = Int32.Parse(item.SubItems[1].Text);
                        int Defaites = Int32.Parse(item.SubItems[2].Text);
                        int Classement = Int32.Parse(lst_ListeJoueurs.SelectedItems[0].SubItems[3].Text);
                        joueurBlanc = new Joueur(Nom, Victoires, Defaites, Classement);
                    }
                    else if (item.Name == Name2)
                    {
                        //Crée le joueur noir
                        string Nom = item.SubItems[0].Text;
                        int Victoires = Int32.Parse(item.SubItems[1].Text);
                        int Defaites = Int32.Parse(item.SubItems[2].Text);
                        int Classement = Int32.Parse(lst_ListeJoueurs.SelectedItems[0].SubItems[3].Text);
                        joueurNoir = new Joueur(Nom, Victoires, Defaites, Classement);
                    }
                }
            }
        }
    }
}
