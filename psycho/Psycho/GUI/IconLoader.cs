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
using Gdk;

namespace Psycho
{
        static class IconLoader
        {
                const string resourceDirectory = "Resources";
                const string psychoIconFile = "psycho-small.png";
                const string topicIconFile = "psycho-topic.png";
                const string subtopicIconFile = "psycho-subtopic.png";
                const string deleteIconFile = "psycho-delete.png";
                const string calloutIconFile = "psycho-callout.png";
                const string borderIconFile = "psycho-border.png";
                const string relationIconFile = "psycho-relation.png";
                const string notesIconFile = "psycho-notes-small.png";
                //const string paperFile = "paper.png";
                //const string paperFile = "paper_12.png";
                const string paperFile = "paper_6.png";
                //const string paperFile = "blue_tile.png";
                //const string paperFile = "checker.png";

                static string psychoIconPath = System.IO.Path.Combine (resourceDirectory, psychoIconFile);
                static string topicIconPath = System.IO.Path.Combine (resourceDirectory, topicIconFile);
                static string subtopicIconPath = System.IO.Path.Combine (resourceDirectory, subtopicIconFile);
                static string deleteIconPath = System.IO.Path.Combine (resourceDirectory, deleteIconFile);
                static string calloutIconPath = System.IO.Path.Combine (resourceDirectory, calloutIconFile);
                static string borderIconPath = System.IO.Path.Combine (resourceDirectory, borderIconFile);
                static string relationIconPath = System.IO.Path.Combine (resourceDirectory, relationIconFile);
                static string notesIconPath = System.IO.Path.Combine (resourceDirectory, notesIconFile);
                public static string paperPath = System.IO.Path.Combine (resourceDirectory, paperFile);

                public static Pixbuf psychoIcon;
                public static Pixbuf topicIcon;
                public static Pixbuf subtopicIcon;
                public static Pixbuf deleteIcon;
                public static Pixbuf calloutIcon;
                public static Pixbuf borderIcon;
                public static Pixbuf relationIcon;
                public static Pixbuf notesIcon;
                public static Pixbuf paper;

                static IconLoader ()
                {
                        psychoIcon = new Pixbuf (psychoIconPath);
                        topicIcon = new Pixbuf (topicIconPath);
                        subtopicIcon = new Pixbuf (subtopicIconPath);
                        deleteIcon = new Pixbuf (deleteIconPath);
                        calloutIcon = new Pixbuf (calloutIconPath);
                        borderIcon = new Pixbuf (borderIconPath);
                        relationIcon = new Pixbuf (relationIconPath);
                        notesIcon = new Pixbuf (notesIconPath);
                        paper = new Pixbuf (paperPath);
                }
        }
}