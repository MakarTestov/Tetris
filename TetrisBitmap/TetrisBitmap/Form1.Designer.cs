namespace TetrisBitmap
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.panGame = new System.Windows.Forms.Panel();
            this.butStartGame = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.labCount = new System.Windows.Forms.Label();
            this.panNextFigure = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // panGame
            // 
            this.panGame.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.panGame.Dock = System.Windows.Forms.DockStyle.Left;
            this.panGame.Location = new System.Drawing.Point(0, 0);
            this.panGame.Name = "panGame";
            this.panGame.Size = new System.Drawing.Size(300, 400);
            this.panGame.TabIndex = 0;
            // 
            // butStartGame
            // 
            this.butStartGame.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.butStartGame.Location = new System.Drawing.Point(300, 377);
            this.butStartGame.Name = "butStartGame";
            this.butStartGame.Size = new System.Drawing.Size(148, 23);
            this.butStartGame.TabIndex = 1;
            this.butStartGame.Text = "StartGame";
            this.butStartGame.UseVisualStyleBackColor = true;
            this.butStartGame.Click += new System.EventHandler(this.butStartGame_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(307, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 29);
            this.label1.TabIndex = 2;
            this.label1.Text = "Count:";
            // 
            // labCount
            // 
            this.labCount.AutoSize = true;
            this.labCount.Location = new System.Drawing.Point(379, 13);
            this.labCount.Name = "labCount";
            this.labCount.Size = new System.Drawing.Size(26, 29);
            this.labCount.TabIndex = 3;
            this.labCount.Text = "0";
            // 
            // panNextFigure
            // 
            this.panNextFigure.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.panNextFigure.Location = new System.Drawing.Point(325, 112);
            this.panNextFigure.Name = "panNextFigure";
            this.panNextFigure.Size = new System.Drawing.Size(80, 60);
            this.panNextFigure.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Location = new System.Drawing.Point(306, 80);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(132, 29);
            this.label3.TabIndex = 6;
            this.label3.Text = "NextFigure";
            // 
            // Form1
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(448, 400);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.panNextFigure);
            this.Controls.Add(this.labCount);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.butStartGame);
            this.Controls.Add(this.panGame);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.Text = "Tetris";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panGame;
        private System.Windows.Forms.Button butStartGame;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labCount;
        private System.Windows.Forms.Panel panNextFigure;
        private System.Windows.Forms.Label label3;
    }
}

