using System.Text;
using System.Text.RegularExpressions;

namespace lab2
{
    public partial class Form1 : Form
    {

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
    }
}
