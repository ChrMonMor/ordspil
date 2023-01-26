using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace ordspil.Models
{
    [Serializable()]
    public class SecretWordModel : ISerializable
    {
        public string SecretWord { get; set; }
        public string[] WordArray { get; set; }
        public string[] OldGuesses { get; set; }
        public string[] TriedLetters { get; set; }
        public SecretWordModel(int min, int max) 
        {
            WordArray = editwordlist.Program.WordListDansk(min, max);
            SecretWord = WordArray[new Random().Next(0, WordArray.Length)];
            OldGuesses = new string[] { };
            TriedLetters = new string[] { };
        }
        public override string ToString() => SecretWord;

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("secretword", SecretWord);
            info.AddValue("oldguesses", OldGuesses);
            info.AddValue("wordArray", WordArray);
            info.AddValue("triedLetters", TriedLetters);
        }

        public SecretWordModel(SerializationInfo info, StreamingContext context) 
        {
            SecretWord = (string)info.GetValue("secretword", typeof(string));
            OldGuesses = (string[])info.GetValue("oldguesses", typeof(string[]));
            WordArray = (string[])info.GetValue("wordArray", typeof(string[]));
            TriedLetters = (string[])info.GetValue("triedLetters", typeof(string[]));

        }
        public SecretWordModel() { }
        public void AddGuess(string word, string answer)
        {
            if (OldGuesses.Count() == 0)
            {
                OldGuesses = new string[] {word};
                TriedLetters = new string[] {answer};
                return;
            }
            string[] ary = new string[OldGuesses.Count()+1];
            string[] ary1 = new string[TriedLetters.Count() + 1];
            for (int i = 0; i < ary.Length; i++)
            {
                if (OldGuesses.Count() == i)
                {
                    ary[i] = word;
                    ary1[i] = answer;
                    continue;
                }
                ary[i] = OldGuesses[i];
                ary1[i] = TriedLetters[i];
            }
            OldGuesses = ary;
            TriedLetters = ary1;
        }
    }
}