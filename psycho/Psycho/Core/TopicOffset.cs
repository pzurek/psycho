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
                                        case SubtopicsLayout.Map:
                                        if (iTopic.IsOnLeft) {
                                                if (iTopic.IsFirst) {
                                                        baseX = System.Math.Floor (iTopic.Parent.Offset.BaseX -
                                                                                   iTopic.Parent.Frame.Width / 2 -
                                                                                   iTopic.Frame.Width / 2 -
                                                                                   iTopic.Parent.Style.HorChildDist);
                                                }
                                                else {
                                                        baseX = System.Math.Floor (iTopic.Previous.Offset.BaseX);
                                                }
                                        }
                                        else {
                                                if (iTopic.IsFirst) {
                                                        if (iTopic.Level == 1)
                                                                baseX = System.Math.Floor (iTopic.Parent.Offset.BaseX +
                                                                                           iTopic.Parent.TotalWidth -
                                                                                           iTopic.Parent.SubtopicList.Width);
                                                        else
                                                                baseX = System.Math.Floor (iTopic.Parent.Offset.BaseX +
                                                                                           iTopic.Parent.TotalWidth -
                                                                                           iTopic.Parent.SubtopicList.Width);
                                                }
                                                else {
                                                        baseX = System.Math.Floor (iTopic.Previous.Offset.BaseX);
                                                }
                                        }
                                        break;
                                        case SubtopicsLayout.OneSideMap: {
                                                if (iTopic.IsFirst) {
                                                        if (iTopic.Level == 1)
                                                                baseX = System.Math.Floor (iTopic.Parent.Offset.BaseX +
                                                                                           iTopic.Parent.TotalWidth -
                                                                                           iTopic.Parent.SubtopicList.Width);
                                                        else
                                                                baseX = System.Math.Floor (iTopic.Parent.Offset.BaseX +
                                                                                           iTopic.Parent.TotalWidth -
                                                                                           iTopic.Parent.SubtopicList.Width);
                                                }
                                                else {
                                                        baseX = System.Math.Floor (iTopic.Previous.Offset.BaseX);
                                                }
                                        }
                                        break;
                                        case SubtopicsLayout.Root:
                                        if (iTopic.IsOnLeft) {
                                                if (iTopic.IsFirst) {
                                                        baseX = System.Math.Floor (iTopic.Parent.Offset.BaseX -
                                                                                   iTopic.Parent.Width / 2 -
                                                                                   iTopic.Parent.Style.HorChildDist);
                                                }
                                                else {
                                                        baseX = System.Math.Floor (iTopic.Previous.Offset.BaseX);
                                                }
                                        }
                                        else {
                                                if (iTopic.IsFirst) {
                                                        if (iTopic.Level == 1)
                                                                baseX = System.Math.Floor (iTopic.Parent.Offset.BaseX +
                                                                                           iTopic.Parent.Width / 2 +
                                                                                           iTopic.Parent.Style.HorChildDist / 2);
                                                        else
                                                                baseX = System.Math.Floor (iTopic.Parent.Offset.BaseX +
                                                                                           iTopic.Parent.Width / 2 +
                                                                                           iTopic.Parent.Style.HorChildDist / 2);
                                                }
                                                else {
                                                        baseX = System.Math.Floor (iTopic.Previous.Offset.BaseX);
                                                }
                                        } break;
                                        case SubtopicsLayout.OneSideRoot: {
                                                if (iTopic.IsFirst) {
                                                        if (iTopic.Level == 1)
                                                                baseX = System.Math.Floor (iTopic.Parent.Offset.BaseX +
                                                                                           iTopic.Parent.Width / 2 +
                                                                                           iTopic.Parent.Style.HorChildDist / 2);
                                                        else
                                                                baseX = System.Math.Floor (iTopic.Parent.Offset.BaseX +
                                                                                           iTopic.Parent.Width / 2 +
                                                                                           iTopic.Parent.Style.HorChildDist / 2);
                                                }
                                                else {
                                                        baseX = System.Math.Floor (iTopic.Previous.Offset.BaseX);
                                                }
                                        }
                                        break;
                                        case SubtopicsLayout.DoubleOrgChart: {
                                                if (iTopic.IsOnTop) {
                                                        if (iTopic.IsFirst) {
                                                                baseX = System.Math.Floor (iTopic.Parent.Offset.BaseX -
                                                                                       iTopic.Parent.Frame.Width / 2 -
                                                                                       iTopic.Frame.Width / 2 -
                                                                                       iTopic.Parent.Style.HorChildDist);
                                                        }
                                                        else {
                                                                baseX = System.Math.Floor (iTopic.Previous.Offset.BaseX);
                                                        }
                                                }
                                                else {
                                                        if (iTopic.IsFirst) {
                                                                if (iTopic.Level == 1)
                                                                        baseX = System.Math.Floor (iTopic.Parent.Offset.BaseX -
                                                                                                   iTopic.Parent.TotalWidth / 2);
                                                                else
                                                                        baseX = System.Math.Floor (iTopic.Parent.Offset.BaseX);
                                                        }
                                                        else {
                                                                baseX = System.Math.Floor (iTopic.Previous.Offset.BaseX +
                                                                                           iTopic.Previous.TotalWidth);
                                                        }
                                                }
                                        }
                                        break;
                                        case SubtopicsLayout.OrgChart:{
                                                if (iTopic.IsOnTop) {
                                                        if (iTopic.IsFirst) {
                                                                baseX = System.Math.Floor (iTopic.Parent.Offset.BaseX -
                                                                                       iTopic.Parent.Frame.Width / 2 -
                                                                                       iTopic.Frame.Width / 2 -
                                                                                       iTopic.Parent.Style.HorChildDist);
                                                        }
                                                        else {
                                                                baseX = System.Math.Floor (iTopic.Previous.Offset.BaseX);
                                                        }
                                                }
                                                else {
                                                        if (iTopic.IsFirst) {
                                                                if (iTopic.Level == 1)
                                                                        baseX = System.Math.Floor (iTopic.Parent.Offset.BaseX -
                                                                                                   iTopic.Parent.TotalWidth / 2);
                                                                else
                                                                        baseX = System.Math.Floor (iTopic.Parent.Offset.BaseX);
                                                        }
                                                        else {
                                                                baseX = System.Math.Floor (iTopic.Previous.Offset.BaseX +
                                                                                           iTopic.Previous.TotalWidth);
                                                        }
                                                }
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
                                        case SubtopicsLayout.Map:
                                        if (iTopic.IsFirst) {
                                                if (iTopic.Level == 1)
                                                        baseY = iTopic.Parent.Offset.BaseY -
                                                                iTopic.Parent.SubtopicList.Height / 2;
                                                else
                                                        baseY = iTopic.Parent.Offset.BaseY;
                                        }
                                        else {
                                                baseY = iTopic.Previous.Offset.BaseY +
                                                    iTopic.Previous.TotalHeight;
                                        }

                                        break;
                                        case SubtopicsLayout.OneSideMap: {
                                                if (iTopic.IsFirst) {
                                                        if (iTopic.Level == 1)
                                                                baseY = iTopic.Parent.Offset.BaseY -
                                                                        iTopic.Parent.SubtopicList.Height / 2;
                                                        else
                                                                baseY = iTopic.Parent.Offset.BaseY;
                                                }
                                                else {
                                                        baseY = iTopic.Previous.Offset.BaseY +
                                                            iTopic.Previous.TotalHeight;
                                                }
                                        }
                                        break;
                                        case SubtopicsLayout.Root: {
                                                if (iTopic.IsFirst) {
                                                        if (iTopic.Level == 1)
                                                                baseY = iTopic.Parent.Offset.BaseY -
                                                                        iTopic.Parent.SubtopicList.Height;
                                                        else
                                                                baseY = iTopic.Parent.Offset.BaseY +
                                                                        iTopic.Parent.Height +
                                                                        iTopic.Parent.Style.VerChildDist;
                                                }
                                                else {
                                                        baseY = iTopic.Previous.Offset.BaseY +
                                                                iTopic.Previous.TotalHeight;
                                                }
                                        }
                                        break;
                                        case SubtopicsLayout.OneSideRoot: {
                                                if (iTopic.IsFirst) {
                                                        if (iTopic.Level == 1)
                                                                baseY = iTopic.Parent.Offset.BaseY -
                                                                        iTopic.Parent.SubtopicList.Height;
                                                        else
                                                                baseY = iTopic.Parent.Offset.BaseY +
                                                                        iTopic.Parent.Height +
                                                                        iTopic.Parent.Style.VerChildDist;
                                                }
                                                else {
                                                        baseY = iTopic.Previous.Offset.BaseY +
                                                                iTopic.Previous.TotalHeight;
                                                }
                                        }
                                        break;
                                        case SubtopicsLayout.DoubleOrgChart: {
                                                if (iTopic.IsOnTop) {
                                                        if (iTopic.IsFirst) {
                                                                if (iTopic.Level == 1)
                                                                        baseY = System.Math.Floor (iTopic.Parent.Offset.BaseY +
                                                                                                   iTopic.Parent.Height +
                                                                                                   iTopic.Parent.Style.OrgChartVertDist);
                                                                else
                                                                        baseY = System.Math.Floor (iTopic.Parent.Offset.BaseY +
                                                                                                   iTopic.Parent.Height +
                                                                                                   iTopic.Parent.Style.OrgChartVertDist);
                                                        }
                                                        else {
                                                                baseY = System.Math.Floor (iTopic.Previous.Offset.BaseY);
                                                        }
                                                }
                                                else {
                                                        if (iTopic.IsFirst) {
                                                                if (iTopic.Level == 1)
                                                                        baseY = System.Math.Floor (iTopic.Parent.Offset.BaseY +
                                                                                                   iTopic.Parent.Height +
                                                                                                   iTopic.Parent.Style.OrgChartVertDist);
                                                                else
                                                                        baseY = System.Math.Floor (iTopic.Parent.Offset.BaseY +
                                                                                                   iTopic.Parent.Height +
                                                                                                   iTopic.Parent.Style.OrgChartVertDist);
                                                        }
                                                        else {
                                                                baseY = System.Math.Floor (iTopic.Previous.Offset.BaseY);
                                                        }
                                                }
                                        }
                                        break;
                                        case SubtopicsLayout.OrgChart: {
                                                if (iTopic.IsOnTop) {
                                                        if (iTopic.IsFirst) {
                                                                if (iTopic.Level == 1)
                                                                        baseY = System.Math.Floor (iTopic.Parent.Offset.BaseY +
                                                                                                   iTopic.Parent.Height +
                                                                                                   iTopic.Parent.Style.OrgChartVertDist);
                                                                else
                                                                        baseY = System.Math.Floor (iTopic.Parent.Offset.BaseY +
                                                                                                   iTopic.Parent.Height +
                                                                                                   iTopic.Parent.Style.OrgChartVertDist);
                                                        }
                                                        else {
                                                                baseY = System.Math.Floor (iTopic.Previous.Offset.BaseY);
                                                        }
                                                }
                                                else {
                                                        if (iTopic.IsFirst) {
                                                                if (iTopic.Level == 1)
                                                                        baseY = System.Math.Floor (iTopic.Parent.Offset.BaseY +
                                                                                                   iTopic.Parent.Height +
                                                                                                   iTopic.Parent.Style.OrgChartVertDist);
                                                                else
                                                                        baseY = System.Math.Floor (iTopic.Parent.Offset.BaseY +
                                                                                                   iTopic.Parent.Height +
                                                                                                   iTopic.Parent.Style.OrgChartVertDist);
                                                        }
                                                        else {
                                                                baseY = System.Math.Floor (iTopic.Previous.Offset.BaseY);
                                                        }
                                                }
                                        }
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
                                        case SubtopicsLayout.Map:
                                        if (iTopic.IsOnLeft)
                                                localX = System.Math.Floor (-iTopic.Width / 2);
                                        else
                                                localX = System.Math.Floor (iTopic.Width / 2);
                                        break;
                                        case SubtopicsLayout.OneSideMap:
                                        localX = System.Math.Floor (iTopic.Width / 2);
                                        break;
                                        case SubtopicsLayout.Root:
                                        if (iTopic.IsOnLeft)
                                                localX = System.Math.Floor (-iTopic.Width / 2);
                                        else
                                                localX = System.Math.Floor (iTopic.Width / 2);
                                        break;
                                        case SubtopicsLayout.OneSideRoot: {
                                                localX = System.Math.Floor (iTopic.Width / 2);
                                        }
                                        break;
                                        case SubtopicsLayout.DoubleOrgChart:
                                        localX = System.Math.Floor (iTopic.TotalWidth / 2);
                                        break;
                                        case SubtopicsLayout.OrgChart:
                                        localX = System.Math.Floor (iTopic.TotalWidth / 2);
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
                                        case SubtopicsLayout.Map:
                                        localY = System.Math.Floor (iTopic.TotalHeight / 2);
                                        break;
                                        case SubtopicsLayout.OneSideMap:
                                        localY = System.Math.Floor (iTopic.TotalHeight / 2);
                                        break;
                                        case SubtopicsLayout.Root:
                                        localY = System.Math.Floor (iTopic.Height / 2);
                                        break;
                                        case SubtopicsLayout.OneSideRoot:
                                        localY = System.Math.Floor (iTopic.Height / 2);
                                        break;
                                        case SubtopicsLayout.DoubleOrgChart:
                                        localY = System.Math.Floor (iTopic.Height / 2);
                                        break;
                                        case SubtopicsLayout.OrgChart:
                                        localY = System.Math.Floor (iTopic.Height / 2);
                                        break;
                                }
                        }
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
                                y = baseY + localY;
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