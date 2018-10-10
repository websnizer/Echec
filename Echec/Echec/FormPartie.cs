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
		public void afficherStatut(int code)
		{
			string message = "";
			Color couleur = Color.Black;

			switch (code)
			{
				case 0:
					message = "Coup valide."; 
					couleur = Color.Green;
					break;
				case 1:
					message = "Coup invalide. \n Il n'y a pas de pièce à jouer sur cette case.";
					couleur = Color.Red;
					break;
				case 2:
					message = "Coup invalide. \n La pièce à jouer n'est pas une pièce du joueur auquel c'est le tour.";
					couleur = Color.Red;
					break;
				case 3:
					message = "Coup invalide. \n La case de départ et la case d'arrivée sont la même case.";
					couleur = Color.Red;
					break;
				case 4:
					message = "Coup invalide. \n Il y a une pièce alliée sur la case visée";
					couleur = Color.Red;
					break;
				case 5:
					message = "Coup invalide. \n La pièce ne peut pas se déplacer de cette façon";
					couleur = Color.Red;
					break;
				case 6:
					message = "Coup invalide. \n Une pièce est dans le chemin et empêche le déplacement";
					couleur = Color.Red;
					break;
				case 7:
					message = "Coup invalide. \n Le joueur se met lui-même en échec avec ce coup";
					couleur = Color.Red;
					break;
				case 8:
					message = m_partie.JoueurTour.NomJoueur + " est en Échec";
					couleur = Color.HotPink;
					break;
				case 9:
					message = m_partie.JoueurTour.NomJoueur + " peut upgrader son pion";
					couleur = Color.Orange;
					break;
			}

			lbl_codes.Text = message;
			lbl_codes.ForeColor = couleur;
		}

		public void afficherTour(string p_joueur)
		{
			lbl_infos.Text = "C'est au tour de : " + p_joueur + ".";
		}


		//Todelete, mais peut-être utile
		public void message(string p_msg)
		{
			MessageBox.Show(p_msg); //Messagebox de la form
		}

		//Afficher l'historique des coups
		public void afficherDeplacement(int[] p_posPiece, int[] p_posCase, string p_joueurNom)
		{
			string joueur = "";
			string count = (lst_historique.Items.Count + 1).ToString(); //Compte des données dans l'historique
			string ajout = count + ": " + p_joueurNom + " " + conversionCoordonnes(p_posPiece, p_posCase); //String à ajouter au listbox

			lst_historique.Items.Add(ajout);
		}

		//Convertir les données de 0,0 à A1 par exemple.
		private string conversionCoordonnes(int[] p_posPiece, int[] p_posCase)
		{
			string[] coordsInitial = new string[2]; //Conversion des coordonnées initiales en coordonnées cadrillé
			string[] coordsFinale = new string[2]; //Conversion des coordonnées finales en coordonnées cadrillé
			string resultat = ""; //Le résultat de la conversion

			//Conversion des coordonnées int en str
			coordsInitial[0] = conversionUneCoordonneX(p_posPiece[0]);
			coordsInitial[1] = conversionUneCoordonneY(p_posPiece[1]);
			coordsFinale[0] = conversionUneCoordonneX(p_posCase[0]);
			coordsFinale[1] = conversionUneCoordonneY(p_posCase[1]);

			resultat = coordsInitial[0] + coordsInitial[1] + " -> " + coordsFinale[0] + coordsFinale[1];

			return resultat;
		}


		//Convertir la donnée Y
		private string conversionUneCoordonneY(int coord)
		{
			string result = "";

			switch (coord)
			{
				case 0:
					result = "8";
					break;

				case 1:
					result = "7";
					break;

				case 2:
					result = "6";
					break;

				case 3:
					result = "5";
					break;

				case 4:
					result = "4";
					break;

				case 5:
					result = "3";
					break;

				case 6:
					result = "2";
					break;

				case 7:
					result = "1";
					break;
			}

			return result;
		}

		//Convertir la donnée X
		private string conversionUneCoordonneX(int coord)
		{
			string result = "";

			switch (coord)
			{
				case 0:
					result = "a";
					break;

				case 1:
					result = "b";
					break;

				case 2:
					result = "c";
					break;

				case 3:
					result = "d";
					break;

				case 4:
					result = "e";
					break;

				case 5:
					result = "f";
					break;

				case 6:
					result = "g";
					break;

				case 7:
					result = "h";
					break;
			}

			return result;
		}

		//On click sur une des cases
		private void caseClick(object sender, MouseEventArgs e)
		{
			string[] coordsPieceStr = new string[2]; //Corodonnées initiales X,Y en string
			string[] coordsCaseStr = new string[2]; //Corodonnées finales X,Y en string
			int[] coordPieceInt = new int[2]; //Corodonnées initiales X,Y en int
			int[] coordCaseInt = new int[2]; //Corodonnées finales X,Y en int
			PictureBox snd = (sender as PictureBox);

			if (e.Button == MouseButtons.Left)
			{
				coordPieceStr = (snd.Parent.Tag != null) ? snd.Parent.Tag + "" : snd.Tag + "";
				//couleurCaseInitiale = (sender as PictureBox).BackColor;
				//caseInitiale = (sender as PictureBox);
				//caseInitiale.BackColor = Color.Green;
			}
			else if (e.Button == MouseButtons.Right)
			{
				coordCaseStr = (snd.Parent.Tag != null) ? snd.Parent.Tag + "" : snd.Tag + "";
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
			List<PictureBox> pieceDelete = new List<PictureBox>(); //Liste des label à supprimer

			//Ajouter le label dans la liste
			foreach (PictureBox box in this.Controls.OfType<PictureBox>())
			{
				foreach (PictureBox xx in box.Controls.OfType<PictureBox>())
				{
					if (xx.Tag != null && xx.Tag.ToString() == "piece")
					{
						pieceDelete.Add(xx);
					}
				}
			}
				
			//Disposer tous les elements de la liste
			foreach (PictureBox piece in pieceDelete)
			{
				PictureBox parent = (PictureBox)piece.Parent;
				parent.Controls.Remove(piece);
				piece.Dispose();
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
					PictureBox box = new PictureBox();
					box.Width = 63;
					box.Height = 65;
					box.Tag = "piece";
					box.BackColor = Color.Transparent;
					box.MouseClick += new MouseEventHandler(caseClick);

					if (p == "tour")
						box.Image = (Image)Properties.Resources.ResourceManager.GetObject("Tour");
					else if (p == "TOUR")
						box.Image = (Image)Properties.Resources.ResourceManager.GetObject("TourN");
					else if (p == "reine")
						box.Image = (Image)Properties.Resources.ResourceManager.GetObject("Reine");
					else if (p == "REINE")
						box.Image = (Image)Properties.Resources.ResourceManager.GetObject("ReineN");
					else if (p == "roi")
						box.Image = (Image)Properties.Resources.ResourceManager.GetObject("Roi");
					else if (p == "ROI")
						box.Image = (Image)Properties.Resources.ResourceManager.GetObject("RoiN");
					else if (p == "fou")
						box.Image = (Image)Properties.Resources.ResourceManager.GetObject("Fou");
					else if (p == "FOU")
						box.Image = (Image)Properties.Resources.ResourceManager.GetObject("FouN");
					else if (p == "pion")
						box.Image = (Image)Properties.Resources.ResourceManager.GetObject("Pion");
					else if (p == "PION")
						box.Image = (Image)Properties.Resources.ResourceManager.GetObject("PionN");
					else if (p == "cavalier")
						box.Image = (Image)Properties.Resources.ResourceManager.GetObject("Cavalier");
					else if (p == "CAVALIER")
						box.Image = (Image)Properties.Resources.ResourceManager.GetObject("CavalierN");


					//Positionnement dans la fenêtre
					foreach (PictureBox pb in this.Controls.OfType<PictureBox>())
					{
						if (pb.Tag != null && (pb.Tag.ToString() == (y.ToString() + "," + x.ToString())) )
						{
							coordx =  6;
							coordy = 5;
							box.Parent = pb;
							pb.Controls.Add(box);
						}
					}
					box.Left = coordx;
					box.Top = coordy;

					//Ajout du control et affichage
					//this.Controls.Add(box);
					box.BringToFront();

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
