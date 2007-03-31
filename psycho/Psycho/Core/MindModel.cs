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
using System.Text;

namespace Psycho
{
    /// <summary>
    /// Main data container for mind map model.
    /// </summary>
    public partial class MindModel
    {
    	public MindModel()
		{
    		Console.WriteLine("Creating new Mind Model");
            this.currentTopic = this.centralTopic;
            centralTopic.Title = "Central Topic";
            NotifyObservers();
		}

        public int levelCounter;

        public void AppendSomeNodes(Topic paramTopic)
        {
            while (paramTopic.Subtopics.Count < 4){
                Topic newTopic = new Topic(this.centralTopic.TotalCount);
                newTopic.Parent = paramTopic;
                paramTopic.AddSubtopic(newTopic);
            }

            while (levelCounter < 2) {
                foreach (Topic topic in paramTopic.Subtopics) {
                    levelCounter++;
                    AppendSomeNodes(topic);
                }
            }
        }
    }
}
