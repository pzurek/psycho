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
using Gdk;

namespace Psycho {

    public class IconLoader {

        const string psychoIconFile = "Resources/psycho-small.png";
        const string topicIconFile = "Resources/psycho-topic.png";
        const string subtopicIconFile = "Resources/psycho-subtopic.png";
        const string deleteIconFile = "Resources/psycho-delete.png";
        const string calloutIconFile = "Resources/psycho-callout.png";
        const string borderIconFile = "Resources/psycho-border.png";
        const string relationIconFile = "Resources/psycho-relation.png";
        const string notesIconFile = "Resources/psycho-notes-small.png";

        public Pixbuf psychoIcon;
        public Pixbuf topicIcon;
        public Pixbuf subtopicIcon;
        public Pixbuf deleteIcon;
        public Pixbuf calloutIcon;
        public Pixbuf borderIcon;
        public Pixbuf relationIcon;
        public Pixbuf notesIcon;

        public IconLoader ()
        {
            psychoIcon = new Pixbuf (psychoIconFile);
            topicIcon = new Pixbuf (topicIconFile);
            subtopicIcon = new Pixbuf (subtopicIconFile);
            deleteIcon = new Pixbuf (deleteIconFile);
            calloutIcon = new Pixbuf (calloutIconFile);
            borderIcon = new Pixbuf (borderIconFile);
            relationIcon = new Pixbuf (relationIconFile);
            notesIcon = new Pixbuf (notesIconFile);
        }
    }
}
