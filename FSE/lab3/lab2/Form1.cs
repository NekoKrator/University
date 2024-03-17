using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace lab2
{
    public partial class Form1 : Form
    {
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
            int vowelCount = text.Count(c => "aeiouAEIOU‡Â≥øÓÛˇ∫˛ø¿≈≤ØŒ”ﬂ™ﬁØ".Contains(c));
            int consonantCount = text.Count(c => "bcdfghjklmnpqrstvwxyzBCDFGHJKLMNPQRSTVWXYZ·‚„‰ÊÁÈÍÎÏÌÔÒÚÙıˆ˜¯˘¡¬√ƒ∆«… ÀÃÕœ–—“‘’÷◊ÿŸ".Contains(c));
            int digitCount = text.Count(c => char.IsDigit(c));
            int specialCharCount = text.Count(c => !char.IsLetterOrDigit(c) && !char.IsWhiteSpace(c));
            int punctuationCount = text.Count(c => char.IsPunctuation(c));
            int cyrillicCount = text.Count(c => '\u0400' <= c && c <= '\u04FF');
            int latinCount = text.Count(c => ('a' <= c && c <= 'z') || ('A' <= c && c <= 'Z'));

            toolStripStatusLabel1.Text = $"{sizeInBytes / 1024.0} KB";
            toolStripStatusLabel2.Text = $"{charCount} ÒËÏ‚ÓÎ≥‚";
            toolStripStatusLabel3.Text = $"{paragraphCount} ‡·Á‡ˆ≥‚";
            toolStripStatusLabel4.Text = $"{emptyLineCount} ÔÓÓÊÌ≥ı ˇ‰Í≥‚";
            toolStripStatusLabel5.Text = $"{authorPageCount} ‡‚ÚÓÒ¸ÍËı ÒÚÓ≥ÌÓÍ";
            toolStripStatusLabel6.Text = $"{vowelCount} „ÓÎÓÒÌËı";
            toolStripStatusLabel7.Text = $"{consonantCount} ÔË„ÓÎÓÒÌËı";
            toolStripStatusLabel8.Text = $"{digitCount} ˆËÙ";
            toolStripStatusLabel9.Text = $"{specialCharCount} ÒÔÂˆ. ÒËÏ‚ÓÎ≥‚";
            toolStripStatusLabel10.Text = $"{punctuationCount} ÔÛÌÍÚ. ÒËÏ‚ÓÎ≥‚";
            toolStripStatusLabel11.Text = $"{cyrillicCount} ÍËËÎË˜ÌËı";
            toolStripStatusLabel12.Text = $"{latinCount} Î‡ÚËÌÒ¸ÍËı";
        }

        private void refactorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string originalText = richTextBox1.Text;
            string refactoredText = Regex.Replace(originalText, @"\s+", " ");

            richTextBox2.Text = refactoredText;

            DialogResult dialogResult = MessageBox.Show("◊Ë ·‡Ê‡∫ÚÂ ‚Ë Á·ÂÂ„ÚË ÁÏ≥ÌË?", "œ≥‰Ú‚Â‰ÊÂÌÌˇ", MessageBoxButtons.YesNo);
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

        private void ÍÎ˛˜Ó‚≥—ÎÓ‚‡CToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var words = richTextBox1.Text.Split(new char[] { ' ', '\t', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
            var keywordCount = words.Count(word => keywords.Contains(word));

            MessageBox.Show($" ≥Î¸Í≥ÒÚ¸ ÍÎ˛˜Ó‚Ëı ÒÎ≥‚: {keywordCount}");
        }

        private void Ì‡È‰Ó‚¯≥—ÎÓ‚‡ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var words = richTextBox1.Text.Split(new char[] { ' ', '\t', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
            var longestWords = words.OrderByDescending(word => word.Length).Take(10);

            string result = string.Join(", ", longestWords);
            MessageBox.Show($"10 Ì‡È‰Ó‚¯Ëı ÒÎ≥‚: {result}");
        }

        public void UpdateAmountOfNews(int amount)
        {
            amountOfNews = amount;
        }

        private async void downloadTextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2(this);
            form2.ShowDialog();

            await DownloadNews(amountOfNews);
        }

        private async Task DownloadNews(int amountOfNews)
        {
            var newsItems = new List<Tuple<string, string>>();
            HttpClient client = new HttpClient();

            for (int i = 0; i < amountOfNews; i += 10)
            {
                if (newsItems.Count >= amountOfNews) break;

                string url = $"https://www.znu.edu.ua/cms/index.php?action=news/view&start={i}&site_id=27&lang=ukr";
                string pageContent = await client.GetStringAsync(url);

                Regex newsRegex = new Regex("<div class=\"znu-2016-new\">.*?<div class=\"row\">.*?<div class=\"a-container\">.*?<h4>.*?<a.*?>(.*?)</a>", RegexOptions.Singleline);
                Regex textRegex = new Regex("<div class=\"znu-2016-new\">.*?<div class=\"row\">.*?<div class=\"a-container\">.*?<div class=\"text\">(.*?)</div>", RegexOptions.Singleline);

                var titleMatches = newsRegex.Matches(pageContent);
                var textMatches = textRegex.Matches(pageContent);

                for (int j = 0; j < titleMatches.Count; j++)
                {
                    string title = titleMatches[j].Groups[1].Value;

                    if (j < textMatches.Count)
                    {
                        string text = textMatches[j].Groups[1].Value;
                        text = Regex.Replace(text, "<.*?>", String.Empty); // Remove HTML tags
                        text = Regex.Replace(text, "&\\w+;", String.Empty); // Remove words that start with &
                        newsItems.Add(new Tuple<string, string>(title, text));
                    }
                }
            }

            richTextBox1.Clear();
            for (int i = 0; i < newsItems.Count; i++)
            {
                richTextBox1.SelectionFont = new Font(richTextBox1.Font, FontStyle.Bold);
                richTextBox1.AppendText($"{i + 1}. {newsItems[i].Item1}\n");
                richTextBox1.SelectionFont = new Font(richTextBox1.Font, FontStyle.Regular);
                richTextBox1.AppendText($"{newsItems[i].Item2}\n\n");
            }
        }

    }
}
