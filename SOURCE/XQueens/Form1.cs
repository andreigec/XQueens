using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ANDREICSLIB;

namespace XQueens
{
    public partial class Form1 : Form
   {
        #region licensing

        private const string AppTitle = "XQueens";
        private const double AppVersion = 0.2;
        private const String HelpString = "";

        private const String UpdatePath = "https://github.com/EvilSeven/XQueens/zipball/master";
        private const String VersionPath = "https://raw.github.com/EvilSeven/XQueens/master/INFO/version.txt";
        private const String ChangelogPath = "https://raw.github.com/EvilSeven/XQueens/master/INFO/changelog.txt";

        private readonly String OtherText =
            @"©" + DateTime.Now.Year +
            @" Andrei Gec (http://www.andreigec.net)

Licensed under GNU LGPL (http://www.gnu.org/)

Zip Assets © SharpZipLib (http://www.sharpdevelop.net/OpenSource/SharpZipLib/)
";
        #endregion

		public Form1()
		{
			InitializeComponent();
		}

        private void CreateMatrix()
        {
            var w = int.Parse(widthtext.Text);
            var h = int.Parse(heighttext.Text);

            grid.clearControls();

            var ct = pieceboard.ChesstypesFromString(chesstype.Text);
            controller.CreateMatrix(w, h, ct, grid);  
        }

		private void applybutton_Click(object sender, EventArgs e)
		{
		    CreateMatrix();
		}
		
		private void clearbutton_Click(object sender, EventArgs e)
		{
            controller.ClearMain();
		}

		private void solvebutton_Click(object sender, EventArgs e)
		{
		    var ct = pieceboard.ChesstypesFromString(chesstype.Text);
            controller.ApplySolve(ct);
		}

		private void chesstype_TextChanged(object sender, EventArgs e)
		{
			if (chesstype.Text.Length > 0 && controller.IsPlaying())
			{
                var ct = pieceboard.ChesstypesFromString(chesstype.Text);
                controller.ChangeType(ct);
			}
		}

		private void widthtext_KeyPress_1(object sender, KeyPressEventArgs e)
		{
			e.Handled = TextboxUpdates.HandleInput(TextboxUpdates.InputType.Create(false,true,false,false),e.KeyChar,widthtext);
		}

		private void heighttext_KeyPress(object sender, KeyPressEventArgs e)
		{
            e.Handled = TextboxUpdates.HandleInput(TextboxUpdates.InputType.Create(false, true, false, false), e.KeyChar, heighttext);
		}

        private void InitChessTypes()
        {
            foreach (pieceboard.chesstypes ct in Enum.GetValues(typeof(pieceboard.chesstypes)))
            {
                if (ct == pieceboard.chesstypes.NOTYPE)
                    continue;
                chesstype.Items.Add(ct.ToString());
            }
            chesstype.SelectedIndex = 0;
        }

		private void Form1_Load_1(object sender, EventArgs e)
		{
		    InitChessTypes();
		    CreateMatrix();

            Licensing.CreateLicense(this, HelpString, AppTitle, AppVersion, OtherText, VersionPath, UpdatePath, ChangelogPath, menuStrip1);
		}

        private void exitToolStripMenuItem_Click_2(object sender, EventArgs e)
        {
            Application.Exit();
        }

	}
}
