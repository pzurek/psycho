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
        public class OutlineView : ScrolledWindow, IView
        {
                IModel Model;
                IControl Control;

                TreeStore store = new TreeStore (typeof (Topic));
                TreeView outlineView = new TreeView ();

                TreeIter selectedNode;
                Topic selectedTopic;
                Topic workingTopic;

                bool editPending;
                string deletedTopicPath;
                bool updatePending;

                public string DeletedTopicPath
                {
                        get { return deletedTopicPath; }
                        set { deletedTopicPath = value; }
                }

                TreeViewColumn pathColumn = new TreeViewColumn ();
                TreeViewColumn titleColumn = new TreeViewColumn ();
                TreeViewColumn levelColumn = new TreeViewColumn ();
                TreeViewColumn guidColumn = new TreeViewColumn ();
                TreeViewColumn notesColumn = new TreeViewColumn ();

                public OutlineView ()
                        : base ()
                {
                        titleColumn.Title = "Topic title";
                        CellRendererText titleCell = new CellRendererText ();
                        titleCell.Editable = true;
                        titleCell.Edited += new EditedHandler (titleCell_Edited);
                        titleCell.EditingStarted += new EditingStartedHandler (titleCell_EditingStarted);
                        titleCell.EditingCanceled += new EventHandler (titleCell_EditingCanceled);
                        titleCell.Mode = CellRendererMode.Editable;
                        titleColumn.PackStart (titleCell, true);
                        titleColumn.AddAttribute (titleCell, "text", 0);
                        titleColumn.SetCellDataFunc (titleCell, new Gtk.TreeCellDataFunc (RenderTitle));

                        //guidColumn.Title = "Topic GUID";
                        //guidColumn.Clickable = false;
                        //CellRendererText guidCell = new CellRendererText ();
                        //guidCell.Mode = CellRendererMode.Inert;
                        //guidColumn.PackStart (guidCell, false);
                        //guidColumn.AddAttribute (guidCell, "text", 0);
                        //guidColumn.SetCellDataFunc (guidCell, new Gtk.TreeCellDataFunc (RenderGuid));

                        //pathColumn.Title = "Topic path";
                        //CellRendererText pathCell = new CellRendererText ();
                        //pathCell.Mode = CellRendererMode.Inert;
                        //pathColumn.PackStart (pathCell, false);
                        //pathColumn.AddAttribute (pathCell, "text", 3);
                        //pathColumn.SetCellDataFunc (pathCell, new Gtk.TreeCellDataFunc (RenderPath));

                        //levelColumn.Title = "Topic level";
                        //CellRendererText levelCell = new CellRendererText ();
                        //levelCell.Mode = CellRendererMode.Inert;
                        //levelColumn.PackStart (levelCell, false);
                        //levelColumn.AddAttribute (levelCell, "text", 2);
                        //levelColumn.SetCellDataFunc (levelCell, new Gtk.TreeCellDataFunc (RenderLevel));

                        notesColumn.Title = "Notes";
                        CellRendererPixbuf notesCell = new CellRendererPixbuf ();
                        notesCell.Mode = CellRendererMode.Inert;
                        notesColumn.PackStart (notesCell, false);
                        notesColumn.AddAttribute (notesCell, "pixbuf", 1);
                        notesColumn.SetCellDataFunc (notesCell, new Gtk.TreeCellDataFunc (RenderNotes));

                        outlineView.Model = store;
                        //outlineView.AppendColumn (pathColumn);
                        outlineView.AppendColumn (titleColumn);
                        //outlineView.AppendColumn (levelColumn);
                        outlineView.AppendColumn (notesColumn);
                        outlineView.ExpanderColumn = titleColumn;

                        outlineView.Selection.Changed += new System.EventHandler (OnSelectionChanged);
                        outlineView.RowCollapsed += new RowCollapsedHandler (outlineView_RowCollapsed);
                        outlineView.RowExpanded += new RowExpandedHandler (outlineView_RowExpanded);
                        outlineView.KeyReleaseEvent += new KeyReleaseEventHandler (outlineView_KeyReleaseEvent);
                        outlineView.FocusInEvent += new FocusInEventHandler (outlineView_FocusInEvent);
                        outlineView.FocusOutEvent += new FocusOutEventHandler (outlineView_FocusOutEvent);

                        outlineView.ExpanderColumn.Expand = true;
                        outlineView.RulesHint = true;
                        outlineView.CanFocus = true;
                        this.VscrollbarPolicy = PolicyType.Always;
                        Add (outlineView);
                        ShowAll ();
                }

                void outlineView_FocusInEvent (object o, FocusInEventArgs args)
                {
                        editPending = true;
                        Console.WriteLine ("Outline focused");
                }

                void outlineView_FocusOutEvent (object o, FocusOutEventArgs args)
                {
                        editPending = false;
                        Console.WriteLine ("Outline focus lost");
                        Update (Model);
                }

                void titleCell_EditingCanceled (object sender, EventArgs args)
                {
                        //editPending = false;
                        //TriggerEdit (editPending);
                }

                void titleCell_EditingStarted (object sender, EditingStartedArgs args)
                {
                        //editPending = true;
                        //TriggerEdit (editPending);
                }

                void outlineView_KeyReleaseEvent (object sender, KeyReleaseEventArgs args)
                {
                        //string key = args.Event.Key.ToString ();
                        //Console.WriteLine (key);
                        //switch (key) {
                        //case "Return":
                        //if (isEdited) return;
                        //else {
                        //    AddTopic();
                        //    return;
                        //}
                        //case "Insert":
                        //AddSubtopic();
                        //return;
                        //case "Delete":
                        //DeleteTopic();
                        //return;
                        //default: break;
                        //}
                }

                void RenderTitle (TreeViewColumn column, CellRenderer cell, TreeModel model, TreeIter iter)
                {
                        Topic topic = (Topic) model.GetValue (iter, 0);
                        (cell as CellRendererText).Text = topic.Text;
                }

                //void RenderGuid (TreeViewColumn column, CellRenderer cell, TreeModel model, TreeIter iter)
                //{
                //        Topic topic = (Topic) model.GetValue (iter, 0);
                //        (cell as CellRendererText).Text = topic.GUID;
                //}

                //void RenderPath (TreeViewColumn column, CellRenderer cell, TreeModel model, TreeIter iter)
                //{
                //        Topic topic = (Topic) model.GetValue (iter, 0);
                //        (cell as CellRendererText).Text = topic.Path;
                //}

                //void RenderLevel (TreeViewColumn column, CellRenderer cell, TreeModel model, TreeIter iter)
                //{
                //        Topic topic = (Topic) model.GetValue (iter, 0);
                //        (cell as CellRendererText).Text = topic.Level.ToString ();
                //}

                void RenderNotes (TreeViewColumn column, CellRenderer cell, TreeModel model, TreeIter iter)
                {
                        Topic topic = (Topic) model.GetValue (iter, 0);

                        if (topic.HasNotes)
                                (cell as CellRendererPixbuf).Pixbuf = IconLoader.notesIcon;
                        else
                                (cell as CellRendererPixbuf).Pixbuf = null;
                }

                public void Build (IModel iModel)
                {
                        store.Clear ();
                        TreeIter centralNode = store.AppendValues (iModel.CentralTopic);
                        AddNodesRecursively (store, centralNode, iModel.CentralTopic);
                        outlineView.ExpandAll ();
                        outlineView.ScrollToCell (store.GetPath (selectedNode), titleColumn, true, 0, 0);
                }

                public void Update (IModel iModel)
                {
                        updatePending = true;
                        UpdateNew (iModel);
                        UpdateDeletedPath (iModel);
                        UpdateChanged (iModel);
                        workingTopic = iModel.CurrentTopic;
                        updatePending = false;
                }

                public void UpdateNew (IModel iModel)
                {
                        foreach (Topic topic in iModel.NewTopics) {
                                TreeIter parent;
                                TreePath parentPath = new TreePath (topic.Parent.Path);
                                int position = topic.Parent.Subtopics.IndexOf (topic);
                                store.GetIter (out parent, parentPath);
                                TreeIter iter = store.InsertNode (parent, position);
                                store.SetValue (iter, 0, topic);
                                TreePath path = store.GetPath (iter);
                                outlineView.ExpandToPath (path);
                                outlineView.Selection.SelectIter (iter);
                                outlineView.ScrollToCell (path, null, false, 1, 0);
                                outlineView.SetCursor (path, titleColumn, false);
                                outlineView.QueueDraw ();
                        }
                }

                public void UpdateDeletedPath (IModel iModel)
                {
                        if (iModel.DeletedTopicPath != "")
                                DeletedTopicPath = (iModel.DeletedTopicPath);
                        else
                                return;
                        TreeIter deletedIter;
                        TreePath deletedPath = new TreePath (deletedTopicPath);
                        this.store.GetIter (out deletedIter, deletedPath);
                        this.store.Remove (ref deletedIter);

                        TreePath path = new TreePath (iModel.CurrentTopic.Path);
                        TreeIter iter;
                        store.GetIter (out iter, path);
                        outlineView.Selection.SelectIter (iter);
                        outlineView.ScrollToCell (path, null, false, 1, 0);
                        outlineView.SetCursor (path, titleColumn, false);
                        outlineView.QueueDraw ();
                }

                public void UpdateChanged (IModel iModel)
                {

                        foreach (Topic topic in iModel.ChangedTopics) {
                                TreePath path = new TreePath (topic.Path);
                                TreeIter iter;
                                store.GetIter (out iter, path);
                                store.SetValue (iter, 0, topic);
                                outlineView.Selection.SelectIter (iter);
                                outlineView.ScrollToCell (path, null, false, 1, 0);
                                outlineView.SetCursor (path, titleColumn, false);
                                outlineView.QueueDraw ();
                        }
                }

                public void AddTopic ()
                {
                        Control.RequestAddTopic ();
                }

                public void AddSubtopic ()
                {
                        Control.RequestAddSubtopic ();
                }

                public void DeleteTopic ()
                {
                        Control.RequestDelete ();
                }

                public void ExpandTopic (string iGuid, bool isExpanded)
                {
                        Control.RequestExpand (iGuid, isExpanded);
                }

                public void EditTitle (string Title)
                {
                        Control.RequestSetTitle (Title);
                }

                public void SetCurrentTopic ()
                {
                        Control.RequestSetCurrent (selectedTopic.GUID);
                }

                public void TriggerEdit (bool editPending)
                {
                        Control.RequestEditFlag (editPending);
                }

                public void DisableAddSibling ()
                {
                }

                public void EnableAddSibling ()
                {
                }

                public void DisableDelete ()
                {
                }

                public void EnableDelete ()
                {
                }

                public void WireUp (IControl iControl, IModel iModel)
                {
                        if (Model != null) {
                                Model.RemoveObserver (this);
                        }

                        Model = iModel;
                        Control = iControl;

                        Control.SetModel (Model);
                        Control.SetView (this);
                        Model.AddObserver (this);
                        Build (Model);
                }

                void AddNodesRecursively (TreeStore iStore, TreeIter iParent, Topic iTopic)
                {

                        foreach (Topic child in iTopic.Subtopics) {
                                TreeIter kid = iStore.AppendValues (iParent, child);
                                AddNodesRecursively (iStore, kid, child);
                        }
                }

                void OnSelectionChanged (object sender, System.EventArgs args)
                {
                        TreeModel model;

                        if (((TreeSelection) sender).GetSelected (out model, out selectedNode))
                                selectedTopic = (Topic) model.GetValue (selectedNode, 0);
                        if (!updatePending) SetCurrentTopic ();
                }

                void titleCell_Edited (object sender, Gtk.EditedArgs args)
                {
                        string titleText = args.NewText;
                        EditTitle (titleText);
                }

                void outlineView_RowCollapsed (object sender, Gtk.RowCollapsedArgs args)
                {
                        TreeIter expanded = args.Iter;
                        Topic expandedTopic = (Topic) store.GetValue (expanded, 0);
                        ExpandTopic (expandedTopic.GUID, false);
                }

                void outlineView_RowExpanded (object sender, Gtk.RowExpandedArgs args)
                {
                        TreeIter expanded = args.Iter;
                        Topic expandedTopic = (Topic) store.GetValue (expanded, 0);
                        ExpandTopic (expandedTopic.GUID, true);
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