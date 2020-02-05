using System;
using System.Collections.Generic;
public class Computer
{
    public Player User;
    public int wordLength;
    public List<string> currentDict;
    public string[] wordStatus;
    public void startGame(){
        System.Console.WriteLine("Welcome to Evil Hangman!  Are you prepared to face your certain demise!?");
        System.Console.WriteLine("Please enter your name");
        string name = Console.ReadLine();
        this.User = new Player(name);
        System.Console.WriteLine($"Welcome, {this.User.Name}!");
        this.calcDifficulty();
    }
    public void calcDifficulty(){
        System.Console.WriteLine(@"Please choose a difficulty:
        (1) Easy: 2-7 Letters
        (2) Medium: 8-13 Letters
        (3) Hard: 14-22 Letters");
        string input = Console.ReadLine();
        Random rand = new Random();
        int min;
        int max;
        if (input == "1"){
            min = 2;
            max = 8;
        }
        else if (input == "2") {
            min = 8;
            max = 14;
        }
        else if (input == "3") {
            min = 14;
            max = 23;
        }
        else {
            System.Console.WriteLine("That is not a valid input, please enter 1, 2, or 3");
            this.calcDifficulty();
            return;
        }
        this.wordLength = rand.Next(min, max);
        this.wordStatus = new string[this.wordLength];

        for (int i=0; i<this.wordStatus.Length; i++){
            this.wordStatus[i] = "_";
        }
        this.currentDict = wordDict.generate(this.wordLength);
        // System.Console.WriteLine($"Playing with a word of length {this.wordLength}");
        // System.Console.WriteLine($"First word in Dict {this.currentDict[0]}");
        this.displayTurn();
        return;
    }

    public void displayTurn(string msg=""){
        Console.Clear();
        System.Console.WriteLine($"***************** Letters you can still choose ***********************");

        foreach( string letter in this.User.AvailLetters){
            System.Console.Write($" {letter}");
        }
        System.Console.WriteLine("");
        System.Console.WriteLine("______________________________");
        System.Console.Write("                  ");
        foreach(string place in this.wordStatus){
            System.Console.Write($"{place} ");
        }
        System.Console.WriteLine("");
        System.Console.WriteLine($"You have {this.User.Guesses} guesses remaining...");
        System.Console.WriteLine(msg);
        this.calcTurn();
    }

    public void calcTurn(){
        System.Console.WriteLine("Please enter a letter to guess:");
        string inputGuess = Console.ReadLine();
        int availIndex = this.User.AvailLetters.IndexOf(inputGuess);
        if (availIndex == -1 || inputGuess == "-"){
            System.Console.WriteLine("Sorry that's not a valid letter to guess");
            this.calcTurn();
            return;
        }

        this.User.AvailLetters[availIndex] = "-";
        this.determineWord(inputGuess);
    }

    public void determineWord(string letter){
        int indexFound;
        List<List<string>> wordGroups = new List<List<string>>();
        for(int j=0; j <= this.currentDict[0].Length; j++){
            wordGroups.Add(new List<string>());
        }
        foreach( string word in this.currentDict){
            indexFound = word.Length;
            for(int i=0; i<word.Length; i++){
                if(word[i].ToString() == letter){
                    if(indexFound != word.Length){
                        indexFound = -1;
                    }
                    else{
                        indexFound = i;
                    }
                }
            }
            if(indexFound != -1){
                wordGroups[indexFound].Add(word);
            }
        }
        int maxLength = wordGroups[0].Count;
        int correctIndex = 0;
        for( int k =1; k < wordGroups.Count; k++){
            if(wordGroups[k].Count > maxLength){
                maxLength = wordGroups[k].Count;
                correctIndex = k;
            }
        }
        this.currentDict = wordGroups[correctIndex];
        if (correctIndex >= this.wordLength) {
            this.User.Guesses --;
            if (this.User.Guesses == 0){
                this.gameOver();
                return;
            }
            else {
            this.displayTurn("Incorrect Guess!  Try again, LOSER!");
            }
        }
        else {
            this.wordStatus[correctIndex] = letter;
            this.displayTurn();
        }
        return;
    }
    public void gameOver(){
        System.Console.WriteLine("GAME OVER!!!! YOU LOSE!!!!");
        System.Console.WriteLine($"The word may or may not have been '{this.currentDict[0]}'");
        System.Console.WriteLine("Would you like to play again? (y/n)");
        string input = Console.ReadLine().ToLower();
        if (input == "y") {
            this.startGame();
            return;
        }
        else {
            return;
        }
    }
}