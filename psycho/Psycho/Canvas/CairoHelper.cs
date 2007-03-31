// This file was generated by the Gtk# code generator.
// Any changes made will be lost if regenerated.

namespace Pango {

	using System;
	using System.Runtime.InteropServices;

#region Autogenerated code
	public class CairoHelper {

		[DllImport("libpangocairo-1.0-0.dll")]
		static extern void pango_cairo_show_layout(IntPtr cr, IntPtr layout);

		public static void ShowLayout(Cairo.Context cr, Pango.Layout layout) {
			pango_cairo_show_layout(cr == null ? IntPtr.Zero : cr.Handle, layout == null ? IntPtr.Zero : layout.Handle);
		}

		[DllImport("libpangocairo-1.0-0.dll")]
		static extern void pango_cairo_show_layout_line(IntPtr cr, IntPtr line);

		public static void ShowLayoutLine(Cairo.Context cr, Pango.LayoutLine line) {
			pango_cairo_show_layout_line(cr == null ? IntPtr.Zero : cr.Handle, line == null ? IntPtr.Zero : line.Handle);
		}

		[DllImport("libpangocairo-1.0-0.dll")]
		static extern void pango_cairo_layout_line_path(IntPtr cr, IntPtr line);

		public static void LayoutLinePath(Cairo.Context cr, Pango.LayoutLine line) {
			pango_cairo_layout_line_path(cr == null ? IntPtr.Zero : cr.Handle, line == null ? IntPtr.Zero : line.Handle);
		}

		[DllImport("libpangocairo-1.0-0.dll")]
		static extern void pango_cairo_update_context(IntPtr cr, IntPtr context);

		public static void UpdateContext(Cairo.Context cr, Pango.Context context) {
			pango_cairo_update_context(cr == null ? IntPtr.Zero : cr.Handle, context == null ? IntPtr.Zero : context.Handle);
		}

		[DllImport("libpangocairo-1.0-0.dll")]
		static extern double pango_cairo_context_get_resolution(IntPtr context);

		public static double ContextGetResolution(Pango.Context context) {
			double raw_ret = pango_cairo_context_get_resolution(context == null ? IntPtr.Zero : context.Handle);
			double ret = raw_ret;
			return ret;
		}

		[DllImport("libpangocairo-1.0-0.dll")]
		static extern void pango_cairo_glyph_string_path(IntPtr cr, IntPtr font, IntPtr glyphs);

		public static void GlyphStringPath(Cairo.Context cr, Pango.Font font, Pango.GlyphString glyphs) {
			pango_cairo_glyph_string_path(cr == null ? IntPtr.Zero : cr.Handle, font == null ? IntPtr.Zero : font.Handle, glyphs == null ? IntPtr.Zero : glyphs.Handle);
		}

		[DllImport("libpangocairo-1.0-0.dll")]
		static extern void pango_cairo_context_set_resolution(IntPtr context, double dpi);

		public static void ContextSetResolution(Pango.Context context, double dpi) {
			pango_cairo_context_set_resolution(context == null ? IntPtr.Zero : context.Handle, dpi);
		}

		[DllImport("libpangocairo-1.0-0.dll")]
		static extern IntPtr pango_cairo_create_layout(IntPtr cr);

		public static Pango.Layout CreateLayout(Cairo.Context cr) {
			IntPtr raw_ret = pango_cairo_create_layout(cr == null ? IntPtr.Zero : cr.Handle);
			Pango.Layout ret = GLib.Object.GetObject(raw_ret) as Pango.Layout;
			return ret;
		}

		[DllImport("libpangocairo-1.0-0.dll")]
		static extern void pango_cairo_show_glyph_string(IntPtr cr, IntPtr font, IntPtr glyphs);

		public static void ShowGlyphString(Cairo.Context cr, Pango.Font font, Pango.GlyphString glyphs) {
			pango_cairo_show_glyph_string(cr == null ? IntPtr.Zero : cr.Handle, font == null ? IntPtr.Zero : font.Handle, glyphs == null ? IntPtr.Zero : glyphs.Handle);
		}

		[DllImport("libpangocairo-1.0-0.dll")]
		static extern void pango_cairo_update_layout(IntPtr cr, IntPtr layout);

		public static void UpdateLayout(Cairo.Context cr, Pango.Layout layout) {
			pango_cairo_update_layout(cr == null ? IntPtr.Zero : cr.Handle, layout == null ? IntPtr.Zero : layout.Handle);
		}

		[DllImport("libpangocairo-1.0-0.dll")]
		static extern void pango_cairo_layout_path(IntPtr cr, IntPtr layout);

		public static void LayoutPath(Cairo.Context cr, Pango.Layout layout) {
			pango_cairo_layout_path(cr == null ? IntPtr.Zero : cr.Handle, layout == null ? IntPtr.Zero : layout.Handle);
		}

#endregion
	}
}
