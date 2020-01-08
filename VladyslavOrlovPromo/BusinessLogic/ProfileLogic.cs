using System;
using System.Linq;
using VladyslavOrlovPromo.DataAccess;
using HtmlAgilityPack;

namespace VladyslavOrlovPromo.BusinessLogic
{
    public class ProfileLogic
    {
        public string GetCurrentRanking()
        {
            //Ranking get updated on Mondays

            DateTime today = DateTime.Today;
            EntityDataModel model = new EntityDataModel();

            var ranking = model.Rankings
                    .OrderByDescending(r => r.DateInserted)
                    .FirstOrDefault();

            if (ranking == null)
            {
                ranking = new Ranking
                {
                    DateInserted = DateTime.Today,
                    Ranking1 = FetchAtpRanking()
                };

                model.Rankings.Add(ranking);
                model.SaveChanges();
            }
            else if (today.DayOfWeek == DayOfWeek.Monday &&
                     ranking.DateInserted.HasValue &&
                     ranking.DateInserted.Value.Date < today)
            {
                ranking.Ranking1 = FetchAtpRanking();
                ranking.DateInserted = DateTime.Today;

                model.Rankings.Add(ranking);
                model.SaveChanges();
            }
            else
            {
                DateTime dt = DateTime.Today;

                while (dt.DayOfWeek != DayOfWeek.Monday)
                {
                    dt = dt.AddDays(-1);
                }

                if (ranking.DateInserted < dt)
                {
                    ranking.Ranking1 = FetchAtpRanking();
                    ranking.DateInserted = DateTime.Today;

                    model.Rankings.Add(ranking);
                    model.SaveChanges();
                }
            }

            return ranking.Ranking1;
        }

        private string FetchAtpRanking()
        {
            HtmlWeb web = new HtmlWeb();
            HtmlDocument document = web.Load("http://www.atpworldtour.com/en/players/vladyslav-orlov/o659/rankings-history?ajax=true");

            //HtmlNode node = document.DocumentNode.SelectNodes("//table[@class='mega-table']").First();
            HtmlNode node = document.DocumentNode.SelectNodes("//table[@class='mega-table']//tbody//tr").First();

            return string.Format("<table class=\"rank\"><thead><tr><th>Last Updated Date</th><th>Singles</th><th>Doubles</th></tr></thead><tbody><tr>{0}</tr></tbody></table>", node.InnerHtml);
        }
    }
}