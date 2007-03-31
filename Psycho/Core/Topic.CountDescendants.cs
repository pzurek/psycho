using System;
using System.Collections.Generic;

namespace Psycho
{
    public partial class Topic
    {
        public void CountDescendants()
        {
            totalCount = 0;
            Queue<Topic> remaining = new Queue<Topic>();
            remaining.Enqueue(this);
            while (remaining.Count > 0)
            {
                Topic topic = remaining.Dequeue();
                foreach (Topic child in topic.Subtopics) remaining.Enqueue(child);
                totalCount++;
            }
        }
    }
}
