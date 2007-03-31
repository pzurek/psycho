using System;
using Gtk;
using Psycho;

namespace Psycho
{
	
	public class MainVBox : VBox
	{
		
		public MainVBox() : base()
		{
			TemporaryButtonBox temporaryControl = new TemporaryButtonBox();
			this.Homogeneous = false;
			this.PackStart(temporaryControl);
			this.BorderWidth = 6;
		}
	}
	
}
