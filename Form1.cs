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

namespace haberBot
{
    public partial class Form1 : Form
    {
        FirestoreDb database;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //31
            string path = AppDomain.CurrentDomain.BaseDirectory + @"haberbot.json";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);

            database = FirestoreDb.Create("haberbot");
        }
        private void button1_Click(object sender, EventArgs e)
        {

        }
        private void button2_Click(object sender, EventArgs e)
        {
            GetAllData_From_A_Collection();
        }

        private void Add_Document_with_CustomID(string site, string baslik, string icerik, string tarih)
        {
            DocumentReference doc = database.Collection(site).Document(baslik);
            Dictionary<string, object> data = new Dictionary<string, object>()
            {
                {"HaberBasligi",baslik },
                {"HaberIcerigi", icerik },
                {"Tarih", tarih }
            };
            doc.SetAsync(data);
        }

        private async void GetAllData_Of_A_Document()
        {
            DocumentReference docRef = database.Collection("Haberler").Document("haber1");
            DocumentSnapshot docSnap = await docRef.GetSnapshotAsync();

            if (docSnap.Exists)
            {
                Dictionary<string, object> haber = docSnap.ToDictionary();
                foreach (var item in haber)
                {
                    richTextBox1.Text += string.Format("{0}: {1}\n", item.Key, item.Value);
                }
            }
        }

        private async void GetAllData_From_A_Collection()
        {
            Query QRef = database.Collection("Haberler").WhereEqualTo("Tarih", "19.05.2024");
            QuerySnapshot QSnap = await QRef.GetSnapshotAsync();

            foreach (DocumentSnapshot documentSnapshot in QSnap)
            {
                Haber haber = documentSnapshot.ConvertTo<Haber>();
                if (documentSnapshot.Exists)
                {
                    richTextBox1.Text += "[HaberID: " + documentSnapshot.Id + "]\n";
                    richTextBox1.Text += haber.HaberBasligi + "\n";
                    richTextBox1.Text += haber.HaberIcerigi + "\n";
                    richTextBox1.Text += haber.Tarih + "\n\n";
                }
            }

        }

        private void run_Click(object sender, EventArgs e)
        {

        }

        private void techInside_Click(object sender, EventArgs e)
        {
            IWebDriver driver1 = new ChromeDriver();
            driver1.Navigate().GoToUrl("https://www.techinside.com/yapay-zeka/");

            string haber_site = "Tech inside";

            for (int i = 1; i <= 10; i++)
            {
                string haber_tarih = driver1.FindElement(By.XPath("//*[@id=\"tdi_70\"]/div[" + i + "]/div/div[2]/div[2]/span/span[2]/time")).Text;

                int x = haber_tarih.IndexOf("|");
                int y = haber_tarih.IndexOf(":");
                int haber_saat = Int32.Parse(haber_tarih.Substring(x + 2, y - x - 2));
                int haber_dakika = Int32.Parse(haber_tarih.Substring(y + 1));



                DateTime now = DateTime.Now;
                string tarih = now.ToString("dd MMMM yyyy", new CultureInfo("tr-TR"));
                int saat = Int32.Parse(now.ToString("HH", new CultureInfo("tr-TR")));
                int dakika = Int32.Parse(now.ToString("mm", new CultureInfo("tr-TR")));

                if (haber_tarih.Substring(0, --x).ToLower().Equals(tarih.ToLower()) && haber_saat <= saat)
                {

                    IWebElement link = driver1.FindElement(By.XPath("//*[@id=\"tdi_70\"]/div[" + i + "]/div/div[2]/h3/a"));

                    IJavaScriptExecutor js = (IJavaScriptExecutor)driver1;
                    js.ExecuteScript("arguments[0].click();", link);

                    Thread.Sleep(1000);

                    var windowHandles = driver1.WindowHandles;

                    driver1.SwitchTo().Window(windowHandles[1]);

                    string haberBasligi = driver1.FindElement(By.XPath("//*[@id=\"tdi_60\"]/div/div/div/div[3]/div/h1")).Text;
                    string icerik = "";

                    foreach (IWebElement test in driver1.FindElements(By.XPath("//*[@id=\"tdi_72\"]/div/div[2]/div/div[3]/div/p")))
                    {
                        icerik += test.Text;
                    }
                    Add_Document_with_CustomID(haber_site, EncodeTextForFirestore(haberBasligi), icerik, haber_tarih);
                    driver1.Close();
                    driver1.SwitchTo().Window(windowHandles[0]);
                }
                else
                {
                    driver1.Close();
                }
            }
            driver1.Close();
        }

        private void shiftDelete_Click(object sender, EventArgs e)
        {
            IWebDriver driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://shiftdelete.net/yapay-zeka");

            string site = "ShiftDelete.Net";

            for (int i = 1; i <= 10; i++)
            {
                IWebElement link = driver.FindElement(By.XPath("//*[@id=\"wrapper\"]/div[4]/div/div/div/div[1]/div[" + i + "]/div/div[1]/h4/a"));

                //MessageBox.Show(newsHeader);
                IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
                js.ExecuteScript("arguments[0].click();", link);

                Thread.Sleep(1000);
                var windowHandles = driver.WindowHandles;
                driver.SwitchTo().Window(windowHandles[1]);
                string newsHeader = driver.FindElement(By.XPath("html/body/div/div[2]/div/div/article/div/div/div[1]/header/h1")).Text;


                string contents = "";
                foreach (IWebElement paragraphs in driver.FindElements(By.XPath("/html/body/div/div[2]/div/div/article/div/div/div[2]/div[1]/div[2]/p")))
                {
                    contents += paragraphs.Text;
                    MessageBox.Show(contents);
                }
                driver.Close();
                driver.SwitchTo().Window(windowHandles[0]);

            }
        }


        private void technologyreview_Click(object sender, EventArgs e)
        {

            IWebDriver driver1 = new ChromeDriver();
            driver1.Navigate().GoToUrl("https://www.technologyreview.com/topic/artificial-intelligence/");

            string news_site = "Technology Review";

            IJavaScriptExecutor jsExecutor = (IJavaScriptExecutor)driver1;

            for (int i = 1; i <= 9; i++)
            {
                string script = @"
                    var timeElement = document.evaluate('//*[@id=""content""]/div/div[1]/ul/li[" + i + "]/div/div[1]/div/div/time', document, null, " +
                    "XPathResult.FIRST_ORDERED_NODE_TYPE, null).singleNodeValue;" +
                    "return timeElement.textContent.trim();" +
                    "";

                string timeText = (string)jsExecutor.ExecuteScript(script);

                DateTime currentTime = DateTime.Now;
                DateTime postTime = currentTime.Add(-ParseTime(timeText));
                string date = currentTime.ToString();


                if (postTime.ToString("dd.MM.yyyy").Equals(date.Substring(0, date.IndexOf(" "))))
                {
                    string news_date = postTime.ToString("dd.MM.yyyy");

                    IWebElement link = driver1.FindElement(By.XPath("//*[@id=\"content\"]/div/div[1]/ul/li[" + i + "]/div/div[1]/header/h3/a"));
                    jsExecutor.ExecuteScript("arguments[0].click();", link);

                    WebDriverWait wait = new WebDriverWait(driver1, TimeSpan.FromSeconds(10));

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
                        driver1.SwitchTo().Frame(iframe);

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
                        driver1.SwitchTo().DefaultContent();
                    }

                    string news_Header = driver1.FindElement(By.XPath("/html/body/div[1]/div[1]/main/div[1]/div[1]/header/div[1]/div[1]/h1")).Text;

                    script = @"
                    var paragraphs = document.evaluate('//*[@id=""content--body""]//p', document, null, 
                        XPathResult.ORDERED_NODE_SNAPSHOT_TYPE, null);
                    var texts = '';
                    for (var i = 0; i < paragraphs.snapshotLength; i++) {
                        texts += paragraphs.snapshotItem(i).innerText + ' ';
                    }
                    return texts.trim();
                    ";

                    string allParagraphTexts = (string)jsExecutor.ExecuteScript(script);
                    Add_Document_with_CustomID(news_site, EncodeTextForFirestore(news_Header), allParagraphTexts, news_date);

                    driver1.Navigate().Back();
                }
                else
                {
                    break;
                }
            }
            driver1.Close();
        }
        private void mashable_Click(object sender, EventArgs e)
        {
            IWebDriver driver1 = new ChromeDriver();
            driver1.Navigate().GoToUrl("https://mashable.com/category/artificial-intelligence");

            string news_site = "Mashable";


            IJavaScriptExecutor jsExecutor = (IJavaScriptExecutor)driver1;
            bool morePost = true;
            
            for(int i=1; i<=3; i++)
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
                    string news_Header = driver1.FindElement(By.XPath("/html/body/header/h1")).Text;

                    script = @"
                    var paragraphElements = document.querySelectorAll('#article p');
                    var text = '';
                    paragraphElements.forEach(function(paragraph) {
                        text += paragraph.textContent + ' ';
                    });
                    return text.trim();
                    ";
                    string newsText = (string)jsExecutor.ExecuteScript(script);

                    Add_Document_with_CustomID(news_site, EncodeTextForFirestore(news_Header), newsText, newsDate);
                    driver1.Navigate().Back();
                }
                else
                {
                    morePost = false;
                    driver1.Close();
                    break;
                }
            }
            int a = 1;
            while (morePost)
            {
                string script = @"var timeElement = document.evaluate('/html/body/main/section/section/div/div[@class=""w-full""][" + a+"]/div/div/div[2]/time', document, null, XPathResult.FIRST_ORDERED_NODE_TYPE, null).singleNodeValue;" +
                "return timeElement.textContent;";
                string newsDate = (string)jsExecutor.ExecuteScript(script);
                newsDate = newsDate.Trim();
                DateTime date = DateTime.ParseExact(newsDate, "MM/dd/yyyy", null);
                newsDate = date.ToString("dd.MM.yyyy");

                string now = DateTime.Now.ToString();
                string dateNow = now.Substring(0, now.IndexOf(" "));

                if (newsDate.Equals(dateNow)){
                    script = @"
                    var link = document.evaluate('/html/body/main/section/section/div/div["+a+"]/div/div/a', document, null, XPathResult.FIRST_ORDERED_NODE_TYPE, null).singleNodeValue;" +
                    "if (link){link.click();}else{console.log('Link not found');}";
                    jsExecutor.ExecuteScript(script);

                    string news_Header = driver1.FindElement(By.XPath("/html/body/header/h1")).Text;

                    script = @"
                    var paragraphElements = document.querySelectorAll('#article p');
                    var text = '';
                    paragraphElements.forEach(function(paragraph) {
                        text += paragraph.textContent + ' ';
                    });
                    return text.trim();
                    ";
                    string newsText = (string)jsExecutor.ExecuteScript(script);

                    Add_Document_with_CustomID(news_site, EncodeTextForFirestore(news_Header), newsText, newsDate);
                    driver1.Navigate().Back();
                    a++;
                }
                else
                {
                    driver1.Close();
                    break;
                }
            }

        }

        static TimeSpan ParseTime(string timeText)
        {
            if (timeText.Contains("hours"))
            {
                int hours = int.Parse(timeText.Split(' ')[0]);
                return TimeSpan.FromHours(hours);
            }
            else if (timeText.Contains("mins"))
            {
                int minutes = int.Parse(timeText.Split(' ')[0]);
                return TimeSpan.FromMinutes(minutes);
            }
            else if (timeText.Contains("days"))
            {
                int days = int.Parse(timeText.Split(' ')[0]);
                return TimeSpan.FromDays(days);
            }
            else if (timeText.Contains("week") || timeText.Contains("weeks"))
            {
                int weeks = int.Parse(timeText.Split(' ')[0]);
                return TimeSpan.FromDays(weeks * 7);
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

        void frekans(string text)
        {
            // Metni temizleme ve kelimelere ayırma
            string cleanedText = Regex.Replace(text.ToLower(), @"[^\w\s]", ""); // Noktalama işaretlerini kaldır ve küçük harfe çevir
            string[] words = cleanedText.Split(new[] { ' ', '\t', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

            // Kelime frekanslarını hesaplamak için bir sözlük oluşturma
            Dictionary<string, int> wordFrequency = new Dictionary<string, int>();

            foreach (var word in words)
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

            // Sonuçları yazdırma
            foreach (var kvp in wordFrequency)
            {
                MessageBox.Show($"Kelime: {kvp.Key}, Frekans: {kvp.Value}");
            }
        }

    }
}