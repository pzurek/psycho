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

namespace Psycho
{
        public class TopicConnection
        {
                Topic topic;
                Cairo.Context context;

                public TopicConnection (Topic iTopic)
                {
                        this.topic = iTopic;
                }

                public void Sketch (Cairo.Context iContext)
                {
                        context = iContext;
                        switch (this.topic.Parent.Style.ConnectShape) {
                        case ConnectionShape.Straight:
                        sketchStraight ();
                        break;
                        case ConnectionShape.Crank:
                        sketchCrank ();
                        break;
                        case ConnectionShape.ChamferedCrank:
                        sketchChamferedCrank ();
                        break;
                        case ConnectionShape.RoundedCrank:
                        sketchRoundedCrank ();
                        break;
                        case ConnectionShape.Arc:
                        sketchArc ();
                        break;
                        case ConnectionShape.Curve:
                        sketchCurve ();
                        break;
                        case ConnectionShape.None:
                        break;
                        default:
                        sketchStraight ();
                        break;
                        }
                }

                private void sketchCurve ()
                {
                        throw new Exception ("The method or operation is not implemented.");
                }

                private void sketchArc ()
                {
                        throw new Exception ("The method or operation is not implemented.");
                }

                private void sketchRoundedCrank ()
                {
                        throw new Exception ("The method or operation is not implemented.");
                }

                private void sketchChamferedCrank ()
                {
                        throw new Exception ("The method or operation is not implemented.");
                }

                private void sketchCrank ()
                {
                        throw new Exception ("The method or operation is not implemented.");
                }

                private void sketchStraight ()
                {
                        context.MoveTo (this.topic.Frame.Left);
                        context.LineTo (this.topic.Parent.Frame.Right);
                }
        }
}