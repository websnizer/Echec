namespace Echec
{
	partial class FormChoixPiece
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.pct_Tour = new System.Windows.Forms.PictureBox();
			this.pct_Fou = new System.Windows.Forms.PictureBox();
			this.pct_Cavalier = new System.Windows.Forms.PictureBox();
			this.pct_Reine = new System.Windows.Forms.PictureBox();
			((System.ComponentModel.ISupportInitialize)(this.pct_Tour)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pct_Fou)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pct_Cavalier)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pct_Reine)).BeginInit();
			this.SuspendLayout();
			// 
			// pct_Tour
			// 
			this.pct_Tour.Location = new System.Drawing.Point(214, 7);
			this.pct_Tour.Name = "pct_Tour";
			this.pct_Tour.Size = new System.Drawing.Size(63, 65);
			this.pct_Tour.TabIndex = 3;
			this.pct_Tour.TabStop = false;
			this.pct_Tour.Tag = "Tour";
			this.pct_Tour.Click += new System.EventHandler(this.choixClick);
			// 
			// pct_Fou
			// 
			this.pct_Fou.Location = new System.Drawing.Point(145, 7);
			this.pct_Fou.Name = "pct_Fou";
			this.pct_Fou.Size = new System.Drawing.Size(63, 65);
			this.pct_Fou.TabIndex = 2;
			this.pct_Fou.TabStop = false;
			this.pct_Fou.Tag = "Fou";
			this.pct_Fou.Click += new System.EventHandler(this.choixClick);
			// 
			// pct_Cavalier
			// 
			this.pct_Cavalier.Location = new System.Drawing.Point(76, 7);
			this.pct_Cavalier.Name = "pct_Cavalier";
			this.pct_Cavalier.Size = new System.Drawing.Size(63, 65);
			this.pct_Cavalier.TabIndex = 1;
			this.pct_Cavalier.TabStop = false;
			this.pct_Cavalier.Tag = "Cavalier";
			this.pct_Cavalier.Click += new System.EventHandler(this.choixClick);
			// 
			// pct_Reine
			// 
			this.pct_Reine.Location = new System.Drawing.Point(7, 7);
			this.pct_Reine.Name = "pct_Reine";
			this.pct_Reine.Size = new System.Drawing.Size(63, 65);
			this.pct_Reine.TabIndex = 0;
			this.pct_Reine.TabStop = false;
			this.pct_Reine.Tag = "Reine";
			this.pct_Reine.Click += new System.EventHandler(this.choixClick);
			// 
			// FormChoixPiece
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.ScrollBar;
			this.ClientSize = new System.Drawing.Size(283, 80);
			this.Controls.Add(this.pct_Tour);
			this.Controls.Add(this.pct_Fou);
			this.Controls.Add(this.pct_Cavalier);
			this.Controls.Add(this.pct_Reine);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "FormChoixPiece";
			this.Text = "Choix de pièce";
			this.TopMost = true;
			((System.ComponentModel.ISupportInitialize)(this.pct_Tour)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pct_Fou)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pct_Cavalier)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pct_Reine)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.PictureBox pct_Reine;
		private System.Windows.Forms.PictureBox pct_Cavalier;
		private System.Windows.Forms.PictureBox pct_Fou;
		private System.Windows.Forms.PictureBox pct_Tour;
	}
}