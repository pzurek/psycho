
using System;
using Gtk;

namespace Psycho
{
	[Gtk.TreeNode]
	public class PsychoTreeNode : TreeNode
	{
		private string guid;
		private string title;
		
		public PsychoTreeNode(string paramTitle, string paramGuid) : base()
		{
			this.title = paramTitle;
			this.guid = paramGuid;
  		}

        [Gtk.TreeNodeValue (Column=0)]
        public string Title
        {
        	get { return title; }
        }

        [Gtk.TreeNodeValue (Column=1)]
        public string GUID
        {
        	get { return guid; }
        }

    }
}
