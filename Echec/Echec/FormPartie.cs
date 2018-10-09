using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Echec
{
    public partial class FormPartie : Form
    {
		Partie m_partie; //Référence à la partie pour les communications
		string coordPieceStr; //Coordonnées de la pièce qui bouge
		string coordCaseStr; //Coordonnées de la case cible
		PictureBox caseInitiale; //Stoque la case initiale
		PictureBox caseFinale; //Stoque la case finale
		Color couleurCaseInitiale; //Stoque la couleur de la case initiale
		Color couleurCaseFinale; //Stoque la couleur de la case finale

		//Constructeur
		public FormPartie(Partie p_partie)
        {
            InitializeComponent();
			m_partie = p_partie;
		}

		//Afficher le statut de la partie à l'utilisateur
		private void afficherStatut()
		{
			
		}


		//Todelete, mais peut-être utile
		public void message(string p_msg)
		{
			MessageBox.Show(p_msg); //Messagebox de la form
		}

		//Afficher l'historique des coups
		public void afficherDeplacement(int[] p_posPiece, int[] p_posCase)
		{
			string count = (lst_historique.Items.Count + 1).ToString(); //Compte des données dans l'historique
			string ajout = count + ": " + conversionCoordonnes(p_posPiece, p_posCase); //String à ajouter au listbox
			lst_historique.Items.Add(ajout);
		}

		//Convertir les données de 0,0 à A1 par exemple.
		private string conversionCoordonnes(int[] p_posPiece, int[] p_posCase)
		{
			string[] coordsInitial = new string[2]; //Conversion des coordonnées initiales en coordonnées cadrillé
			string[] coordsFinale = new string[2]; //Conversion des coordonnées finales en coordonnées cadrillé

			//Conversion des coordonnées int en str
			coordsInitial[0] = p_posPiece[0].ToString();
			coordsInitial[1] = p_posPiece[1].ToString();
			coordsFinale[0] = p_posCase[0].ToString();
			coordsFinale[1] = p_posCase[1].ToString();

			return "";
		}

		//On click sur une des cases
		private void caseClick(object sender, MouseEventArgs e)
		{
			string[] coordsPieceStr = new string[2]; //Corodonnées initiales X,Y en string
			string[] coordsCaseStr = new string[2]; //Corodonnées finales X,Y en string
			int[] coordPieceInt = new int[2]; //Corodonnées initiales X,Y en int
			int[] coordCaseInt = new int[2]; //Corodonnées finales X,Y en int

			if (e.Button == MouseButtons.Left)
			{
				coordPieceStr = (sender as PictureBox).Tag + "";
				//couleurCaseInitiale = (sender as PictureBox).BackColor;
				//caseInitiale = (sender as PictureBox);
				//caseInitiale.BackColor = Color.Green;
			}
			else if (e.Button == MouseButtons.Right)
			{
				coordCaseStr = (sender as PictureBox).Tag + "";
				//couleurCaseFinale = (sender as PictureBox).BackColor;
				//caseFinale = (sender as PictureBox);
				//caseInitiale.BackColor = Color.Green;
			}

			if (coordPieceStr != null && coordCaseStr != null)
			{
				//Split les coordonnées x et y dans un tableau
				coordsPieceStr = coordPieceStr.Split(',');
				coordsCaseStr = coordCaseStr.Split(',');

				//Conversion de str en int
				Int32.TryParse(coordsPieceStr[0], out coordPieceInt[0]);
				Int32.TryParse(coordsPieceStr[1], out coordPieceInt[1]);
				Int32.TryParse(coordsCaseStr[0], out coordCaseInt[0]);
				Int32.TryParse(coordsCaseStr[1], out coordCaseInt[1]);

				//Jouer un coup avec les coordonnées
				m_partie.jouerCoup(coordPieceInt, coordCaseInt);

				coordPieceStr = null;
				coordCaseStr = null;
				//caseInitiale.BackColor = couleurCaseInitiale;
				//caseFinale.BackColor = couleurCaseFinale;
				//caseInitiale = null;
				//caseFinale = null;
			}
		}

		//Effacer toutes les pièces
		public void effacerPiece()
		{
			List<Label> labelDelete = new List<Label>(); //Liste des label à supprimer

			//Ajouter le label dans la liste
			foreach (Label lbl in this.Controls.OfType<Label>())
			{
				if (lbl.Tag != null && lbl.Tag.ToString() == "piece")
				{
					labelDelete.Add(lbl);
				}
			}
				
			//Disposer tous les elements de la liste
			foreach (Label lbl in labelDelete)
			{
				this.Controls.Remove(lbl);
				lbl.Dispose();
			}


		}

		//Afficher les pièces selon la chaine recue
		public void afficherPiece(string p_string)
		{
			string[] pieces = p_string.Split(','); //Tableau des coordonnées reconstruit selon la string de sérialiasation
			int x = 0; //Position en X sur l'échiquier
			int y = 0; //Position en Y sur l'échiquier
			int coordx = 0; //La coordonnée X dans la fenêtre
			int coordy = 0; //La coordonnée Y dans la fenêtre

			//Pour toutes les pièces
			foreach (string p in pieces)
			{
				if (p != "x") //Si la case n'est pas vide
				{
					//Création d'un label
					Label lbl = new Label();
					lbl.Text = p;
					lbl.Font = new Font("Arial", 24, FontStyle.Bold);
					lbl.Width = 40;
					lbl.Height = 40;
					lbl.Tag = "piece";

					//Positionnement dans la fenêtre
					foreach (PictureBox pb in this.Controls.OfType<PictureBox>())
					{
						if (pb.Tag != null && (pb.Tag.ToString() == (y.ToString() + "," + x.ToString())) )
						{
							coordx = pb.Left;
							coordy = pb.Top;
						}
					}
					lbl.Left = coordx;
					lbl.Top = coordy;

					//Ajout du control et affichage
					this.Controls.Add(lbl);
					lbl.BringToFront();

					//Passer à la prochaine case
					x++;
					if (x == 8)
					{
						//Passer à la prochaine ligne
						x = 0;
						y++;
					}
				}
				else
				{
					//Passer à la prochaine case
					x++;
					if (x == 8)
					{
						//Passer à la prochaine ligne
						x = 0;
						y++;
					}
				}
			}
		}

		//Initialiser des paramètres au chargement de la fenêtre
		private void FormPartie_Load(object sender, EventArgs e)
		{
			changeColor(75,75,75);
		}

		//Changement de couleur
		private void changeColor(int p_rouge, int o_vert, int o_bleu)
		{
				List<PictureBox> pictures = new List<PictureBox>(); //Liste des pictures à changer le background
				List<Label> labels = new List<Label>(); //Liste des pictures à changer le background

				//Ajout de tous les pictures box dans une liste
				foreach (PictureBox img in this.Controls.OfType<PictureBox>())
					if (img.BackColor == Color.Black)
						pictures.Add(img);

				//Modification de la couleur des pictures
				foreach (PictureBox pct in pictures)
					pct.BackColor = Color.FromArgb(p_rouge, o_vert, o_bleu);

				//Ajout de tous les label dans une liste
				foreach (Label lbl in this.Controls.OfType<Label>())
					if ( (lbl.Tag != null) && (lbl.Tag.ToString() == "grid") )
						labels.Add(lbl);

				//Modification de la couleur et du parent des labels
				foreach (Label lb in labels)
				{
					lb.Parent = pictureBox67;
					lb.BackColor = Color.Transparent;
				}
		}
	}
}
