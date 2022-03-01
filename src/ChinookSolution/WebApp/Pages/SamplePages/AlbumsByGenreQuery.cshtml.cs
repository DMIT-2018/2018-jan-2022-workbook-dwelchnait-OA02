#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

#region Additional Namespaces
using ChinookSystem.BLL;
using ChinookSystem.ViewModels;
#endregion


namespace WebApp.Pages.SamplePages
{
    public class AlbumsByGenreQueryModel : PageModel
    {
        #region Private varailable and DI constructor
        private readonly ILogger<IndexModel> _logger;
        private readonly AlbumServices _albumServices;
        private readonly GenreServices _genreServices;

        public AlbumsByGenreQueryModel(ILogger<IndexModel> logger,
                            AlbumServices albumservices,
                            GenreServices genreservices)
        {
            _logger = logger;
            _albumServices = albumservices;
            _genreServices = genreservices;
        }
        #endregion

        #region FeedBack and ErrorHandling
        [TempData]
        public string FeedBack { get; set; }
        public bool HasFeedBack => !string.IsNullOrWhiteSpace(FeedBack);

        [TempData]
        public string ErrorMsg { get; set; }
        public bool HasErrorMsg => !string.IsNullOrWhiteSpace(ErrorMsg);
        #endregion

        [BindProperty]
        public List<SelectionList> GenreList { get; set; }

        [BindProperty(SupportsGet = true)]
        public int? GenreId { get; set; }

        [BindProperty]
        public List<AlbumsListBy> AlbumsByGenre { get; set; }

        public void OnGet()
        {
            //OnGet is executed as the page first is processed (as it comes up)

            //consume a service: GetAllGenres in register services of _genreServices
            GenreList = _genreServices.GetAllGenres();
            //sort the List<T> using the method .Sort
            GenreList.Sort((x,y) => x.DisplayText.CompareTo(y.DisplayText));

            //remember that this method executes as the page FIRST comes up BEFORE
            //      anything has happened on the page (including the FIRST display)
            //any code in this method MUST handle the possibility of missing data for the query argument

            if (GenreId.HasValue && GenreId.Value > 0)
            {
                AlbumsByGenre = _albumServices.AlbumsByGenre((int)GenreId);
            }
        }

        public IActionResult OnPost()
        {
            if(GenreId == 0)
            {
                //prompt line test
                FeedBack = "You did not select a genre";
            }
            else
            {
                FeedBack = $"You select genre id of {GenreId}";
            }
            return RedirectToPage(new {GenreId = GenreId }); //cause a Get request which forces OnGet execution
        }
    }
}
