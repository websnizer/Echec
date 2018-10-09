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
		Partie m_partie;
		string coordPieceStr;
		string coordCaseStr;

		public FormPartie(Partie p_partie)
        {
            InitializeComponent();
			m_partie = p_partie;
		}


		public void afficherPieces( string p_plateau)
		{
			
		}

		private void caseClick()
		{
			
		}

		private void afficherStatut()
		{
			
		}

		public void message(string p_msg)
		{
			MessageBox.Show(p_msg);
		}

		private void calculerPos()
		{
			//Chacune des icones à un .tag avec ses coordonnées btw
		}

		//On click sur une des cases
		private void caseClick(object sender, MouseEventArgs e)
		{
			string[] coordsPieceStr = new string[2];
			string[] coordsCaseStr = new string[2];
			int[] coordPieceInt = new int[2];
			int[] coordCaseInt = new int[2];

			if (e.Button == MouseButtons.Left)
			{
				coordPieceStr = (sender as PictureBox).Tag + "";
				//MessageBox.Show("Coordonnées initiales: " + coordPieceStr);
			}
			else if (e.Button == MouseButtons.Right)
			{
				coordCaseStr = (sender as PictureBox).Tag + "";
				//MessageBox.Show("Coordonnées finales: " + coordCaseStr);
			}

			if (coordPieceStr != null && coordCaseStr != null)
			{
				coordsPieceStr = coordPieceStr.Split(',');
				coordsCaseStr = coordCaseStr.Split(',');

				Int32.TryParse(coordsPieceStr[0], out coordPieceInt[0]);
				Int32.TryParse(coordsPieceStr[1], out coordPieceInt[1]);
				Int32.TryParse(coordsCaseStr[0], out coordCaseInt[0]);
				Int32.TryParse(coordsCaseStr[1], out coordCaseInt[1]);

				m_partie.jouerCoup(coordPieceInt, coordCaseInt);
				coordPieceStr = null;
				coordCaseStr = null;
			}
		}


		public void effacerPiece()
		{
			foreach (Label lbl in this.Controls.OfType<Label>())
			{
				if (lbl.Tag != null && lbl.Tag.ToString() == "piece")
				{
					this.Controls.Remove(lbl);
					lbl.Dispose();
				}
				
			}
		}

		public void afficherPiece(string p_string)
		{
			string[] pieces = p_string.Split(',');
			int x = 0;
			int y = 0;
			int coordx = 0;
			int coordy = 0;

			effacerPiece();

			foreach (string p in pieces)
			{
				if (p != "x")
				{
					Label lbl = new Label();
					lbl.Text = p;
					lbl.Font = new Font("Arial", 24, FontStyle.Bold);
					lbl.Width = 40;
					lbl.Height = 40;
					lbl.Tag = "piece";

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

					this.Controls.Add(lbl);
					lbl.BringToFront();
					x++;
					if (x == 8)
					{
						x = 0;
						y++;
					}
				}
				else
				{
					x++;
					if (x == 8)
					{
						x = 0;
						y++;
					}
				}
			}
		}

		//Initialiser des paramètres au chargement de la fenêtre
		private void FormPartie_Load(object sender, EventArgs e)
		{
			changeColor();
		}

		//Changement de couleur
		private void changeColor()
		{
			foreach (PictureBox img in this.Controls.OfType<PictureBox>())
			{
				if (img.BackColor == Color.Black)
				{
					img.BackColor = Color.FromArgb(25, 25, 25);
				}
			}

			foreach (Label lbl in this.Controls.OfType<Label>())
			{
				if (lbl.BackColor == Color.Black)
				{
					lbl.BackColor = Color.FromArgb(25, 25, 25);
				}
			}
		}
	}
}
