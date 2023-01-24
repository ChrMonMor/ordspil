using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using editwordlist;
using ordspil.Models;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;
using System.Security.Policy;

namespace ordspil.Controllers
{
    public class HomeController : Controller
    {
        public SecretWordModel Game = new SecretWordModel(4, 4);

        public ActionResult Index()
        {
            StartGame(); 
            ViewBag.SecretWordLenght = Game.SecretWord.Length;
            ViewBag.OldGuesses = Game.OldGuesses;

            return View();
        }
        [HttpPost]
        public ActionResult Index(string[] userGuess)
        {
            string test = "";
            foreach(string word in userGuess) 
            {
                test+=word;
            }
            UpdateGame(test);
            SecretWordModel GetGame = GetSecretWord();
            ViewBag.SecretWordLenght = GetGame.SecretWord.Length;
            ViewBag.OldGuesses = GetGame.OldGuesses;

            return View();
        }
        private void StartGame()
        {
            Stream stream = System.IO.File.Open("Game.dat", FileMode.Open);
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(stream, Game);
            stream.Close();
        }
        private SecretWordModel GetSecretWord()
        {
            Stream stream = System.IO.File.Open("Game.dat",FileMode.Open);
            BinaryFormatter bf = new BinaryFormatter();
            SecretWordModel GetGame = (SecretWordModel)bf.Deserialize(stream);
            stream.Close();
            return GetGame;
        }
        private void UpdateGame(string userGuess)
        {
            SecretWordModel GetGame = GetSecretWord();

            GetGame.AddGuess(userGuess);

            Console.WriteLine(GetGame.OldGuesses);

            XmlSerializer serializer = new XmlSerializer(typeof(SecretWordModel));

            using (TextWriter ms = new StreamWriter(@"C:\Users\chri615w\source\repos\ordspil\Game.xml"))
            {
                serializer.Serialize(ms, GetGame);
            }

        }
    }
}