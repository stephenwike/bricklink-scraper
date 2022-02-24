// See https://aka.ms/new-console-template for more information

using System.Net;
using BricklinkSharp.Client;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

Console.WriteLine("Hello, World!");

BricklinkClientConfiguration.Instance.TokenValue = "B36C8A65F2884186B589C27FC70821BB";
BricklinkClientConfiguration.Instance.TokenSecret = "85AE883FC5A045818389DC196CD3D722";
BricklinkClientConfiguration.Instance.ConsumerKey = "65B02D870A614787B0E4FCB38D35DF73";
BricklinkClientConfiguration.Instance.ConsumerSecret = "23601E828E0343C79C5AAEA704995C28";

HttpClient httpClient = new HttpClient();
//var client = BricklinkClientFactory.Build(httpClient);
var url = @"https://store.bricklink.com/dwarunek?p=dwarunek#/shop?o=%7B%22sort%22:2,%22pgSize%22:100,%22wantedMoreArrayID%22:%227003995%22,%22bOnWantedList%22:1,%22showHomeItems%22:0%7D";

IWebDriver driver = new ChromeDriver(@"D:\drivers\");
driver.Navigate().GoToUrl(url);

var storeApp = driver.FindElement(By.Id("storeApp"));
var storeContent = storeApp.Get();

var response = await httpClient.GetStringAsync(url).ConfigureAwait(false);
httpClient.Dispose();