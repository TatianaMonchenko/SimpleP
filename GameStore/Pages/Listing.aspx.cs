using GameStore.Models;
using GameStore.Models.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GameStore.Pages
{
    public partial class Listing : System.Web.UI.Page
    {
        private Repository repository = new Repository();
        private int pageSize = 4;
        protected int CurrentPage
        {
            get
            {
                int page;
                page = int.TryParse(Request.QueryString["page"], out page) ? page : 1;
                return page > MaxPage ? MaxPage : page;
            }
        }
        protected int MaxPage
        {
            get
            {
                return (int)Math.Ceiling((decimal)repository.Games.Count() / pageSize);
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected IEnumerable<Game> GetGames()
        {
            return repository.Games
               .OrderBy(g => g.GameId)
               .Skip((CurrentPage - 1) * pageSize)
               .Take(pageSize);

        }
        public string FillList()
        {
            StringBuilder html = new StringBuilder();
            foreach (GameStore.Models.Game game in GetGames())
            {
                html.Append(String.Format(@"<div class='item'><h3>{0}</h3>{1}<h4>{2:c}</h4></div>",
                    game.Name, game.Description, game.Price));
            }
            return html.ToString();
        }
    }
    
}
