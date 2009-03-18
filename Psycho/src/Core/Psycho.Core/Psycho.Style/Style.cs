//------10--------20--------30--------40--------50--------60--------70--------80
//
// Style.cs
// 
// Copyright (C) 2008 Piotr Zurek p.zurek@gmail.com
//
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.
//

using System;
using System.Xml;
using System.Xml.Serialization;

namespace Psycho.Core
{
	public class Style
	{
		public string Font {get; set;}
		public FrameShape FrameShape {get; set;}
		public SubtopicLayout SubtopicLayout {get; set;}
		public ConnectionShape ConnectionShape {get; set;}
		public IColor FillColor {get; set;}
		public IColor StrokeColor {get; set;}
		public int LineWidth {get; set;}

		public bool EqualMargins {get; set;}
		public int LeftMargin {get; set;}
		public int RightMargin
		{
			get {
				if (EqualMargins)
					return LeftMargin;
				else
					return RightMargin;
			}
			set { RightMargin = value; }
		}

		public int TopMargin
		{
			get {
				if (EqualMargins)
					return LeftMargin;
				else
					return TopMargin;
			}
			set { TopMargin = value; }
		}

		public int BottomMargin
		{
			get {
				if (EqualMargins)
					return LeftMargin;
				else
					return BottomMargin;
			}
			set { BottomMargin = value; }
		}

		public int MinMargin
		{
			get {
				int minMargin = LeftMargin;
				if (RightMargin < minMargin)
					minMargin = RightMargin;
				if (TopMargin < minMargin)
					minMargin = TopMargin;
				if (BottomMargin < minMargin)
					minMargin = BottomMargin;
				return minMargin;
			}
		}

		public int MaxMargin
		{
 			get {
				int maxMargin = LeftMargin;
				if (RightMargin > maxMargin)
					maxMargin = RightMargin;
				if (TopMargin > maxMargin)
					maxMargin = TopMargin;
				if (BottomMargin > maxMargin)
					maxMargin = BottomMargin;
				return maxMargin;
			}
		}

		public int CrankRadius {get; set;}
		public int CrankChamfer {get; set;}
		public int PolyDistance {get; set;}
		public int Padding {get; set;}
		public int HorChildDist {get; set;}
		public int VerChildDist {get; set;}
		public int OrgChartVertDist {get; set;}
		public bool FixedWidth {get; set;}
		public int Width {get; set;}
		public TextAlignment TextAlignment {get; set;}
	}
}
