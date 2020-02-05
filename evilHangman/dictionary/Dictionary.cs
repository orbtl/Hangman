using System.Collections.Generic;
using System.IO;
public class wordDict {
    public List<string>[] allWords;
    public wordDict(){
        this.allWords = new List<string>[23];
    }
    
    public void generate(){
        int wordLength;
        for (int i=0; i<=22; i++) {
            this.allWords[i] = new List<string>();
        }
        foreach (string line in File.ReadLines(@"./dictionary/dictionary.txt")){
            wordLength = line.Length;
            if (wordLength <= 22) {
                this.allWords[wordLength].Add(line);
            }
        }
    }

}