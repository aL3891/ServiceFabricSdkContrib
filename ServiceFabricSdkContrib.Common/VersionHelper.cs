﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ServiceFabricSdkContrib.Common
{
	public class VersionHelper
	{
		public static string Hash(string data, int maxHashLength)
		{
			if (data != "")
			{
				var res = Uri.EscapeDataString(Convert.ToBase64String(new SHA256Managed().ComputeHash(Encoding.ASCII.GetBytes(data))));
				return res.Substring(0, Math.Min(res.Length, maxHashLength));
			}
			else
				return "";
		}

		public static string AppendVersion(string version, params string[] segments)
		{
			var res = string.Join(".", segments.Where(s => !string.IsNullOrWhiteSpace(s)));

			if (version != "")
				if (res != "")
					return version + "." + res;
				else
					return version;
			else
				return res;
		}
	}
}
