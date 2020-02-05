using System;

namespace evilHangman
{
    class Program
    {
        static void Main(string[] args)
        {
            wordDict myDict = new wordDict();
            myDict.generate();
            
            // Console.Clear();
            // Computer enemy = new Computer();
            // enemy.startGame();
            // while (1==1) {
            //     Computer.nextTurn();
            //     if (Computer.gameOver == true){
            //         break
            //     }
            // }
            
            // System.Console.WriteLine(thisPlayer.Name);
        }
    }
}
