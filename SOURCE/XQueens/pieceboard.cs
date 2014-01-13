using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ANDREICSLIB;

namespace XQueens
{
	public class Pieceboard
	{
		public enum Chesstypes
		{
			Notype,
			Queen,
			Rook,
			Bishop,
			HKnight,
			King
		}

		public enum PieceDirection
		{
			Nodir,
			Allstraight,
			Alldiag,
			Onearound,
			L
		}

		private static Dictionary<Chesstypes, List<PieceDirection>> Piecedirections =
			new Dictionary<Chesstypes, List<PieceDirection>>();
		public short[][] GridBlocked;
		public Chesstypes[][] GridPieces;
		public int PieceCount;
		public int BestScore = -1;
		public int Height;
		public Chesstypes Thistype;
		public int Width;

		static Pieceboard()
		{
			var dirs = new List<PieceDirection>();
			dirs.Add(PieceDirection.Allstraight);
			dirs.Add(PieceDirection.Alldiag);
			Piecedirections.Add(Chesstypes.Queen, dirs);

			dirs = new List<PieceDirection>();
			dirs.Add(PieceDirection.Allstraight);
			Piecedirections.Add(Chesstypes.Rook, dirs);

			dirs = new List<PieceDirection>();
			dirs.Add(PieceDirection.Alldiag);
			Piecedirections.Add(Chesstypes.Bishop, dirs);

			dirs = new List<PieceDirection>();
			dirs.Add(PieceDirection.Onearound);
			Piecedirections.Add(Chesstypes.King, dirs);

			dirs = new List<PieceDirection>();
			dirs.Add(PieceDirection.L);
			Piecedirections.Add(Chesstypes.HKnight, dirs);
		}

		public Pieceboard(int w, int h)
		{
			Width = w;
			Height = h;
		}

		public static Chesstypes ChesstypesFromString(String s)
		{
			return ((Chesstypes)Enum.Parse(typeof(Chesstypes), s, true));
		}

		public static String ChesstypesToCharIdent(Chesstypes ct)
		{
			return ct.ToString().Substring(0, 1);
		}

		public static int BestScoreF(Chesstypes ct, int w, int h)
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
			GridPieces = new Chesstypes[0][];
			PieceCount = Width = Height = 0;
		}

		public void clear()
		{
			for (var y = 0; y < Height; y++)
			{
				for (var x = 0; x < Width; x++)
				{
					GridBlocked[y][x] = 0;
					GridPieces[y][x] = Chesstypes.Notype;
				}
			}
			PieceCount = 0;
		}

		private bool moveleftright(int x1, int y1, bool set)
		{
			var ret = true;
			for (var x = 0; x < Width; x++)
			{
				if (applypiecepos(x, y1, set, x1, y1) == false)
					ret = false;
			}
			return ret;
		}

		private bool moveupdown(int x1, int y1, bool set)
		{
			var ret = true;
			for (var y = 0; y < Height; y++)
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
					if (y < 0 || x < 0 || y >= Height || x >= Width)
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
				if (y2 < 0 || x2 < 0 || y2 >= Height || x2 >= Width)
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

			while (x2 < Width && y2 < Height)
			{
				if (applypiecepos(x2, y2, set, x1, y1) == false)
					ret = false;
				x2++;
				y2++;
			}

			x2 = x1;
			y2 = y1;

			while (x2 >= 0 && y2 < Height)
			{
				if (applypiecepos(x2, y2, set, x1, y1) == false)
					ret = false;
				x2--;
				y2++;
			}

			x2 = x1;
			y2 = y1;

			while (x2 < Width && y2 >= 0)
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
			if ((set && GridPieces[y1][x1] != Chesstypes.Notype) || (set == false && GridPieces[y1][x1] == Chesstypes.Notype))
				return false;

			Chesstypes CT;
			if (set)
			{
				GridPieces[y1][x1] = Thistype;
				CT = Thistype;
				PieceCount++;
			}
			else
			{
				PieceCount--;
				CT = GridPieces[y1][x1];
				GridPieces[y1][x1] = Chesstypes.Notype;
			}

			//get the directions this piece can move, and do
			var dirs = new List<PieceDirection>();
			dirs = Piecedirections[CT];
			var ret = true;

			foreach (var pd in dirs)
			{
				switch (pd)
				{
					case PieceDirection.Allstraight:
						if (moveleftright(x1, y1, set) == false)
							ret = false;
						if (moveupdown(x1, y1, set) == false)
							ret = false;
						break;
					case PieceDirection.Alldiag:
						if (movealldiag(x1, y1, set) == false)
							ret = false;
						break;

					case PieceDirection.L:
						if (moveLshape(x1, y1, set) == false)
							ret = false;
						break;

					case PieceDirection.Onearound:
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

		public List<Pieceboard> solveH(Chesstypes solvefor)
		{
			BestScore = Pieceboard.BestScoreF(solvefor, Width, Height);
			var ret = solve();
			BestScore = -1;
			return ret;
		}

		private bool isOptimum(Pieceboard pb)
		{
			var fs = pb.getFreeSpotCount();
			return pb.PieceCount >= pb.BestScore && fs == 0;
		}

		private List<Pieceboard> solve()
		{
			var retl2 = new List<Pieceboard>();
			var pb2 = clone();

			var set = 0;
			var retl = new List<Pieceboard>();
			var FS = 0;
			for (var y = 0; y < pb2.Height; y++)
			{
				for (var x = 0; x < pb2.Width; x++)
				{
					if (pb2.GridPieces[y][x] != Pieceboard.Chesstypes.Notype || pb2.GridBlocked[y][x] > 0)
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
						return new List<Pieceboard> {pbo};

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
			for (var y = 0; y < Height; y++)
			{
				for (var x = 0; x < Width; x++)
				{
					if (GridBlocked[y][x] == 0 && GridPieces[y][x] == Chesstypes.Notype)
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

			if ((x != mainx || y != mainy) && GridPieces[y][x] != Chesstypes.Notype)
				return false;

			return true;
		}

		public Pieceboard clone()
		{
			var ret = new Pieceboard(Width, Height) { PieceCount = PieceCount, BestScore = BestScore, Thistype = Thistype };

			ret.GridBlocked = MatrixOps.CloneMatrix(GridBlocked, Width, Height);
			ret.GridPieces = MatrixOps.CloneMatrix(GridPieces, Width, Height);
			return ret;
		}
	}
}
