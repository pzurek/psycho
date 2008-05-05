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

namespace Psycho.Core
{
	public class Style
	{
		ITopic topic;
		string font;
		FrameShape frameShape;
		ConnectionPoint connectionPoint;
		ConnectionShape connectionShape;
		Color fillColor, strokeColor;
		bool equalMargins;
		int leftMargin, rightMargin, topMargin, bottomMargin;
		int minMargin, maxMargin;
		int padding, horChildDist, verChildDist, orgChartVertDist;
		int crankRadius, crankChamfer;
		int polyDistance;
		bool fixedWidth;
		int width, lineWidth;
		SubtopicLayout subtopicLayout;
		TextAlignment textAlignment;

		public Style (Topic my_topic)
		{
			this.Topic = my_topic;
		}

		public ITopic Topic
		{
			get { return Topic; }
			set { Topic = value; }
		}

		public string Font
		{
			get { return font; }
			set { font = value; }
		}

		public FrameShape FrameShape
		{
			get { return frameShape; }
			set { frameShape = value; }
		}

		public SubtopicLayout SubtopicLayout
		{
			get { return subtopicLayout; }
			set { subtopicLayout = value; }
		}

		public ConnectionShape ConnectionShape
		{
			get { return connectionShape; }
			set { connectionShape = value; }
		}

		public Color FillColor
		{
			get { return fillColor; }
			set { fillColor = value; }
		}

		public Color StrokeColor
		{
			get { return strokeColor; }
			set { strokeColor = value; }
		}

		public int LineWidth
		{
			get { return lineWidth; }
			set { lineWidth = value; }
		}

		public bool EqualMargins
		{
			get { return equalMargins; }
			set { equalMargins = value; }
		}

		public int LeftMargin
		{
			get { return leftMargin; }
			set { leftMargin = value; }
		}

		public int RightMargin
		{
			get {
				if (equalMargins)
					return leftMargin;
				else
					return rightMargin;
			}
			set { rightMargin = value; }
		}

		public int TopMargin
		{
			get {
				if (equalMargins)
					return leftMargin;
				else
					return topMargin;
			}
			set { topMargin = value; }
		}

		public int BottomMargin
		{
			get {
				if (equalMargins)
					return leftMargin;
				else
					return bottomMargin;
			}
			set { bottomMargin = value; }
		}

		public int MinMargin
		{
			get {
				minMargin = LeftMargin;
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
				maxMargin = LeftMargin;
				if (RightMargin > maxMargin)
					maxMargin = RightMargin;
				if (TopMargin > maxMargin)
					maxMargin = TopMargin;
				if (BottomMargin > maxMargin)
					maxMargin = BottomMargin;
				return maxMargin;
			}
		}

		public int CrankRadius
		{
			get { return crankRadius; }
			set { crankRadius = value; }
		}

		public int CrankChamfer
		{
			get { return crankChamfer; }
			set { crankChamfer = value; }
		}

		public int PolyDistance
		{
			get { return polyDistance; }
			set { polyDistance = value; }
		}

		public int Padding
		{
			get { return padding; }
			set { padding = value; }
		}

		public int HorChildDist
		{
			get { return horChildDist; }
			set { horChildDist = value; }
		}

		public int VerChildDist
		{
			get { return verChildDist; }
			set { verChildDist = value; }
		}

		public int OrgChartVertDist
		{
			get { return orgChartVertDist; }
			set { orgChartVertDist = value; }
		}

		public bool FixedWidth
		{
			get { return fixedWidth; }
			set { fixedWidth = value; }
		}

		public int Width
		{
			get { return width; }
			set { width = value; }
		}

		public TextAlignment TextAlignment{
			get { return textAlignment; }
			set { textAlignment = value; }
		}
	}
}
