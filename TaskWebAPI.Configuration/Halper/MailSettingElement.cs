using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace TaskWebAPI.Configuration.Halper
{
	public class MailSettingElement : ConfigurationElement
	{
		[ConfigurationProperty("host")]
		public string Host
		{
			get { return (string)base["host"]; }
			set { base["host"] = value; }
		}

		[ConfigurationProperty("username")]
		public string UserName
		{
			get { return (string)base["username"]; }
			set { base["username"] = value; }
		}

		[ConfigurationProperty("uselocal")]
		public bool UseLocal
		{
			get { return (bool)base["uselocal"]; }
			set { base["uselocal"] = value; }
		}


		[ConfigurationProperty("displayName")]
		public string DisplayName
		{
			get { return (string)base["displayName"]; }
			set { base["displayName"] = value; }
		}

		[ConfigurationProperty("password")]
		public string Password
		{
			get { return (string)base["password"]; }
			set { base["password"] = value; }
		}

		[ConfigurationProperty("from")]
		public string FromAddress
		{
			get { return (string)base["from"]; }
			set { base["from"] = value; }
		}
		[ConfigurationProperty("to")]
		public string ToAddress
		{
			get { return (string)base["to"]; }
			set { base["to"] = value; }
		}
		[ConfigurationProperty("cc")]
		public string CCAddress
		{
			get { return (string)base["cc"]; }
			set { base["cc"] = value; }
		}
		[ConfigurationProperty("bcc")]
		public string BCCAddress
		{
			get { return (string)base["bcc"]; }
			set { base["bcc"] = value; }
		}
	}
}
