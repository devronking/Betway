using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Support.PageObjects;

namespace Betway2.Pages
{
    //Repository for Google News site
    class GoogleNewsPage
    {
        //Xpath for all Headlines
        public static string Headlines = "//a[@role='heading' and @aria-level='2']";
    }
}
