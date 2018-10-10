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
	public partial class FormChoixPiece : Form
	{
		Partie m_partie;

		public FormChoixPiece(Partie p_partie, bool m_tour)
		{
			InitializeComponent();
			m_partie = p_partie;
			if ( m_tour )
			{
				pct_Reine.Image = (Image)Properties.Resources.ResourceManager.GetObject("Reine");
				pct_Cavalier.Image = (Image)Properties.Resources.ResourceManager.GetObject("Cavalier");
				pct_Fou.Image = (Image)Properties.Resources.ResourceManager.GetObject("Fou");
				pct_Tour.Image = (Image)Properties.Resources.ResourceManager.GetObject("Tour");
			}
			else
			{
				pct_Reine.Image = (Image)Properties.Resources.ResourceManager.GetObject("ReineN");
				pct_Cavalier.Image = (Image)Properties.Resources.ResourceManager.GetObject("CavalierN");
				pct_Fou.Image = (Image)Properties.Resources.ResourceManager.GetObject("FouN");
				pct_Tour.Image = (Image)Properties.Resources.ResourceManager.GetObject("TourN");
			}
		}

		private void choixClick(object sender, EventArgs e)
		{
			PictureBox snd = sender as PictureBox;
			m_partie.promouvoir(snd.Tag.ToString());
			this.Close();
		}
	}
}
