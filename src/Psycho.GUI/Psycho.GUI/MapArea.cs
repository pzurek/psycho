using System;
using System.Collections.Generic;

using Cairo;

using Gdk;
using Gtk;

namespace Psycho.GUI
{
	public class MapArea : DrawingArea
	{
		List<MapItem> items;

		public MapArea()
		{
			items = new List<MapItem>();
			this.CanFocus = true;
			this.AddEvents((int)EventMask.ButtonPressMask);
			this.AddEvents((int)EventMask.KeyPressMask);
			this.AddEvents((int)EventMask.KeyReleaseMask);
			this.AddEvents((int)EventMask.PointerMotionMask);
			this.AddEvents((int)EventMask.AllEventsMask);
			
			this.ButtonPressEvent += (object o, ButtonPressEventArgs args) =>
			{
				PathItem pathItem = new PathItem();
				items.Add(pathItem);
				this.QueueDraw();
			};
			
			this.KeyReleaseEvent += (object o, KeyReleaseEventArgs args) => { Console.WriteLine(string.Format("{0} key released", args.Event.Key.ToString())); };
			this.KeyPressEvent += (object o, KeyPressEventArgs args) => { Console.WriteLine(string.Format("{0} key pressed", args.Event.Key.ToString())); };
			
			this.DestroyEvent += (object o, DestroyEventArgs args) => {
				foreach (MapItem item in items)
					item.Dispose();
			};
		}

		protected override bool OnExposeEvent(Gdk.EventExpose args)
		{
			using (Context g = Gdk.CairoHelper.Create(args.Window)) {
				foreach (MapItem item in items) {
					var i = items.IndexOf(item);
					item.Draw(g, 50 + i * 5, 50 + i * 5, 300 + i * 5, 200 + i * 5);
					g.Color = new Cairo.Color(1, 0.1, 0.1, 1);
					g.FillPreserve();
					g.Color = new Cairo.Color(1, 0.7, 0.3, 1);
					g.LineWidth = 5;
					g.Stroke();
				}
			}
			return true;
		}
	}
}
