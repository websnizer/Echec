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
			this.SuspendLayout();
			// 
			// btn_Partie
			// 
			this.btn_Partie.Location = new System.Drawing.Point(12, 12);
			this.btn_Partie.Name = "btn_Partie";
			this.btn_Partie.Size = new System.Drawing.Size(163, 55);
			this.btn_Partie.TabIndex = 0;
			this.btn_Partie.Text = "Partie";
			this.btn_Partie.UseVisualStyleBackColor = true;
			this.btn_Partie.Click += new System.EventHandler(this.btn_Partie_Click);
			// 
			// FormMenu
			// 
			this.ClientSize = new System.Drawing.Size(187, 99);
			this.Controls.Add(this.btn_Partie);
			this.Name = "FormMenu";
			this.ResumeLayout(false);

        }

		#endregion
		private System.Windows.Forms.Button button3;
		private System.Windows.Forms.Button btn_Partie;
	}
}

