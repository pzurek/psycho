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
using System.Collections.Generic;
using System.Text;

namespace Psycho
{
        class MindControl : IControl
        {
                IModel Model;
                IView View;

                public MindControl (IModel iModel, IView iView)
                {
                        Model = iModel;
                        View = iView;
                }

                public void SetModel (IModel iModel)
                {
                        Model = iModel;
                }

                public void SetView (IView iView)
                {
                        View = iView;
                }

                public void RequestSetCurrent (string iGuid)
                {
                        if (Model != null)
                                Model.SetCurrent (iGuid);
                }

                public void RequestCurrentForward ()
                {
                        if (Model != null)
                                Model.CurrentGoForward ();
                }

                public void RequestCurrentBack ()
                {
                        if (Model != null)
                                Model.CurrentGoBack ();
                }


                public void RequestCurrentUp ()
                {
                        if (Model != null)
                                Model.CurrentGoUp ();
                }

                public void RequestCurrentDown ()
                {
                        if (Model != null)
                                Model.CurrentGoDown ();
                }

                public void RequestAddSubtopic ()
                {
                        if (Model != null)
                                Model.CreateSubtopic ();
                }

                public void RequestAddTopic ()
                {
                        if (Model != null)
                                Model.CreateTopic ();
                }

                public void RequestDelete ()
                {
                        if (Model != null)
                                Model.DeleteTopic ();
                }

                public void RequestSetTitle (string iTitle)
                {
                        if (Model != null)
                                Model.SetTitle (iTitle);
                }

                public void RequestExpand (string iGuid, bool isExpanded)
                {
                        if (Model != null)
                                Model.ExpandTopic (iGuid, isExpanded);
                }

                public void RequestEditFlag (bool editPending)
                {
                        if (Model != null)
                                Model.EditPending = editPending;
                }

                public void RequestChange (Topic iTopic)
                {
                        if (Model != null)
                                Model.ChangeTopic (iTopic);
                }

                public void RequestSetCurrentByCoords (int iX, int iY)
                {
                        if (Model != null)
                                Model.SetCurrentByCoords (iX, iY);
                }

                public void RequestClearCurrent ()
                {
                        if (Model != null)
                                Model.ClearCurrent ();
                }

                #region IControl Members


                public void RequestSetStyle (TopicStyle iStyle)
                {
                        throw new Exception ("The method or operation is not implemented.");
                }

                #endregion
        }
}
