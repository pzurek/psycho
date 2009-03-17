using System;

namespace Psycho.Core
{
	public class Author
	{
		var name;
		var email;
		
		public var Name {
			get {
				return name;
			}
			set {
				name = value;
			}
		}
		
		public var Email {
			get {
				return email;
			}
			set {
				email = value;
			}
		}
	}
}
