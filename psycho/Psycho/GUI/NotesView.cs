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
using Gtk;

namespace Psycho
{
        class NotesView : ScrolledWindow, IView
        {
                IModel Model;
                IControl Control;

                Topic workingTopic;
                TextView notesView;
                TextBuffer notesBuffer;
                TextTagTable notesTagTable;
                bool editPending;

                public NotesView ()
                {
                        notesView = new TextView ();
                        notesView.WrapMode = WrapMode.Word;
                        notesBuffer = notesView.Buffer;
                        notesTagTable = notesBuffer.TagTable;
                        notesView.AcceptsTab = false;
                        notesBuffer.Changed += new EventHandler (notesBuffer_Changed);
                        notesView.FocusOutEvent += new FocusOutEventHandler (notesView_FocusOutEvent);
                        notesView.FocusInEvent += new FocusInEventHandler (notesView_FocusInEvent);

                        this.ShadowType = ShadowType.EtchedIn;
                        notesView.RedrawOnAllocate = true;
                        this.Add (notesView);
                        editPending = false;
                }

                void notesView_FocusInEvent (object sender, FocusInEventArgs args)
                {
                        editPending = true;
                        Console.WriteLine ("Notes focused");
                }

                void notesView_FocusOutEvent (object sender, FocusOutEventArgs args)
                {
                        editPending = false;
                        Console.WriteLine ("Notes focus lost");
                        Update (Model);
                }

                void notesBuffer_Changed (object sender, EventArgs args)
                {
                        if (notesView.HasFocus) {
                                if (workingTopic.Note == null)
                                        workingTopic.Note = new TopicNote (workingTopic);
                                workingTopic.Note.Text = notesBuffer.Text;
                                CommitChange (workingTopic);
                        }

                }

                public void Update (IModel iModel)
                {
                        if (editPending == false) {
                                workingTopic = iModel.CurrentTopic;
                                if (workingTopic.Note != null && workingTopic.Note.Text != "")
                                        notesBuffer.Text = workingTopic.Note.Text;
                                else
                                        notesBuffer.Clear ();
                        }
                }

                public void WireUp (IControl iControl, IModel iModel)
                {
                        if (Model != null)
                                Model.RemoveObserver (this);

                        Model = iModel;
                        Control = iControl;

                        Control.SetModel (Model);
                        Control.SetView (this);
                        Model.AddObserver (this);
                        Update (Model);
                }

                public void AddTopic ()
                {
                        throw new Exception ("The method is not implemented for Notes View.");
                }

                public void AddSubtopic ()
                {
                        throw new Exception ("The method or operation is not implemented.");
                }

                public void DeleteTopic ()
                {
                        throw new Exception ("The method or operation is not implemented.");
                }

                public void ExpandTopic (string iGuid, bool isExpanded)
                {
                        throw new Exception ("The method or operation is not implemented.");
                }

                public void EditTitle (string Title)
                {
                        throw new Exception ("The method or operation is not implemented.");
                }

                public void SetCurrentTopic ()
                {
                        throw new Exception ("The method or operation is not implemented.");
                }

                public void TriggerEdit (bool editPending)
                {
                        throw new Exception ("The method or operation is not implemented.");
                }

                public void DisableAddSibling ()
                {
                        throw new Exception ("The method or operation is not implemented.");
                }

                public void DisableDelete ()
                {
                        throw new Exception ("The method or operation is not implemented.");
                }

                public void EnableAddSibling ()
                {
                        throw new Exception ("The method or operation is not implemented.");
                }

                public void EnableDelete ()
                {
                        throw new Exception ("The method or operation is not implemented.");
                }

                public void CommitChange (Topic iTopic)
                {
                        Control.RequestChange (iTopic);
                }

                public void SetCurrentForward ()
                {
                        throw new Exception ("The method or operation is not implemented.");
                }

                public void SetCurrentBack ()
                {
                        throw new Exception ("The method or operation is not implemented.");
                }

                public void SetCurrentUp ()
                {
                        throw new Exception ("The method or operation is not implemented.");
                }

                public void SetCurrentDown ()
                {
                        throw new Exception ("The method or operation is not implemented.");
                }

                #region IView Members


                public void SetCurrentByCoords (int iX, int iY)
                {
                        throw new Exception ("The method or operation is not implemented.");
                }

                #endregion

                #region IView Members


                public void ClearCurrentTopic ()
                {
                        throw new Exception ("The method or operation is not implemented.");
                }

                #endregion
        }
}