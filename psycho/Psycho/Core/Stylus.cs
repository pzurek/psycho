// Copyright (C) 2006 by:
//
// Author:
//   Piotr Zurek, p.zurek@gmail.com
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
        public class Stylus
        {
                List<TopicStyle> StyleStore = new List<TopicStyle> ();
                List<Color> ColorStore = new List<Color> ();

                public Color GetColorByBranchNo (int iNumber, out Color outColor)
                {
                        Color color = ColorStore[iNumber];
                        if (color != null) {
                                outColor = color;
                                return outColor;
                        }
                        else {
                                outColor = null;
                                return outColor;
                        }
                }

                public Color GetFirstColor (out Color outColor)
                {
                        Color color = ColorStore[0];
                        if (color != null) {
                                outColor = color;
                                return outColor;
                        }
                        else {
                                outColor = null;
                                return outColor;
                        }
                }

                public Stylus ()
                {
                        ColorStore.Add (new Color (255, 47, 77, 135));
                        ColorStore.Add (new Color ("red", 255, 255, 0, 0));
                        ColorStore.Add (new Color ("green", 255, 0, 255, 0));
                        ColorStore.Add (new Color ("blue", 255, 0, 0, 255));
                        ColorStore.Add (new Color ("alfared", 122, 255, 0, 0));
                        ColorStore.Add (new Color ("alfagreen", 122, 0, 255, 0));
                        ColorStore.Add (new Color ("alfablue", 122, 0, 0, 255));
                }
        }
}
