using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Linq;
using HabrParser.Contracts;
using HabrParser.Models;

namespace HabrParser.Services;

public class FeedParsingService : IFeedParsingService
{
    public FeedParsingService()
    {
    }

    public async Task<List<Article>> ParseRSSPage(string rssContent)
    {
        XDocument xDocumnet = XDocument.Parse(rssContent);
        var items = xDocumnet.Root.Descendants()
            .First(i => i.Name.LocalName == "channel")
            .Elements()
            .Where(i => i.Name.LocalName == "item");

        return items.Select(item => ParseArticle(item)).ToList();
    }

    private Article ParseArticle(XElement item)
    {
        return new Article
        {
            Id = ParseId(item.Element("guid").Value),
            Creator = item.Element("{http://purl.org/dc/elements/1.1/}creator").Value,
            Title = item.Element("title").Value,
            Content = StripHTML(item.Element("description").Value),
            PublishedAt = DateTime.Parse(item.Element("pubDate").Value),
            Link = item.Element("link").Value,
            ImageLink = TryExtractImageLink(item.Element("description").Value)
        };
    }

    private int ParseId(string linkId)
    {
        var uri = new Uri(linkId);
        var segment = uri.Segments.Last();
        segment = segment.Remove(segment.Length - 1);
        return int.Parse(segment);
    }
    
    private string StripHTML(string text)
    {
       return Regex.Replace(text, "<.*?>", String.Empty);
    }
    
    private string? TryExtractImageLink(string text)
    {
        Regex regex = new Regex(@"<img\s.*?src=(?:'|\"")([^'\"">]+)(?:'|\"").*?\/?>");
        Match match = regex.Match(text);
        if (match.Success)
        {
            XmlDocument xDoc = new XmlDocument();
            var imgTag = match.Value;
            if (imgTag.EndsWith("/>"))
                xDoc.LoadXml(match.Value);
            else
                xDoc.LoadXml(match.Value + "</img>");
            return xDoc.DocumentElement.Attributes["src"].Value;
        }
        return null;
    }
}