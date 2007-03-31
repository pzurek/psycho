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
using System.Text;

namespace Psycho
{

        public class Font
        {

                string description;
                string family;
                Single size;
                Color color;
                Color strikethroughColor;
                bool bold;
                bool italic;
                bool underline;
                bool strikethrough;
                bool isAuto;

                public Font (string paramFamily, int paramSize)
                {
                        this.family = paramFamily;
                        this.size = paramSize;
                        this.color = new Color ();
                        this.strikethroughColor = new Color ();
                        this.bold = false;
                        this.italic = false;
                        this.underline = false;
                        this.strikethrough = false;
                }

                public string Description
                {
                        get
                        {
                                description = (family + ", " + size.ToString ());
                                return description;
                        }
                }

                public string Family
                {
                        get { return family; }
                        set { family = value; }
                }

                public Single Size
                {
                        get { return size; }
                        set { size = value; }
                }

                public Color FontColor
                {
                        get { return color; }
                        set { color = value; }
                }

                public Color StrikethroughColor
                {
                        get { return strikethroughColor; }
                        set { strikethroughColor = value; }
                }

                public bool Bold
                {
                        get { return bold; }
                        set { bold = value; }
                }

                public bool Italic
                {
                        get { return italic; }
                        set { italic = value; }
                }

                public bool Underline
                {
                        get { return underline; }
                        set { underline = value; }
                }

                public bool Strikethrough
                {
                        get { return strikethrough; }
                        set { strikethrough = value; }
                }

                public bool IsAuto
                {
                        get { return isAuto; }
                        set { isAuto = value; }
                }
        }
}
