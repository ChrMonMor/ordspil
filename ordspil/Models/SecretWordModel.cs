using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace ordspil.Models
{
    [Serializable()]
    public class SecretWordModel : ISerializable
    {
        public string SecretWord { get; set; }
        public string[] WordArray { get; set; }
        public string[] OldGuesses { get; set; }
        public SecretWordModel(int min, int max) 
        {
            WordArray = editwordlist.Program.WordListDansk(min, max);
            SecretWord = WordArray[new Random().Next(0, WordArray.Length)];
            OldGuesses = new string[] { };
        }
        public override string ToString() => SecretWord;

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("secretword", SecretWord);
            info.AddValue("oldguesses", OldGuesses);
        }

        public SecretWordModel(SerializationInfo info, StreamingContext context) 
        {
            SecretWord = (string)info.GetValue("secretword", typeof(string));
            OldGuesses = (string[])info.GetValue("oldguesses", typeof(string[]));
        }
        public SecretWordModel() { }
        public void AddGuess(string word)
        {
            if (OldGuesses.Count() == 0)
            {
                OldGuesses = new[] {word};
                return;
            }
            string[] ary = new string[OldGuesses.Count()+1];
            for (int i = 0; i < ary.Length; i++)
            {
                if (OldGuesses.Count() + 1 == i )
                {
                    ary[i] = word;
                    continue;
                }
                ary[i] = OldGuesses[i];
            }
            OldGuesses = ary;
        }
    }
}