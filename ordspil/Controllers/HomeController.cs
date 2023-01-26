using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using editwordlist;
using ordspil.Models;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Policy;
using Antlr.Runtime.Tree;

namespace ordspil.Controllers
{
    public class HomeController : Controller
    {
        public SecretWordModel Game = new SecretWordModel(4, 4);

        public ActionResult Index()
        {
            SecretWordModel Game = StartGame(5,5);
            ViewBag.GuessResult = "0000000000000000000000000000000000000000000000000000000000000";
            ViewBag.SecretWordLenght = Game.SecretWord.Length;
            ViewBag.OldGuesses = Game.OldGuesses;
            ViewBag.OldResults = Game.TriedLetters;

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
            SecretWordModel GetGame = GetSecretWord();
            string newAnswer =  CheckingGuess(test, GetGame.SecretWord);
            GetGame = UpdateData(test, newAnswer, GetGame);
            ViewBag.GuessResult = newAnswer;
            ViewBag.SecretWordLenght = GetGame.SecretWord.Length;
            ViewBag.OldGuesses = GetGame.OldGuesses;
            ViewBag.OldResults = GetGame.TriedLetters;

            return View();
        }
        [HttpGet]
        public ActionResult Index(int? min, int? max)
        {
            if (min == null)
            {
                min = 5;
            }
            if (max == null)
            {
                max = 5;
            }
            SecretWordModel Game = StartGame((int)min, (int)max);
            ViewBag.GuessResult = "0000000000000000000000000000000000000000000000000000000000000";
            ViewBag.SecretWordLenght = Game.SecretWord.Length;
            ViewBag.OldGuesses = Game.OldGuesses;
            ViewBag.OldResults = Game.TriedLetters;
            return View();
        }
        private SecretWordModel StartGame(int min, int max)
        {
            SecretWordModel Game = new SecretWordModel(min, max);
            Stream stream = System.IO.File.Open("Game.dat", FileMode.Open);
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(stream, Game);
            stream.Close();
            return Game;
        }
        private SecretWordModel GetSecretWord()
        {
            Stream stream = System.IO.File.Open("Game.dat",FileMode.Open);
            BinaryFormatter bf = new BinaryFormatter();
            SecretWordModel GetGame = (SecretWordModel)bf.Deserialize(stream);
            stream.Close();
            return GetGame;
        }
        private SecretWordModel UpdateData(string userGuess, string answer,SecretWordModel game)
        {
            game.AddGuess(userGuess, answer);
            Stream stream = System.IO.File.Open("Game.dat", FileMode.Open);
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(stream, game);
            stream.Close();
            return game;
        }
        // 0: Letter is not in word
        // 1: Letter is in Word, but not this position
        // 2: Correct letter for this position
        private string CheckingGuess(string guess, string secretWord)
        {
            string result = string.Empty;
            string correct = string.Empty;
            string wrongplace = string.Empty;
            for (int i = 0; i < secretWord.Length; i++)
            {
                if (guess[i] == secretWord[i])
                {
                    result += "2";
                    correct += guess[i];
                }
                else if (secretWord.Substring(i, secretWord.Length-i).Contains(guess[i]))
                {
                    result += "1";

                    wrongplace += guess[i];
                }
                else if (secretWord.Substring(0,i).Contains(guess[i]))
                {
                    result += "1";
                    wrongplace += guess[i];
                }
                else
                {
                    result += "0";
                }
            }
            return result;
        }
    }
}