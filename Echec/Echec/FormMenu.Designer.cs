namespace Echec
{
	partial class FormMenu
	{
		/// <summary>
		/// Variable nécessaire au concepteur.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Nettoyage des ressources utilisées.
		/// </summary>
		/// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Code généré par le Concepteur Windows Form

		/// <summary>
		/// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
		/// le contenu de cette méthode avec l'éditeur de code.
		/// </summary>
		private void InitializeComponent()
		{
			this.btn_Partie = new System.Windows.Forms.Button();
			this.btn_Quitter = new System.Windows.Forms.Button();
			this.lst_ListeJoueurs = new System.Windows.Forms.ListView();
			this.lst_classementsFuturs = new System.Windows.Forms.ListBox();
			this.label1 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// btn_Partie
			// 
			this.btn_Partie.Location = new System.Drawing.Point(1, 353);
			this.btn_Partie.Name = "btn_Partie";
			this.btn_Partie.Size = new System.Drawing.Size(367, 55);
			this.btn_Partie.TabIndex = 0;
			this.btn_Partie.Text = "Jouer";
			this.btn_Partie.UseVisualStyleBackColor = true;
			this.btn_Partie.Click += new System.EventHandler(this.btn_Partie_Click);
			// 
			// btn_Quitter
			// 
			this.btn_Quitter.Location = new System.Drawing.Point(1, 414);
			this.btn_Quitter.Name = "btn_Quitter";
			this.btn_Quitter.Size = new System.Drawing.Size(367, 27);
			this.btn_Quitter.TabIndex = 2;
			this.btn_Quitter.Text = "Quitter";
			this.btn_Quitter.UseVisualStyleBackColor = true;
			this.btn_Quitter.Click += new System.EventHandler(this.btn_Quitter_Click);
			// 
			// lst_ListeJoueurs
			// 
			this.lst_ListeJoueurs.FullRowSelect = true;
			this.lst_ListeJoueurs.Location = new System.Drawing.Point(1, 1);
			this.lst_ListeJoueurs.Name = "lst_ListeJoueurs";
			this.lst_ListeJoueurs.Size = new System.Drawing.Size(367, 266);
			this.lst_ListeJoueurs.TabIndex = 3;
			this.lst_ListeJoueurs.UseCompatibleStateImageBehavior = false;
			this.lst_ListeJoueurs.Click += new System.EventHandler(this.lst_ListeJoueurs_Click);
			// 
			// lst_classementsFuturs
			// 
			this.lst_classementsFuturs.FormattingEnabled = true;
			this.lst_classementsFuturs.Location = new System.Drawing.Point(1, 304);
			this.lst_classementsFuturs.Name = "lst_classementsFuturs";
			this.lst_classementsFuturs.Size = new System.Drawing.Size(366, 43);
			this.lst_classementsFuturs.TabIndex = 4;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(-1, 270);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(93, 13);
			this.label1.TabIndex = 5;
			this.label1.Text = "Classement Futurs";
			this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// FormMenu
			// 
			this.ClientSize = new System.Drawing.Size(370, 443);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.lst_classementsFuturs);
			this.Controls.Add(this.lst_ListeJoueurs);
			this.Controls.Add(this.btn_Quitter);
			this.Controls.Add(this.btn_Partie);
			this.MaximizeBox = false;
			this.MaximumSize = new System.Drawing.Size(386, 482);
			this.Name = "FormMenu";
			this.Text = "Menu Echec";
			this.Activated += new System.EventHandler(this.FormMenu_Activated);
			this.Load += new System.EventHandler(this.FormMenu_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.Button button3;
		private System.Windows.Forms.Button btn_Partie;
		private System.Windows.Forms.Button btn_Quitter;
		private System.Windows.Forms.ListView lst_ListeJoueurs;
		private System.Windows.Forms.ListBox lst_classementsFuturs;
		private System.Windows.Forms.Label label1;
	}
}