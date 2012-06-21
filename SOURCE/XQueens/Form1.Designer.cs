using System.Windows.Forms;
using ANDREICSLIB;

namespace XQueens
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.button1 = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.widthtext = new System.Windows.Forms.TextBox();
            this.Widthsdaasd = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.heighttext = new System.Windows.Forms.TextBox();
            this.chesstype = new System.Windows.Forms.ComboBox();
            this.clearbutton = new System.Windows.Forms.Button();
            this.solvebutton = new System.Windows.Forms.Button();
            this.grid = new ANDREICSLIB.PanelUpdates();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(304, 29);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Apply";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.applybutton_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.Silver;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(388, 24);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(92, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click_2);
            // 
            // widthtext
            // 
            this.widthtext.Location = new System.Drawing.Point(48, 31);
            this.widthtext.Name = "widthtext";
            this.widthtext.Size = new System.Drawing.Size(100, 20);
            this.widthtext.TabIndex = 3;
            this.widthtext.Text = "8";
            this.widthtext.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.widthtext_KeyPress_1);
            // 
            // Widthsdaasd
            // 
            this.Widthsdaasd.AutoSize = true;
            this.Widthsdaasd.Location = new System.Drawing.Point(12, 34);
            this.Widthsdaasd.Name = "Widthsdaasd";
            this.Widthsdaasd.Size = new System.Drawing.Size(35, 13);
            this.Widthsdaasd.TabIndex = 4;
            this.Widthsdaasd.Text = "Width";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(154, 34);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Height";
            // 
            // heighttext
            // 
            this.heighttext.Location = new System.Drawing.Point(198, 31);
            this.heighttext.Name = "heighttext";
            this.heighttext.Size = new System.Drawing.Size(100, 20);
            this.heighttext.TabIndex = 5;
            this.heighttext.Text = "8";
            this.heighttext.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.heighttext_KeyPress);
            // 
            // chesstype
            // 
            this.chesstype.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.chesstype.FormattingEnabled = true;
            this.chesstype.Location = new System.Drawing.Point(15, 57);
            this.chesstype.Name = "chesstype";
            this.chesstype.Size = new System.Drawing.Size(121, 21);
            this.chesstype.TabIndex = 7;
            this.chesstype.TextChanged += new System.EventHandler(this.chesstype_TextChanged);
            // 
            // clearbutton
            // 
            this.clearbutton.Location = new System.Drawing.Point(142, 57);
            this.clearbutton.Name = "clearbutton";
            this.clearbutton.Size = new System.Drawing.Size(75, 23);
            this.clearbutton.TabIndex = 8;
            this.clearbutton.Text = "Clear";
            this.clearbutton.UseVisualStyleBackColor = true;
            this.clearbutton.Click += new System.EventHandler(this.clearbutton_Click);
            // 
            // solvebutton
            // 
            this.solvebutton.Location = new System.Drawing.Point(223, 57);
            this.solvebutton.Name = "solvebutton";
            this.solvebutton.Size = new System.Drawing.Size(156, 23);
            this.solvebutton.TabIndex = 9;
            this.solvebutton.Text = "Solve";
            this.solvebutton.UseVisualStyleBackColor = true;
            this.solvebutton.Click += new System.EventHandler(this.solvebutton_Click);
            // 
            // grid
            // 
            this.grid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grid.BorderColour = System.Drawing.Color.Black;
            this.grid.BorderWidth = 0;
            this.grid.Location = new System.Drawing.Point(15, 84);
            this.grid.Name = "grid";
            this.grid.Size = new System.Drawing.Size(361, 248);
            this.grid.TabIndex = 1;
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(388, 344);
            this.Controls.Add(this.solvebutton);
            this.Controls.Add(this.clearbutton);
            this.Controls.Add(this.chesstype);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.heighttext);
            this.Controls.Add(this.Widthsdaasd);
            this.Controls.Add(this.widthtext);
            this.Controls.Add(this.grid);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "P";
            this.Load += new System.EventHandler(this.Form1_Load_1);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
		
        #endregion

        private Label Widthsdaasd;
        private Button button1;
        private ComboBox chesstype;
        private Button clearbutton;
        private ToolStripMenuItem exitToolStripMenuItem;
        private ToolStripMenuItem fileToolStripMenuItem;
        private PanelUpdates grid;
        private TextBox heighttext;
        private Label label2;
        private MenuStrip menuStrip1;
        private Button solvebutton;
        private TextBox widthtext;
    }
}