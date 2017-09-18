namespace VoiceRecognization
{
    partial class Nova
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

            this.components = new System.ComponentModel.Container();
            this.btEnable = new System.Windows.Forms.Button();
            this.btDisable = new System.Windows.Forms.Button();
            this.rtbLog = new System.Windows.Forms.RichTextBox();
            this.TimeSet = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // btEnable
            // 
            this.btEnable.Location = new System.Drawing.Point(12, 282);
            this.btEnable.Name = "btEnable";
            this.btEnable.Size = new System.Drawing.Size(155, 23);
            this.btEnable.TabIndex = 0;
            this.btEnable.Text = "Enable Vocal";
            this.btEnable.UseVisualStyleBackColor = true;
            this.btEnable.Click += new System.EventHandler(maitre.btnEnable_Click);
            // 
            // btDisable
            // 
            this.btDisable.Location = new System.Drawing.Point(600, 282);
            this.btDisable.Name = "btDisable";
            this.btDisable.Size = new System.Drawing.Size(173, 23);
            this.btDisable.TabIndex = 1;
            this.btDisable.Text = "Disable Vocal";
            this.btDisable.UseVisualStyleBackColor = true;
            this.btDisable.Click += new System.EventHandler(maitre.btnDisable_Click);
            // 
            // rtbLog
            // 
            this.rtbLog.Cursor = System.Windows.Forms.Cursors.Cross;
            this.rtbLog.Location = new System.Drawing.Point(12, 12);
            this.rtbLog.Name = "rtbLog";
            this.rtbLog.Size = new System.Drawing.Size(761, 264);
            this.rtbLog.TabIndex = 2;
            this.rtbLog.Text = "-log-";
            // 
            // Nova
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(785, 317);
            this.Controls.Add(this.rtbLog);
            this.Controls.Add(this.btDisable);
            this.Controls.Add(this.btEnable);
            this.Name = "Nova";
            this.Text = "Nova";
            this.ResumeLayout(false);

        }

        #endregion
        internal System.Windows.Forms.Timer TimeSet;
        internal System.Windows.Forms.Button btEnable;
        internal System.Windows.Forms.Button btDisable;
        internal System.Windows.Forms.RichTextBox rtbLog;
    }
}

