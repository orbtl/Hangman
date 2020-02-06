using System;
using System.Collections.Generic;
public class Computer
{
    public Player User;
    public int wordLength;
    public List<string> currentDict;
    public string[] wordStatus;
    public int correctLetters;
    public string[] drawing;
    public void startGame(){
        System.Console.WriteLine(@"
oooooooooooo              o8o  oooo       ooooo   ooooo                                                                          
`888'     `8              `''  `888       `888'   `888'                                                                          
 888         oooo    ooo oooo   888        888     888   .oooo.   ooo. .oo.    .oooooooo ooo. .oo.  .oo.    .oooo.   ooo. .oo.   
 888oooo8     `88.  .8'  `888   888        888ooooo888  `P  )88b  `888P'Y88b  888' `88b  `888P'Y88bP'Y88b  `P  )88b  `888P'Y88b  
 888    '      `88..8'    888   888        888     888   .oP'888   888   888  888   888   888   888   888   .oP'888   888   888  
 888       o    `888'     888   888        888     888  d8(  888   888   888  `88bod8P'   888   888   888  d8(  888   888   888  
o888ooooood8     `8'     o888o o888o      o888o   o888o `Y888''8o o888o o888o `8oooooo.  o888o o888o o888o `Y888''8o o888o o888o 
                                                                              d'     YD                                          
                                                                              'Y88888P'                                          
                                                                                                                                 
");
        System.Console.WriteLine("Welcome to Evil Hangman!  Are you prepared to face your certain demise!?");
        System.Console.WriteLine("Please enter your name");
        this.correctLetters = 0;
        string name = Console.ReadLine();
        this.User = new Player(name);
        this.generateASCII();
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
        if (this.correctLetters == this.wordLength) {
            this.winGame();
            return;
        }
        System.Console.WriteLine($"***************** Letters you can still choose ***********************");
        System.Console.WriteLine("");
        foreach( string letter in this.User.AvailLetters){
            System.Console.Write($" {letter}");
        }
        System.Console.WriteLine("");
        System.Console.WriteLine("");
        System.Console.WriteLine("______________________________________________________________________");
        System.Console.WriteLine("");
        System.Console.WriteLine(this.drawing[this.User.Guesses]);
        System.Console.WriteLine("");

        System.Console.Write("                  ");
        foreach(string place in this.wordStatus){
            System.Console.Write($"{place} ");
        }
        System.Console.WriteLine("");
        System.Console.WriteLine("");
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
        // System.Console.WriteLine($"Debug: currentDict length: {currentDict.Count}");
        // System.Console.WriteLine($"Debug: currentDict[0] = {currentDict[0]}");
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
        // foreach (List<string> wordGroup in wordGroups) {
        //     System.Console.WriteLine($"word group length: {wordGroup.Count}");
        // }
        int maxLength = wordGroups[0].Count;
        int correctIndex = 0;
        for( int k =1; k < wordGroups.Count; k++){
            if(wordGroups[k].Count > maxLength){
                maxLength = wordGroups[k].Count;
                correctIndex = k;
            }
        }
        if (wordGroups[correctIndex].Count == 0) { // no words available without doubles
            while (this.currentDict.Count > 1) {
                this.currentDict.RemoveAt(this.currentDict.Count-1);
            }
            for (int l=0; l<this.currentDict[0].Length; l++) {
                if (this.currentDict[0][l].ToString() == letter) {
                    this.wordStatus[l] = letter;
                    this.correctLetters ++;
                }
            }
            this.displayTurn();
            return;
        }
        this.currentDict = wordGroups[correctIndex];
        // System.Console.WriteLine($"current dict length: {this.currentDict.Count}");
        // System.Console.WriteLine($"current dict[0]: {this.currentDict[0]}");
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
            this.correctLetters ++;
            this.displayTurn();
        }
        return;
    }
    public void gameOver(){
        Console.Clear();
        System.Console.WriteLine(@"
  .oooooo.          .o.       ooo        ooooo oooooooooooo        .oooooo.   oooooo     oooo oooooooooooo ooooooooo.   
 d8P'  `Y8b        .888.      `88.       .888' `888'     `8       d8P'  `Y8b   `888.     .8'  `888'     `8 `888   `Y88. 
888               .8'888.      888b     d'888   888              888      888   `888.   .8'    888          888   .d88' 
888              .8' `888.     8 Y88. .P  888   888oooo8         888      888    `888. .8'     888oooo8     888ooo88P'  
888     ooooo   .88ooo8888.    8  `888'   888   888    '         888      888     `888.8'      888    '     888`88b.    
`88.    .88'   .8'     `888.   8    Y     888   888       o      `88b    d88'      `888'       888       o  888  `88b.  
 `Y8bood8P'   o88o     o8888o o8o        o888o o888ooooood8       `Y8bood8P'        `8'       o888ooooood8 o888o  o888o 
                                                                                                                        
                                                                                                                        
                                                                                                                        
");
        System.Console.WriteLine("YOU LOSE!!!!");
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
    public void winGame(){
        Console.Clear();
        System.Console.WriteLine(@"
  .oooooo.          .o.       ooo        ooooo oooooooooooo        .oooooo.   oooooo     oooo oooooooooooo ooooooooo.   
 d8P'  `Y8b        .888.      `88.       .888' `888'     `8       d8P'  `Y8b   `888.     .8'  `888'     `8 `888   `Y88. 
888               .8'888.      888b     d'888   888              888      888   `888.   .8'    888          888   .d88' 
888              .8' `888.     8 Y88. .P  888   888oooo8         888      888    `888. .8'     888oooo8     888ooo88P'  
888     ooooo   .88ooo8888.    8  `888'   888   888    '         888      888     `888.8'      888    '     888`88b.    
`88.    .88'   .8'     `888.   8    Y     888   888       o      `88b    d88'      `888'       888       o  888  `88b.  
 `Y8bood8P'   o88o     o8888o o8o        o888o o888ooooood8       `Y8bood8P'        `8'       o888ooooood8 o888o  o888o 
                                                                                                                        
                                                                                                                        
                                                                                                                        
");
        System.Console.WriteLine("YOU WON!  OH MY GOD YOU MUST  BE CHEATING!  AMAZING!");
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
    public void generateASCII(){
        this.drawing = new string[8];
        drawing[0] = "";
        drawing[7] = @"  +---+
  |   |
      |
      |
      |
      |
=========";
        drawing[6] = @"  +---+
  |   |
  O   |
      |
      |
      |
=========";
        drawing[5] = @"  +---+
  |   |
  O   |
  |   |
      |
      |
=========";
        drawing[4] = @"  +---+
  |   |
  O   |
 /|   |
      |
      |
=========";
        drawing[3] = @"  +---+
  |   |
  O   |
 /|\  |
      |
      |
=========";
        drawing[2] = @"  +---+
  |   |
  O   |
 /|\  |
 /    |
      |
=========";
        drawing[1] = @"  +---+
  |   |
  O   |
 /|\  |
 / \  |
      |
=========";
        

    }
}