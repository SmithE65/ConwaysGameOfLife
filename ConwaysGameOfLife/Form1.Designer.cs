namespace ConwaysGameOfLife
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tickLabel = new System.Windows.Forms.Label();
            this.drawLabel = new System.Windows.Forms.Label();
            this.numTicksLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // tickLabel
            // 
            this.tickLabel.AutoSize = true;
            this.tickLabel.Location = new System.Drawing.Point(12, 24);
            this.tickLabel.Name = "tickLabel";
            this.tickLabel.Size = new System.Drawing.Size(38, 15);
            this.tickLabel.TabIndex = 0;
            this.tickLabel.Text = "label1";
            // 
            // drawLabel
            // 
            this.drawLabel.AutoSize = true;
            this.drawLabel.Location = new System.Drawing.Point(12, 39);
            this.drawLabel.Name = "drawLabel";
            this.drawLabel.Size = new System.Drawing.Size(38, 15);
            this.drawLabel.TabIndex = 1;
            this.drawLabel.Text = "label2";
            // 
            // numTicksLabel
            // 
            this.numTicksLabel.AutoSize = true;
            this.numTicksLabel.Location = new System.Drawing.Point(12, 9);
            this.numTicksLabel.Name = "numTicksLabel";
            this.numTicksLabel.Size = new System.Drawing.Size(38, 15);
            this.numTicksLabel.TabIndex = 2;
            this.numTicksLabel.Text = "label1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.numTicksLabel);
            this.Controls.Add(this.drawLabel);
            this.Controls.Add(this.tickLabel);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label tickLabel;
        private Label drawLabel;
        private Label numTicksLabel;
    }
}