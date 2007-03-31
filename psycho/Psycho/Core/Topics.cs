// Copyright (C) 2006 by:
//
// Author:
//   Piotr Zurek
//
// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// "Software"), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to
// the following conditions:
// 
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//

using System;
using System.Collections.Generic;
using System.Text;

namespace Psycho
{
        public class Topics : List<Topic>
        {
                int height;
                int width;

                //The calculation below will have to be conditional if Org Chart is to be implemented
                //For Org Chart width has to sum and height has to be max

                public int Height
                {
                        get
                        {
                                if (this.Count > 0) {
                                        height = 0;
                                        foreach (Topic child in this)
                                                height += child.TextHeight;
                                        return height;
                                }
                                else
                                        return 0;
                        }
                }

                public int Width
                {
                        get
                        {
                                if (this.Count > 0) {
                                        width = 0;
                                        foreach (Topic child in this)
                                                if (child.TextWidth > width)
                                                        width = child.TextWidth;
                                        return width;
                                }
                                else
                                        return 0;
                        }
                }

                public Topics ()
                        : base ()
                {
                }
        }
}
