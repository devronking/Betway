using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using Excel = Microsoft.Office.Interop.Excel;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System.Threading;


namespace Betway2.Utils
{

    class FunctionLibrary
    {
        //Make driver accessible to other classes
        public static IWebDriver CurrentDriver { get; set; }
        
        //Initialise browser
        public static IWebDriver BrowserInit(string browser)
        {
            IWebDriver driver;

            switch (browser)
            {
                case "chrome":
                    driver = new OpenQA.Selenium.Chrome.ChromeDriver();
                    break;
                default:
                    driver = new OpenQA.Selenium.Chrome.ChromeDriver();
                    break;
            }
            //Delete all coockies
            driver.Manage().Cookies.DeleteAllCookies();
            CurrentDriver = driver;
            return driver;
        }

        //Wait for element to be displayed (in seconds)
        public static void WaitForElement(IWebElement element, int timeout)
        {
            var currentTime = DateTime.Now;
            var futureTime = DateTime.Now.AddSeconds(timeout);

            //Keep checking if element is displayed until specified time has elapsed
            while (currentTime <= futureTime)
            {
                if (element.Displayed)
                {
                    //Exit while loop
                    break;
                }
                currentTime = DateTime.Now;
            }
        }

        //Print elements to Excel (location = C:\Temp)
        public static void PrintoutToExcel(IList<IWebElement> list, string filename)
        {
            //Initialise new Excel file
            Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();
            
            Excel.Workbook xlWorkBook;
            Excel.Worksheet xlWorkSheet;
            object misValue = System.Reflection.Missing.Value;

            xlWorkBook = xlApp.Workbooks.Add(misValue);
            xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);

            int row = 1;
            //Enter filename as header
            xlWorkSheet.Cells[row, 1] = filename;

            //Print out each element
            foreach (var e in list)
            {
                if(e.Text.ToString() != "")
                {
                    row++;
                    switch (filename)
                    {
                        case "NewsHeadlines":
                            xlWorkSheet.Cells[row, 1] = e.Text.ToString();
                            break;
                        case "LiveGames":
                            xlWorkSheet.Cells[row, 1] = e.GetAttribute("data-eventtitle").ToString();
                            break;
                    }
                }
            }

            //Save file to disk and close
            string timestamp = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            System.IO.Directory.CreateDirectory("c:\\temp");
            xlWorkBook.SaveAs("c:\\temp\\" + filename + "_" + timestamp + ".xls", Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
            xlWorkBook.Close(true, misValue, misValue);
            xlApp.Quit();

            Marshal.ReleaseComObject(xlWorkSheet);
            Marshal.ReleaseComObject(xlWorkBook);
            Marshal.ReleaseComObject(xlApp);
            
        }
    }
}
