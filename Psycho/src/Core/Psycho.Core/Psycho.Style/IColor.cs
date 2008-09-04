//------10--------20--------30--------40--------50--------60--------70--------80
//
// IColor.cs
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
	public interface IColor
	{
		string Name {get; set; }

		bool IsAuto { get; set; }

		ushort Alfa { get; set; }
		ushort Red { get; set; }
		ushort Green { get; set; }
		ushort Blue { get; set; }

		void SetARGB (ushort new_alfa, ushort new_red,
			      ushort new_green, ushort new_blue);

		void SetRGB (ushort new_red, ushort new_green, ushort new_blue);

		void GetARGB (out ushort out_alfa, out ushort out_red,
			      out ushort out_green, out ushort out_blue);

		void GetRGB (out ushort out_red,
			     out ushort out_green,
			     out ushort out_blue);
	}
}
