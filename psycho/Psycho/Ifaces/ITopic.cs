using System;
using System.Collections.Generic;
using System.Text;

namespace Psycho {
    public interface ITopic {

        string Title { get; set; }
        ITopic Parent { get; set; }
        int Level { get; }
        string Id { get; set; }
        string Path { get; }
        string GUID { get; }
        bool IsExpanded { get; set; }
        Topics Subtopics { get; set; }

        void AddSubtopicAt (int paramIndex, ITopic paramTopic);
        void AddSubtopic (ITopic paramTopic);
        void Delete ();
    }
}
