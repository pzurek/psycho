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

		public Color (ushort red, ushort green, ushort blue)
		{
			this.name = "";
			this.alfa = 255;
			this.red = red;
			this.green = green;
			this.blue = blue;
		}

		public Color (ushort alfa, ushort red,
			      ushort green, ushort blue)
		{
			this.name = "";
			this.alfa = alfa;
			this.red = red;
			this.green = green;
			this.blue = blue;
		}

		public Color (string name,
			      ushort alfa, ushort red,
			      ushort green, ushort blue)
		{
			this.name = name;
			this.alfa = alfa;
			this.red = red;
			this.green = green;
			this.blue = blue;
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

		public void SetARGB (ushort new_alfa, ushort new_red,
				     ushort new_green, ushort new_blue)
		{
			alfa = new_alfa;
			red = new_red;
			green = new_green;
			blue = new_blue;
		}

		public void SetRGB (ushort new_red,
				    ushort new_green,
				    ushort new_blue)
		{
			alfa = 255;
			red = new_red;
			green = new_green;
			blue = new_blue;
		}

		public void SetAlfa (ushort new_alfa)
		{
			this.alfa = new_alfa;
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
