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
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
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
            this.Nom = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Victoires = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Defaites = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Classement = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // btn_Partie
            // 
            this.btn_Partie.Location = new System.Drawing.Point(12, 12);
            this.btn_Partie.Name = "btn_Partie";
            this.btn_Partie.Size = new System.Drawing.Size(290, 55);
            this.btn_Partie.TabIndex = 0;
            this.btn_Partie.Text = "Partie";
            this.btn_Partie.UseVisualStyleBackColor = true;
            this.btn_Partie.Click += new System.EventHandler(this.btn_Partie_Click);
            // 
            // btn_Quitter
            // 
            this.btn_Quitter.Location = new System.Drawing.Point(12, 227);
            this.btn_Quitter.Name = "btn_Quitter";
            this.btn_Quitter.Size = new System.Drawing.Size(290, 55);
            this.btn_Quitter.TabIndex = 2;
            this.btn_Quitter.Text = "Quitter";
            this.btn_Quitter.UseVisualStyleBackColor = true;
            this.btn_Quitter.Click += new System.EventHandler(this.btn_Quitter_Click);
            // 
            // lst_ListeJoueurs
            // 
            this.lst_ListeJoueurs.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Nom,
            this.Victoires,
            this.Defaites,
            this.Classement});
            this.lst_ListeJoueurs.Location = new System.Drawing.Point(13, 74);
            this.lst_ListeJoueurs.Name = "lst_ListeJoueurs";
            this.lst_ListeJoueurs.Size = new System.Drawing.Size(289, 147);
            this.lst_ListeJoueurs.TabIndex = 3;
            this.lst_ListeJoueurs.UseCompatibleStateImageBehavior = false;
            // 
            // FormMenu
            // 
            this.ClientSize = new System.Drawing.Size(324, 294);
            this.Controls.Add(this.lst_ListeJoueurs);
            this.Controls.Add(this.btn_Quitter);
            this.Controls.Add(this.btn_Partie);
            this.Name = "FormMenu";
            this.Load += new System.EventHandler(this.FormMenu_Load);
            this.ResumeLayout(false);

        }

		#endregion
		private System.Windows.Forms.Button button3;
		private System.Windows.Forms.Button btn_Partie;
        private System.Windows.Forms.Button btn_Quitter;
        private System.Windows.Forms.ListView lst_ListeJoueurs;
        private System.Windows.Forms.ColumnHeader Nom;
        private System.Windows.Forms.ColumnHeader Victoires;
        private System.Windows.Forms.ColumnHeader Defaites;
        private System.Windows.Forms.ColumnHeader Classement;
    }
}

