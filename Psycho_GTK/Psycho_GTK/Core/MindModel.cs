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
    }
}
