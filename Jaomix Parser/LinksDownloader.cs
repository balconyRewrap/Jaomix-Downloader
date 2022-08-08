using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Jaomix_Parser;

internal class LinksDownloader
{
    private static List<string> OnClick(string url)
    {
        string? x = "";
        var options = new ChromeOptions();
        options.AddArguments("headless");
        IWebDriver driver = new ChromeDriver(options);
        /*            IWebDriver driver = new ChromeDriver();*/
        driver.Navigate().GoToUrl(url);
        try
            /*            OpenQA.Selenium.NoSuchElementException*/
        {
            driver.FindElements(
                By.XPath("/html/body/div[1]/div[3]/div/div/div/div/div[2]/div[2]/div/div[1]/select"));
            var optionsElement =
                driver.FindElement(
                    By.XPath("/html/body/div[1]/div[3]/div/div/div/div/div[2]/div[2]/div/div[1]/select"));
            x = optionsElement.GetAttribute("innerHTML");
            bool elem = false;

            while (!elem)
            {
                if (!x.Contains("загружается")) elem = true;

                optionsElement =
                    driver.FindElement(
                        By.XPath("/html/body/div[1]/div[3]/div/div/div/div/div[2]/div[2]/div/div[1]/select"));
                try
                {
                    x = optionsElement.GetAttribute("innerHTML");
                }
                // В нашем случае баг будет связан с хреновой загрузкой страницы и решается ожиданием, потому в catch ничего нет
                catch
                {
                }
            }
        }
        catch (NoSuchElementException)
        {
            x = "0";
        }

        string chapterCount = "0";

        if (x != "0")
        {
            string[] subs = x.Split("<option");
            chapterCount = ChaptFounder(subs.Last());

            Console.WriteLine(chapterCount);
        }
        else
        {
            Console.WriteLine(chapterCount);
        }

        int chapterCountInt = Convert.ToInt32(chapterCount);
        if (chapterCountInt > 0)
            for (int i = 1; i < chapterCountInt + 2; i++)
            {
                string k = Convert.ToString(i);
                var element =
                    driver.FindElement(By.XPath(
                        "/html/body/div/div[3]/div/div/div/div/div[2]/div[2]/div/div[1]/select/option[" + k + "]"));
                element.Click();
                element.Click();
                Thread.Sleep(1000);
                Console.WriteLine(k);
            }

        url = url.Split("/")[2] + "/" + url.Split("/")[4];
        var elems = driver.FindElements(By.XPath("//a[@href]"));
        var urls = new List<string>();
        foreach (var elem in elems)
            if (elem.GetAttribute("href").Contains(url))
                urls.Add(elem.GetAttribute("href"));
        driver.Close();
        return urls;
    }

    private static string ChaptFounder(string line)
    {
        int count = 0;
        string n = "0";
        bool usl;

        foreach (char i in line)
        {
            if (char.IsDigit(i))
            {
                count += 1;
                n += i;
                usl = true;
            }
            else
            {
                usl = false;
                if (count > 0 && usl != true) return n;
            }

            if (count >= 4) return n;
        }

        return "0";
    }

    public static void Downloader(string url, string folder)
    {
        Console.WriteLine("Введите имя файла, куда записать ссылки");
        string path = folder + Console.ReadLine();
        var urls = OnClick(url);
        var fileMaker = new FileMaker();
        fileMaker.Maker(urls, folder, path);
    }
}