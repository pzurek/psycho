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

using System;

namespace Psycho
{
        public class TopicConnection
        {
                Topic topic;
                ConnectionShape shape;
                Cairo.Context context;
                Cairo.Distance connectionVector;
                Cairo.PointD start, end, extendedStart;
                Cairo.PointD middleStart, middleEnd;
                Cairo.PointD roundedCrankCenterStart, roundedCrankCenterEnd;
                Cairo.PointD chamferedCrankStart1, chamferedCrankStart2;
                Cairo.PointD chamferedCrankEnd1, chamferedCrankEnd2;
                Cairo.PointD arcControlStart, arcControlEnd;
                Cairo.PointD curveControlStart, curveControlEnd;
                Cairo.PointD roundedAngleCrankStart, roundedAngleCrankEnd;
                Cairo.PointD rootCenter;
                double crankRadius, crankChamfer;
                bool mapExtenstion, orgChartExtension;

                static double PI = System.Math.PI;
                static double angle1 = 0.0 * (PI / 180.0);
                static double angle2 = 90.0 * (PI / 180.0);
                static double angle3 = 180.0 * (PI / 180.0);
                static double angle4 = 270.0 * (PI / 180.0);

                public TopicConnection (Topic iTopic)
                {
                        this.topic = iTopic;
                        Update (iTopic);
                }

                public void Update (Topic iTopic)
                {
                        if (iTopic.Parent == null) 
                                return;
                        
                        this.start = iTopic.InPoint;

                        if (iTopic.IsMain) {
                                switch (iTopic.Parent.Style.SubLayout) {
                                case SubtopicLayout.Map:
                                        if (iTopic.InPrimarySubtopicList)
                                                this.end = iTopic.Parent.Frame.Right;
                                        else
                                                this.end = iTopic.Parent.Frame.Left;
                                break;

                                case SubtopicLayout.Tree:
                                this.end = iTopic.Parent.Frame.Bottom;
                                break;

                                case SubtopicLayout.OrgChart:
                                        if (iTopic.InPrimarySubtopicList)
                                                this.end = iTopic.Parent.Frame.Bottom;
                                        else
                                                this.end = iTopic.Parent.Frame.Top;
                                break;
                                }
                        }
                        else
                                this.end = iTopic.Parent.OutPoint;

                        this.mapExtenstion = false;
                        this.orgChartExtension = false;

                        if (iTopic.Style.SubLayout == SubtopicLayout.Map)
                                if (iTopic.InPoint.Equals (iTopic.Frame.Top)) {
                                        this.start.Y -= (this.Topic.TotalHeight - this.Topic.Height) / 2;
                                        this.mapExtenstion = true;
                                        this.extendedStart = iTopic.InPoint;
                                }
                                else
                                        if (iTopic.InPoint.Equals (iTopic.Frame.Bottom)) {
                                                this.start.Y += (this.Topic.TotalHeight - this.Topic.Height) / 2;
                                                this.mapExtenstion = true;
                                                this.extendedStart = iTopic.InPoint;
                                }
                                     



                        if (iTopic.Style.SubLayout == SubtopicLayout.OrgChart)
                                if (iTopic.InPoint.Equals (iTopic.Frame.Left)) {
                                        this.start.X -= (this.Topic.TotalWidth - this.Topic.Width) / 2;
                                        this.orgChartExtension = true;
                                        this.extendedStart = iTopic.InPoint;
                                }
                                else {
                                        if (iTopic.InPoint.Equals (iTopic.Frame.Right)) {
                                                this.start.X += (this.Topic.TotalWidth - this.Topic.Width) / 2;
                                                this.orgChartExtension = true;
                                                this.extendedStart = iTopic.InPoint;
                                        }
                                }

                        this.connectionVector.Dx = this.End.X - this.Start.X;
                        this.connectionVector.Dy = this.End.Y - this.Start.Y;

                        this.shape = iTopic.Parent.Style.ConnectShape;
                }

                public Topic Topic
                {
                        get { return topic; }
                }

                public Cairo.PointD Start
                {
                        get { return start;  }
                        set { start = value; }
                }

                public Cairo.PointD End
                {
                        get { return end;  }
                        set { end = value; }
                }

                public Cairo.Distance ConnectionVector
                {
                        get { return connectionVector; }
                }

                ConnectionShape Shape
                {
                        get { return shape; }
                        set { shape = value; }
                }


                public double CrankRadius
                {
                        get
                        {
                                crankRadius = this.Topic.Style.CrankRadius;
                                if (System.Math.Abs (this.ConnectionVector.Dy / 2) < crankRadius)
                                        crankRadius = System.Math.Abs (((this.ConnectionVector.Dy) / 3));
                                if (System.Math.Abs (this.ConnectionVector.Dx / 2) < crankRadius)
                                        crankRadius = System.Math.Abs (((this.ConnectionVector.Dx) / 3));
                                return crankRadius;
                        }
                }

                public double CrankChamfer
                {
                        get
                        {
                                crankChamfer = this.Topic.Style.CrankChamfer;
                                if (System.Math.Abs (this.ConnectionVector.Dy / 2) < crankChamfer)
                                        crankChamfer = System.Math.Abs (((this.ConnectionVector.Dy) / 3));
                                if (System.Math.Abs (this.ConnectionVector.Dx / 2) < crankChamfer)
                                        crankChamfer = System.Math.Abs (((this.ConnectionVector.Dx) / 3));
                                return crankChamfer;
                        }
                }

                public Cairo.PointD MiddleStart
                {
                        get
                        {
                                switch (this.Topic.Parent.Style.SubLayout) {
                                case SubtopicLayout.Map: {
                                        middleStart.X = System.Math.Floor (this.Start.X + this.ConnectionVector.Dx / 2 + 0.5);
                                        middleStart.Y = System.Math.Floor (this.Start.Y);
                                }
                                break;
                                case SubtopicLayout.OrgChart: {
                                        middleStart.X = System.Math.Floor (this.Start.X);
                                        middleStart.Y = System.Math.Floor (this.Start.Y + this.ConnectionVector.Dy / 2 + 0.5);
                                }
                                break;
                                case SubtopicLayout.Tree: {
                                        middleStart.X = System.Math.Floor (this.Start.X + this.ConnectionVector.Dx / 2 + 0.5);
                                        middleStart.Y = System.Math.Floor (this.Start.Y);
                                }
                                break;
                                }
                                return middleStart;
                        }
                }

                public Cairo.PointD MiddleEnd
                {
                        get
                        {
                                switch (this.Topic.Parent.Style.SubLayout) {
                                case SubtopicLayout.Map: {
                                        middleEnd.X = System.Math.Floor (this.End.X - this.ConnectionVector.Dx / 2 + 0.5);
                                        middleEnd.Y = System.Math.Floor (this.End.Y);
                                }
                                break;
                                case SubtopicLayout.OrgChart: {
                                        middleEnd.X = System.Math.Floor (this.End.X);
                                        middleEnd.Y = System.Math.Floor (this.End.Y - this.ConnectionVector.Dy / 2 + 0.5);
                                }
                                break;
                                case SubtopicLayout.Tree: {
                                        middleEnd.X = System.Math.Floor (this.End.X);
                                        middleEnd.Y = System.Math.Floor (this.Start.Y);
                                }
                                break;
                                }
                                return middleEnd;
                        }
                }

                public Cairo.PointD RootCenter
                {
                        get
                        {
                                rootCenter.X = System.Math.Floor (this.End.X);
                                rootCenter.Y = System.Math.Floor (this.Start.Y);
                                return rootCenter;
                        }
                }


                public Cairo.PointD CurveControlStart
                {
                        get
                        {
                                switch (this.Topic.Parent.Style.SubLayout) {
                                case SubtopicLayout.Map: {
                                        curveControlStart.X = System.Math.Floor (this.Start.X + ConnectionVector.Dx / 1.61);
                                        curveControlStart.Y = System.Math.Floor (this.Start.Y);
                                }
                                break;
                                case SubtopicLayout.OrgChart: {
                                        curveControlStart.X = System.Math.Floor (this.Start.X);
                                        curveControlStart.Y = System.Math.Floor (this.Start.Y + ConnectionVector.Dy / 1.61);
                                }
                                break;
                                case SubtopicLayout.Tree: {
                                        curveControlStart.X = System.Math.Floor (this.Start.X + ConnectionVector.Dx / 1.61);
                                        curveControlStart.Y = System.Math.Floor (this.Start.Y);
                                }
                                break;
                                }
                                return curveControlStart;
                        }
                }

                public Cairo.PointD CurveControlEnd
                {
                        get
                        {
                                switch (this.Topic.Parent.Style.SubLayout) {
                                case SubtopicLayout.Map: {
                                        curveControlEnd.X = System.Math.Floor (this.End.X - ConnectionVector.Dx / 1.61);
                                        curveControlEnd.Y = System.Math.Floor (this.End.Y);
                                }
                                break;
                                case SubtopicLayout.OrgChart: {
                                        curveControlEnd.X = System.Math.Floor (this.End.X);
                                        curveControlEnd.Y = System.Math.Floor (this.End.Y - ConnectionVector.Dy / 1.61);
                                }
                                break;
                                case SubtopicLayout.Tree: {
                                        curveControlEnd.X = System.Math.Floor (this.End.X - ConnectionVector.Dx / 1.61);
                                        curveControlEnd.Y = System.Math.Floor (this.End.Y);
                                }
                                break;
                                }
                                return curveControlEnd;
                        }
                }

                public Cairo.PointD RoundedCrankCenterStart
                {
                        get
                        {
                                switch (this.Topic.Parent.Style.SubLayout) {
                                case SubtopicLayout.Map: {
                                        if (this.ConnectionVector.Dx * this.ConnectionVector.Dy > 0) {
                                                if (this.ConnectionVector.Dx > 0) {
                                                        roundedCrankCenterStart.X = System.Math.Floor (MiddleStart.X - CrankRadius);
                                                        roundedCrankCenterStart.Y = System.Math.Floor (MiddleStart.Y + CrankRadius);
                                                }
                                                else {
                                                        roundedCrankCenterStart.X = System.Math.Floor (MiddleStart.X + CrankRadius);
                                                        roundedCrankCenterStart.Y = System.Math.Floor (MiddleStart.Y - CrankRadius);
                                                }
                                        }
                                        else {
                                                if (this.ConnectionVector.Dx < 0) {
                                                        roundedCrankCenterStart.X = System.Math.Floor (MiddleStart.X + CrankRadius);
                                                        roundedCrankCenterStart.Y = System.Math.Floor (MiddleStart.Y + CrankRadius);
                                                }
                                                else {
                                                        roundedCrankCenterStart.X = System.Math.Floor (MiddleStart.X - CrankRadius);
                                                        roundedCrankCenterStart.Y = System.Math.Floor (MiddleStart.Y - CrankRadius);
                                                }
                                        }
                                }
                                break;
                                case SubtopicLayout.OrgChart: {
                                        if (this.ConnectionVector.Dx * this.ConnectionVector.Dy > 0) {
                                                if (this.ConnectionVector.Dx > 0) {
                                                        roundedCrankCenterStart.X = System.Math.Floor (MiddleStart.X + CrankRadius);
                                                        roundedCrankCenterStart.Y = System.Math.Floor (MiddleStart.Y - CrankRadius);
                                                }
                                                else {
                                                        roundedCrankCenterStart.X = System.Math.Floor (MiddleStart.X - CrankRadius);
                                                        roundedCrankCenterStart.Y = System.Math.Floor (MiddleStart.Y + CrankRadius);
                                                }
                                        }
                                        else {
                                                if (this.ConnectionVector.Dx < 0) {
                                                        roundedCrankCenterStart.X = System.Math.Floor (MiddleStart.X - CrankRadius);
                                                        roundedCrankCenterStart.Y = System.Math.Floor (MiddleStart.Y - CrankRadius);
                                                }
                                                else {
                                                        roundedCrankCenterStart.X = System.Math.Floor (MiddleStart.X + CrankRadius);
                                                        roundedCrankCenterStart.Y = System.Math.Floor (MiddleStart.Y + CrankRadius);
                                                }
                                        }
                                }
                                break;
                                case SubtopicLayout.Tree: {
                                        if (this.ConnectionVector.Dx * this.ConnectionVector.Dy > 0) {
                                                if (this.ConnectionVector.Dx > 0) {
                                                        roundedCrankCenterStart.X = System.Math.Floor (RootCenter.X - CrankRadius);
                                                        roundedCrankCenterStart.Y = System.Math.Floor (RootCenter.Y + CrankRadius);
                                                }
                                                else {
                                                        roundedCrankCenterStart.X = System.Math.Floor (RootCenter.X + CrankRadius);
                                                        roundedCrankCenterStart.Y = System.Math.Floor (RootCenter.Y - CrankRadius);
                                                }
                                        }
                                        else {
                                                if (this.ConnectionVector.Dx < 0) {
                                                        roundedCrankCenterStart.X = System.Math.Floor (RootCenter.X + CrankRadius);
                                                        roundedCrankCenterStart.Y = System.Math.Floor (RootCenter.Y + CrankRadius);
                                                }
                                                else {
                                                        roundedCrankCenterStart.X = System.Math.Floor (RootCenter.X - CrankRadius);
                                                        roundedCrankCenterStart.Y = System.Math.Floor (RootCenter.Y - CrankRadius);
                                                }
                                        }
                                }
                                break;
                                }
                                return roundedCrankCenterStart;
                        }
                }

                public Cairo.PointD RoundedCrankCenterEnd
                {
                        get
                        {
                                switch (this.Topic.Parent.Style.SubLayout) {
                                case SubtopicLayout.Map: {
                                        if (this.ConnectionVector.Dx * this.ConnectionVector.Dy > 0) {
                                                if (this.ConnectionVector.Dx > 0) {
                                                        roundedCrankCenterEnd.X = System.Math.Floor (MiddleEnd.X + CrankRadius);
                                                        roundedCrankCenterEnd.Y = System.Math.Floor (MiddleEnd.Y - CrankRadius);
                                                }
                                                else {
                                                        roundedCrankCenterEnd.X = System.Math.Floor (MiddleEnd.X - CrankRadius);
                                                        roundedCrankCenterEnd.Y = System.Math.Floor (MiddleEnd.Y + CrankRadius);
                                                }
                                        }
                                        else {
                                                if (this.ConnectionVector.Dx < 0) {
                                                        roundedCrankCenterEnd.X = System.Math.Floor (MiddleEnd.X - CrankRadius);
                                                        roundedCrankCenterEnd.Y = System.Math.Floor (MiddleEnd.Y - CrankRadius);
                                                }
                                                else {
                                                        roundedCrankCenterEnd.X = System.Math.Floor (MiddleEnd.X + CrankRadius);
                                                        roundedCrankCenterEnd.Y = System.Math.Floor (MiddleEnd.Y + CrankRadius);
                                                }
                                        }
                                }
                                break;
                                case SubtopicLayout.OrgChart: {
                                        if (this.ConnectionVector.Dx * this.ConnectionVector.Dy > 0) {
                                                if (this.ConnectionVector.Dx > 0) {
                                                        roundedCrankCenterEnd.X = System.Math.Floor (MiddleEnd.X - CrankRadius);
                                                        roundedCrankCenterEnd.Y = System.Math.Floor (MiddleEnd.Y + CrankRadius);
                                                }
                                                else {
                                                        roundedCrankCenterEnd.X = System.Math.Floor (MiddleEnd.X + CrankRadius);
                                                        roundedCrankCenterEnd.Y = System.Math.Floor (MiddleEnd.Y - CrankRadius);
                                                }
                                        }
                                        else {
                                                if (this.ConnectionVector.Dx < 0) {
                                                        roundedCrankCenterEnd.X = System.Math.Floor (MiddleEnd.X + CrankRadius);
                                                        roundedCrankCenterEnd.Y = System.Math.Floor (MiddleEnd.Y + CrankRadius);
                                                }
                                                else {
                                                        roundedCrankCenterEnd.X = System.Math.Floor (MiddleEnd.X - CrankRadius);
                                                        roundedCrankCenterEnd.Y = System.Math.Floor (MiddleEnd.Y - CrankRadius);
                                                }
                                        }
                                }
                                break;
                                case SubtopicLayout.Tree:
                                break;
                                }
                                return roundedCrankCenterEnd;
                        }
                }

                public Cairo.PointD ChamferedCrankStart1
                {
                        get
                        {
                                switch (this.Topic.Parent.Style.SubLayout) {
                                case SubtopicLayout.Map: {
                                        chamferedCrankStart1.X = System.Math.Floor (this.MiddleStart.X - this.CrankChamfer * System.Math.Sign (this.connectionVector.Dx));
                                        chamferedCrankStart1.Y = System.Math.Floor (this.MiddleStart.Y);
                                }
                                break;
                                case SubtopicLayout.OrgChart: {
                                        chamferedCrankStart1.X = System.Math.Floor (this.MiddleStart.X);
                                        chamferedCrankStart1.Y = System.Math.Floor (this.MiddleStart.Y - this.CrankChamfer * System.Math.Sign (this.connectionVector.Dy));
                                }
                                break;
                                case SubtopicLayout.Tree: {
                                        chamferedCrankStart1.X = System.Math.Floor (this.RootCenter.X - this.CrankChamfer * System.Math.Sign (this.connectionVector.Dx));
                                        chamferedCrankStart1.Y = System.Math.Floor (this.RootCenter.Y);
                                }
                                break;
                                }
                                return chamferedCrankStart1;
                        }
                }

                public Cairo.PointD ChamferedCrankStart2
                {
                        get
                        {
                                switch (this.Topic.Parent.Style.SubLayout) {
                                case SubtopicLayout.Map: {
                                        chamferedCrankStart2.X = System.Math.Floor (this.MiddleStart.X);
                                        chamferedCrankStart2.Y = System.Math.Floor (this.MiddleStart.Y + this.CrankChamfer * System.Math.Sign (this.connectionVector.Dy));
                                }
                                break;
                                case SubtopicLayout.OrgChart: {
                                        chamferedCrankStart2.X = System.Math.Floor (this.MiddleStart.X + this.CrankChamfer * System.Math.Sign (this.connectionVector.Dx));
                                        chamferedCrankStart2.Y = System.Math.Floor (this.MiddleStart.Y);
                                }
                                break;
                                case SubtopicLayout.Tree: {
                                        chamferedCrankStart2.X = System.Math.Floor (this.RootCenter.X);
                                        chamferedCrankStart2.Y = System.Math.Floor (this.RootCenter.Y + this.CrankChamfer * System.Math.Sign (this.connectionVector.Dy));
                                }
                                break;
                                }
                                return chamferedCrankStart2;
                        }
                }

                public Cairo.PointD ChamferedCrankEnd1
                {
                        get
                        {
                                switch (this.Topic.Parent.Style.SubLayout) {
                                case SubtopicLayout.Map: {
                                        chamferedCrankEnd1.X = System.Math.Floor (this.MiddleEnd.X + this.CrankChamfer * System.Math.Sign (this.connectionVector.Dx));
                                        chamferedCrankEnd1.Y = System.Math.Floor (this.MiddleEnd.Y);
                                }
                                break;
                                case SubtopicLayout.OrgChart: {
                                        chamferedCrankEnd1.X = System.Math.Floor (this.MiddleEnd.X);
                                        chamferedCrankEnd1.Y = System.Math.Floor (this.MiddleEnd.Y + this.CrankChamfer * System.Math.Sign (this.connectionVector.Dy));
                                }
                                break;
                                case SubtopicLayout.Tree:
                                break;
                                }
                                return chamferedCrankEnd1;
                        }
                }

                public Cairo.PointD ChamferedCrankEnd2
                {
                        get
                        {
                                switch (this.Topic.Parent.Style.SubLayout) {
                                case SubtopicLayout.Map: {
                                        chamferedCrankEnd2.X = System.Math.Floor (this.MiddleEnd.X);
                                        chamferedCrankEnd2.Y = System.Math.Floor (this.MiddleEnd.Y - this.CrankChamfer * System.Math.Sign (this.connectionVector.Dy));
                                }
                                break;
                                case SubtopicLayout.OrgChart: {
                                        chamferedCrankEnd2.X = System.Math.Floor (this.MiddleEnd.X - this.CrankChamfer * System.Math.Sign (this.connectionVector.Dx));
                                        chamferedCrankEnd2.Y = System.Math.Floor (this.MiddleEnd.Y);
                                }
                                break;
                                case SubtopicLayout.Tree:
                                break;
                                }
                                return chamferedCrankEnd2;
                        }
                }

                public Cairo.PointD ArcControlStart
                {
                        get
                        {
                                switch (this.Topic.Parent.Style.SubLayout) {
                                case SubtopicLayout.Map: {
                                        arcControlStart.X = System.Math.Floor (this.Start.X + this.ConnectionVector.Dx / 2 + 0.5);
                                        arcControlStart.Y = System.Math.Floor (this.Start.Y);
                                }
                                break;
                                case SubtopicLayout.OrgChart: {
                                        arcControlStart.X = System.Math.Floor (this.Start.X);
                                        arcControlStart.Y = System.Math.Floor (this.Start.Y + this.ConnectionVector.Dy / 2 + 0.5);
                                }
                                break;
                                case SubtopicLayout.Tree: {
                                        arcControlStart.X = System.Math.Floor (this.Start.X + this.ConnectionVector.Dx / 2 + 0.5);
                                        arcControlStart.Y = System.Math.Floor (this.Start.Y);
                                }
                                break;
                                }
                                return arcControlStart;
                        }
                }

                public Cairo.PointD ArcControlEnd
                {
                        get
                        {
                                switch (this.Topic.Parent.Style.SubLayout) {
                                case SubtopicLayout.Map: {
                                        arcControlEnd.X = System.Math.Floor (this.End.X);
                                        arcControlEnd.Y = System.Math.Floor (this.End.Y - this.ConnectionVector.Dy / 2 + 0.5);
                                }
                                break;
                                case SubtopicLayout.OrgChart: {
                                        arcControlEnd.X = System.Math.Floor (this.End.X - this.ConnectionVector.Dx / 2 + 0.5);
                                        arcControlEnd.Y = System.Math.Floor (this.End.Y);
                                }
                                break;
                                case SubtopicLayout.Tree: {
                                        arcControlEnd.X = System.Math.Floor (this.End.X);
                                        arcControlEnd.Y = System.Math.Floor (this.End.Y - this.ConnectionVector.Dy / 2 + 0.5);
                                }
                                break;
                                }
                                return arcControlEnd;
                        }
                }

                public Cairo.PointD RoundedAngleCrankStart
                {
                        get
                        {
                                switch (this.Topic.Parent.Style.SubLayout) {
                                case SubtopicLayout.Map: {
                                        roundedAngleCrankStart.X = System.Math.Floor (this.Start.X + this.ConnectionVector.Dx / 8 + 0.5);
                                        roundedAngleCrankStart.Y = System.Math.Floor (this.Start.Y);
                                }
                                break;
                                case SubtopicLayout.OrgChart: {
                                        roundedAngleCrankStart.X = System.Math.Floor (this.Start.X);
                                        roundedAngleCrankStart.Y = System.Math.Floor (this.Start.Y + this.ConnectionVector.Dy / 8 + 0.5);
                                }
                                break;
                                case SubtopicLayout.Tree: {
                                        roundedAngleCrankStart.X = System.Math.Floor (this.Start.X + this.ConnectionVector.Dx / 8 + 0.5);
                                        roundedAngleCrankStart.Y = System.Math.Floor (this.Start.Y);
                                }
                                break;
                                }
                                return roundedAngleCrankStart;
                        }
                }

                public Cairo.PointD RoundedAngleCrankEnd
                {
                        get
                        {
                                switch (this.Topic.Parent.Style.SubLayout) {
                                case SubtopicLayout.Map: {
                                        roundedAngleCrankEnd.X = System.Math.Floor (this.End.X - this.ConnectionVector.Dx / 8 + 0.5);
                                        roundedAngleCrankEnd.Y = System.Math.Floor (this.End.Y - this.ConnectionVector.Dy / 4 + 0.5);
                                }
                                break;
                                case SubtopicLayout.OrgChart: {
                                        roundedAngleCrankEnd.X = System.Math.Floor (this.End.X - this.ConnectionVector.Dx / 4 + 0.5);
                                        roundedAngleCrankEnd.Y = System.Math.Floor (this.End.Y - this.ConnectionVector.Dy / 8 + 0.5);
                                }
                                break;
                                case SubtopicLayout.Tree: {
                                        roundedAngleCrankEnd.X = System.Math.Floor (this.End.X - this.ConnectionVector.Dx / 8 + 0.5);
                                        roundedAngleCrankEnd.Y = System.Math.Floor (this.End.Y - this.ConnectionVector.Dy / 4 + 0.5);
                                }
                                break;
                                }
                                return roundedAngleCrankEnd;
                        }
                }

                public void Sketch (Cairo.Context iContext)
                {
                        if (this.Topic.Parent == null)
                                return;
                        context = iContext;
                        fillGap ();
                        switch (this.shape) {
                        case ConnectionShape.Straight:
                        sketchStraight ();
                        break;
                        case ConnectionShape.Crank:
                        sketchCrank ();
                        break;
                        case ConnectionShape.AngleCrank:
                        sketchAngleCrank ();
                        break;
                        case ConnectionShape.RoundedAngleCrank:
                        sketchRoundedAngleCrank ();
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

                private void fillGap ()
                {
                        if (this.shape != ConnectionShape.None) {
                                if (mapExtenstion || orgChartExtension) {
                                        context.MoveTo (extendedStart);
                                        context.LineTo (Start);
                                }
                                else
                                        context.MoveTo (Start);
                        }
                }

                private void sketchCurve ()
                {
                        context.CurveTo (CurveControlStart, CurveControlEnd, End);
                }

                private void sketchArc ()
                {
                        context.CurveTo (ArcControlStart, ArcControlEnd, End);
                }

                private void sketchRoundedCrank ()
                {
                        switch (this.Topic.Parent.Style.SubLayout) {
                        case SubtopicLayout.Map: {
                                if (this.ConnectionVector.Dx * this.ConnectionVector.Dy > 0) {
                                        if (this.ConnectionVector.Dx > 0) {
                                                context.Arc (RoundedCrankCenterStart.X, RoundedCrankCenterStart.Y, this.CrankRadius, angle4, angle1);
                                                context.ArcNegative (RoundedCrankCenterEnd.X, RoundedCrankCenterEnd.Y, this.CrankRadius, angle3, angle2);
                                        }
                                        else {
                                                context.Arc (RoundedCrankCenterStart.X, RoundedCrankCenterStart.Y, this.CrankRadius, angle2, angle3);
                                                context.ArcNegative (RoundedCrankCenterEnd.X, RoundedCrankCenterEnd.Y, this.CrankRadius, angle1, angle4);
                                        }
                                }
                                else {
                                        if (this.ConnectionVector.Dx > 0) {
                                                context.ArcNegative (RoundedCrankCenterStart.X, RoundedCrankCenterStart.Y, this.CrankRadius, angle2, angle1);
                                                context.Arc (RoundedCrankCenterEnd.X, RoundedCrankCenterEnd.Y, this.CrankRadius, angle3, angle4);
                                        }
                                        else {
                                                context.ArcNegative (RoundedCrankCenterStart.X, RoundedCrankCenterStart.Y, this.CrankRadius, angle4, angle3);
                                                context.Arc (RoundedCrankCenterEnd.X, RoundedCrankCenterEnd.Y, this.CrankRadius, angle1, angle2);
                                        }
                                }
                        }
                        break;
                        case SubtopicLayout.OrgChart: {
                                if (this.ConnectionVector.Dx * this.ConnectionVector.Dy > 0) {
                                        if (this.ConnectionVector.Dx > 0) {
                                                context.ArcNegative (RoundedCrankCenterStart.X, RoundedCrankCenterStart.Y, this.CrankRadius, angle3, angle2);
                                                context.Arc (RoundedCrankCenterEnd.X, RoundedCrankCenterEnd.Y, this.CrankRadius, angle4, angle1);
                                        }
                                        else {
                                                context.ArcNegative (RoundedCrankCenterStart.X, RoundedCrankCenterStart.Y, this.CrankRadius, angle1, angle4);
                                                context.Arc (RoundedCrankCenterEnd.X, RoundedCrankCenterEnd.Y, this.CrankRadius, angle2, angle3);
                                        }
                                }
                                else {
                                        if (this.ConnectionVector.Dx > 0) {
                                                context.Arc (RoundedCrankCenterStart.X, RoundedCrankCenterStart.Y, this.CrankRadius, angle3, angle4);
                                                context.ArcNegative (RoundedCrankCenterEnd.X, RoundedCrankCenterEnd.Y, this.CrankRadius, angle2, angle1);
                                        }
                                        else {
                                                context.Arc (RoundedCrankCenterStart.X, RoundedCrankCenterStart.Y, this.CrankRadius, angle1, angle2);
                                                context.ArcNegative (RoundedCrankCenterEnd.X, RoundedCrankCenterEnd.Y, this.CrankRadius, angle4, angle3);
                                        }
                                }
                        }
                        break;
                        case SubtopicLayout.Tree: {
                                if (this.ConnectionVector.Dx * this.ConnectionVector.Dy > 0) {
                                        if (this.ConnectionVector.Dx > 0) {
                                                context.Arc (RoundedCrankCenterStart.X, RoundedCrankCenterStart.Y, this.CrankRadius, angle4, angle1);
                                        }
                                        else {
                                                context.Arc (RoundedCrankCenterStart.X, RoundedCrankCenterStart.Y, this.CrankRadius, angle2, angle3);
                                        }
                                }
                                else {
                                        if (this.ConnectionVector.Dx > 0) {
                                                context.ArcNegative (RoundedCrankCenterStart.X, RoundedCrankCenterStart.Y, this.CrankRadius, angle2, angle1);
                                        }
                                        else {
                                                context.ArcNegative (RoundedCrankCenterStart.X, RoundedCrankCenterStart.Y, this.CrankRadius, angle4, angle3);
                                        }
                                }
                        }
                        break;
                        }

                        context.LineTo (End);
                }

                private void sketchChamferedCrank ()
                {
                        context.LineTo (ChamferedCrankStart1);
                        context.LineTo (ChamferedCrankStart2);
                        if (this.Topic.Parent.Style.SubLayout != SubtopicLayout.Tree) {
                                context.LineTo (ChamferedCrankEnd2);
                                context.LineTo (ChamferedCrankEnd1);
                        }
                        context.LineTo (End);
                }

                private void sketchCrank ()
                {
                        if (this.Topic.Parent.Style.SubLayout == SubtopicLayout.Tree)
                                context.LineTo (RootCenter);
                        else {
                                context.LineTo (MiddleStart);
                                context.LineTo (MiddleEnd);
                        }
                        context.LineTo (End);
                }

                private void sketchStraight ()
                {
                        context.LineTo (End);
                }

                private void sketchAngleCrank ()
                {
                        context.LineTo (MiddleStart);
                        context.LineTo (End);
                }

                private void sketchRoundedAngleCrank ()
                {
                        context.LineTo (RoundedAngleCrankStart);
                        context.CurveTo (MiddleStart, MiddleStart, RoundedAngleCrankEnd);
                        context.LineTo (End);
                }
        }
}