using System;
using System.Collections.Generic;

namespace IgpayAtinlayAblay
{
    class Program
    {
        static List<char> vowels = new List<char> { 'a', 'e', 'i', 'o', 'u' };
        static List<char> punctuation = new List<char> { '.', ',', '!', '?' };

        static void Main(string[] args)
        {
            Console.Title = "Pig Latin Translation Tool";
            Console.WriteLine("Welcome to the Pig Latin Translation Tool!");

            bool cont = true;
            while (cont)
            {
                string input = GetUserInput("Please enter a sentence to be translated: ");

                string[] words = input.Split(" ");
                string translated = "";
                foreach(string s in words)
                {
                    translated += ToPigLatin(s) + " ";
                }
                translated = translated.Trim();
                Console.WriteLine(translated);
                Console.Write("Enter y(es) to continue or anything else to exit: ");
                cont = Console.ReadLine().ToLower().StartsWith('y');
            }
        }

        static string GetUserInput(string prompt)
        {
            string ret = "";
            while (ret.Equals(""))
            {
                Console.Write(prompt);
                ret = Console.ReadLine();

                if (ret.Equals(""))
                {
                    Console.WriteLine("Input cannot be empty! Please try again.");
                }
            }

            return ret;
        }

        static string ToPigLatin(string word)
        {
            string wordOrg = word;
            string consonants = "";
            string punctuation = "";
            word = word.ToLower();

            //first special case: make sure there are no 'bad' characters (that we don't translate) in the string
            foreach (char c in word)
            {
                if (!OkayToTranslate(c))
                {
                    return wordOrg;
                }
                if (IsPunctuation(c)) //it's easier to handle punctuation here, since the last loop will end when a vowel is encountered
                {
                    punctuation += c;
                    continue;
                }
            }

            foreach (char c in punctuation) // since the punctuation is handled, we remove all of it from 
            {
                word = word.Replace(c.ToString(), "");
                wordOrg = wordOrg.Replace(c.ToString(), "");
            }

            foreach (char c in vowels) // if the first letter is a vowel
            {
                if (word[0] == c)
                {
                    return wordOrg+"way";
                }
            }

            for (int i = 0; i < word.Length; i++)
            {
                char c = word[i];

                if (IsVowel(c))
                {
                    return wordOrg.Substring(i) + consonants + "ay" + punctuation;
                }

                consonants += wordOrg[i];
            }
            return wordOrg;
        }

        static bool OkayToTranslate(char c)
        {
            return (c >= 'a' && c <= 'z') 
                || (c >= 'A' && c <= 'Z') 
                || (c == '.') 
                || (c == ',') 
                || (c == '!') 
                || (c == '?') 
                || (c == '\''); 
        }
        static bool IsPunctuation(char c)
        {
            return punctuation.Contains(c);
        }
        static bool IsVowel(char c)
        {
            return vowels.Contains(c);
        }
    }
}