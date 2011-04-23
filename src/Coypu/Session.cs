﻿using System;
using Coypu.Robustness;

namespace Coypu
{
	public class Session : IDisposable
	{
		private readonly Driver driver;
		private readonly RobustWrapper robustWrapper;
		public bool WasDisposed { get; private set; }

		public Driver Driver
		{
			get { return driver; }
		}

		public object Native
		{
			get { return driver.Native; }
		}

		public Session(Driver driver, RobustWrapper robustWrapper)
		{
			this.robustWrapper = robustWrapper;
			this.driver = driver;
		}

		public void Dispose()
		{
			if (WasDisposed) return;
			driver.Dispose();
			WasDisposed = true;
		}

		public void ClickButton(string locator)
		{
			robustWrapper.Robustly(() => driver.Click(driver.FindButton(locator)));
		}

		public void ClickLink(string locator)
		{
			robustWrapper.Robustly(() => driver.Click(driver.FindLink(locator)));
		}

		public void Visit(string url)
		{
			driver.Visit(url);
		}

		public void Click(Node node)
		{
			robustWrapper.Robustly(() => driver.Click(node));
		}

		public Node FindButton(string locator)
		{
			return robustWrapper.Robustly(() => driver.FindButton(locator));
		}

		public Node FindLink(string locator)
		{
			return robustWrapper.Robustly(() => driver.FindLink(locator));
		}

		public Node FindField(string locator)
		{
			return robustWrapper.Robustly(() => driver.FindField(locator));
		}

		public FillInWith FillIn(string locator)
		{
			return new FillInWith(value => robustWrapper.Robustly(() => driver.Set(driver.FindField(locator), value)));
		}

		public SelectFrom Select(string option)
		{
			return new SelectFrom(locator => robustWrapper.Robustly(() => driver.Select(driver.FindField(locator), option)));
		}
	}

	public class FillInWith
	{
		private readonly Action<string> action;

		public FillInWith(Action<string> action)
		{
			this.action = action;
		}

		public void With(string value)
		{
			action(value);
		}
	}

	public class SelectFrom
	{
		private readonly Action<string> action;

		public SelectFrom(Action<string> action)
		{
			this.action = action;
		}

		public void From(string locator)
		{
			action(locator);
		}
	}
}