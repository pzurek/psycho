//------10--------20--------30--------40--------50--------60--------70--------80
//
// ICanvas.cs
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
using Teddy.Core;

namespace Teddy.Core
{
	public interface ICanvas
	{
		double Width { get; set; }
		double Height { get; set; }
		bool IsDirty { get; set; }
		IShapeList<IShape> Shapes { get; set; }
		IShapeList<IShape> Selection { get; set; }
		ILayerList<ILayer> Layers { get; set; }
		void AddShape(IShape new_shape);
		void MoveSelectedShapes (double x, double y);
		void DeleteSelectedShapes();
		void ClearSelection();
		IShape GetShapeAt (double x, double y);
		IList<IShape> GetShapesAt (double x, double y,
		                           double width, double height);
	}
}
