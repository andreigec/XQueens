using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ANDREICSLIB;

namespace XQueens
{
	public class pieceboard
	{
		public enum chesstypes
		{
			NOTYPE,
			Queen,
			Rook,
			Bishop,
			HKnight,
			King
		}

		public enum pieceDirection
		{
			NODIR,
			ALLSTRAIGHT,
			ALLDIAG,
			ONEAROUND,
			L
		}

		private static Dictionary<chesstypes, List<pieceDirection>> piecedirections =
			new Dictionary<chesstypes, List<pieceDirection>>();
		public short[][] GridBlocked;
		public chesstypes[][] GridPieces;
		public int PieceCount;
		public int bestScore = -1;
		public int height;
		public chesstypes thistype;
		public int width;

		static pieceboard()
		{
			var dirs = new List<pieceDirection>();
			dirs.Add(pieceDirection.ALLSTRAIGHT);
			dirs.Add(pieceDirection.ALLDIAG);
			piecedirections.Add(chesstypes.Queen, dirs);

			dirs = new List<pieceDirection>();
			dirs.Add(pieceDirection.ALLSTRAIGHT);
			piecedirections.Add(chesstypes.Rook, dirs);

			dirs = new List<pieceDirection>();
			dirs.Add(pieceDirection.ALLDIAG);
			piecedirections.Add(chesstypes.Bishop, dirs);

			dirs = new List<pieceDirection>();
			dirs.Add(pieceDirection.ONEAROUND);
			piecedirections.Add(chesstypes.King, dirs);

			dirs = new List<pieceDirection>();
			dirs.Add(pieceDirection.L);
			piecedirections.Add(chesstypes.HKnight, dirs);
		}

		public pieceboard(int w, int h)
		{
			width = w;
			height = h;
		}

		public static chesstypes ChesstypesFromString(String s)
		{
			return ((chesstypes)Enum.Parse(typeof(chesstypes), s, true));
		}

		public static String ChesstypesToCharIdent(chesstypes ct)
		{
			return ct.ToString().Substring(0, 1);
		}

		public static int BestScoreF(chesstypes ct, int w, int h)
		{
			var sq = w < h ? w : h;
			if (sq < 3)
				return 0;

			if (sq % 2 == 0)
				return sq;

			return sq - 1;
		}

		public void delete()
		{
			GridBlocked = null;
			GridPieces = new chesstypes[0][];
			PieceCount = width = height = 0;
		}

		public void clear()
		{
			for (var y = 0; y < height; y++)
			{
				for (var x = 0; x < width; x++)
				{
					GridBlocked[y][x] = 0;
					GridPieces[y][x] = chesstypes.NOTYPE;
				}
			}
			PieceCount = 0;
		}

		private bool moveleftright(int x1, int y1, bool set)
		{
			var ret = true;
			for (var x = 0; x < width; x++)
			{
				if (applypiecepos(x, y1, set, x1, y1) == false)
					ret = false;
			}
			return ret;
		}

		private bool moveupdown(int x1, int y1, bool set)
		{
			var ret = true;
			for (var y = 0; y < height; y++)
			{
				if (applypiecepos(x1, y, set, x1, y1) == false)
					ret = false;
			}
			return ret;
		}

		private bool moveonearound(int x1, int y1, bool set)
		{
			var ret = true;
			for (var y = y1 - 1; y <= y1 + 1; y++) for (var x = x1 - 1; x <= x1 + 1; x++)
				{
					if (y < 0 || x < 0 || y >= height || x >= width)
						continue;
					if (applypiecepos(x, y, set, x1, y1) == false)
						ret = false;
				}
			return ret;
		}

		private bool moveLshape(int x1, int y1, bool set)
		{
			var ret = true;
			var TL = new List<Tuple<short, short>>();
			TL.Add(new Tuple<short, short>(2, 1));
			TL.Add(new Tuple<short, short>(2, -1));
			TL.Add(new Tuple<short, short>(-2, 1));
			TL.Add(new Tuple<short, short>(-2, -1));

			TL.Add(new Tuple<short, short>(1, 2));
			TL.Add(new Tuple<short, short>(-1, 2));
			TL.Add(new Tuple<short, short>(1, -2));
			TL.Add(new Tuple<short, short>(-1, -2));

			foreach (var T in TL)
			{
				var x2 = x1 + T.Item1;
				var y2 = y1 + T.Item2;
				if (y2 < 0 || x2 < 0 || y2 >= height || x2 >= width)
					continue;
				if (applypiecepos(x2, y2, set, x1, y1) == false)
					ret = false;
			}

			return ret;
		}

		private bool movealldiag(int x1, int y1, bool set)
		{
			var ret = true;
			var x2 = x1;
			var y2 = y1;

			while (x2 >= 0 && y2 >= 0)
			{
				if (applypiecepos(x2, y2, set, x1, y1) == false)
					ret = false;
				x2--;
				y2--;
			}

			x2 = x1;
			y2 = y1;

			while (x2 < width && y2 < height)
			{
				if (applypiecepos(x2, y2, set, x1, y1) == false)
					ret = false;
				x2++;
				y2++;
			}

			x2 = x1;
			y2 = y1;

			while (x2 >= 0 && y2 < height)
			{
				if (applypiecepos(x2, y2, set, x1, y1) == false)
					ret = false;
				x2--;
				y2++;
			}

			x2 = x1;
			y2 = y1;

			while (x2 < width && y2 >= 0)
			{
				if (applypiecepos(x2, y2, set, x1, y1) == false)
					ret = false;
				x2++;
				y2--;
			}
			return ret;
		}

		public bool applypiece(int x1, int y1, bool set)
		{
			if ((set && GridPieces[y1][x1] != chesstypes.NOTYPE) || (set == false && GridPieces[y1][x1] == chesstypes.NOTYPE))
				return false;

			chesstypes CT;
			if (set)
			{
				GridPieces[y1][x1] = thistype;
				CT = thistype;
				PieceCount++;
			}
			else
			{
				PieceCount--;
				CT = GridPieces[y1][x1];
				GridPieces[y1][x1] = chesstypes.NOTYPE;
			}

			//get the directions this piece can move, and do
			var dirs = new List<pieceDirection>();
			dirs = piecedirections[CT];
			var ret = true;

			foreach (var pd in dirs)
			{
				switch (pd)
				{
					case pieceDirection.ALLSTRAIGHT:
						if (moveleftright(x1, y1, set) == false)
							ret = false;
						if (moveupdown(x1, y1, set) == false)
							ret = false;
						break;
					case pieceDirection.ALLDIAG:
						if (movealldiag(x1, y1, set) == false)
							ret = false;
						break;

					case pieceDirection.L:
						if (moveLshape(x1, y1, set) == false)
							ret = false;
						break;

					case pieceDirection.ONEAROUND:
						if (moveonearound(x1, y1, set) == false)
							ret = false;
						break;
				}
			}
			if (ret == false && set)
			{
				applypiece(x1, y1, false);
				return false;
			}
			return ret;
		}

		public List<pieceboard> solveH(chesstypes solvefor)
		{
			bestScore = pieceboard.BestScoreF(solvefor, width, height);
			var ret = solve();
			bestScore = -1;
			return ret;
		}

		private bool isOptimum(pieceboard pb)
		{
			var fs = pb.getFreeSpotCount();
			return pb.PieceCount >= pb.bestScore && fs == 0;
		}

		private List<pieceboard> solve()
		{
			var retl2 = new List<pieceboard>();
			var pb2 = clone();

			var set = 0;
			var retl = new List<pieceboard>();
			var FS = 0;
			for (var y = 0; y < pb2.height; y++)
			{
				for (var x = 0; x < pb2.width; x++)
				{
					if (pb2.GridPieces[y][x] != pieceboard.chesstypes.NOTYPE || pb2.GridBlocked[y][x] > 0)
						continue;

					var good = pb2.applypiece(x, y, true);
					if (good == false)
					{
						pb2.applypiece(x, y, false);
						continue;
					}

					set = 1;
					retl.AddRange(pb2.solve());
					//see if any of the returned results were optimal
					var foundoptimal = false;
					var pbo = pb2;
					foreach (var v1 in retl)
					{
						if (isOptimum(v1))
						{
							foundoptimal = true;
							pbo = v1;
							break;
						}
					}
					if (foundoptimal)
						return new List<pieceboard> {pbo};

					pb2.applypiece(x, y, false);
				}
			}

				//best scenario, we have gotten at least the best score, and no free spaces remain
				if (isOptimum(pb2))
				{
					retl2.Add(pb2);
					return retl2;
				}
				
			//get the best scoring item otherwise
			var best = -1;
			var bestpb = pb2;
			foreach (var x in retl)
			{
				if (best == -1 || best < x.PieceCount)
				{
					best = x.PieceCount;
					bestpb = x;
				}
			}
			retl2.Add(bestpb);

			return retl2;
		}

		private int getFreeSpotCount()
		{
			var ret = 0;
			for (var y = 0; y < height; y++)
			{
				for (var x = 0; x < width; x++)
				{
					if (GridBlocked[y][x] == 0 && GridPieces[y][x] == chesstypes.NOTYPE)
						ret++;
				}
			}
			return ret;
		}

		private bool applypiecepos(int x, int y, bool set, int mainx, int mainy)
		{
			if (set)
				GridBlocked[y][x]++;
			else
				GridBlocked[y][x]--;

			if ((x != mainx || y != mainy) && GridPieces[y][x] != chesstypes.NOTYPE)
				return false;

			return true;
		}

		public pieceboard clone()
		{
			var ret = new pieceboard(width, height) { PieceCount = PieceCount, bestScore = bestScore, thistype = thistype };

			ret.GridBlocked = MatrixOps.CloneMatrix(GridBlocked, width, height);
			ret.GridPieces = MatrixOps.CloneMatrix(GridPieces, width, height);
			return ret;
		}
	}
}
