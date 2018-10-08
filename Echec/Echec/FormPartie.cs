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
    public partial class FormPartie : Form
    {
		Partie m_partie;

        public FormPartie(/*Partie p_partie*/)
        {
            InitializeComponent();
			//m_partie = p_partie;
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


		private void calculerPos()
		{
			//Chacune des icones à un .tag avec ses coordonnées btw
		}






		//Initialiser des paramètres au chargement de la fenêtre
		private void FormPartie_Load(object sender, EventArgs e)
		{
			changeColor();
		}

		//Changement de couleur
		private void changeColor()
		{
			var c = GetAll(this, typeof(PictureBox));
			var d = GetAll(this, typeof(Label));

			foreach (PictureBox img in c)
			{
				if (img.BackColor == Color.Black)
				{
					img.BackColor = Color.FromArgb(25, 25, 25);
				}
			}

			foreach (Label lbl in d)
			{
				if (lbl.BackColor == Color.Black)
				{
					lbl.BackColor = Color.FromArgb(25, 25, 25);
				}
			}
		}

		public IEnumerable<Control> GetAll(Control control, Type type)
		{
			var controls = control.Controls.Cast<Control>();

			return controls.SelectMany(ctrl => GetAll(ctrl, type))
									  .Concat(controls)
									  .Where(c => c.GetType() == type);
		}
    }
}
