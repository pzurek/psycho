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
using System.Collections;
using System.Collections.Generic;
using Gtk;
using Gdk;

namespace Psycho {
    ///<summary>
    ///Temporary view.
    ///Buttons creating and deleting topics
    /// and a nodeview to show them and select the current.
    ///</summary>
    public class ButtonView : VBox, IView {
        
        Entry titleEntry = new Entry();
        Button addSiblingButton = new Button();
        Button addChildButton = new Button();
        Button deleteButton = new Button();

        private IModel Model;
        private IControl Control;


        public ButtonView() : base() {

            this.Homogeneous = false;
            this.BorderWidth = 6;

            HButtonBox buttonBox = new HButtonBox();

            titleEntry.KeyReleaseEvent += new KeyReleaseEventHandler (titleEntry_KeyReleaseEvent);

            addSiblingButton.Label = ("Add Sibling");
            addSiblingButton.Clicked += new EventHandler(btnAddSibling_Click);
            
            addChildButton.Label = ("Add Child");
            addChildButton.Clicked += new EventHandler(btnAddChild_Click);
            
            deleteButton.Label = ("Delete");
            deleteButton.Clicked += new EventHandler(btnDelete_Click);

            buttonBox.Homogeneous = true;
            buttonBox.Layout = (Gtk.ButtonBoxStyle.End);
            buttonBox.Spacing = 6;
            buttonBox.PackStart(addSiblingButton, false, true, 6);
            buttonBox.PackStart(addChildButton, false, true, 6);
            buttonBox.PackStart(deleteButton, false, true, 6);

            this.PackStart(titleEntry, false, false, 6);
            this.PackStart(buttonBox, false, false, 6);
        }

        void titleEntry_KeyReleaseEvent (object o, KeyReleaseEventArgs args)
        {
            string key = args.Event.Key.ToString();
            if (args.Event.Key == Gdk.Key.Return)
            EditTitle(titleEntry.Text);
            Console.WriteLine("Title edited: " + titleEntry.Text);
        }

        public void WireUp(IControl paramControl, IModel paramModel)
        {
            if (Model != null) {
                Model.RemoveObserver(this);
            }

            Model = paramModel;
            Control = paramControl;

            Control.SetModel(Model);
            Control.SetView(this);
            Model.AddObserver(this);
            Update(Model);
        }

        public void CheckButtonsLegal ()
        {
            if (Model.CurrentTopic == Model.CentralTopic) {
                DisableAddSibling();
                DisableDelete();
            }
            else {
                EnableAddSibling();
                EnableDelete();
            }
        }

        public void EditTitle(string paramString)
        {
            Control.RequestSetTitle(paramString);
        }

        private void btnAddChild_Click(object sender, System.EventArgs args)
        {
            Console.WriteLine("Add Child Button clicked");
            AddSubtopic();
        }

        public void AddSubtopic()
        {
            Control.RequestAddSubtopic();
        }

        private void btnAddSibling_Click(object sender, System.EventArgs args)
        {
            Console.WriteLine("Add Sibling Button clicked");
            AddTopic();
        }

        public void AddTopic()
        {
            Control.RequestAddTopic();
        }

        private void btnDelete_Click(object sender, System.EventArgs args)
        {
            Console.WriteLine("Delete Button clicked");
            DeleteTopic();
        }

        public void DeleteTopic()
        {
            Control.RequestDelete();
        }

        public void SetCurrentTopic()
        {
        }

        public void Update(IModel paramModel)
        {
            titleEntry.Text = paramModel.CurrentTopic.Title;
            CheckButtonsLegal();
        }

        public void DisableAddSibling()
        {
            addSiblingButton.Sensitive = (false);
        }

        public void EnableAddSibling()
        {
            addSiblingButton.Sensitive = (true);
        }

        public void DisableDelete()
        {
            deleteButton.Sensitive = (false);
        }

        public void EnableDelete()
        {
            deleteButton.Sensitive = (true);
        }

        public void ExpandTopic(string paramGuid, bool isExpanded)
        {
            Control.RequestExpand(paramGuid, isExpanded);
        }
    }
}
