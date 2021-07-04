using Arbetsprov.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml;
using System.Collections.Generic;

namespace Arbetsprov.Controllers {
    public class ApiController : Controller {

        private static readonly HttpClient client = new HttpClient();

        [HttpGet("api/search")]
        public async Task<List<SearchResult>> SearchAsync([FromQuery] string query)
        {
            var responseString = await client.GetStringAsync(query);

            return SearchResultsFromXML(responseString);
        }

        public List<SearchResult> SearchResultsFromXML(string responseString)
        {
            //Creating the list which will hold all the results of the query
            List<SearchResult> searchResultList = new List<SearchResult>();

            XmlDocument doc = new XmlDocument();
            doc.LoadXml(responseString);

            //Gets a list of all the records found by the query
            XmlNodeList recordList = doc.GetElementsByTagName("record");

            //Looping through each record
            foreach (XmlNode record in recordList)
            {
                //For each record a SearchResult is created, populated and added to the searchResultList                
                searchResultList.Add(recordToSearchResult(record));
            }

            return searchResultList;
        }

        public SearchResult recordToSearchResult(XmlNode record)
        {
            SearchResult result = new SearchResult();

            //Looping over all the children of the record to find the ones with the correct tag.
            foreach (XmlNode child in record.ChildNodes)
            {
                if (child.Name == "datafield")
                {
                    string tag = child.Attributes["tag"].Value;

                    switch (tag)
                    {
                        case "020":
                            foreach (XmlNode node in child.ChildNodes)
                            {
                                if (node.Attributes["code"].Value == "a")
                                {
                                    result.HasIBM = true;
                                }
                            }
                            break;

                        case "100":
                            foreach (XmlNode node in child.ChildNodes)
                            {
                                if (node.Attributes["code"].Value == "a")
                                {
                                    result.Author = node.InnerXml;
                                }

                                if (result.HasIBM == true && node.Attributes["code"].Value == "0")
                                {
                                    result.Source = node.InnerXml;
                                }
                            }
                            break;
                        case "245":
                            foreach (XmlNode node in child.ChildNodes)
                            {
                                if (node.Attributes["code"].Value == "a")
                                {
                                    result.Title = node.InnerXml;
                                }
                            }
                            break;
                    }
                }
            }
            return result;
        }
    }
}