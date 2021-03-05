using System;
using HtmlAgilityPack;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace FinParser
{
    public class Parse
    {
        public static decimal NowPrice(string Ticker, ref System.Net.WebClient wc, ref HtmlDocument html)
        {
            decimal output_price = 0;
            html.LoadHtml(wc.DownloadString("https://finance.yahoo.com/quote/" + Ticker));
            HtmlNodeCollection nodes = html.DocumentNode.SelectNodes("//div");

            foreach (var tag in nodes)
            {

                foreach (var attr in tag.Attributes)
                {
                    if (attr.Value == "D(ib) Mend(20px)")
                    {
                        try
                        {
                            output_price = decimal.Parse(tag.ChildNodes[0].InnerText.Replace(".", ","));
                        }
                        catch
                        {
                            output_price = 0;
                        }
                    }
                }
            }
            int t = nodes.Count - 1;

            for (int j = t; j > 0; j--)
            {
                nodes.Remove(j);
            }

            return output_price;
        }
    }

}
