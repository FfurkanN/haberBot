using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Google.Cloud.Firestore;
using System.Globalization;
using OpenQA.Selenium.Support.UI;
using System.IO;
using System.Xml.Linq;
using OpenQA.Selenium.Interactions;
using SeleniumExtras.WaitHelpers;
using System.Text.RegularExpressions;
using System.CodeDom.Compiler;
using System.Timers;
using Google.Cloud.Firestore.V1;
using System.Security.Policy;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace haberBot
{
    public partial class Form1 : Form
    {
        FirestoreDb database;
        private List<Button> buttons = new List<Button>();
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string basePath = AppDomain.CurrentDomain.BaseDirectory;
            string path =  "haberbot.json";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);

            database = FirestoreDb.Create("haberbot");

            panel1.BorderStyle = BorderStyle.None;
            panel1.BackColor = Color.Transparent;
            documentPanel.BorderStyle = BorderStyle.None;
            documentPanel.BackColor = Color.Transparent;

        }
        private void Add_Document_with_CustomID(string site, string baslik, string icerik, string tarih, Dictionary<string,int> frequency)
        {
            DocumentReference doc = database.Collection(site).Document(baslik);
            Dictionary<string, object> data = new Dictionary<string, object>()
            {
                {"newsHeader",baslik },
                {"newsText", icerik },
                {"newsDate", tarih },
                {"frequency", frequency}
            };
            doc.SetAsync(data);
        }
        private async void GetNewsDocument(string collection)
        {
            newsRichTextbox.Text = "";
            documentPanel.Controls.Clear();
            CollectionReference newsCollection = database.Collection(collection);
            QuerySnapshot snapshots = await newsCollection.OrderByDescending("newsDate").GetSnapshotAsync();
            int y = 5;
            foreach (DocumentSnapshot documentSnapshot in snapshots.Documents)
            {
                Button btn = new Button();
                btn.Text = documentSnapshot.Id;
                btn.Width = 195;
                btn.Height = 60;
                btn.Location = new Point(2, y);
                btn.BackColor = Color.FromArgb(92, 92, 92);
                btn.Click += (sender, e) => getNewsDocumentText(collection, documentSnapshot.Id);
                buttons.Add(btn);
                documentPanel.Controls.Add(btn);
                y += 65;
            }
        }

        private async void getNewsDocumentText(string collection, string documentName)
        {
            int y = 5;
            newsRichTextbox.Text = "";
            frekansPanel.Controls.Clear();

            foreach (Button btn in buttons)
            {
                btn.ForeColor = Color.White;
            }
            Button clickedButton = buttons.Find(btn => btn.Text == documentName);
            if (clickedButton != null)
            {
                clickedButton.ForeColor = Color.FromArgb(252, 59, 59 );
            }

            DocumentReference docRef = database.Collection(collection).Document(documentName);
            DocumentSnapshot docSnap = await docRef.GetSnapshotAsync();

            if (docSnap.Exists)
            {
                News news = docSnap.ConvertTo<News>();
                newsRichTextbox.Text += news.newsDate + "\n" + news.newsHeader + "\n\n";
                newsRichTextbox.Text += news.newsText + "\n\n";

                foreach(KeyValuePair<string, int> data in news.frequency)
                {
                    Label label = new Label();
                    label.Text = data.Key+":"+data.Value;
                    label.Location = new Point(2, y);
                    label.Font = new Font(label.Font.FontFamily,10);
                    frekansPanel.Controls.Add((Label)label);
                    y += 20;
                }
            }
        }
        private void run_Click(object sender, EventArgs e)
        {
            ThreadStart[] threadStarts = new ThreadStart[]
                {
                    new ThreadStart(techInsideNews),
                    new ThreadStart(shiftdeleteNews),
                    new ThreadStart(technologyReviewNews),
                    new ThreadStart(mashableNews),
                    new ThreadStart(zdnetNews),
                    new ThreadStart(webrazziNews),
                    new ThreadStart(futurismNews),
                    new ThreadStart(readWriteNews)
                };
            Thread[] threads = new Thread[8];
            for (int i = 0; i < threads.Length; i++)
            {
                threads[i] = new Thread(threadStarts[i]);
            }
            foreach (Thread thread in threads)
            {
                Thread.Sleep(5000);
                thread.Start();
            }
            foreach (Thread thread in threads)
            {
                thread.Join();
            }

        }
        private void techInside_Click(object sender, EventArgs e)
        {
            ChangeButtonColor(techInside);
            GetNewsDocument("Tech inside");
            
        }
        private void shiftDelete_Click(object sender, EventArgs e)
        {
            GetNewsDocument("ShiftDelete.Net");
            ChangeButtonColor(shiftDelete);
        }
        private void technologyreview_Click(object sender, EventArgs e)
        {
            GetNewsDocument("Technology Review");
            ChangeButtonColor(technologyreview);
        }
        private void mashable_Click(object sender, EventArgs e)
        {
            GetNewsDocument("Mashable");
            ChangeButtonColor(mashable);
        }
        private void zdnet_Click(object sender, EventArgs e)
        {
            GetNewsDocument("Zdnet");
            ChangeButtonColor(zdnet);
        }
        private void webrazzi_Click(object sender, EventArgs e)
        {
            GetNewsDocument("Webrazzi");
            ChangeButtonColor (webrazzi);
        }
        private void futurism_Click(object sender, EventArgs e)
        {
            GetNewsDocument("Futurism");
            ChangeButtonColor(futurism);
        }
        private void readwrite_Click(object sender, EventArgs e)
        {
            GetNewsDocument("Readwrite");
            ChangeButtonColor(readwrite);
        }

        private void techInsideNews()
        {
            ChromeOptions options = new ChromeOptions();
            options.AddArgument("--disable-gpu");
            IWebDriver driver = new ChromeDriver(options);
            driver.Navigate().GoToUrl("https://www.techinside.com/yapay-zeka/");

            string haber_site = "Tech inside";
            IJavaScriptExecutor jsExecutor = (IJavaScriptExecutor)driver;

            int i = 1;
            string newsDate = "", dateNow = "";

            while (true)
            {
                string script = @"
                var timeElement = document.evaluate('/html/body/div[6]/div[2]/div/div/div/div[2]/div/div[1]/div/div/div[1]/div[" + i + "]/div/div[2]/div[2]/span/span[2]/time', document, null, XPathResult.FIRST_ORDERED_NODE_TYPE, null).singleNodeValue;" +
                "if (timeElement) {return timeElement.textContent;} else {return null;}";

                newsDate = jsExecutor.ExecuteScript(script)?.ToString();

                if (newsDate == null)
                {
                    i++;
                    continue;
                }

                DateTime currentTime = DateTime.Now;

                newsDate = newsDate.Substring(0, newsDate.IndexOf("|"));
                
                newsDate = ParseTime(newsDate);
                dateNow = currentTime.ToString("dd.MM.yyyy");

                if (newsDate.Equals(dateNow))
                {
                    script = @"
                    var link = document.evaluate('//*[@id=""tdi_70""]/div[" + (i++) + "]/div/div[2]/h3/a', document, null, XPathResult.FIRST_ORDERED_NODE_TYPE, null).singleNodeValue;" +
                    "if (link){link.click();}else{console.log('Link not found');}";
                    jsExecutor.ExecuteScript(script);
                    var windowHandles = driver.WindowHandles;

                    driver.SwitchTo().Window(windowHandles[1]);

                    WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));

                    IWebElement iframe = null;
                    try
                    {
                        iframe = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id=\"aswift_2\"]")));
                    }
                    catch (WebDriverTimeoutException)
                    {
                        Console.WriteLine("Iframe bulunamadı.");
                    }


                    if (iframe != null)
                    {
                        driver.SwitchTo().Frame(iframe);

                        IWebElement closeButton = null;
                        try
                        {
                            closeButton = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//*[@id=\"dismiss-button\"]")));
                        }
                        catch (WebDriverTimeoutException)
                        {
                            Console.WriteLine("Kapatma butonu bulunamadı.");
                        }

                        if (closeButton != null)
                        {
                            closeButton.Click();
                        }
                        driver.SwitchTo().DefaultContent();
                    }

                    script = @"
                    var h1Element = document.evaluate('//*[@id=""tdi_60""]/div/div/div/div[3]/div/h1', document, null, XPathResult.FIRST_ORDERED_NODE_TYPE, null).singleNodeValue;
                    if (h1Element)                     
                    {
                        return h1Element.textContent;
                    }
                    else
                    {
                        return null;
                    }
                    ";

                    string haberBasligi = jsExecutor.ExecuteScript(script)?.ToString();

                    script = @"
                    var paragraphs = document.evaluate('//*[@id=""tdi_72""]/div/div[2]/div/div[3]/div/p', document, null, 
                        XPathResult.ORDERED_NODE_SNAPSHOT_TYPE, null);
                    var texts = '';
                    for (var i = 0; i < paragraphs.snapshotLength; i++) {
                        texts += paragraphs.snapshotItem(i).innerText + ' ';
                    }
                    return texts.trim();
                    ";

                    string newsText = (string)jsExecutor.ExecuteScript(script);

                    Add_Document_with_CustomID(haber_site, EncodeTextForFirestore(haberBasligi), newsText, newsDate, frequency(newsText));
                    driver.Close();
                    driver.SwitchTo().Window(windowHandles[0]);
                }
                else
                {
                    driver.Quit();
                    break;
                }
            }
        }
        private void shiftdeleteNews()
        {
            ChromeOptions options = new ChromeOptions();
            options.AddArgument("--disable-gpu");
            IWebDriver driver = new ChromeDriver(options);
            driver.Navigate().GoToUrl("https://shiftdelete.net/yapay-zeka");
            IJavaScriptExecutor jsExecutor = (IJavaScriptExecutor)driver;

            string site = "ShiftDelete.Net";
            int i = 1;
            while(true)
            {
                string script = @"
                var element = document.evaluate('//*[@id=""wrapper""]/div[4]/div/div/div/div[1]/div[" + i + "]/div/aside[2]/span[2]', document, null, XPathResult.FIRST_ORDERED_NODE_TYPE, null).singleNodeValue;" +
                "if (element){return element.textContent;}else{return null;}";

                string newsDate = jsExecutor.ExecuteScript(script)?.ToString();

                if (newsDate == null)
                {
                    i++;
                    continue;
                }
                newsDate = ParseTime(newsDate);
                DateTime currentTime = DateTime.Now;
                string date = currentTime.ToString("dd.MM.yyyy");

                if (newsDate.Equals(date))
                {
                    script = @"
                    var element = document.evaluate('//*[@id=""wrapper""]/div[4]/div/div/div/div[1]/div[" + (i++) + "]/div/div[1]/h4/a', document, null, XPathResult.FIRST_ORDERED_NODE_TYPE, null).singleNodeValue;" +
                    "if (element){element.click();}else{console.log('Element not found.');}";
                    jsExecutor.ExecuteScript(script);

                    Thread.Sleep(1000);

                    var windowHandles = driver.WindowHandles;
                    driver.SwitchTo().Window(windowHandles[1]);

                    script = @"
                    var element = document.evaluate('/html/body/div/div[2]/div/div/article/div/div/div[1]/header/h1', document, null, XPathResult.FIRST_ORDERED_NODE_TYPE, null).singleNodeValue;
                    if (element) {
                        return element.textContent;
                    } else {
                        return null;
                    }
                    ";

                    string newsHeader = jsExecutor.ExecuteScript(script)?.ToString();

                    script = @"
                    var paragraphs = document.evaluate('/html/body/div/div[2]/div/div/article/div/div/div[2]/div[1]/div[2]/p', document, null, 
                        XPathResult.ORDERED_NODE_SNAPSHOT_TYPE, null);
                    var texts = '';
                    for (var i = 0; i < paragraphs.snapshotLength; i++) {
                        texts += paragraphs.snapshotItem(i).innerText + ' ';
                    }
                    return texts.trim();
                    ";

                    string newsText = (string)jsExecutor.ExecuteScript(script);

                    Add_Document_with_CustomID(site, EncodeTextForFirestore(newsHeader), newsText, newsDate, frequency(newsText));

                    driver.Close();
                    driver.SwitchTo().Window(windowHandles[0]);
                }
                else
                {
                    driver.Quit();
                    break;
                }
            }
        }
        private void technologyReviewNews()
        {
            ChromeOptions options = new ChromeOptions();
            options.AddArgument("--disable-gpu");
            IWebDriver driver = new ChromeDriver(options);
            driver.Navigate().GoToUrl("https://www.technologyreview.com/topic/artificial-intelligence/");

            string news_site = "Technology Review";

            IJavaScriptExecutor jsExecutor = (IJavaScriptExecutor)driver;
            int i = 1;
            while (true)
            {
                string script = @"
                    var timeElement = document.evaluate('//*[@id=""content""]/div/div[1]/ul/li[" + i + "]/div/div[1]/div/div/time', document, null, " +
                    "XPathResult.FIRST_ORDERED_NODE_TYPE, null).singleNodeValue;" +
                    "return timeElement.textContent.trim();" +
                    "";

                string timeText = (string)jsExecutor.ExecuteScript(script);

                string newsDate = ParseTime(timeText);
                string dateNow = DateTime.Now.ToString("dd.MM.yyyy");


                if (newsDate.Equals(dateNow))
                {

                    script = @"
                    var element = document.evaluate('//*[@id=\""content\""]/div/div[1]/ul/li[" + (i++) + "]/div/div[1]/header/h3/a', document, null, XPathResult.FIRST_ORDERED_NODE_TYPE, null).singleNodeValue;" +
                    "if (element){element.click();}else{console.log('Element not found.');}";
                    jsExecutor.ExecuteScript(script);

                    WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

                    IWebElement iframe = null;
                    try
                    {
                        iframe = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id=\"offer_acc4eead7b497d22e752-1\"]")));
                    }
                    catch (WebDriverTimeoutException)
                    {
                        Console.WriteLine("Iframe bulunamadı.");
                    }
                    if (iframe != null)
                    {
                        driver.SwitchTo().Frame(iframe);

                        IWebElement closeButton = null;
                        try
                        {
                            closeButton = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("/html/body/div[1]/div/div[2]/article/button")));
                        }
                        catch (WebDriverTimeoutException)
                        {
                            Console.WriteLine("Kapatma butonu bulunamadı.");
                        }

                        if (closeButton != null)
                        {
                            closeButton.Click();
                        }
                        driver.SwitchTo().DefaultContent();
                    }
                    script = @"var h1Element = document.evaluate('/html/body/div[1]/div[1]/main/div[1]/div[1]/header/div[1]/div[1]/h1', document, null, XPathResult.FIRST_ORDERED_NODE_TYPE, null).singleNodeValue;
                    if (h1Element){return h1Element.textContent;}else{return null;}";

                    string newsHeader = jsExecutor.ExecuteScript(script)?.ToString();

                    script = @"
                    var paragraphs = document.evaluate('//*[@id=""content--body""]//p', document, null, 
                        XPathResult.ORDERED_NODE_SNAPSHOT_TYPE, null);
                    var texts = '';
                    for (var i = 0; i < paragraphs.snapshotLength; i++) {
                        texts += paragraphs.snapshotItem(i).innerText + ' ';
                    }
                    return texts.trim();
                    ";

                    string newsText = (string)jsExecutor.ExecuteScript(script);
                    Add_Document_with_CustomID(news_site, EncodeTextForFirestore(newsHeader), newsText, newsDate, frequency(newsText));

                    driver.Navigate().Back();
                }
                else
                {
                    driver.Quit();
                    break;
                }
            }   
        }
        private void mashableNews()
        {
            ChromeOptions options = new ChromeOptions();
            options.AddArgument("--disable-gpu");
            IWebDriver driver = new ChromeDriver(options);
            driver.Navigate().GoToUrl("https://mashable.com/category/artificial-intelligence");

            string news_site = "Mashable";


            IJavaScriptExecutor jsExecutor = (IJavaScriptExecutor)driver;
            bool morePost = true;

            for(int i=1;i<=3;i++)
            {
                string script = @"
                var link = document.evaluate('/html/body/div[2]/div[" + i + "]/div/a', document, null, XPathResult.FIRST_ORDERED_NODE_TYPE, null).singleNodeValue;" +
                "if (link){link.click();}else{console.log('Link not found');}";
                jsExecutor.ExecuteScript(script);

                script = @"
                var timeElement = document.evaluate('/html/body/header/div[3]/div[1]/time', document, null, XPathResult.FIRST_ORDERED_NODE_TYPE, null).singleNodeValue;
                return timeElement.textContent;
                ";

                string datetimeValue = (string)jsExecutor.ExecuteScript(script);
                DateTime date = DateTime.ParseExact(datetimeValue, "MMMM dd, yyyy", CultureInfo.InvariantCulture);
                string newsDate = date.ToString("dd.MM.yyyy");

                string now = DateTime.Now.ToString();
                string dateNow = now.Substring(0, now.IndexOf(" "));

                if (newsDate.Equals(dateNow))
                {
                    string news_Header = driver.FindElement(By.XPath("/html/body/header/h1")).Text;

                    script = @"
                    var paragraphElements = document.querySelectorAll('#article p');
                    var text = '';
                    paragraphElements.forEach(function(paragraph) {
                        text += paragraph.textContent + ' ';
                    });
                    return text.trim();
                    ";
                    string newsText = (string)jsExecutor.ExecuteScript(script);

                    Add_Document_with_CustomID(news_site, EncodeTextForFirestore(news_Header), newsText, newsDate, frequency(newsText));
                    driver.Navigate().Back();
                }
                else
                {
                    morePost = false;
                    driver.Quit();
                    break;
                }
            }
            int a = 1;
            while (morePost)
            {
                string script = @"
                    var link = document.evaluate('/html/body/main/section/section/div/div[" + a + "]/div/div/a', document, null, XPathResult.FIRST_ORDERED_NODE_TYPE, null).singleNodeValue;" +
                "if (link){link.click();}else{console.log('Link not found');}";
                jsExecutor.ExecuteScript(script);

                Thread.Sleep(1000);

                script = @"
                var timeElement = document.evaluate('/html/body/header/div[3]/div[1]/time', document, null, XPathResult.FIRST_ORDERED_NODE_TYPE, null).singleNodeValue;
                return timeElement.textContent;
                ";

                string datetimeValue = (string)jsExecutor.ExecuteScript(script);
                DateTime date = DateTime.ParseExact(datetimeValue, "MMMM dd, yyyy", CultureInfo.InvariantCulture);
                string newsDate = date.ToString("dd.MM.yyyy");

                string now = DateTime.Now.ToString();
                string dateNow = now.Substring(0, now.IndexOf(" "));

                if (newsDate.Equals(dateNow))
                {
                    string news_Header = driver.FindElement(By.XPath("/html/body/header/h1")).Text;

                    script = @"
                    var paragraphElements = document.querySelectorAll('#article p');
                    var text = '';
                    paragraphElements.forEach(function(paragraph) {
                        text += paragraph.textContent + ' ';
                    });
                    return text.trim();
                    ";
                    string newsText = (string)jsExecutor.ExecuteScript(script);

                    Add_Document_with_CustomID(news_site, EncodeTextForFirestore(news_Header), newsText, newsDate, frequency(newsText));
                    driver.Navigate().Back();
                    a++;
                }
                else
                {
                    driver.Quit();
                    morePost = false;
                    break;
                }
            }
        }
        private void zdnetNews()
        {
            ChromeOptions options = new ChromeOptions();
            options.AddArgument("--disable-gpu");
            IWebDriver driver = new ChromeDriver(options);
            driver.Navigate().GoToUrl("https://www.zdnet.com/topic/artificial-intelligence/");

            string haber_site = "Zdnet";
            IJavaScriptExecutor jsExecutor = (IJavaScriptExecutor)driver;

            //load more button click
            string script = @"
            var link = document.evaluate('/html/body/div[3]/div/div/div[1]/div/div/div/div[2]/div/div/div[2]/div/div/div[2]/div/div/div[2]/div/div/div[1]/div/section/div/section/div/div[2]/a', document, null, XPathResult.FIRST_ORDERED_NODE_TYPE, null).singleNodeValue;" +
            "if (link){link.click();}else{console.log('Link not found');}";
            jsExecutor.ExecuteScript(script);
            int i = 1;
            
            while (true)
            {
                script = @"
                var timeElement = document.evaluate('/html/body/div[3]/div/div/div[1]/div/div/div/div[2]/div/div/div[2]/div/div/div[2]/div/div/div[2]/div/div/div[1]/div/section/div/section/div/div[1]/div[1]/article["+i+"]/div/div/p/span[1]', document, null, XPathResult.FIRST_ORDERED_NODE_TYPE, null).singleNodeValue;" +
                "if (timeElement) {return timeElement.textContent;} else {return null;}";

                string newsDate = jsExecutor.ExecuteScript(script)?.ToString();
                if(newsDate == null)
                {
                    i++;
                    continue;
                }
                newsDate = ParseTime(newsDate);
                DateTime currentTime = DateTime.Now;
                string dateNow = DateTime.Now.ToString("dd.MM.yyyy");

                if (newsDate.Equals(dateNow))
                {
                    script = @"
                    var link = document.evaluate('/html/body/div[3]/div/div/div[1]/div/div/div/div[2]/div/div/div[2]/div/div/div[2]/div/div/div[2]/div/div/div[1]/div/section/div/section/div/div[1]/div[1]/article["+(i++)+"]/div/a', document, null, XPathResult.FIRST_ORDERED_NODE_TYPE, null).singleNodeValue;" +
                    "if (link){link.click();}else{console.log('Link not found');}";
                    jsExecutor.ExecuteScript(script);

                    WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));

                    IWebElement iframe = null;
                    try
                    {
                        iframe = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id=\"google_ads_iframe_/22309610186/aw-zdnet/interstitial_0\"]")));
                    }
                    catch (WebDriverTimeoutException)
                    {
                        Console.WriteLine("Iframe bulunamadı.");
                    }
                    if (iframe != null)
                    {
                        driver.SwitchTo().Frame(iframe);

                        IWebElement closeButton = null;
                        try
                        {
                            closeButton = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//*[@id=\"dismiss-button\"]")));
                        }
                        catch (WebDriverTimeoutException)
                        {
                            Console.WriteLine("Kapatma butonu bulunamadı.");
                        }

                        if (closeButton != null)
                        {
                            closeButton.Click();
                        }
                        driver.SwitchTo().DefaultContent();
                    }
                    script = @"
                    var h1Element = document.evaluate('//*[@id=""__layout""]/div/div[3]/main/div/div[1]/div[1]/div[1]/div[1]/div[2]/div/h1', document, null, XPathResult.FIRST_ORDERED_NODE_TYPE, null).singleNodeValue;
                    if (h1Element)
                    {
                        return h1Element.textContent;
                    }
                    else
                    {
                        return null;
                    }
                    ";

                    string haberBasligi = jsExecutor.ExecuteScript(script)?.ToString();

                    script = @"
                    var paragraphs = document.evaluate('html/body/div[1]/div/div/div[3]/main/div/div[1]/div[1]/div[1]/div[2]/div[2]/div[1]/div/p', document, null, 
                        XPathResult.ORDERED_NODE_SNAPSHOT_TYPE, null);
                    var texts = '';
                    for (var i = 0; i < paragraphs.snapshotLength; i++) {
                        texts += paragraphs.snapshotItem(i).innerText + ' ';
                    }
                    return texts.trim();
                    ";

                    string newsText = (string)jsExecutor.ExecuteScript(script);

                    Add_Document_with_CustomID(haber_site, EncodeTextForFirestore(haberBasligi), newsText, newsDate, frequency(newsText));
                    driver.Navigate().Back();
                }
                else
                {
                    driver.Quit();
                    break;
                }
            }
        }
        private void webrazziNews()
        {
            ChromeOptions options = new ChromeOptions();
            options.AddArgument("--disable-gpu");
            IWebDriver driver = new ChromeDriver(options);
            driver.Navigate().GoToUrl("https://webrazzi.com/kategori/yapay-zeka/");

            string haber_site = "Webrazzi";
            IJavaScriptExecutor jsExecutor = (IJavaScriptExecutor)driver;

            int i = 1;
            string newsDate = "", dateNow = "";
            while (true)
            {
                string script = @"
                var timeElement = document.evaluate('/html/body/div[8]/div/div/div[1]/div["+i+"]/div[2]/div/div/span', document, null, XPathResult.FIRST_ORDERED_NODE_TYPE, null).singleNodeValue;" +
                "if (timeElement) {return timeElement.textContent;} else {return null;}";

                newsDate = jsExecutor.ExecuteScript(script)?.ToString();
                if(newsDate == null)
                {
                    i++;
                    continue;
                }
                DateTime currentTime = DateTime.Now;
                newsDate = ParseTime(newsDate);
                dateNow = currentTime.ToString("dd.MM.yyyy");

                if (newsDate.Equals(dateNow))
                {
                    script = @"
                    var link = document.evaluate('//*[@id=""wrapper""]/div/div/div[1]/div["+(i++)+"]/div[1]/div/div[2]/div[1]/a', document, null, XPathResult.FIRST_ORDERED_NODE_TYPE, null).singleNodeValue;" +
                    "if (link){link.click();}else{console.log('Link not found');}";
                    jsExecutor.ExecuteScript(script);

                    script = @"
                    var h1Element = document.evaluate('//*[@id=""wrapper""]/div/div[1]/article/div[1]/div/h1', document, null, XPathResult.FIRST_ORDERED_NODE_TYPE, null).singleNodeValue;
                    if (h1Element)
                    {
                        return h1Element.textContent;
                    }
                    else
                    {
                        return null;
                    }
                    ";

                    string haberBasligi = jsExecutor.ExecuteScript(script)?.ToString();

                    script = @"
                    var paragraphs = document.evaluate('//*[@id=""wrapper""]/div/div[1]/article/div[3]/div/div[2]/div[1]/p', document, null, 
                        XPathResult.ORDERED_NODE_SNAPSHOT_TYPE, null);
                    var texts = '';
                    for (var i = 0; i < paragraphs.snapshotLength; i++) {
                        texts += paragraphs.snapshotItem(i).innerText + ' ';
                    }
                    return texts.trim();
                    ";

                    string newsText = (string)jsExecutor.ExecuteScript(script);

                    Add_Document_with_CustomID(haber_site, EncodeTextForFirestore(haberBasligi), newsText, newsDate, frequency(newsText));
                    driver.Navigate().Back();
                }
                else
                {
                    driver.Quit();
                    break;
                }
            }
        }
        private void futurismNews()
        {
            ChromeOptions options = new ChromeOptions();
            options.AddArgument("--disable-gpu");
            IWebDriver driver = new ChromeDriver(options);
            driver.Navigate().GoToUrl("https://futurism.com/categories/ai-artificial-intelligence");

            string site = "Futurism";
            IJavaScriptExecutor jsExecutor = (IJavaScriptExecutor)driver;

            int i = 1;
            string newsDate = "", dateNow = "";
            while (true)
            {
                string script = @"
                var timeElement = document.evaluate('//*[@id=""incCategoryTypes""]/a["+i+"]/div/div[2]/div[1]/div[2]', document, null, XPathResult.FIRST_ORDERED_NODE_TYPE, null).singleNodeValue;" +
                "if (timeElement) {return timeElement.textContent;} else {return null;}";

                newsDate = jsExecutor.ExecuteScript(script)?.ToString();
                
                if (newsDate == null)
                {
                    i++;
                    continue;
                }
                newsDate = newsDate.Substring(0, newsDate.IndexOf(","));
                newsDate = ParseTime(newsDate);
                DateTime currentTime = DateTime.Now;
                dateNow = currentTime.ToString("dd.MM.yyyy");

                if (newsDate.Equals(dateNow))
                {
                    script = @"
                    var link = document.evaluate('//*[@id=""incCategoryTypes""]/a["+(i++)+"]', document, null, XPathResult.FIRST_ORDERED_NODE_TYPE, null).singleNodeValue;" +
                    "if (link){link.click();}else{console.log('Link not found');}";
                    jsExecutor.ExecuteScript(script);
                    Thread.Sleep(2000);
                    script = @"
                    var h1Element = document.evaluate('//*[@id=""__next""]/main/article/header/h1', document, null, XPathResult.FIRST_ORDERED_NODE_TYPE, null).singleNodeValue;
                    if (h1Element)
                    {
                        return h1Element.textContent;
                    }
                    else
                    {
                        return null;
                    }
                    ";

                    string newsHeader = jsExecutor.ExecuteScript(script)?.ToString();

                    if(newsHeader == null)
                    {
                        script = @"
                        var h1Element = document.evaluate('//*[@id=""__next""]/div/div[2]/div[1]/article/div[1]/div[3]/div[1]/div[2]/h1', document, null, XPathResult.FIRST_ORDERED_NODE_TYPE, null).singleNodeValue;
                        if (h1Element)                  
                        {
                            return h1Element.textContent;
                        }
                        else
                        {
                            return null;
                        }
                        ";

                         newsHeader = jsExecutor.ExecuteScript(script)?.ToString();
                    }

                    script = @"
                    var paragraphs = document.evaluate('//*[@id=""incArticle""]/p', document, null, 
                        XPathResult.ORDERED_NODE_SNAPSHOT_TYPE, null);
                    var texts = '';
                    for (var i = 0; i < paragraphs.snapshotLength; i++) {
                        texts += paragraphs.snapshotItem(i).innerText + ' ';
                    }
                    return texts.trim();
                    ";

                    string newsText = (string)jsExecutor.ExecuteScript(script);

                    Add_Document_with_CustomID(site, EncodeTextForFirestore(newsHeader), newsText, newsDate, frequency(newsText));
                    driver.Navigate().Back();
                }
                else
                {
                    driver.Quit();
                    break;
                }
            }
        }
        private void readWriteNews()
        {
            ChromeOptions options = new ChromeOptions();
            options.AddArgument("--disable-gpu");
            options.AddArgument("--window-size=600,800");
            IWebDriver driver = new ChromeDriver(options);
            driver.Navigate().GoToUrl("https://readwrite.com/category/ai/");

            string site = "Readwrite";
            IJavaScriptExecutor jsExecutor = (IJavaScriptExecutor)driver;

            string[] scripts = new string[] 
            {
            "//*[@id=\"site-content\"]/section[1]/div/div/div/div[1]/div[2]/ul/li[1]/div[2]/h5/a",
            "//*[@id=\"site-content\"]/section[1]/div/div/div/div[1]/div[2]/ul/li[2]/div[2]/h5/a",
            "//*[@id=\"site-content\"]/section[1]/div/div/div/div[1]/div[2]/ul/li[3]/div[2]/h5/a",
            "//*[@id=\"site-content\"]/section[1]/div/div/div/div[1]/div[2]/ul/div/div[1]/div[2]/h5/a",
            "//*[@id=\"site-content\"]/section[1]/div/div/div/div[1]/div[2]/ul/div/div[2]/div[2]/h5/a",
            "//*[@id=\"site-content\"]/section[1]/div/div/div/div[1]/div[2]/ul/div/div[3]/div[2]/h5/a",
            ""
            };

            int i = 0;
            string newsDate = "", dateNow = "";
            while (i<=6)
            {
                string script = @"
                var link = document.evaluate('" + scripts[i++] +"', document, null, XPathResult.FIRST_ORDERED_NODE_TYPE, null).singleNodeValue;" +
                "if (link){link.click();}else{console.log('Link not found');}";
                jsExecutor.ExecuteScript(script);

                script = @"
                var timeElement = document.evaluate('//*[@id=""site-content""]/section[1]/div/div/div/div/div/div/div[1]/div[2]/div/div[4]/span/time', document, null, XPathResult.FIRST_ORDERED_NODE_TYPE, null).singleNodeValue;" +
                "if (timeElement) {return timeElement.textContent;} else {return null;}";
                Thread.Sleep(2000);
                newsDate = jsExecutor.ExecuteScript(script)?.ToString();
                if (newsDate == null)
                {
                    i++;
                    continue;
                }
                newsDate = newsDate.Substring(newsDate.IndexOf(":") + 2);

                DateTime currentTime = DateTime.Now;

                newsDate = ParseTime(newsDate);
                dateNow = currentTime.ToString("dd.MM.yyyy");

                if (newsDate.Equals(dateNow))
                {

                    script = @"
                    var h1Element = document.evaluate('//*[@id=""site-content""]/section[1]/div/div/div/div/div/h1', document, null, XPathResult.FIRST_ORDERED_NODE_TYPE, null).singleNodeValue;
                    if (h1Element)
                    {
                        return h1Element.textContent;
                    }
                    else
                    {
                        return null;
                    }
                    ";

                    string haberBasligi = jsExecutor.ExecuteScript(script)?.ToString();

                    script = @"
                        var paragraphs = document.evaluate(
                            ""//div[contains(@class, 'entry-content')]//p"", 
                            document, 
                            null, 
                            XPathResult.ORDERED_NODE_SNAPSHOT_TYPE, 
                            null
                        );
                        var texts = '';
                        for (var i = 0; i < paragraphs.snapshotLength; i++) {
                            texts += paragraphs.snapshotItem(i).innerText + ' ';
                        }
                        return texts.trim();
                    ";

                    string newsText = (string)jsExecutor.ExecuteScript(script);
                    if(newsText == null)
                    {
                        script = @"                      
                        var paragraphs = document.evaluate('/html/body/main/article/div/div/div[1]/div[2]/p', document, null,
                            XPathResult.ORDERED_NODE_SNAPSHOT_TYPE, null);
                        var texts = '';
                        for (var i = 0; i < paragraphs.snapshotLength; i++)
                        {
                            texts += paragraphs.snapshotItem(i).innerText + ' ';
                        }
                        return texts.trim();
                        ";

                        newsText = (string)jsExecutor.ExecuteScript(script);
                    }

                    Add_Document_with_CustomID(site, EncodeTextForFirestore(haberBasligi), newsText, newsDate, frequency(newsText));
                    driver.Navigate().Back();
                }
                else
                {
                    driver.Quit();
                    break;
                }
            }
        }
        static string ParseTime(string timeText)
        {
            DateTime currentDate = DateTime.Now;
            if (timeText.Contains("hours") || timeText.Contains("saat") || timeText.Contains("hour"))
            {
                int hours = int.Parse(timeText.Split(' ')[0]);
                return currentDate.AddHours(-hours).ToString("dd.MM.yyyy");
            }
            else if (timeText.Contains("mins") || timeText.Contains("min") || timeText.Contains("dakika"))
            {
                int minutes = int.Parse(timeText.Split(' ')[0]);
                return currentDate.AddMinutes(-minutes).ToString("dd.MM.yyyy");
            }
            else if (timeText.Contains("saniye") || timeText.Contains("second") || timeText.Contains("seconds"))
            {
                int seconds = int.Parse(timeText.Split(' ')[0]);
                return currentDate.AddSeconds(-seconds).ToString("dd.MM.yyyy");
            }
            else if (timeText.Contains("days") || timeText.Contains("day") || timeText.Contains("gün"))
            {
                int days = int.Parse(timeText.Split(' ')[0]);
                return currentDate.AddDays(-days).ToString("dd.MM.yyyy");
            }
            else if (timeText.Contains("week") || timeText.Contains("weeks") || timeText.Contains("hafta"))
            {
                int weeks = int.Parse(timeText.Split(' ')[0]);
                return currentDate.AddDays(-weeks * 7).ToString("dd.MM.yyyy");
            }
            else if (DateTime.TryParseExact(timeText, new[] { "d MMMM yyyy", "dd MMMM yyyy", "dd MMM, yyyy" }, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime date))
            {
                return date.ToString("dd.MM.yyyy");
            }
            else if (DateTime.TryParseExact(timeText, new[] { "MMMM d", "MMM d" }, new CultureInfo("en-US"), DateTimeStyles.None, out DateTime date1))
            {
                return date1.ToString("dd.MM.yyyy");
            }
            else if (DateTime.TryParseExact(timeText, new[] { "d MMMM yyyy ", "dd MMMM yyyy ", "d MMMM yyyy", "dd MMMM yyyy" }, new CultureInfo("tr-TR"), DateTimeStyles.None, out DateTime date2))
            {
                return date2.ToString("dd.MM.yyyy");
            }
            else
            {
                throw new ArgumentException("Beklenmeyen zaman formatı");
            }
        }

        static string EncodeTextForFirestore(string input)
        {
            string firestoreText = input.Replace("/", "-");
            return firestoreText;
        }
        private Dictionary<string, int> frequency(string text)
        {
            HashSet<string> stopWords = new HashSet<string>();

            // Türkçe stop words dosyası
            foreach (var line in File.ReadLines("tr_stopword.txt"))
            {
                stopWords.Add(line.Trim().ToLower());
            }

            // İngilizce stop words dosyası
            foreach (var line in File.ReadLines("en_stopword.txt"))
            {
                stopWords.Add(line.Trim().ToLower());
            }

            // Metni temizleme ve kelimelere ayırma
            string cleanedText = Regex.Replace(text.ToLower(), @"[^\w\s]", ""); // Noktalama işaretlerini kaldır ve küçük harfe çevir
            string[] words = cleanedText.Split(new[] { ' ', '\t', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

            // Kelime frekanslarını hesaplamak için bir sözlük oluşturma
            Dictionary<string, int> wordFrequency = new Dictionary<string, int>();

            foreach (var word in words)
            {
                if (!stopWords.Contains(word))
                {
                    if (wordFrequency.ContainsKey(word))
                    {
                        wordFrequency[word]++;
                    }
                    else
                    {
                        wordFrequency[word] = 1;
                    }
                }
            }

            // Frekansı 5 ve 5'ten büyük olan kelimeleri içeren yeni bir sözlük oluşturma
            Dictionary<string, int> filteredWordFrequency = new Dictionary<string, int>();

            foreach (var kvp in wordFrequency)
            {
                if (kvp.Value >= 4)
                {
                    filteredWordFrequency[kvp.Key] = kvp.Value;
                }
            }
            return filteredWordFrequency;
        }
        private void ChangeButtonColor(Button clickedButton)
        {
            techInside.ForeColor = Color.White;
            shiftDelete.ForeColor = Color.White;
            technologyreview.ForeColor = Color.White;
            mashable.ForeColor = Color.White;
            zdnet.ForeColor = Color.White;
            webrazzi.ForeColor = Color.White;
            futurism.ForeColor = Color.White;
            readwrite.ForeColor = Color.White;

            clickedButton.ForeColor = Color.FromArgb(252, 59, 59 );
        }


    }
}