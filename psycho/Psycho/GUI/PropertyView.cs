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
using System.Collections;
using System.Collections.Generic;
using Gtk;
using Gdk;

namespace Psycho
{
        public class PropertyView : VBox, IView
        {
                Entry titleEntry;
                Button addSiblingButton;
                Button addChildButton;
                Button deleteButton;
                FontButton fontButton;
                ColorButton fontColor;
                ColorButton lineColor;
                ColorButton fillColor;

                IModel Model;
                IControl Control;

                public PropertyView ()
                        : base ()
                {

                        this.Homogeneous = false;
                        this.BorderWidth = 6;

                        HButtonBox buttonBox = new HButtonBox ();
                        HButtonBox basicPropertyBox = new HButtonBox ();

                        titleEntry = new Entry ();
                        titleEntry.KeyReleaseEvent += new KeyReleaseEventHandler (titleEntry_KeyReleaseEvent);

                        addSiblingButton = new Button ();
                        addSiblingButton.Label = ("Add Sibling");
                        addSiblingButton.Clicked += new EventHandler (btnAddSibling_Click);

                        addChildButton = new Button ();
                        addChildButton.Label = ("Add Child");
                        addChildButton.Clicked += new EventHandler (btnAddChild_Click);

                        deleteButton = new Button ();
                        deleteButton.Label = ("Delete");
                        deleteButton.Clicked += new EventHandler (btnDelete_Click);

                        fontButton = new FontButton ();
                        fontButton.SetFontName ("Tahoma 10");

                        fontColor = new ColorButton ();
                        fontColor.Label = "Font color";
                        lineColor = new ColorButton ();
                        lineColor.Label = "Line color";
                        fillColor = new ColorButton ();
                        fillColor.Label = "Fill color";

                        buttonBox.Homogeneous = true;
                        buttonBox.Layout = (Gtk.ButtonBoxStyle.Start);
                        buttonBox.Spacing = 6;
                        buttonBox.PackStart (addSiblingButton, false, true, 6);
                        buttonBox.PackStart (addChildButton, false, true, 6);
                        buttonBox.PackStart (deleteButton, false, true, 6);

                        basicPropertyBox.Homogeneous = false;
                        basicPropertyBox.Layout = ButtonBoxStyle.Start;
                        basicPropertyBox.Spacing = 6;
                        basicPropertyBox.PackStart (fontButton, false, false, 6);
                        basicPropertyBox.PackStart (fontColor, false, false, 6);
                        basicPropertyBox.PackStart (lineColor, false, false, 6);
                        basicPropertyBox.PackStart (fillColor, false, false, 6);

                        this.PackStart (titleEntry, false, false, 6);
                        this.PackStart (buttonBox, false, false, 6);
                        this.PackStart (basicPropertyBox, false, false, 6);
                }

                void titleEntry_KeyReleaseEvent (object o, KeyReleaseEventArgs args)
                {
                        string key = args.Event.Key.ToString ();
                        if (args.Event.Key == Gdk.Key.Return)
                                EditTitle (titleEntry.Text);
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

                public void CheckButtonsLegal ()
                {
                        if (Model.CurrentTopic == Model.CentralTopic) {
                                DisableAddSibling ();
                                DisableDelete ();
                        }
                        else {
                                EnableAddSibling ();
                                EnableDelete ();
                        }
                }

                public void EditTitle (string iString)
                {
                        Control.RequestSetTitle (iString);
                }

                void btnAddChild_Click (object sender, System.EventArgs args)
                {
                        AddSubtopic ();
                }

                public void AddSubtopic ()
                {
                        Control.RequestAddSubtopic ();
                }

                void btnAddSibling_Click (object sender, System.EventArgs args)
                {
                        AddTopic ();
                }

                public void AddTopic ()
                {
                        Control.RequestAddTopic ();
                }

                void btnDelete_Click (object sender, System.EventArgs args)
                {
                        DeleteTopic ();
                }

                public void DeleteTopic ()
                {
                        Control.RequestDelete ();
                }

                public void SetCurrentTopic ()
                {
                }

                public void TriggerEdit (bool editPending)
                {
                        Control.RequestEditFlag (editPending);
                }

                public void Update (IModel iModel)
                {
                        titleEntry.Text = iModel.CurrentTopic.Text;
                        CheckButtonsLegal ();
                }

                public void DisableAddSibling ()
                {
                        addSiblingButton.Sensitive = (false);
                }

                public void EnableAddSibling ()
                {
                        addSiblingButton.Sensitive = (true);
                }

                public void DisableDelete ()
                {
                        deleteButton.Sensitive = (false);
                }

                public void EnableDelete ()
                {
                        deleteButton.Sensitive = (true);
                }

                public void ExpandTopic (string iGuid, bool isExpanded)
                {
                        Control.RequestExpand (iGuid, isExpanded);
                }

                public void CommitChange (Topic iTopic)
                {
                        throw new Exception ("The method or operation is not implemented.");
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
        }
}