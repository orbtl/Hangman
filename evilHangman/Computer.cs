using System;
public class Computer
{
    public Player User;
    public int wordLength;
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
        (1) Easy: 1-7 Letters
        (2) Medium: 8-15 Letters
        (3) Hard: 16-45 Letters");
        string input = Console.ReadLine();
        Random rand = new Random();
        int min;
        int max;
        if (input == "1"){
            min = 1;
            max = 8;
        }
        else if (input == "2") {
            min = 8;
            max = 16;
        }
        else if (input == "3") {
            min = 16;
            max = 46;
        }
        else {
            System.Console.WriteLine("That is not a valid input, please enter 1, 2, or 3");
            this.calcDifficulty();
            return;
        }
        this.wordLength = rand.Next(min, max);
        System.Console.WriteLine($"Playing with a word of length {this.wordLength}");
        return;
    }
}