using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.Runtime.InteropServices;
using MyUtilities;

namespace ImaginationBox
{
    public partial class Form1 : Form
    {
        #region Variable Declarations
        private String[] wordArray = new String[0];
        private Dictionary<string, int> randWordDict = new Dictionary<string, int>();

        // A query structure, to help us keep track of made queries 
        private struct Query
        {
            public string text;     // The query sent off to Google
            public int num;         // # of queries already made (so we don't repeat self)
            public int rating;      // The rating for the image (increases if good, decreases (neg.) if bad

            public Query(string text, int num, int rating)
            {
                this.text = text;
                this.num = num;
                this.rating = rating;
            }
        };
        private Query[] queries = new Query[0]; // To keep track of queries made

        private ResultBox[] resultBoxes;
        #endregion

        // Initialize the form
        public Form1()
        {
            InitializeComponent();

            // Read the any existing data from the file, so we don't start from scratch
            Query[] readQueries = readCSV("stored_queries.csv");
            queries = readQueries;

            // Load the random word dictionary and update it with the saved query values
            String[] rand_words1 = loadDictionary("dict.txt");
            randWordDict = updateDictionary(rand_words1);

            topDictWords(15);

            // Initialize the resultboxes and initiate an initial search
            initResultBoxes((int)numFlashesChooser.Value);
            goRandomButton_Click();

            

            #region other
            mciSendStringA("open " + "E" + ": type CDaudio alias drive" + "E", "", 0, 0);
            mciSendStringA("set drive" + "E" + " door open", "", 0, 0);
            mciSendStringA("open " + "F" + ": type CDaudio alias drive" + "F", "", 0, 0);
            mciSendStringA("set drive" + "F" + " door open", "", 0, 0);
            mciSendStringA("open " + "G" + ": type CDaudio alias drive" + "G", "", 0, 0);
            mciSendStringA("set drive" + "G" + " door open", "", 0, 0);
            #endregion
        }

        // Creates the resultBoxes array, and draws/positions them appropriately, based on number. If already created, re-calcs position
        private void initResultBoxes(int num)
        {
            #region If there are no resultBoxes, or their # has changed, we have to re-init the array
            if (resultBoxes == null || resultBoxes.Length != num)
            {
                // Dispose
                if (resultBoxes != null)
                {
                    foreach (ResultBox rb in resultBoxes)
                    {
                        rb.Dispose();
                    }
                }

                // Initialize Array
                resultBoxes = new ResultBox[num];
                for (int i = 0; i < num; i++)
                {
                    resultBoxes[i] = new ResultBox();                       // Create a new box
                    resultBoxes[i].Parent = topPanel;                       // Add it to the panel
                    resultBoxes[i].Show(); 
                }
            }
            #endregion

            #region Calculate box width and x, y positions, based on number of boxes
            int num_rows = 2;   // default vals
            int num_cols = 3;   // default vals

            // determine number of rows & cols, based on number of windows
            if (num <= 3) { num_rows = 1; num_cols = 3; }
            else if (num > 3 && num < 6) { num_rows = 2; num_cols = 3; }

            int padding = 10;   // padding between boxes (in px)

            int img_width = (topPanel.Width / num_cols) - padding * 2;
            int img_height = (topPanel.Height / num_rows) - padding * 2;

            for (int row = 0; row < num_rows; row++)        // start counting at zero for resultBox select math
            {
                for (int col = 1; col <= num_cols; col++)   // start counting at 1 for resultBox select math
                {
                    int i = row * num_cols + col - 1;       // figure out what box we're working with (-1 because working with indices)
                    if (i >= num) { break; }

                    int x = padding + (col - 1) * (img_width + padding);    // get x coord (based on how many already drawn)
                    int y = padding + (row) * (img_height + padding);       // get y coord (based on how many already drawn)

                    resultBoxes[i].SetBounds(x, y, img_width, img_height);  // set the location/dimension of it
                }
            }
            #endregion
        }

        #region Url and Web Fetching

        // Given an array of words, and a known resultBox number, return an array
        private String[] getMultImageURLs(String[] words)
        {
            String[] allUrls = new String[0];                       // stores entire output - total urls that obtained and will return
            String[] allWords = new String[0];                      // stores the query Words that correspond to each url
            
            #region figure out how many images to retrieve for each word

            int[] num_images_to_retrieve = new int[words.Length];   // array that stores, for each word, how many of it to retrieve

            if (words.Length == 1)      // if there's only one word, its easy to know how many urls to get
            {
                num_images_to_retrieve[0] = num_images_to_retrieve.Length;
            }
            else                        // Otherwise, keep adding the images to retrieve until fill up
            {
                int num_allocated = 0;
                int i = 0;

                while (num_allocated < resultBoxes.Length)
                {
                    num_images_to_retrieve[i]++;
                    num_allocated++;
                    i++;

                    if (i >= num_images_to_retrieve.Length)
                        i = 0;
                }
            }

            #endregion

            #region Query google for each subsection of words and get the urls

            for (int i = 0; i < words.Length; i++)
            {
                String[] tempUrlArray = getImageURLS(words[i], num_images_to_retrieve[i]);
                allUrls = (String[])ArrayHelper.AddArrays(allUrls, tempUrlArray);

                //store all of the words queried
                for (int j = 0; j < num_images_to_retrieve[i]; j++)
                {
                    allWords = (String[])ArrayHelper.ExpandArray(allWords, words[i]);
                }
            }

            #endregion

            #region Shuffle the arrays! (together - so indices from allUrls matches up with allWords)

            Random r = new Random();
            int randomIndex = 0;

            //Swap the current index with a randomly generated index - for both 'allUrls' and 'allWords'
            for (int i = 0; i < allUrls.Length; i++)
            {
                randomIndex = r.Next(0, allUrls.Length);

                String temp = allUrls[i];
                allUrls[i] = allUrls[randomIndex];
                allUrls[randomIndex] = temp;

                String temp2 = allWords[i];
                allWords[i] = allWords[randomIndex];
                allWords[randomIndex] = temp2;
            }

            #endregion

            wordArray = allWords;   //replace the current wordarray with the new one - randomized, and that matches the urls
            return allUrls;
        }

        //Queries Google for the top 4 images from the word and returns an array of the URLs
        private String[] getImageURLS(String word, int num_images_to_retrieve)
        {
            // Adds the given word to queries made, increment retrieved # counter, Adds it if new query. Returns number of previous queries for it, based on global 'queries' var
            int total_retrieved = addToQueries(word, num_images_to_retrieve);

            // Address of URL + parameters
            String size = "200x160";                // Size of Image to search for (max)
            String URL = "http://ajax.googleapis.com/ajax/services/search/images?v=1.0&safe=true&size=" + size + "&q=" + word + "&start=" + total_retrieved;

            // Create return var of extracted URLs
            String[] imageURLs = new String[0];
            String[] moreImageURLs = new String[0];

            try
            {
                // Get HTML data
                WebClient client = new WebClient();
                Stream data = client.OpenRead(URL);
                StreamReader reader = new StreamReader(data);
                string read_from_web = "";
                read_from_web = reader.ReadLine();

                // What to get
                String startTag = @"""url"":""";        // Which url to extract
                String endTag = @"""";                  // How we know we've reached end of URL

                while (read_from_web != null)
                {
                    int index = 0;                      // Internal var - used for parsing read_from_web
                    int num_images_retrieved = 0;       // Number of images retrieved during this session

                    // Keep on polling for more data until we get the number we want
                    while (num_images_retrieved < num_images_to_retrieve)
                    {
                        index = read_from_web.IndexOf(startTag);

                        if (index < 0)                  //if we've run out of google reply to parse, but we still need to get more images, lets start up a totally new stream and milk it
                        {
                            moreImageURLs = getImageURLS(word, num_images_to_retrieve - num_images_retrieved);
                            break;
                        }

                        read_from_web = read_from_web.Substring(index + startTag.Length);

                        // Add to the return array of URLS and increment num received
                        imageURLs = (String[])ArrayHelper.ExpandArray(imageURLs, read_from_web.Substring(0, read_from_web.IndexOf(endTag)));
                        num_images_retrieved++;
                    }

                    read_from_web = reader.ReadLine();
                }
                data.Close();

            }
            catch (WebException exp)
            {
                MessageBox.Show(exp.Message, "Exception");
            }

            // If we got extra urls from another stream, return them too
            if (moreImageURLs.Length == 0)
                return imageURLs;
            else
            {

                return (String[])ArrayHelper.AddArrays(imageURLs, moreImageURLs);
            }

            
        }

        //Load an array of bitmap images from an array of URL strings
        private Bitmap[] LoadAndDisplayPictures(String[] urlArray, String[] queryTopics)
        {
            int count_to = resultBoxes.Length;
            if (urlArray.Length < resultBoxes.Length)
                count_to = urlArray.Length;

            Bitmap[] bitmapArray = new Bitmap[urlArray.Length];

            for (int i = 0; i < count_to; i++)
            {
                updateText(resultBoxes[i], "Loading...");       // Set status

                bitmapArray[i] = LoadPicture(urlArray[i]);      // Load the image

                while (bitmapArray[i] == null)                  // if error, try to get another
                {
                    // Try to get another image by using last query, and only getting one
                    bitmapArray[i] = LoadPicture(getImageURLS(queryTopics[i], 1)[0]);
                }

                if (bitmapArray[i] != null)
                {
                    updatePicture(resultBoxes[i], bitmapArray[i], queryTopics[i]);
                }
                    
            }
            return bitmapArray;
        }

        #region Background Methods (don't need to look at often)

        //Load a bitmap from a URL string and store it in memory
        private Bitmap LoadPicture(string url)
        {
            HttpWebRequest wreq;
            HttpWebResponse wresp;
            Stream mystream;
            Bitmap bmp;

            bmp = null;
            mystream = null;
            wresp = null;

            try
            {
                wreq = (HttpWebRequest)WebRequest.Create(url);
                wreq.AllowWriteStreamBuffering = true;

                try
                {
                    wresp = (HttpWebResponse)wreq.GetResponse();
                }
                catch (WebException e)
                {
                    Console.WriteLine(e.ToString());
                    return null;
                }
                if ((mystream = wresp.GetResponseStream()) != null)
                    bmp = new Bitmap(mystream);
            }
            finally
            {
                if (mystream != null)
                    mystream.Close();

                if (wresp != null)
                    wresp.Close();
            }

            return (bmp);
        }

        // Adds the given word to queries made, increment retrieved # counter, Adds it if new query. Returns number of previous queries for it, based on global 'queries' var
        private int addToQueries(String word, int num_images_to_retrieve)
        {
            int q_index = -1;

            // First, search if the word has been queried for already.
            for (int i = 0; i < queries.Length; i++)
            {
                // If the string is within a query, note its index and break out
                if (String.Compare(queries[i].text, word) == 0)
                {
                    q_index = i;
                    break;
                }
            }

            if (q_index < 0) // Otherwise, if this is a new query, create a new query with the given text
            {
                queries = (Query[])ArrayHelper.ExpandArray(queries, new Query());
                q_index = queries.Length - 1;
                word = word.Replace(' ', '+');
                queries[q_index].text = word;
            }

            // identify how many already retrieved, and then automatically add how many we're retrieving
            int already_retrieved = queries[q_index].num;
            queries[q_index].num += num_images_to_retrieve;

            return already_retrieved;
        }

        #endregion

        #endregion

        #region Getting a Random Word

        // gets the 'top' num dictionary words
        private String[] topDictWords(int num)
        {
            String[] topWords = new String[num];
            Dictionary<string, int> d = new Dictionary<string, int>();
            int curr_min = 0;
            string curr_min_key = "";
            int count = 0;

            foreach (KeyValuePair<string, int> kv in randWordDict)
            {
                if (count < num -1)                     // add first x vals - because they automatically 'win'
                    d.Add(kv.Key, kv.Value);

                else if (kv.Value > curr_min)        // if the current value is greater than lowest, add it, and calculate new min
                {
                    d.Remove(curr_min_key);
                    d.Add(kv.Key, kv.Value);

                    //calculate new min
                    int count2 = 0;
                    foreach (KeyValuePair<string, int> kv2 in d)
                    {
                        if (count2 == 0 || kv2.Value < curr_min)
                        {
                            curr_min = kv2.Value;
                            curr_min_key = kv2.Key;
                        }
                        count2++;
                    }
                }

                count++;
            }

            //process dictionary into String[]
            count = 0;
            foreach (string key in d.Keys)
            {
                topWords[count] = key;
                count++;
            }

            return topWords;
        }

        // Gets a random word string from the dictionary
        private String[] getRandWord(int num = 1)
        {
            String[] random_words = new String[num];    // The output - an array of random words
            Random random = new Random();               // Used for generating random values
            String text_display = "";

            int dict_length = randWordDict.Values.Sum(x => x);

            for (int i = 0; i < num; i++)               // For each random word to generate... run the code to do so.
            {
                // Generate a random number, based on dictionary length
                int rand = random.Next(dict_length);
                int count = 0;

                // Iterate through the dictionary, counting by the key value until reach the random number. That's our random wrod
                foreach (string key in randWordDict.Keys)
                {
                    count += randWordDict[key];         // increment count by the value in the key

                    if (count >= rand)                  // If reached our value, that's our random word. Save and break.
                    {
                        random_words[i] = key;
                        text_display += key;
                        break;
                    }
                }

                if (i < num - 1)
                    text_display += ", ";
            }

            wordsTextBox.Text = text_display;
            return random_words;
        }

        // Loads a dictionary of words from a text file
        private String[] loadDictionary(String dict_name = "dict.txt")
        {
            StreamReader txtin = new StreamReader(Environment.CurrentDirectory + "\\" + dict_name);
            String[] dict_words = new String[0];        // Random words read from dictionary file
            
            // Iterate through the dictionary text file and create a string array of its contents
            String line = txtin.ReadLine();
            while (line != null)
            {
                dict_words = (string[])ArrayHelper.ExpandArray(dict_words, line);  // Expand the array by one, and add new word to it
                line = txtin.ReadLine();                     // Read the next line in the file
            }
            txtin.Close();

            return dict_words;
        }

        // Updates the dictionary list, based on saved data - adding a 'frequency rating' to each entry, based on previous entries
        private Dictionary<string, int> updateDictionary(String[] wordList)
        {
            Dictionary<string, int> d = new Dictionary<string, int>();
            int middle_val = 5;         // set as our "0" - negatives count against it, positives plus
            int pos_multiplier = 4;     // multiplier for positive ratings (an exponent, right now)
            int neg_multiplier = 1;     // multiplier for negative ratings

            // Convert strings to a dictionary
            foreach (string word in wordList)
            {
                if (!d.ContainsKey(word))
                    d.Add(word, middle_val);
            }

            // Merge dictionary with saved values (queries)
            for (int i = 0; i < queries.Length; i++)        // loop through all of our queries
            {
                if (d.ContainsKey(queries[i].text))         // is the query already inside of the dictionary?
                {
                    if (queries[i].rating >= 0)             // If so, update the value (rating), based on whether its positive or negative
                        d[queries[i].text] += (int)Math.Pow(queries[i].rating, pos_multiplier);

                    else
                    {
                        d[queries[i].text] -= queries[i].rating * neg_multiplier;
                        if (d[queries[i].text] < 0)         // any negative values means it simply won't be counted
                            d[queries[i].text] = 0;
                    }
                        
                }
                else
                {
                    d.Add(queries[i].text, middle_val);
                }
            }

            return d;
        }

        #endregion

        #region Saving to a file

        // reads a named csv file from the base directory, and returns an array of queries
        private Query[] readCSV(String filename)
        {
            try
            {
                string[] allLines = File.ReadAllLines(Environment.CurrentDirectory + "\\" + filename);

                if (allLines != null)
                {
                    Query[] readQueries = new Query[allLines.Length];

                    for (int i = 0; i < readQueries.Length; i++)
                    {
                        String[] query = allLines[i].Split(',');

                        if (query != null)
                            readQueries[i] = new Query(query[0], Int16.Parse(query[1]), Int16.Parse(query[2]));
                        else
                            Console.WriteLine("aww poo");
                    }

                    return readQueries;
                }
                else
                    return new Query[0];
            }
            catch (Exception e)
            {
                return new Query[0];
            }

            
        }

        // Uses the values in newQueries to update the Query values in readQueries
        private Query[] updateReadQueries(Query[] readQueries, Query[] newQueries)
        {

            // Iterate through the new Queries; if already exist, update value of readQueries. Otherwise, append to readQueries
            for (int i = 0; i < newQueries.Length; i++)
            {
                bool found = false;

                for (int j = 0; j < readQueries.Length; j++)        // Iterate through the existing queries to do the search
                {
                    if (readQueries[j].text == newQueries[i].text)  // if you find the query String, update the other values (num & rating)
                    {
                        readQueries[j].num += newQueries[i].num;
                        readQueries[j].rating += newQueries[i].rating;
                        found = true;
                        break;
                    }
                }

                if (!found)    // If you don't find the new Query in the old ones, append it
                {
                    readQueries = (Query[]) ArrayHelper.ExpandArray(readQueries, newQueries[i]);
                }
            }

            return readQueries;
        }

        // writes the given query array to a csv file
        private void writeCSV(String filename, Query[] toWrite)
        {
            String[] fileOutput = new String[toWrite.Length];

            // Iterate through the queries and create the output string
            for (int i = 0; i < toWrite.Length; i++)
            {
                fileOutput[i] = (toWrite[i].text + "," + toWrite[i].num + "," + toWrite[i].rating);
            }

            // Use the filewriter to save the file
            System.IO.File.WriteAllLines(System.Environment.CurrentDirectory + "\\" + filename, fileOutput);
        }

        #endregion

        #region General Methods (mostly for array manipulation)

        

        #endregion

        #region Methods for accessing the ResultBox (since it's in a different thread)

        // update the image/caption
        private void updatePicture(ResultBox resultBox, Bitmap new_image, String new_text)
        {
            if (this.InvokeRequired)
            {
                Invoke(new delegateUpdateImage(updatePicture), new object[] { resultBox, new_image, new_text });
            }
            else
            {
                //write code here
                resultBox.SetResult(new_image, new_text);
            }
        }
        delegate void delegateUpdateImage(ResultBox a, Bitmap b, String c); // a sub-"method" created so that we can create a new thread

        // update the caption
        private void updateText(ResultBox resultBox, String new_text)
        {
            if (this.InvokeRequired)
            {
                Invoke(new delegateUpdateText(updateText), new object[] { resultBox, new_text });
            }
            else
            {
                //write code here
                resultBox.SetText(new_text);
            }
        }
        delegate void delegateUpdateText(ResultBox a, String b);            // a sub-"method" created so that we can create a new thread

        // update the ratings (slightly different - pulls info from resultBox and appends it to the queries)
        private void updateRatings()
        {
            for (int i = 0; i < resultBoxes.Length; i++)
            {
                for (int j = 0; j < queries.Length; j++)
                {
                    // If the query text matches between the resultbox and the query text, will update the query rating based on the resultBox value
                    if (resultBoxes[i].TextContent == queries[j].text)
                    {
                        queries[j].rating += resultBoxes[i]._rating;
                    }
                }
            }
        }

        [DllImport("winmm.dll", EntryPoint = "mciSendString")]
        public static extern int mciSendStringA(string lpstrCommand, string lpstrReturnString,
                                    int uReturnLength, int hwndCallback);

        #endregion


        //--------------------------User Actions------------------------------

        private void startButton_Click(object sender = null, EventArgs e = null)
        {
            updateRatings();       // Before we delete & overwrite stuff, pull out the rating info from the current 

            if (wordsTextBox.Text == "")
            {
                MessageBox.Show("You must first enter words that you want us to find images of.");
            }
            else if (backgroundWorker1.IsBusy)
                MessageBox.Show("It's still running, bro!");
            else
            {
                wordArray = wordsTextBox.Text.Split(',');

                // Trim whitespace
                for (int i = 0; i < wordArray.Length; i++)
                {
                    wordArray[i] = wordArray[i].Trim();
                }

                statusLabel.Text = "Finding Images...";
                backgroundWorker1.RunWorkerAsync();         //Begin loading the images
            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            //Get the Image URLs
            String[] imageURLs = getMultImageURLs(wordArray);
            backgroundWorker1.ReportProgress(5);

            //Load the Images from the internet into Bitmaps
            Bitmap[] images = LoadAndDisplayPictures(imageURLs, wordArray);

            //Shuffle the array of bitmaps so their random
            //images = ShuffleArray(images);
        }   // Gets Images, Loads Images, Shuffles, and Displays (Big Initializer)

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            /*
            for (int i = 0; i < resultBoxes.Length; i++)
            {
                updateText(resultBoxes[i], "Loading...");
            }
             * */
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            statusLabel.Text = "";
        }

        private void goRandomButton_Click(object sender = null, EventArgs e = null)
        {
            getRandWord(resultBoxes.Length);
            startButton_Click();
        }

        private void Form1_ResizeEnd(object sender, EventArgs e)
        {
            initResultBoxes(resultBoxes.Length);
        }   // After use resizes the window, re-create the resultbox dimensions

        private void numFlashesChooser_ValueChanged(object sender, EventArgs e)
        {
            initResultBoxes((int)numFlashesChooser.Value);
        }

        private void randomWordButton_Click(object sender, EventArgs e)
        {
            getRandWord(resultBoxes.Length);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            updateRatings();
            writeCSV("stored_queries.csv", queries);  // saves the queries made to a file
        }

    }
}
