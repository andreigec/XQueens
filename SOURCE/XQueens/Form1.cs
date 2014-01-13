using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ANDREICSLIB;
using XQueens.ServiceReference1;

namespace XQueens
{
    public partial class Form1 : Form
   {
        #region licensing

        private const string AppTitle = "XQueens";
        private const double AppVersion = 0.3;
        private const String HelpString = "";

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

            var ct = Pieceboard.ChesstypesFromString(chesstype.Text);
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
		    var ct = Pieceboard.ChesstypesFromString(chesstype.Text);
            controller.ApplySolve(ct);
		}

		private void chesstype_TextChanged(object sender, EventArgs e)
		{
			if (chesstype.Text.Length > 0 && controller.IsPlaying())
			{
                var ct = Pieceboard.ChesstypesFromString(chesstype.Text);
                controller.ChangeType(ct);
			}
		}

		private void widthtext_KeyPress_1(object sender, KeyPressEventArgs e)
		{
            e.Handled = TextboxExtras.HandleInput(TextboxExtras.InputType.Create(false, true, false, false), e.KeyChar, widthtext);
		}

		private void heighttext_KeyPress(object sender, KeyPressEventArgs e)
		{
            e.Handled = TextboxExtras.HandleInput(TextboxExtras.InputType.Create(false, true, false, false), e.KeyChar, heighttext);
		}

        private void InitChessTypes()
        {
            foreach (Pieceboard.Chesstypes ct in Enum.GetValues(typeof(Pieceboard.Chesstypes)))
            {
                if (ct == Pieceboard.Chesstypes.Notype)
                    continue;
                chesstype.Items.Add(ct.ToString());
            }
            chesstype.SelectedIndex = 0;
        }

		private void Form1_Load_1(object sender, EventArgs e)
		{
		    InitChessTypes();
		    CreateMatrix();

            Licensing.CreateLicense(this, menuStrip1, new Licensing.SolutionDetails(GetDetails, HelpString, AppTitle, AppVersion, OtherText));
		}

        public Licensing.DownloadedSolutionDetails GetDetails()
        {
            try
            {
                var sr = new ServicesClient();
                var ti = sr.GetTitleInfo(AppTitle);
                if (ti == null)
                    return null;
                return ToDownloadedSolutionDetails(ti);

            }
            catch (Exception)
            {
            }
            return null;
        }

        public static Licensing.DownloadedSolutionDetails ToDownloadedSolutionDetails(TitleInfoServiceModel tism)
        {
            return new Licensing.DownloadedSolutionDetails()
            {
                ZipFileLocation = tism.LatestTitleDownloadPath,
                ChangeLog = tism.LatestTitleChangelog,
                Version = tism.LatestTitleVersion
            };
        }


        private void exitToolStripMenuItem_Click_2(object sender, EventArgs e)
        {
            Application.Exit();
        }

	}
}
