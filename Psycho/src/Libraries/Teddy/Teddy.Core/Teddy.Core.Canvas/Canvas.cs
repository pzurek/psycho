//------10--------20--------30--------40--------50--------60--------70--------80
//
// Canvas.cs
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
using System.Collections;
using System.Collections.Generic;

using Cairo;

using Teddy.Core;

namespace Teddy.Core
{
	public class Canvas : ICanvas
	{
		double width;
		double height;
		bool isDirty;
		IShapeList<IShape> shapes;
		ILayerList<ILayer> layers;
		IShapeList<IShape> selection;
		
		public double Width {
			get {
				return width;
			}
			set {
				width = value;
			}
		}

		public double Height {
			get {
				return height;
			}
			set {
				height = value;
			}
		}

		public bool IsDirty {
			get {
				return isDirty;
			}
			set {
				isDirty = value;
			}
		}

		public IShapeList<IShape> Shapes {
			get {
				return shapes;
			}
			set {
				shapes = value;
			}
		}

		public IShapeList<IShape> Selection {
			get {
				return selection;
			}
			set {
				selection = value;
			}
		}

		public ILayerList<ILayer> Layers {
			get {
				return layers;
			}
			set {
				layers = value;
			}
		}
		
		public Canvas()
		{
		}

		public void AddShape (IShape new_shape)
		{
			throw new NotImplementedException();
		}

		public void MoveSelectedShapes (double x, double y)
		{
			throw new NotImplementedException();
		}

		public void DeleteSelectedShapes ()
		{
			throw new NotImplementedException();
		}

		public void ClearSelection ()
		{
			throw new NotImplementedException();
		}

		public Shape GetShapeAt (double x, double y)
		{
			throw new NotImplementedException();
		}

		public IList<IShape> GetShapesAt (double x, double y,
		                                  double width, double height)
		{
			throw new NotImplementedException();
		}
	}
}
