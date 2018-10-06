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
			FormPartie m_form = new FormPartie();
			m_form.Show();
		}

		private void jouer()
		{

		}

		private void quitter()
		{

		}

		private void afficherJoueurs()
		{

		}
	}
}
