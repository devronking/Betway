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
    //Repository for Betway site
    class BetwayPage
    {
        //Xpath for Live Now link
        public static string LiveNowLink = "//a[text()='Live Now']";
        
        //Xpath for Live Games
        public static string LiveGames = "//div[@class='row eventRow']";
        
    }
}
