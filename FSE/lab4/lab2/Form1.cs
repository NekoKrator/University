using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab2
{

    public partial class Form1 : Form
    {
        private static readonly object padlock = new object();

        private bool isNewsDownloaded = false;

        public static EventLogger Instance { get { return Nested.instance; } }

        private class Nested
        {
            static Nested() { }
            internal static readonly EventLogger instance = new EventLogger();
        }

        public int amountOfNews { get; set; }

        private string[] keywords = new string[] { "abstract", "as", "base", "bool", "break", "byte", "case", "catch", "char", "checked", "class", "const", "continue", "decimal", "default", "delegate", "do", "double", "else", "enum", "event", "explicit", "extern", "false", "finally", "fixed", "float", "for", "foreach", "goto", "if", "implicit", "in", "int", "interface", "internal", "is", "lock", "long", "namespace", "new", "null", "object", "operator", "out", "override", "params", "private", "protected", "public", "readonly", "ref", "return", "sbyte", "sealed", "short", "sizeof", "stackalloc", "static", "string", "struct", "switch", "this", "throw", "true", "try", "typeof", "uint", "ulong", "unchecked", "unsafe", "ushort", "using", "virtual", "void", "volatile", "while" };

        public Form1()
        {
            InitializeComponent();
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                richTextBox1.Text = File.ReadAllText(openFileDialog1.FileName);
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.DefaultExt = ".txt";
            saveFileDialog1.Filter = "Text File | *.txt";
            DialogResult dr = saveFileDialog1.ShowDialog();
            if (dr == DialogResult.OK)
            {
                File.WriteAllText(saveFileDialog1.FileName, richTextBox1.Text);
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Undo();
        }

        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Redo();
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Cut();
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Copy();
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Paste();
        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectAll();
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            string text = richTextBox1.Text;
            long sizeInBytes = Encoding.UTF8.GetByteCount(text);
            int charCount = text.Length;
            int paragraphCount = text.Split(new string[] { "\n\n" }, StringSplitOptions.RemoveEmptyEntries).Length;
            int emptyLineCount = text.Split(new string[] { "\n" }, StringSplitOptions.None).Count(line => String.IsNullOrWhiteSpace(line));
            int authorPageCount = charCount / 1800;
            int vowelCount = text.Count(c => "aeiouAEIOUаеіїоуяєюїАЕІЇОУЯЄЮЇ".Contains(c));
            int consonantCount = text.Count(c => "bcdfghjklmnpqrstvwxyzBCDFGHJKLMNPQRSTVWXYZбвгджзйклмнпрстфхцчшщБВГДЖЗЙКЛМНПРСТФХЦЧШЩ".Contains(c));
            int digitCount = text.Count(c => char.IsDigit(c));
            int specialCharCount = text.Count(c => !char.IsLetterOrDigit(c) && !char.IsWhiteSpace(c));
            int punctuationCount = text.Count(c => char.IsPunctuation(c));
            int cyrillicCount = text.Count(c => '\u0400' <= c && c <= '\u04FF');
            int latinCount = text.Count(c => ('a' <= c && c <= 'z') || ('A' <= c && c <= 'Z'));

            toolStripStatusLabel1.Text = $"{sizeInBytes / 1024.0} KB";
            toolStripStatusLabel2.Text = $"{charCount} символів";
            toolStripStatusLabel3.Text = $"{paragraphCount} абзаців";
            toolStripStatusLabel4.Text = $"{emptyLineCount} порожніх рядків";
            toolStripStatusLabel5.Text = $"{authorPageCount} авторських сторінок";
            toolStripStatusLabel6.Text = $"{vowelCount} голосних";
            toolStripStatusLabel7.Text = $"{consonantCount} приголосних";
            toolStripStatusLabel8.Text = $"{digitCount} цифр";
            toolStripStatusLabel9.Text = $"{specialCharCount} спец. символів";
            toolStripStatusLabel10.Text = $"{punctuationCount} пункт. символів";
            toolStripStatusLabel11.Text = $"{cyrillicCount} кириличних";
            toolStripStatusLabel12.Text = $"{latinCount} латинських";

            EventLogger.Instance.LogEvent("Текст змінено", richTextBox1.Text);
        }

        private void UpdateRichTextBox()
        {
            richTextBox1_TextChanged(null, EventArgs.Empty);
        }

        private void refactorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string originalText = richTextBox1.Text;
            string refactoredText = Regex.Replace(originalText, @"\s+", " ");

            richTextBox2.Text = refactoredText;

            DialogResult dialogResult = MessageBox.Show("Чи бажаєте ви зберегти зміни?", "Підтвердження", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                richTextBox1.Text = refactoredText;
                richTextBox2.Clear();
            }
            else if (dialogResult == DialogResult.No)
            {
                richTextBox2.Clear();
            }
        }

        private void ключовіСловаCToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var words = richTextBox1.Text.Split(new char[] { ' ', '\t', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
            var keywordCount = words.Count(word => keywords.Contains(word));

            MessageBox.Show($"Кількість ключових слів: {keywordCount}");
        }

        private void найдовшіСловаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var words = richTextBox1.Text.Split(new char[] { ' ', '\t', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
            var longestWords = words.OrderByDescending(word => word.Length).Take(10);

            string result = string.Join(", ", longestWords);
            MessageBox.Show($"10 найдовших слів: {result}");
        }

        public void UpdateAmountOfNews(int amount)
        {
            amountOfNews = amount;
        }

        private async void downloadTextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!isNewsDownloaded)
            {
                Form2 form2 = new Form2(this);
                form2.ShowDialog();

                EventLogger.Instance.LogEvent("Завантаження новин", "Опис події: завантаження новин з вказаною кількістю");
                await DownloadNews(amountOfNews);

                isNewsDownloaded = true;
            }
            else
            {
                MessageBox.Show("Новини вже були завантажені.");
            }
        }

        private async Task DownloadNews(int amountOfNews)
        {
            var newsItems = new HashSet<string>(); // HashSet to store unique titles
            string logFilePath = "log.txt"; // Declare logFilePath outside loop

            string url = $"https://www.znu.edu.ua/cms/index.php?action=news/view&start=0&site_id=27&lang=ukr";
            WebRequest request = WebRequest.Create(url);
            WebResponse response = await request.GetResponseAsync();
            string pageContent;
            using (StreamReader reader = new StreamReader(response.GetResponseStream()))
            {
                pageContent = await reader.ReadToEndAsync();
            }

            Regex newsRegex = new Regex("<h4>\\s*<a[^>]+>(.*?)</a>", RegexOptions.Singleline);
            var titleMatches = newsRegex.Matches(pageContent);

            foreach (Match match in titleMatches)
            {
                string title = match.Groups[1].Value.Trim();

                // Check if the title has been encountered before
                if (!newsItems.Contains(title))
                {
                    // Log the title
                    string logMessage = $"{DateTime.Now}: Завантажені новини - Заголовок: {title}" + Environment.NewLine;
                    File.AppendAllText(logFilePath, logMessage, Encoding.UTF8);

                    // Add title to the list of news items
                    newsItems.Add(title);
                }
                else
                {
                    // Log duplicate title for investigation
                    string logMessage = $"{DateTime.Now}: Дубль заголовка - {title}" + Environment.NewLine;
                    File.AppendAllText(logFilePath, logMessage, Encoding.UTF8);
                }
            }
        }

        public class EventLogger
        {
            private static EventLogger instance;
            internal static readonly object padlock = new object();

            public EventLogger() { }

            public static EventLogger Instance
            {
                get
                {
                    lock (padlock)
                    {
                        return instance ?? (instance = new EventLogger());
                    }
                }
            }

            public void LogEvent(string eventType, string eventText)
            {
                string logFilePath = "log.txt";

                string logMessage = $"{DateTime.Now}: {eventType} - {eventText}" + Environment.NewLine;

                lock (padlock)
                {
                    File.AppendAllText(logFilePath, logMessage, Encoding.UTF8);
                }
            }
        }
    }
}
