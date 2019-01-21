using System;
using OpenQA.Selenium;
using TestAutomationEssentials.Common;

namespace TestAutomationEssentials.Selenium
{
    // TODO: remove the following line when merging to master
#pragma warning disable CS1591, CS1572 // Missing XML comment for publicly visible type or member

    /// <summary>
    /// Represents a browser window
    /// </summary>
    public class BrowserWindow //: ElementsContainer, IDOMRoot
    {
        private readonly Browser _browser;
        private string _windowHandle;

        /// <summary>
        /// Initializes the <see cref="BrowserWindow"/> given the specified browser, window handle, and a description
        /// </summary>
        /// <param name="browser">The browser object that this window belongs to</param>
        /// <param name="windowHandle">The handle of the browser window as returned from <see cref="IWebDriver.WindowHandles"/> or <see cref="IWebDriver.CurrentWindowHandle"/></param>
        /// <param name="description">The description of the window, as you want to appear in the log</param>
        internal BrowserWindow(Browser browser, string windowHandle/*, string description*/)
//            : base(description)
        {
            _browser = browser;
            _windowHandle = windowHandle;
        }

        protected internal /*override*/ void Activate()
        {
            //if (_browser.ActiveDOM == this)
            //    return;

            // Logger.WriteLine("Switching to window '{0}'", Description);
            Logger.WriteLine("Switching to window {0}", _windowHandle);
            _browser.GetWebDriver().SwitchTo().Window(_windowHandle);
            //_browser.ActiveDOM = this;
        }

        //	/// <summary>
        //	/// Always returns itself
        //	/// </summary>
        //	public override IDOMRoot DOMRoot
        //	{
        //		get { return this; }
        //	}

        //	/// <summary>
        //	/// Returns the owning browser
        //	/// </summary>
        //	public Browser Browser
        //	{
        //		get { return _browser; }
        //	}

        /// <summary>
        /// Returns the current title of the window
        /// </summary>
        public string Title
        	{
        		get
        		{
        			Activate();
        //			return Browser.GetWebDriver().Title;
		            return _browser.GetWebDriver().Title;
		        }
			}

        //	/// <summary>
        //	/// Gets or sets the current URL of the browser window
        //	/// </summary>
        //	/// <remarks>
        //	/// Setting this property is exactly the same as calling <see cref="NavigateToUrl"/>
        //	/// </remarks>
        //	public string Url
        //	{
        //		get
        //		{
        //			Activate();
        //			return Browser.GetWebDriver().Url;
        //		}
        //		set
        //		{
        //			NavigateToUrl(value);
        //		}
        //	}

        /// <summary>
        /// Closes the current window
        /// </summary>
        public void Close()
        {
            if (_browser.IsDisposed)
                return;

            var webDriver = _browser.GetWebDriver();

            //		using (Logger.StartSection("Closing '{0}' Window, with id={1} ({2})", Description, _windowHandle.GetHashCode(), _windowHandle))
            //		{
            //			if (!webDriver.WindowHandles.Contains(_windowHandle))
            //			{
            //				Logger.WriteLine("Window '{0}' is already closed", Description);
            //				return;
            //			}

            //			Activate();
            webDriver.Close();
            //			Logger.WriteLine("Window '{0}' closed", Description);
            //		}
        }

        //	void IDOMRoot.Activate()
        //	{
        //		Activate();
        //	}

        /// <summary>
        /// Navigates the current browser window to the specified URL
        /// </summary>
        /// <param name="url">The URL to navigate to</param>
        /// <remarks>
        /// This method records the operation to the log using <see cref="Logger"/>
        /// </remarks>
        public void NavigateToUrl(string url)
        {
            //Logger.WriteLine("Navigating to '{0}' on '{1}' window", url, Description);
            //Activate();
            var driver = _browser.GetWebDriver();
            driver.Url = url;
            _windowHandle = driver.CurrentWindowHandle; // Workaround for GeckoDriver. See test: GeckoDriverChangesWindowHandleAfterSettingUrlForTheFirstTime
        }
    }
}