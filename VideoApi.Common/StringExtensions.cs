using System;
using System.Collections.Generic;
using System.Text;

namespace VideoApi.Common
{
	public static class StringExtensions
	{
		public static bool IsNullOrEmpty(this string s)
		{
			return s == null || s == String.Empty;
		}
	}
}
