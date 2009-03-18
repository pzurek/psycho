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
using System.Xml;
using System.Xml.Serialization;

namespace Psycho.Core
{
	public class Color : IColor
	{
		public Color ()
		{
			this.Name = "";
			this.Alfa = 255;
			this.Red = 0;
			this.Green = 0;
			this.Blue = 0;
		}

		public Color (ushort red,
		              ushort green,
		              ushort blue)
		{
			this.Name = "";
			this.Alfa = 255;
			this.Red = red;
			this.Green = green;
			this.Blue = blue;
		}

		public Color (ushort alfa,
		              ushort red,
					  ushort green,
		              ushort blue)
		{
			this.Name = "";
			this.Alfa = alfa;
			this.Red = red;
			this.Green = green;
			this.Blue = blue;
		}

		public Color (string name,
			      	  ushort alfa, 
		              ushort red,
					  ushort green,
		              ushort blue)
		{
			this.Name = Name;
			this.Alfa = alfa;
			this.Red = red;
			this.Green = green;
			this.Blue = blue;
		}

		[XmlAttribute ("name")]  public string Name { get; set; }
		[XmlAttribute ("auto")]  public bool IsAuto { get; set; }
		[XmlAttribute ("red")]   public ushort Red { get; set; }
		[XmlAttribute ("green")] public ushort Green { get; set; }
		[XmlAttribute ("blue")]  public ushort Blue { get; set; }
		[XmlAttribute ("alfa")]  public ushort Alfa { get; set; }

		public void SetARGB (ushort new_alfa,
		                     ushort new_red,
				     		 ushort new_green,
		                     ushort new_blue)
		{
			Alfa = new_alfa;
			Red = new_red;
			Green = new_green;
			Blue = new_blue;
		}

		public void SetRGB (ushort new_red,
				    		ushort new_green,
				    		ushort new_blue)
		{
			Alfa = 255;
			Red = new_red;
			Green = new_green;
			Blue = new_blue;
		}

		public void GetARGB (out ushort out_alfa,
		                     out ushort out_red,
				     		 out ushort out_green,
		                     out ushort out_blue)
		{
			out_alfa = Alfa;
			out_red = Red;
			out_green = Green;
			out_blue = Blue;
		}

		public void GetRGB (out ushort out_red,
				    		out ushort out_green,
				    		out ushort out_blue)
		{
			out_red = Red;
			out_green = Green;
			out_blue = Blue;
		}
	}
}
