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

                public void UpdateX (Topic iTopic)
                {
                        UpdateBaseX (iTopic);
                        UpdateLocalX (iTopic);
                }

                public void UpdateY (Topic iTopic)
                {
                        UpdateBaseY (iTopic);
                        UpdateLocalY (iTopic);
                }

                void UpdateBaseX (Topic iTopic)
                {
                        baseX = 0;
                        if (iTopic.IsCentral)
                                return;
                        else {
                                switch (this.Topic.Parent.Style.SubLayout) {
                                case SubtopicLayout.Map: {
                                        if (iTopic.IsFirst) {
                                                if (iTopic.Level == 1)
                                                        baseX = HorizontalMainFirstMap (iTopic);
                                                else
                                                        baseX = HorizontalSubFirstMap (iTopic);
                                        }
                                        else
                                                baseX = HorizontalSubNextMapTree (iTopic);
                                }
                                break;

                                case SubtopicLayout.Tree: {
                                        if (iTopic.IsFirst) {
                                                if (iTopic.Level == 1)
                                                        baseX = HorizontalMainFirstTree (iTopic);
                                                else
                                                        baseX = HorizontalSubFirstTree (iTopic);
                                        }
                                        else
                                                baseX = HorizontalSubNextMapTree (iTopic);
                                }
                                break;

                                case SubtopicLayout.OrgChart: {
                                        if (iTopic.IsFirst) {
                                                if (iTopic.Level == 1)
                                                        baseX = HorizontalMainFirstOrgChart (iTopic);
                                                else
                                                        baseX = HorizontalSubFirstOrgChart (iTopic);
                                        }
                                        else
                                                baseX = HorizontalSubNextOrgChart (iTopic);
                                }
                                break;

                                }
                        }
                }

                void UpdateBaseY (Topic iTopic)
                {
                        baseY = 0;
                        if (iTopic.IsCentral)
                                return;
                        else {
                                switch (this.Topic.Parent.Style.SubLayout) {
                                case SubtopicLayout.Map:
                                if (iTopic.IsFirst) {
                                        if (iTopic.Level == 1)
                                                baseY = VerticalMainFirstMap (iTopic);
                                        else
                                                baseY = VerticalSubFirstMap (iTopic);
                                }
                                else
                                        baseY = VerticalSubNextMapTree (iTopic);
                                break;

                                case SubtopicLayout.Tree:
                                if (iTopic.IsFirst) {
                                        if (iTopic.Level == 1)
                                                baseY = VerticalMainFirstTree (iTopic);
                                        else
                                                baseY = VerticalSubFirstTree (iTopic);
                                }
                                else
                                        baseY = VerticalSubNextMapTree (iTopic);
                                break;

                                case SubtopicLayout.OrgChart:
                                if (iTopic.IsFirst) {
                                        if (iTopic.Level == 1)
                                                baseY = VerticalMainFirstOrgChart (iTopic);
                                        else
                                                baseY = VerticalSubFirstOrgChart (iTopic);

                                }
                                else
                                        baseY = VerticalSubNextOrgChart (iTopic);
                                break;
                                }
                        }
                }

                void UpdateLocalX (Topic iTopic)
                {
                        localX = 0;
                        if (iTopic.IsCentral)
                                return;
                        else {
                                switch (this.Topic.Style.SubLayout) {
                                case SubtopicLayout.Map:
                                if (iTopic.InPrimarySubtopicList)
                                        localX = iTopic.Width / 2;
                                else
                                        localX = - iTopic.Width / 2;
                                break;
                                case SubtopicLayout.Tree:
                                if (iTopic.InPrimarySubtopicList)
                                        localX = iTopic.Width / 2;
                                else
                                        localX = - iTopic.Width / 2;
                                break;
                                case SubtopicLayout.OrgChart:
                                if (iTopic.InPrimarySubtopicList)
                                        localX = iTopic.TotalWidth / 2;
                                else
                                        localX = - iTopic.TotalWidth / 2;
                                break;
                                }
                        }
                }

                void UpdateLocalY (Topic iTopic)
                {
                        localY = 0;
                        if (iTopic.IsCentral)
                                return;
                        else {
                                switch (this.Topic.Style.SubLayout) {
                                case SubtopicLayout.Map:
                                localY = System.Math.Floor (iTopic.TotalHeight / 2);
                                break;
                                case SubtopicLayout.Tree:
                                localY = System.Math.Floor (iTopic.Height / 2);
                                break;
                                case SubtopicLayout.OrgChart:
                                localY = System.Math.Floor (iTopic.Height / 2);
                                break;
                                }
                        }
                }

                private static double EvaluateSideSign (Topic iTopic, double x)
                {
                        if (!iTopic.InPrimarySubtopicList)
                                x = -x;
                        return x;
                }

                static double VerticalMainFirstMap (Topic iTopic)
                {
                        double y;
                        if (iTopic.InPrimarySubtopicList)
                                y = -iTopic.Parent.PrimarySubtopicList.Height / 2;
                        else
                                y = -iTopic.Parent.SecondarySubtopicList.Height / 2;
                        return y;
                }

                static double VerticalMainFirstTree (Topic iTopic)
                {
                        double y;
                        y = iTopic.Parent.Frame.Height / 2 +
                            iTopic.Parent.Style.VerChildDist;
                        return y;
                }

                static double VerticalMainFirstOrgChart (Topic iTopic)
                {
                        double y;
                        y = iTopic.Parent.Frame.Height / 2 +
                            iTopic.Parent.Style.OrgChartVertDist;
                        y = EvaluateSideSign (iTopic, y);
                        return y;
                }

                static double HorizontalMainFirstMap (Topic iTopic)
                {
                        double x;
                        x = iTopic.Parent.Frame.Width / 2 +
                            iTopic.Parent.Style.HorChildDist;
                        x = EvaluateSideSign (iTopic, x);
                        return x;
                }

                static double HorizontalMainFirstTree (Topic iTopic)
                {
                        double x;
                        x = iTopic.Parent.Style.HorChildDist / 2;
                        x = EvaluateSideSign (iTopic, x);
                        return x;
                }

                static double HorizontalMainFirstOrgChart (Topic iTopic)
                {
                        double x;
                        x = - iTopic.Parent.SubtopicList.Width / 2;
                        return x;
                }

                static double VerticalMainNextMapTree (Topic iTopic)
                {
                        double y;
                        y = iTopic.Previous.Offset.BaseY +
                            iTopic.Previous.TotalHeight;
                        return y;
                }

                static double VerticalMainNextOrgChart (Topic iTopic)
                {
                        double y;
                        y = iTopic.Previous.Offset.BaseY;
                        return y;
                }

                static double VerticalSubFirstMap (Topic iTopic)
                {
                        double y;
                        y = iTopic.Parent.Offset.BaseY;
                        return y;
                }

                static double VerticalSubFirstTree (Topic iTopic)
                {
                        double y;
                        y = iTopic.Parent.Offset.BaseY +
                            iTopic.Parent.Height +
                            iTopic.Parent.Style.VerChildDist;
                        return y;
                }

                static double VerticalSubFirstOrgChart (Topic iTopic)
                {
                        double y;
                        y = iTopic.Parent.Offset.BaseY +
                            iTopic.Parent.Height +
                            iTopic.Parent.Style.OrgChartVertDist;
                        return y;
                }

                private double HorizontalSubFirstMap (Topic iTopic)
                {
                        double x;
                        x = System.Math.Abs(iTopic.Parent.Offset.BaseX) +
                            iTopic.Parent.Frame.Width +
                            iTopic.Parent.Style.HorChildDist;
                        x = EvaluateSideSign (iTopic, x);
                        return x;
                }

                static double HorizontalSubFirstTree (Topic iTopic)
                {
                        double x;
                        x = System.Math.Abs(iTopic.Parent.Offset.BaseX) +
                            iTopic.Parent.Width / 2 +
                            iTopic.Parent.Style.HorChildDist / 2;
                        x = EvaluateSideSign (iTopic, x);
                        return x;
                }

                static double HorizontalSubFirstOrgChart (Topic iTopic)
                {
                        double x;
                        if (iTopic.InPrimarySubtopicList)
                                x = iTopic.Parent.Offset.BaseX;
                        else
                                //FIXME: That requires some adjustment. Some more logic.
                                x = System.Math.Abs (iTopic.Parent.Offset.BaseX) +
                                    iTopic.Parent.TotalWidth; 
                        x = EvaluateSideSign (iTopic, x);
                        return x;
                }

                static double VerticalSubNextMapTree (Topic iTopic)
                {
                        double y;
                        y = iTopic.Previous.Offset.BaseY +
                            iTopic.Previous.TotalHeight;
                        return y;
                }

                static double VerticalSubNextOrgChart (Topic iTopic)
                {
                        double y;
                        y = iTopic.Previous.Offset.BaseY;
                        return y;
                }

                static double HorizontalSubNextMapTree (Topic iTopic)
                {
                        double x;
                        x = iTopic.Previous.Offset.BaseX;
                        return x;
                }

                static double HorizontalSubNextOrgChart (Topic iTopic)
                {
                        double x;
                        x = iTopic.Previous.Offset.BaseX +
                            iTopic.Previous.TotalWidth;
                        return x;
                }


                public double BaseX
                {
                        get { return baseX; }
                }

                public double BaseY
                {
                        get { return baseY; }
                }

                public double LocalX
                {
                        get { return localX; }
                }
                public double LocalY
                {
                        get { return localY; }
                }

                public double X
                {
                        get
                        {
                                x = baseX + localX;
                                return System.Math.Floor (x);
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
                                y = baseY + localY;
                                return System.Math.Floor (y);
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

                public void ResetValue ()
                {
                        isAuto = true;
                }

                public void GetValue (out double outXOffset, out double outYOffset)
                {
                        outXOffset = X;
                        outYOffset = Y;
                }
        }
}