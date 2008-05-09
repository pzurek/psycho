//------10--------20--------30--------40--------50--------60--------70--------80
//
// IStyle.cs
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
	public interface IStyle
	{
		string Font {get; set; }

		FrameShape FrameShape {get; set; }

		SubtopicLayout SubtopicLayout {get; set; }

		ConnectionShape ConnectionShape {get; set; }

		IColor FillColor {get; set; }

		IColor StrokeColor {get; set; }

		int LineWidth {get; set; }

		bool EqualMargins {get; set; }

		int LeftMargin {get; set; }

		int RightMargin {get; set; }

		int TopMargin {get; set; }

		int BottomMargin {get; set; }

		int MinMargin {get; }

		int MaxMargin {get; }

		int CrankRadius {get; set; }

		int CrankChamfer {get; set; }

		int PolyDistance {get; set; }

		int Padding {get; set; }

		int HorChildDist {get; set; }

		int VerChildDist {get; set; }

		int OrgChartVertDist {get; set; }

		bool FixedWidth {get; set; }

		int Width {get; set; }

		TextAlignment TextAlignment {get; set; }
	}
}
