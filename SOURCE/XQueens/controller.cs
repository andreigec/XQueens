using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ANDREICSLIB;

namespace XQueens
{
    public class controller
    {
        public static pieceboard Main;
        public static PanelUpdates grid;

        public static bool IsPlaying()
        {
            return Main != null;
        }

        public static void ClearMain()
        {
            Main.clear();
            ApplyColour();
        }

        public static void ChangeType(pieceboard.chesstypes ct)
        {
            Main.thistype = ct;
            OverlayPossibleMoves();
        }

        public static void ApplySolve(pieceboard.chesstypes ct)
        {
            var ret = Main.solveH(ct);
            if (ret.Count == 0)
                return;

            Main = null;
            Main = ret[0];
            ApplyColour();
        }

        private static void piecechange(object sender, EventArgs e)
        {
            var TB = sender as Button;
            var s = TB.Name;
            var y = int.Parse(s.Substring(0, s.IndexOf(':')));
            var x = int.Parse(s.Substring(s.IndexOf(':') + 1));
            //if blocked by another piece, cant place
            if (Main.GridBlocked[y][x] > 0 && Main.GridPieces[y][x] == pieceboard.chesstypes.NOTYPE)
                return;
            var set = TB.Text.Length == 0;
            Main.applypiece(x, y, set);

            ApplyColour();
        }

        public static void CreateMatrix(int width, int height,pieceboard.chesstypes CT,PanelUpdates Grid)
        {
            if (Main!=null)
            {
                Main.clear();
                Main = null;
            }
            
            grid = Grid;
            Main= new pieceboard(width, height) { thistype = CT };
            Main.GridBlocked = new short[height][];
            for (var y = 0; y < height; y++)
            {
                Main.GridBlocked[y] = new short[width];
                for (var x = 0; x < width; x++)
                {
                    var TB = new Button();
                    TB.Size = new Size(20, 20);
                    TB.Click += piecechange;
                    TB.Name = GetName(x, y);
                    TB.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((0)));
                    grid.addControl(TB, x != (width - 1));
                }
            }
            Main.GridPieces = MatrixOps.CreateMatrix<pieceboard.chesstypes>(width, height);
            ApplyColour();
        }

        private static void OverlayPossibleMoves()
        {
            var goodV = 0;
            for (var y = 0; y < Main.height; y++)
            {
                for (var x = 0; x < Main.width; x++)
                {
                    if (Main.GridPieces[y][x] != pieceboard.chesstypes.NOTYPE || Main.GridBlocked[y][x] > 0)
                        continue;

                    var good = Main.applypiece(x, y, true);
                    if (good)
                    {
                        goodV++;
                        Main.applypiece(x, y, false);
                    }

                    ApplyColourCell(x, y, !good);
                }
            }
        }

        private static void ApplyColourCell(int x, int y, bool bad)
        {
            if (bad)
            {
                var b = grid.getControlByName(GetName(x, y)) as Button;
                b.BackColor = Color.Tomato;
                if (Main.GridPieces[y][x] == pieceboard.chesstypes.NOTYPE)
                    b.Enabled = false;
                else
                    b.BackColor = Color.Gold;
            }
            else
            {
                var b = grid.getControlByName(GetName(x, y)) as Button;
                b.BackColor = Color.GreenYellow;
                b.Enabled = true;
            }
        }

        private static void ClearColours(PanelUpdates grid)
        {
            for (var y = 0; y < Main.height; y++)
            {
                for (var x = 0; x < Main.width; x++)
                {
                    ApplyColourCell(x, y, false);
                }
            }
        }

        private static void ApplyColour()
        {
            for (var y = 0; y < Main.height; y++)
            {
                for (var x = 0; x < Main.width; x++)
                {
                    var TB = grid.getControlByName(GetName(x, y)) as Button;
                    if (TB == null)
                        continue;

                    var bad = Main.GridBlocked[y][x] > 0 || Main.GridPieces[y][x] != pieceboard.chesstypes.NOTYPE;
                    ApplyColourCell(x, y, bad);

                    if (Main.GridPieces[y][x] != pieceboard.chesstypes.NOTYPE)
                        TB.Text = pieceboard.ChesstypesToCharIdent(Main.GridPieces[y][x]);
                    else
                        TB.Text = "";
                }
            }
            OverlayPossibleMoves();
        }

        private static String GetName(int x, int y)
        {
            return y.ToString() + ":" + x.ToString();
        }

    }
}
