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

        public class Color
        {

                string name;
                ushort red;
                ushort green;
                ushort blue;
                ushort alfa;
                bool isAuto;

                public bool IsAuto
                {
                        get { return isAuto; }
                        set { isAuto = value; }
                }

                public void SetARGB (ushort paramAlfa, ushort paramRed, ushort paramGreen, ushort paramBlue)
                {
                        alfa = paramAlfa;
                        red = paramRed;
                        green = paramGreen;
                        blue = paramBlue;
                }

                public void SetAlfa (ushort paramAlfa)
                {
                        this.alfa = paramAlfa;
                }

                public void GetARGB (out ushort outAlfa, out ushort outRed, out ushort outGreen, out ushort outBlue)
                {
                        outAlfa = alfa;
                        outRed = red;
                        outGreen = green;
                        outBlue = blue;
                }

                public string Name
                {
                        get { return name; }
                        set { name = value; }
                }

                public Color ()
                {
                        this.name = "";
                        this.alfa = 0;
                        this.red = 0;
                        this.green = 0;
                        this.blue = 0;
                }

                public Color (string name, ushort paramAlfa, ushort paramRed, ushort paramGreen, ushort paramBlue)
                {
                        this.name = name;
                        this.alfa = paramAlfa;
                        this.red = paramRed;
                        this.green = paramGreen;
                        this.blue = paramBlue;
                }

                public Color (ushort paramAlfa, ushort paramRed, ushort paramGreen, ushort paramBlue)
                {
                        this.name = "";
                        this.alfa = paramAlfa;
                        this.red = paramRed;
                        this.green = paramGreen;
                        this.blue = paramBlue;
                }

                public Color (Color paramColor)
                {
                        ushort paramAlfa = new ushort ();
                        ushort paramRed = new ushort ();
                        ushort paramGreen = new ushort ();
                        ushort paramBlue = new ushort ();
                        paramColor.GetARGB (out paramAlfa, out paramRed, out paramGreen, out paramBlue);
                        this.alfa = paramAlfa;
                        this.red = paramRed;
                        this.green = paramGreen;
                        this.blue = paramBlue;
                }
        }
}
