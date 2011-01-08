//------10--------20--------30--------40--------50--------60--------70--------80
//
// IController.cs
// 
// Copyright (C) 2009 Piotr Zurek p.zurek@gmail.com
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
using Psycho.Core.Data;
using Psycho.Core.Style;


namespace Psycho.Core
{
        public interface IMindController
        {
                void RequestAddTopic ();
                void RequestAddSubtopic ();
                void RequestDelete ();
                void RequestChange (ITopic Topic);
                void RequestSetCurrent (string Guid);
                void RequestClearCurrent ();
                void RequestSetCurrentByCoords (int X, int Y);
                void RequestExpand (string Guid, bool expand);

                void SetModel (IMindMap Map);
                void SetView (IMindView View);
        }
}
