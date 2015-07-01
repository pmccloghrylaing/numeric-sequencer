using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.PhantomJS;
using OpenQA.Selenium.Remote;
using TestStack.Seleno;
using TestStack.Seleno.Extensions;
using TestStack.Seleno.PageObjects;
using TestStack.Seleno.PageObjects.Controls;
using TestStack.Seleno.Configuration;
using Microsoft.Owin.Hosting;
using FluentAssertions;
using OpenQA.Selenium;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using System.Net.Sockets;
using System.Net;
using Microsoft.Owin;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using System.Runtime.InteropServices;
using System.Collections.Generic;

namespace NumericSequencer.Tests.EndToEnd
{
	[TestClass]
	public class SequencerEndToEndTests
	{
		IDisposable webApp;

		[TestInitialize]
		public void Init()
		{
			Startup.BaseDirectory = "..\\..\\..\\NumericSequencer";
			webApp = WebApp.Start<Startup>("localhost:" + Host.Port);
		}

		[TestCleanup]
		public void Cleanup()
		{
			webApp.Dispose();
		}

		[TestMethod]
		public async Task EndToEnd()
		{
			var page = Host.Instance.NavigateToInitialPage<SequencerPageObject>();

			page.InputValidity().Should().BeTrue();

			page.InputValue("-1")
				.InputValidity().Should().BeFalse();
			page.ValidationMessage().Should().Be("Input must be a positive integer");

			page.InputValue("")
				.InputValidity().Should().BeFalse();

			page.InputValue("32")
				.InputValidity().Should().BeTrue();

			var results = await page.GenerateSequencesAsync();

			results.First(kvp => kvp.Key.StartsWith("All integers"))
				.Value
				.Should().Be("1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32");

			results.Single(kvp => kvp.Key.StartsWith("Odd integers"))
				.Value
				.Should().Be("1, 3, 5, 7, 9, 11, 13, 15, 17, 19, 21, 23, 25, 27, 29, 31");

			results.Single(kvp => kvp.Key.StartsWith("Even integers"))
				.Value
				.Should().Be("2, 4, 6, 8, 10, 12, 14, 16, 18, 20, 22, 24, 26, 28, 30, 32");

			results.Single(kvp => kvp.Key.Contains("replaces multiple of"))
				.Value
				.Should().Be("1, 2, C, 4, E, C, 7, 8, C, E, 11, C, 13, 14, Z, 16, 17, C, 19, E, C, 22, 23, C, E, 26, C, 28, 29, Z, 31, 32");

			results.Single(kvp => kvp.Key.StartsWith("Fibonacci integers"))
				.Value
				.Should().Be("1, 1, 2, 3, 5, 8, 13, 21");
		}
	}

	public class SequencerPageObject : Page
	{
		public bool InputValidity()
		{
			return !Find.Element(By.Id("limit-input"))
				.GetAttribute("class")
				.Split(' ')
				.Contains("invalid");
		}

		public SequencerPageObject InputValue(string value)
		{
			var input = Find.Element(By.Id("limit-input"));
			input.Clear();
			input.SendKeys(value);
			return this;
		}

		public string ValidationMessage()
		{
			var message = Find.Element(By.ClassName("error"));
			if (message.Displayed && !string.IsNullOrWhiteSpace(message.Text))
			{
				return message.Text;
			}
			return null;
		}

		public async Task<KeyValuePair<string,string>[]> GenerateSequencesAsync()
		{
			Find.Element(By.CssSelector("input[type=submit]")).Click();

			var startTime = DateTime.Now;
			while (!Find.Element(By.Id("results")).Displayed)
			{
				if (DateTime.Now - startTime > TimeSpan.FromSeconds(10))
				{
					throw new TimeoutException();
				}
				await Task.Delay(100);
			}

			return Find.Elements(By.CssSelector("#results dt"))
				.Zip(Find.Elements(By.CssSelector("#results dd")),
					(title, result) => new KeyValuePair<string, string>(title.Text, result.Text))
				.ToArray();
		}
	}
}
