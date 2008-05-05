//------10--------20--------30--------40--------50--------60--------70--------80
//
// Color.cs
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
	public class Color
	{
		string name;
		ushort red;
		ushort green;
		ushort blue;
		ushort alfa;
		bool isAuto;

		public Color ()
		{
			this.name = "";
			this.alfa = 255;
			this.red = 0;
			this.green = 0;
			this.blue = 0;
		}

		public Color (ushort my_red, ushort my_green, ushort my_blue)
		{
			this.name = "";
			this.alfa = 255;
			this.red = my_red;
			this.green = my_green;
			this.blue = my_blue;
		}

		public Color (ushort my_alfa, ushort my_red,
			      ushort my_green, ushort my_blue)
		{
			this.name = "";
			this.alfa = my_alfa;
			this.red = my_red;
			this.green = my_green;
			this.blue = my_blue;
		}

		public Color (string name,
			      ushort my_alfa, ushort my_red,
			      ushort my_green, ushort my_blue)
		{
			this.name = name;
			this.alfa = my_alfa;
			this.red = my_red;
			this.green = my_green;
			this.blue = my_blue;
		}

		public string Name
		{
			get { return name; }
			set { name = value; }
		}

		public bool IsAuto
		{
			get { return isAuto; }
			set { isAuto = value; }
		}

		public void SetARGB (ushort my_alfa, ushort my_red,
				     ushort my_green, ushort my_blue)
		{
			alfa = my_alfa;
			red = my_red;
			green = my_green;
			blue = my_blue;
		}

		public void SetRGB (ushort my_red,
				    ushort my_green,
				    ushort my_blue)
		{
			alfa = 255;
			red = my_red;
			green = my_green;
			blue = my_blue;
		}

		public void SetAlfa (ushort my_alfa)
		{
			this.alfa = my_alfa;
		}

		public void GetARGB (out ushort out_alfa, out ushort out_red,
				     out ushort out_green, out ushort out_blue)
		{
			out_alfa = alfa;
			out_red = red;
			out_green = green;
			out_blue = blue;
		}

		public void GetARGB (out ushort out_red,
				     out ushort out_green,
				     out ushort out_blue)
		{
			out_red = red;
			out_green = green;
			out_blue = blue;
		}
	}
}
