using System.Collections.Generic;
using System.IO;
public static class wordDict {
    public static List<string> generate(int targetLength){
        int wordLength;
        List<string> allWords = new List<string>();
    
        foreach (string line in File.ReadLines(@"./dictionary/dictionary.txt")){
            wordLength = line.Length;
            if (wordLength == targetLength) {
                allWords.Add(line);
            }
        }
        return allWords;
    }

}