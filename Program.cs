using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace practic
{
    class Program
    {
        static void Main(string[] args)
        {
            Hangman hang = new Hangman();
            Other other = new Other();

            Console.WriteLine("Welcom to hangman\n" +
                "You want to play?\n" +
                "Or you want to read rules?(type 'rules')");
            string ans = Console.ReadLine();

            if (ans == "rules")
            {
                Console.WriteLine(other.Rules());
                other.StartOfGame();

            }
            else
            {
                hang.MainLoop();
            }

        }
    }
    class Hangman
    {
        private string Switch_a_word()
        {
            Random rand = new Random();
            string path = @"C:\Users\User\Desktop\CODE\C#\Hangman\Data\Words.txt";
            List<string> words = new List<string>();

            using (StreamReader sr = File.OpenText(path))
            {
                string s;
                while ((s = sr.ReadLine()) != null)
                {
                    words.Add(s);
                }
            }

            int randomNum = rand.Next(1, words.Count);

            return words[randomNum];
        }

        private string Convert_word()
        {
            string word = Switch_a_word();
            string conv_word = new string('*', word.Length);
            return conv_word;
        }
        public void MainLoop()
        {
            string word = Switch_a_word();
            string conv_word = Convert_word();
            int lose_points = 0;


            while (true)
            {
                Console.WriteLine("Your word : {0}", conv_word);

                string a = Console.ReadLine();
                

                int chet = 0;
                for (int i = 0; i != word.Length; i++)
                {
                    if (word[i] == Convert.ToChar(a))
                    {
                        chet++;
                    }
                }
                if (chet >= 2)
                {
                    conv_word = TwoOrMoreCharsInWord(word, conv_word, a, chet);
                }
                else if (chet == 1)
                {
                    conv_word = OneCharInWord(word, conv_word, a);
                }
                else
                {
                    lose_points++;
                    Console.WriteLine("{0} attempts remain", 10 - lose_points);
                    if (lose_points == 10)
                    {
                        Output(word, "Lose");
                    }
                }
                if (conv_word == word)
                {
                    Output(word, "Win");
                }

            }
        }
        private string OneCharInWord(string word, string conv_word, string symb)
        {
            if (word.Contains(symb))
            {
                int replace_indx = word.IndexOf(symb);
                conv_word = conv_word.Remove(replace_indx, 1).Insert(replace_indx, symb);
            }
            return conv_word;
        }
        private string TwoOrMoreCharsInWord(string word, string conv_word, string symb, int rep_chars) //TODO : finish for 3 or more repeated chars
        {
            string cut_copy = word.Remove(word.IndexOf(symb), 1);

            
            int replace_indx = word.IndexOf(symb);
            conv_word = conv_word.Remove(replace_indx, 1).Insert(replace_indx, symb);
            int replace_indx2 = cut_copy.IndexOf(symb) + 1;
            conv_word = conv_word.Remove(replace_indx2, 1).Insert(replace_indx2, symb);
            return conv_word;

        }
        private void Output(string word, string state)
        {
            Console.WriteLine("********You {0}********\n" +
                              "Your word : {1}", state, word);
            Console.WriteLine("Want to play again ?");
            string ans = Console.ReadLine();
            if (ans.ToLower() == "yes")
            {
                MainLoop();
            }
            else
            {
                Environment.Exit(0);
            }
        }
    }
    class Other
    {
        public string Rules()
        {
            return "Enter ONLY ONE LETTER and if it is in the randomly generated word it is shown ,\n" +
                "otherwise you get penalty points and if you guess the word you win\n" +
                "(you have 10 attempts to guess the word)\n";
        }
        public void StartOfGame()
        {
            Hangman hang = new Hangman();

            string[] replic = new string[2] { "Now you want to play?\n", "I think yes\n" };
            for (int i = 0; i != 2; i++)
            {
                Console.WriteLine(replic[i]);
                Thread.Sleep(1000);
            }
            hang.MainLoop();
        }
    }

}
