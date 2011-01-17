// //------10--------20--------30--------40--------50--------60--------70--------80
// //
// // $Filename
// // 
// // Copyright (C) 2008 Piotr Zurek p.zurek@gmail.com
// //
// // This program is free software: you can redistribute it and/or modify
// // it under the terms of the GNU General Public License as published by
// // the Free Software Foundation, either version 3 of the License, or
// // (at your option) any later version.
// //
// // This program is distributed in the hope that it will be useful,
// // but WITHOUT ANY WARRANTY; without even the implied warranty of
// // MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// // GNU General Public License for more details.
// //
// // You should have received a copy of the GNU General Public License
// // along with this program.  If not, see <http://www.gnu.org/licenses/>.
// //
using System;
using System.Collections.Generic;

namespace Psycho.GUI
{
	public abstract class MapItem : IDisposable
	{
		public double Width {get; set;}
		public double Height {get; set;}
		
		public double Left {get; set;}
		public double Top {get; set;}
		
		public double Right {
			get {return Left + Width;}
		}
		public double Bottom {
			get {return Top + Height;}
		}
		
		public virtual void Draw(Cairo.Context context, double left, double top, double width, double height)
		{		
		}
		
		#region IDisposable implementation
		public void Dispose()
		{
			this.Dispose();
		}
		#endregion
		//		List<MapItem> Children;
	}
}

