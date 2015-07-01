using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestStack.Seleno.Configuration;
using OpenQA.Selenium.PhantomJS;
using TestStack.Seleno.Configuration.WebServers;
using System.Net.Sockets;
using System.Net;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;

namespace NumericSequencer.Tests.EndToEnd
{
	public static class Host
	{
		public static int Port = 5432;
		public static readonly SelenoHost Instance = new SelenoHost();

		static Host()
		{
			Instance.Run("NumericSequencer", Port,
				cfg => cfg.WithRemoteWebDriver(() => new PhantomJSDriver()));
		}
	}
}
