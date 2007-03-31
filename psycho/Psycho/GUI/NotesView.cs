using System;
using System.Collections.Generic;
using System.Text;
using Gtk;

namespace Psycho {
    class NotesView : ScrolledWindow, IView {

        private IModel Model;
        private IControl Control;

        private Topic workingTopic;
        private TextView notesView;
        private TextBuffer notesBuffer;
        private TextTagTable notesTagTable;
        private bool editPending;

        public NotesView ()
        {
            notesView = new TextView ();
            notesView.WrapMode = WrapMode.Word;
            notesBuffer = notesView.Buffer;
            notesTagTable = notesBuffer.TagTable;
            editPending = false;
            notesBuffer.Changed += new EventHandler (notesBuffer_Changed);

            this.ShadowType = ShadowType.EtchedIn;
            notesView.RedrawOnAllocate = true;
            this.Add (notesView);
        }

        void notesBuffer_Changed (object sender, EventArgs args)
        {
            editPending = true;
            if (workingTopic.TopicNotes == null)
                workingTopic.TopicNotes = new Notes (workingTopic);
            workingTopic.TopicNotes.Text = notesBuffer.Text;
            CommitChange (workingTopic);
            editPending = false;
        }

        #region IView Members

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
            if (Model != null) {
                Model.RemoveObserver (this);
            }

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
            Control.RequestChange(paramTopic);
        }

        #endregion
    }
}
