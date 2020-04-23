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
        /// The filtered genres
        /// </summary>
        [BindProperty]
        public string[] Genres { get; set; }


        /// <summary>
        /// The movies to display on the index page 
        /// </summary>
        public IEnumerable<Movie> Movies { get; protected set; }


        /// <summary>
        /// Gets and sets the search terms
        /// </summary>
        [BindProperty]
        public string SearchTerms { get; set; } = " ";

        /// <summary>
        /// Gets and sets the MPAA rating filters
        /// </summary>
        [BindProperty]
        public string[] MPAARating { get; set; } = new string[0];


        /// <summary>
        /// Gets and sets the IMDB minimium rating
        /// </summary>
        [BindProperty]
        public double? IMDBMin { get; set; }

        /// <summary>
        /// Gets and sets the IMDB maximum rating
        /// </summary>
        [BindProperty]
        public double? IMDBMax { get; set; }

        /// <summary>
        /// Gets and sets the Rotten Tomato minimium rating
        /// </summary>
        [BindProperty]
        public double? RTMin { get; set; }

        /// <summary>
        /// Gets and sets the Rotten Tomato maximum rating
        /// </summary>
        [BindProperty]
        public double? RTMax { get; set; }

        /// <summary>
        /// Does the response initialization for incoming GET requests
        /// </summary>
        public void OnGet()
        {
            Movies = MovieDatabase.All;
            
        }

        public void OnPost()
        {
            Movies = MovieDatabase.Search(SearchTerms);
            Movies = MovieDatabase.FilterByMPAARating(Movies, MPAARating);
            Movies = MovieDatabase.FilterByIMDBRating(Movies, IMDBMin, IMDBMax);
            Movies = MovieDatabase.FilterByGenre(Movies, Genres);
            Movies = MovieDatabase.FilterByRottenTomatoRatings(Movies, RTMin, RTMax);
        }

    }
}