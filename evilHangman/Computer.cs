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
        System.Console.WriteLine($"Playing with a word of length {this.wordLength}");
        System.Console.WriteLine($"First word in Dict {this.currentDict[0]}");
        return;
    }

    public void displayTurn(){
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
    }
}