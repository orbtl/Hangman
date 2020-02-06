using System.Collections.Generic;
public class Player 
{
    public string Name {get; set;}
    public int Guesses {get; set;}
    public List<string> AvailLetters {get; set;}
    public Player(string name) {
        this.Name = name;
        this.Guesses = 7;
        this.resetLetters();
    }
    public void resetLetters(){
        this.AvailLetters = new List<string>{
            "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z"
        };
    }
}