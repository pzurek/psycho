// Copyright (C) 2006 by:
//
// Author:
//   Piotr Zurek, p.zurek@gmail.com
//
//   www.psycho-project.org
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

namespace Psycho
{
        using System;
        using Psycho;

        public class TopicOffset
        {
                double x;
                double y;
                double baseX;
                double baseY;
                double localX;
                double localY;
                bool isAuto;
                Topic topic;

                public TopicOffset (Topic iTopic)
                {
                        this.topic = iTopic;
                        this.isAuto = true;
                        this.Update (iTopic);
                }

                public Topic Topic
                {
                        get { return topic; }
                }

                public bool IsAuto
                {
                        get { return isAuto; }
                        set { isAuto = value; }
                }

                public void Update (Topic iTopic)
                {
                        UpdateX (iTopic);
                        UpdateY (iTopic);
                }

                void UpdateX (Topic iTopic)
                {
                        x = 0;
                        if (iTopic.IsCentral)
                                return;
                        else {
                                switch (this.Topic.Parent.Style.SubLayout) {
                                        case SubtopicsLayout.Map:
                                        if (iTopic.IsOnLeft) {
                                                x = System.Math.Floor (iTopic.Parent.Offset.X -
                                                                       iTopic.Parent.Frame.Width / 2 -
                                                                       iTopic.Frame.Width / 2 -
                                                                       iTopic.Parent.Style.HorChildDist);
                                        }
                                        else {
                                                x = System.Math.Floor (iTopic.Parent.Offset.X +
                                                                       iTopic.Parent.Frame.Width / 2 +
                                                                       iTopic.Frame.Width / 2 +
                                                                       iTopic.Parent.Style.HorChildDist);
                                        }
                                        break;
                                        case SubtopicsLayout.OneSideMap: {
                                                x = System.Math.Floor (iTopic.Parent.Offset.X +
                                                                       iTopic.Parent.Frame.Width / 2 +
                                                                       iTopic.Frame.Width / 2 +
                                                                       iTopic.Parent.Style.HorChildDist);
                                        }
                                        break;
                                        case SubtopicsLayout.Root:
                                        if (iTopic.IsOnLeft) {
                                                x = System.Math.Floor (iTopic.Parent.Offset.X -
                                                                       iTopic.Frame.Width / 2 -
                                                                       iTopic.Parent.Style.HorChildDist);
                                        }
                                        else {
                                                x = System.Math.Floor (iTopic.Parent.Offset.X +
                                                                       iTopic.Frame.Width / 2 +
                                                                       iTopic.Parent.Style.HorChildDist);
                                        }
                                        break;
                                        case SubtopicsLayout.OneSideRoot: {
                                                x = System.Math.Floor (iTopic.Parent.Offset.X +
                                                                       iTopic.Frame.Width / 2 +
                                                                       iTopic.Parent.Style.HorChildDist);
                                        }
                                        break;
                                        case SubtopicsLayout.DoubleOrgChart:
                                        break;
                                        case SubtopicsLayout.OrgChart:
                                        break;
                                }
                        }
                }

                void UpdateY (Topic iTopic)
                {
                        y = 0;
                        if (iTopic.IsCentral)
                                return;
                        else {
                                switch (this.Topic.Parent.Style.SubLayout) {
                                        case SubtopicsLayout.Map:
                                        break;
                                        case SubtopicsLayout.OneSideMap: {
                                                if (iTopic.IsFirst) {
                                                        y = iTopic.Parent.Offset.Y -
                                                            iTopic.Parent.TotalHeight / 2 +
                                                            iTopic.TotalHeight / 2;
                                                }
                                                else {
                                                        y = iTopic.Previous.Offset.Y +
                                                            iTopic.Previous.TotalHeight / 2 +
                                                            iTopic.TotalHeight / 2;
                                                }
                                        }
                                        break;
                                        case SubtopicsLayout.Root:
                                        break;
                                        case SubtopicsLayout.OneSideRoot: 
                                        break;
                                        case SubtopicsLayout.DoubleOrgChart:
                                        break;
                                        case SubtopicsLayout.OrgChart:
                                        break;
                                }
                        }
                }

                public double X
                {
                        get
                        {
                                return x;
                        }
                        set
                        {
                                IsAuto = false;
                                x = value;
                        }
                }

                public double Y
                {
                        get
                        {
                                return y;
                        }
                        set
                        {
                                IsAuto = false;
                                y = value;
                        }
                }

                public void SetValue (int iXOffset, int iYOffset)
                {
                        isAuto = false;
                        X = iXOffset;
                        Y = iYOffset;
                }

                public void GetValue (out double outXOffset, out double outYOffset)
                {
                        outXOffset = X;
                        outYOffset = Y;
                }
        }
}