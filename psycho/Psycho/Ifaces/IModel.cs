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
using System.Xml;

namespace Psycho
{
        public interface IModel
        {
                Topic CurrentTopic { get; set; }
                Topic CentralTopic { get; set; }
                bool EditPending { get; set; }
                Topics NewTopics { get; }
                Topics DeletedTopics { get; }
                string DeletedTopicPath { get; }
                Topics ChangedTopics { get; }
                int CurrentLevel { get; }
                XmlDocument XMLModel { get; }

                void CreateTopic ();
                void CreateSubtopic ();
                void DeleteTopic ();
                void ChangeTopic (Topic iTopic);
                void SetCurrent (string iGuid);
                void SetCurrent (Topic iTopic);
                void SetTitle (string iString);
                void ExpandTopic (string iGuid, bool isExpanded);
                //void TriggerEdit (bool editPending);

                void AddObserver (IView iView);
                void RemoveObserver (IView iView);
                void NotifyObservers ();
        }
}
