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
        [BindProperty(SupportsGet = true)]
        public string[] Genres { get; set; }


        /// <summary>
        /// The movies to display on the index page 
        /// </summary>
        public IEnumerable<Movie> Movies { get; protected set; }


        /// <summary>
        /// Gets and sets the search terms
        /// </summary>
        [BindProperty(SupportsGet = true)]
        public string SearchTerms { get; set; }

        /// <summary>
        /// Gets and sets the MPAA rating filters
        /// </summary>
        [BindProperty(SupportsGet = true)]
        public string[] MPAARatings { get; set; }


        /// <summary>
        /// Gets and sets the IMDB minimium rating
        /// </summary>
        [BindProperty(SupportsGet = true)]
        public double? IMDBMin { get; set; }

        /// <summary>
        /// Gets and sets the IMDB maximum rating
        /// </summary>
        [BindProperty(SupportsGet = true)]
        public double? IMDBMax { get; set; }

        /// <summary>
        /// Gets and sets the Rotten Tomato minimium rating
        /// </summary>
        [BindProperty(SupportsGet = true)]
        public double? RTMin { get; set; }

        /// <summary>
        /// Gets and sets the Rotten Tomato maximum rating
        /// </summary>
        [BindProperty(SupportsGet = true)]
        public double? RTMax { get; set; }

        /// <summary>
        /// Does the response initialization for incoming GET requests
        /// </summary>
        public void OnGet(double? IMDBMin, double? IMDBMax, double? RTMin, double? RTMax )
        {
            Movies = MovieDatabase.All;
            // Search movie titles for the SearchTerms
            if (SearchTerms != null)
            {
                Movies = Movies.Where(movie => 
                    movie.Title != null &&
                    movie.Title.Contains(SearchTerms, StringComparison.InvariantCultureIgnoreCase));
            }

            // Filter by MPAA Rating 
            if (MPAARatings != null && MPAARatings.Length != 0)
            {
                Movies = Movies.Where(movie =>
                    movie.MPAARating != null &&
                    MPAARatings.Contains(movie.MPAARating));
            }
            
            // Filter by Genre
            if(Genres != null && Genres.Length != 0)
            {
                Movies = Movies.Where(movie =>
                    movie.MajorGenre != null &&
                    Genres.Contains(movie.MajorGenre));
            }

            //Filter by IMDB Rating
            if(IMDBMin != null || IMDBMax != null)
            {
                Movies = Movies.Where(movie =>
                    movie.IMDBRating != null &&
                    (IMDBMin != null && IMDBMax == null && movie.IMDBRating >= IMDBMin) ||
                    (IMDBMin == null && IMDBMax != null && movie.IMDBRating <= IMDBMax) ||
                    (IMDBMin != null && IMDBMax != null && movie.IMDBRating >= IMDBMin && movie.IMDBRating <= IMDBMax));
            }

            //Filter by Rotten Tomatoes
            if (RTMin != null || RTMax != null)
            {
                Movies = Movies.Where(movie =>
                    movie.RottenTomatoesRating != null &&
                    (RTMin != null && RTMax == null && movie.RottenTomatoesRating >= RTMin) ||
                    (RTMin == null && RTMax != null && movie.RottenTomatoesRating <= RTMax) ||
                    (RTMin != null && RTMax != null && movie.RottenTomatoesRating >= RTMin && movie.RottenTomatoesRating <= RTMax));
            }
        }

    }
}