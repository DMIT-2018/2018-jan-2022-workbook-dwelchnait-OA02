using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Pages.SamplePages
{
    public class BasicsModel : PageModel
    {
        //basicly this is an  object, treat it as such

        //data fields
        public string MyName;

        //properties

        //the annotation [TempData] stores data until it's read in another
        //  immediate request
        //this annotation attribute has two method called Keep(string) and
        //  Peek(string) (used on Content page)
        //keep int a dictionary (name/value pair)
        //useful to redirect when data is required for more than a single request
        //Implemented by TempData providers using either cookies or session state
        //TempData is NOT bound to any particular control like BindProperty
        [TempData]
        public string FeedBack { get; set; }

        //the annotation BindProperty ties a property in the PageModel class
        //  directly to a control on the Content Page
        //data is transferred between the two automatically
        //on the Content page, the control to use this property will have
        //  a helper-tag called asp-for

        //to retain a vlaue in the control tied to this property AND retained
        //  via the @page use the SupportGet attribute = true
        [BindProperty(SupportsGet = true)]
        public int? id { get; set; }

        //constructors

        //behaviours (aka methods)
        public void OnGet()
        {
            //executes in response to a Get Request from the browser
            //when the page is "first" accessed, the browser issues a Get request
            //when the page is refreshed, WITHOUT a Post request, the browser issues a Get request
            //when the page is retrieved in response to a form's POST using RedirectToPage()
            //IF NOT RedirectToPage() is used on the POST, there is NO Get requested issued
       
            //Server-side processing
            //contains no html

            Random rnd = new Random();
            int oddeven = rnd.Next(0,25);
            if(oddeven % 2 == 0)
            {
                MyName = $"Don is even {oddeven}";
            }
            else
            {
                MyName = null;
            }
        }

        //processing in response to a request from a form on a web page
        //this request is referred to as a Post (method="post")

        //General Post
        //a general post occurs when a asp-page-handler is NOT used
        //the return datatype can be void, hovwever, your will normally
        //  encounter the datatype IActionResult
        //the IActionResult requires some type of request action
        //  on the return statment of the method OnPost()
        //typical actions:
        //  Page()
        //   :does NOT issue a OnGet request
        //   :remains on the current page
        //   :a good action for form processing involving validation
        //      and with the catch of a try/catch
        // RedirectToPage()
        //   :DOES issue a OnGet request
        //   :is used to retaining input values via the @page and your BindProperty
        //      form controls on your form on the Content Page

        public IActionResult OnPost()
        {
            //this line of code is used to cause a delay in processing
            //  so we can see on the Network Activity some type of
            //  simulated processing
            Thread.Sleep(2000);

            //retreive data via the Request object
            //Request: web page to server
            //Response: server to web page
            string buttonvalue = Request.Form["theButton"];
            FeedBack = $"Button press is {buttonvalue} with numeric input of {id}";
            //return Page(); //does not issue a OnGet()
            return RedirectToPage(new {id = id }); //request for OnGet()
        }
    }
}
