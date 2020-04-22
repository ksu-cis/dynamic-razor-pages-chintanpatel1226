using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Movies.Pages
{
    public class IndexModel : PageModel
    {
        /// <summary>
        /// The movies to display on the index page
        /// </summary>
        public IEnumerable<Movie> Movies { get; protected set; }

        /// <summary>
        /// The current search terms
        /// </summary>
        public string SearchTerms { get; set; }

        /// <summary>
        /// Invokes every time the page is requested using a GET request. 
        /// </summary>
        public void OnGet()
        {
            String terms = Request.Query["SearchTerms"];

        }
    }
}
