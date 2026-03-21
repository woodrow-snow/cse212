using System.Runtime.CompilerServices;
using System.Text.Json;

public static class SetsAndMaps
{
    /// <summary>
    /// The words parameter contains a list of two character 
    /// words (lower case, no duplicates). Using sets, find an O(n) 
    /// solution for returning all symmetric pairs of words.  
    ///
    /// For example, if words was: [am, at, ma, if, fi], we would return :
    ///
    /// ["am & ma", "if & fi"]
    ///
    /// The order of the array does not matter, nor does the order of the specific words in each string in the array.
    /// at would not be returned because ta is not in the list of words.
    ///
    /// As a special case, if the letters are the same (example: 'aa') then
    /// it would not match anything else (remember the assumption above
    /// that there were no duplicates) and therefore should not be returned.
    /// </summary>
    /// <param name="words">An array of 2-character words (lowercase, no duplicates)</param>
    public static string[] FindPairs(string[] words)
    {
        // creating hash set to store values
        HashSet<string> wordSet = new HashSet<string>(words);

        // creating return array
        List<string> returnWords = new List<string>();

        foreach(string w in wordSet)
        {
            // checking to see if word is double letter. EX: aa
            char[] wordArray = w.ToArray();
            if (wordArray[0] == wordArray[1])
            {
                wordSet.Remove(w);
            }

            // getting reversed word
            string reversedWord = new string(wordArray.Reverse().ToArray());

            // checking if reversed word and word are in set
            if (wordSet.Contains(w) && wordSet.Contains(reversedWord))
            {
                // creating concat string and adding to return array
                string addStr = $"{reversedWord} & {w}";
                returnWords.Add(addStr);

                wordSet.Remove(reversedWord);
                wordSet.Remove(w);
            }
            else
            {
                // removing word if it does not have a mirror
                wordSet.Remove(w);
            }
        }

        return returnWords.ToArray();
    }

    /// <summary>
    /// Read a census file and summarize the degrees (education)
    /// earned by those contained in the file.  The summary
    /// should be stored in a dictionary where the key is the
    /// degree earned and the value is the number of people that 
    /// have earned that degree.  The degree information is in
    /// the 4th column of the file.  There is no header row in the
    /// file.
    /// </summary>
    /// <param name="filename">The name of the file to read</param>
    /// <returns>fixed array of divisors</returns>
    public static Dictionary<string, int> SummarizeDegrees(string filename)
    {
        var degrees = new Dictionary<string, int>();
        foreach (var line in File.ReadLines(filename))
        {
            var fields = line.Split(",");
            // TODO Problem 2 - ADD YOUR CODE HERE
            int degreeCol = 3; // which column the degree is in for a 0 based index

            // getting degree from current row
            string degree = fields[degreeCol];

            // checking if degree is in dict, if is - increase counter, if not - add to dict with value of 1
            if (degrees.ContainsKey(degree))
            {
                degrees[degree] += 1;
            }
            else
            {
                degrees.Add(degree, 1);
            }
        }

        return degrees;
    }

    /// <summary>
    /// Determine if 'word1' and 'word2' are anagrams.  An anagram
    /// is when the same letters in a word are re-organized into a 
    /// new word.  A dictionary is used to solve the problem.
    /// 
    /// Examples:
    /// is_anagram("CAT","ACT") would return true
    /// is_anagram("DOG","GOOD") would return false because GOOD has 2 O's
    /// 
    /// Important Note: When determining if two words are anagrams, you
    /// should ignore any spaces.  You should also ignore cases.  For 
    /// example, 'Ab' and 'Ba' should be considered anagrams
    /// 
    /// Reminder: You can access a letter by index in a string by 
    /// using the [] notation.
    /// </summary>
    public static bool IsAnagram(string word1, string word2)
    {
        // creating dict
        var word1Dict = new Dictionary<char, int>();
        var word2Dict = new Dictionary<char, int>();

        // setting word1 and 2 to lower and removing spaces
        word1 = word1.ToLower();
        word2 = word2.ToLower();

        word1 = String.Join("",word1.Split(" "));
        word2 = String.Join("", word2.Split(" "));

        // checking to see if words are the same length
        if (word1.Length != word2.Length)
        {
            return false;
        }

        // adding words to dicts
        for (int i = 0; i < word1.Length; i++)
        {
            // if statement for first word
            if (word1Dict.ContainsKey(word1[i]))
            {
                word1Dict[word1[i]] += 1;
            }
            else
            {
                word1Dict.Add(word1[i], 1);
            }

            // if statement for second word
            if (word2Dict.ContainsKey(word2[i]))
            {
                word2Dict[word2[i]] += 1;
            }
            else
            {
                word2Dict.Add(word2[i], 1);
            }
        }

        // looping through word1Dict and comparing each letter to word2dict
        bool IsAnagram = false;

        foreach (KeyValuePair<char, int> letter in word1Dict)
        {
            // if word1 and 2 dict contain the same key
            var key = letter.Key;
            var value = letter.Value;

            if (word2Dict.ContainsKey(key))
            {
                if (word2Dict[key] == word1Dict[key])
                {
                    IsAnagram = true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        return IsAnagram;
    }

    /// <summary>
    /// This function will read JSON (Javascript Object Notation) data from the 
    /// United States Geological Service (USGS) consisting of earthquake data.
    /// The data will include all earthquakes in the current day.
    /// 
    /// JSON data is organized into a dictionary. After reading the data using
    /// the built-in HTTP client library, this function will return a list of all
    /// earthquake locations ('place' attribute) and magnitudes ('mag' attribute).
    /// Additional information about the format of the JSON data can be found 
    /// at this website:  
    /// 
    /// https://earthquake.usgs.gov/earthquakes/feed/v1.0/geojson.php
    /// 
    /// </summary>
    public static string[] EarthquakeDailySummary()
    {
        const string uri = "https://earthquake.usgs.gov/earthquakes/feed/v1.0/summary/all_day.geojson";
        using var client = new HttpClient();
        using var getRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);
        using var jsonStream = client.Send(getRequestMessage).Content.ReadAsStream();
        using var reader = new StreamReader(jsonStream);
        var json = reader.ReadToEnd();
        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

        var featureCollection = JsonSerializer.Deserialize<FeatureCollection>(json, options);

        // TODO Problem 5:
        // 1. Add code in FeatureCollection.cs to describe the JSON using classes and properties 
        // on those classes so that the call to Deserialize above works properly.
        // 2. Add code below to create a string out each place a earthquake has happened today and its magitude.
        // 3. Return an array of these string descriptions.

        string[] earthquakes = new string[6];

        for (int i = 0; i < 6; i++)
        {
            earthquakes[i] = $"{featureCollection.features[i].properties.place} - Mag {featureCollection.features[i].properties.mag}";
        }

        foreach (var e in earthquakes)
        {
            Console.WriteLine(e);
        }

        return earthquakes;
    }
}