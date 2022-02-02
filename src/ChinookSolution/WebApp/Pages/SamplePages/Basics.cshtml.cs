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
    }
}
