using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Globalization;

namespace JaomixDownloader;
public class LinksDownloader
{
    // TODO: Сделать проверку на количество на правильное количество ссылок (спросить у юзера)
    private const string LinksSelectionElementXpath = "/html/body/div[1]/div[3]/div/div/div/div/div[2]/div[2]/div/div[1]/select";

    private static List<string> ParseLinks(string url)
    {
        
        var options = new ChromeOptions();
        options.AddArguments("headless");
        IWebDriver driver = new ChromeDriver(options);

        string linksSelectionElementHtml = GetHtmlLinksSelectionElement(url,driver);
        int chapterSelectionsCount = 0;

        if (linksSelectionElementHtml != "0")
        {
            chapterSelectionsCount = Convert.ToInt32(FoundChapterSelectionsCount(linksSelectionElementHtml));
            Console.WriteLine(chapterSelectionsCount);
        }

        if (chapterSelectionsCount > 0) ClickAllLinksSelections(chapterSelectionsCount, driver);
        // Немного меняем url, т.к. страница со ссылками и страницы с главами имеют различный url
        url = url.Split("/")[2] + "/" + url.Split("/")[4];
        var elems = driver.FindElements(By.XPath("//a[@href]"));
        var urls = (from elem in elems where elem.GetAttribute("href").Contains(url) select elem.GetAttribute("href")).ToList();
        driver.Close();
        return urls;
    }

    private static string GetHtmlLinksSelectionElement(string url,IWebDriver driver)
    {
        driver.Navigate().GoToUrl(url);
        string linksSelectionElementInnerHtml = "";
        try
        {
            // driver.FindElements(
            //     By.XPath("/html/body/div[1]/div[3]/div/div/div/div/div[2]/div[2]/div/div[1]/select"));
            var optionsElement =
                driver.FindElement(
                    By.XPath(LinksSelectionElementXpath));
            linksSelectionElementInnerHtml = optionsElement.GetAttribute("innerHTML");
            bool elem = false;

            while (!elem)
            {
                if (!linksSelectionElementInnerHtml.Contains("загружается")) elem = true;

                optionsElement =
                    driver.FindElement(
                        By.XPath("/html/body/div[1]/div[3]/div/div/div/div/div[2]/div[2]/div/div[1]/select"));
                try
                {
                    linksSelectionElementInnerHtml = optionsElement.GetAttribute("innerHTML");
                }
                // В нашем случае баг будет связан с неполной загрузкой страницы и решается ожиданием, потому в catch ничего нет
                catch
                {
                }
            }
        }
        catch (NoSuchElementException)
        {
            linksSelectionElementInnerHtml = "0";
        }
        return linksSelectionElementInnerHtml;
    }

    private static string FoundChapterSelectionsCount(string linksSelectionElementHtml)
    {
        // Получаем последний option из массива, с которым и будем работать
        string lastOption = linksSelectionElementHtml.Split("<option").Last();
        int digitsCount = 0;
        string chapterSelectionsCount = "0";

        foreach (char i in lastOption)
        {
            bool usl;
            if (char.IsDigit(i))
            {
                digitsCount += 1;
                chapterSelectionsCount += i;
                usl = true;
            }
            else
            {
                usl = false;
                if (digitsCount > 0 && usl != true) return chapterSelectionsCount;
            }

            if (digitsCount >= 4) return chapterSelectionsCount;
        }
        return chapterSelectionsCount;
    }

    private static void ClickAllLinksSelections(int chapterSelectionsCount, IWebDriver driver)
    {
        for (int i = 1; i < chapterSelectionsCount + 2; i++)
        {
            string k = Convert.ToString(i);
            var element =
                driver.FindElement(By.XPath($"/html/body/div/div[3]/div/div/div/div/div[2]/div[2]/div/div[1]/select/option[{k}]"));
            element.Click();
            element.Click();
            Thread.Sleep(1000);
            Console.WriteLine(k);
        }
    }

    public static void ParseLinksToFile(string url, string folder)
    {
        Console.WriteLine(GlobalStrings.ResourceManager.GetString("linksFileNameNew", CultureInfo.CurrentCulture));
        string path = folder + Console.ReadLine();
        var urls = ParseLinks(url);
        FileMaker.MakeLinksFile(urls, path);
    }
}