using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using editwordlist;

namespace ordspil.Controllers
{
    public class HomeController : Controller
    {
        private string secretWord = "";

        public string SecretWord { get => secretWord; set => secretWord = value; }

        public ActionResult Index()
        {
            string[] ordArray = editwordlist.Program.WordListDansk(3, 5);

            Random random = new Random();
            secretWord = ordArray[random.Next(0, ordArray.Length)];

            ViewBag.SecretWordLenght = secretWord.Length;

            return View();
        }
        [HttpPost]
        public ActionResult index(string userGuess)
        {

            return Content("hyrrat : "+ SecretWord + " : " + 1);
        }
    }
}