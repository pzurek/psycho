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
                }

                void notesView_FocusOutEvent (object sender, FocusOutEventArgs args)
                {
                        editPending = false;
                        Update (Model);
                }

                void notesBuffer_Changed (object sender, EventArgs args)
                {
                        if (notesView.HasFocus) {
                                if (workingTopic.TopicNotes == null)
                                        workingTopic.TopicNotes = new Notes (workingTopic);
                                workingTopic.TopicNotes.Text = notesBuffer.Text;
                                CommitChange (workingTopic);
                        }

                }

                public void Update (IModel paramModel)
                {
                        if (editPending == false) {
                                workingTopic = paramModel.CurrentTopic;
                                if (workingTopic.TopicNotes != null)
                                        notesBuffer.Text = workingTopic.TopicNotes.Text;
                                else
                                        notesBuffer.Clear ();
                        }
                }

                public void WireUp (IControl paramControl, IModel paramModel)
                {
                        if (Model != null)
                                Model.RemoveObserver (this);

                        Model = paramModel;
                        Control = paramControl;

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

                public void ExpandTopic (string paramGuid, bool isExpanded)
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

                public void CommitChange (Topic paramTopic)
                {
                        Control.RequestChange (paramTopic);
                }
        }
}