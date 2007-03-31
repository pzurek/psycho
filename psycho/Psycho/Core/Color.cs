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

                public void SetARGB (ushort iAlfa, ushort iRed, ushort iGreen, ushort iBlue)
                {
                        alfa = iAlfa;
                        red = iRed;
                        green = iGreen;
                        blue = iBlue;
                }

                public void SetRGB (ushort iRed, ushort iGreen, ushort iBlue)
                {
                        alfa = 255;
                        red = iRed;
                        green = iGreen;
                        blue = iBlue;
                }

                public void SetAlfa (ushort iAlfa)
                {
                        this.alfa = iAlfa;
                }

                public void GetARGB (out ushort outAlfa, out ushort outRed, out ushort outGreen, out ushort outBlue)
                {
                        outAlfa = alfa;
                        outRed = red;
                        outGreen = green;
                        outBlue = blue;
                }

                public void GetRGB (out ushort outRed, out ushort outGreen, out ushort outBlue)
                {
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
                        this.alfa = 255;
                        this.red = 0;
                        this.green = 0;
                        this.blue = 0;
                }

                public Color (string name, ushort iAlfa, ushort iRed, ushort iGreen, ushort iBlue)
                {
                        this.name = name;
                        this.alfa = iAlfa;
                        this.red = iRed;
                        this.green = iGreen;
                        this.blue = iBlue;
                }

                public Color (ushort iAlfa, ushort iRed, ushort iGreen, ushort iBlue)
                {
                        this.name = "";
                        this.alfa = iAlfa;
                        this.red = iRed;
                        this.green = iGreen;
                        this.blue = iBlue;
                }

                public Color (Color iColor)
                {
                        ushort iAlfa = new ushort ();
                        ushort iRed = new ushort ();
                        ushort iGreen = new ushort ();
                        ushort iBlue = new ushort ();
                        iColor.GetARGB (out iAlfa, out iRed, out iGreen, out iBlue);
                        this.alfa = iAlfa;
                        this.red = iRed;
                        this.green = iGreen;
                        this.blue = iBlue;
                }

                public Pango.Color ToPangoColor ()
                {
                        Pango.Color outColor = new Pango.Color ();
                        outColor.Red = this.red;
                        outColor.Green = this.green;
                        outColor.Blue = this.blue;
                        return outColor;
                }

                public Cairo.Color ToCairoColor ()
                {
                        Cairo.Color outColor = new Cairo.Color ();
                        outColor.A = this.alfa / 255;
                        outColor.R = this.red / 255;
                        outColor.G = this.green / 255;
                        outColor.B = this.blue / 255;
                        return outColor;
                }

                public Color (Pango.Color iColor)
                {
                        this.alfa = 255;
                        this.red = iColor.Red;
                        this.green = iColor.Green;
                        this.blue = iColor.Blue;
                        this.name = "";
                }

                public Color (Cairo.Color iColor)
                {
                        this.alfa = (ushort) (iColor.A * 255);
                        this.red = (ushort) (iColor.R * 255);
                        this.green = (ushort) (iColor.G * 255);
                        this.blue = (ushort) (iColor.B * 255);
                        this.name = "";
                }
        }
}