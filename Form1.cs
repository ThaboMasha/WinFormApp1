using System;
using System.Collections.Generic;
using System.Drawing;
using System.Media;
using System.Windows.Forms;

namespace WinFormApp1
{
    public partial class Form1 : Form
    {
        // Form-level state variables (Part 2, Steps 11 & 14)
        private string userName = "";
        private string lastTopic = "";
        private string favouriteTopic = "";
        private Random rng = new Random();

        // Data structures for keywords and sentiment (Part 3 & 4)
        private Dictionary<string, List<string>> keywordResponses;
        private List<string> worriedWords;

        public Form1()
        {
            InitializeComponent();
            InitializeBotData();
        }

        private void InitializeBotData()
        {
            // Keyword mapping for cybersecurity topics (Step 13)
            keywordResponses = new Dictionary<string, List<string>>
            {
                { "password", new List<string> {
                    "Always use strong, unique passwords with at least 12 characters.",
                    "Consider using a password manager to store credentials securely."
                }},
                { "scam", new List<string> {
                    "Be cautious of unsolicited emails or texts asking for personal details.",
                    "Verify the sender before clicking on links or downloading attachments."
                }},
                { "privacy", new List<string> {
                    "Review your social media privacy settings regularly.",
                    "Avoid sharing personal identifiers on public forms or networks."
                }}
            };

            // Words used for sentiment check (Step 17)
            worriedWords = new List<string> { "worried", "scared", "confused", "frustrated", "anxious", "help" };
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            PlayGreeting();
            DisplayLogo();

            // Initial greeting prompt in conversation box
            if (rtbConversation != null)
            {
                rtbConversation.AppendText("Bot: Welcome to CyberSecureBot! What is your name?\r\n\r\n");
            }
        }

        private void PlayGreeting()
        {
            try
            {
                // Using relative path to avoid crash on other machines
                SoundPlayer player = new SoundPlayer("voice/greeting.wav");
                player.Play();
            }
            catch
            {
                // Fallback for absolute path testing
                try
                {
                    SoundPlayer player = new SoundPlayer("C:\\Users\\Student\\Downloads\\Absa ATM 2.wav");
                    player.Play();
                }
                catch { /* Audio file missing */ }
            }
        }

        private void DisplayLogo()
        {
            try
            {
                pictureBox1.Image = Image.FromFile("images/logo.png");
            }
            catch
            {
                // Fallback for absolute path testing
                try
                {
                    pictureBox1.Image = Image.FromFile("C:\\Users\\Student\\Pictures\\Screenshots\\image.png");
                }
                catch { /* Image file missing */ }
            }

            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.Location = new Point(0, 0);
        }

        // Send Button Event Handler (Step 5 & 6)
        // Ensure your Send button's Click event in Properties is tied to this method
        private void btnSend_Click(object sender, EventArgs e)
        {
            ProcessUserInput();
        }

        // Enter Key Handler (Step 7)
        // Ensure your input text box's KeyDown event in Properties is tied to this method
        private void txtUserInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // Stop Windows notification chime
                ProcessUserInput();
            }
        }

        private void ProcessUserInput()
        {
            string input = txtUserInput.Text.Trim();

            // Guard against empty input (Step 19)
            if (string.IsNullOrWhiteSpace(input))
                return;

            // Output user text
            rtbConversation.AppendText($"You: {input}\n");
            txtUserInput.Clear();

            // Generate bot reply
            string reply = GetResponse(input);

            // Output bot text
            rtbConversation.AppendText($"Bot: {reply}\n\n");

            // Auto scroll to bottom
            rtbConversation.SelectionStart = rtbConversation.Text.Length;
            rtbConversation.ScrollToCaret();
        }

        // Adapted Response Logic (Step 10, 12 - 18)
        private string GetResponse(string input)
        {
            string text = input.ToLower();

            // Capture Name (Step 12)
            if (string.IsNullOrEmpty(userName))
            {
                userName = input;
                return $"Nice to meet you, {userName}!{Environment.NewLine}How can I help you with cybersecurity today?";
            }

            // Capture Favorite Topic / Memory (Step 16)
            if (text.Contains("my favorite topic is") || text.Contains("i like"))
            {
                if (text.Contains("password")) favouriteTopic = "passwords";
                else if (text.Contains("scam")) favouriteTopic = "scams";
                else favouriteTopic = "cybersecurity";

                return $"Got it! I'll remember that you are interested in {favouriteTopic}.{Environment.NewLine}Ask me anything about it.";
            }

            // Sentiment Detection (Step 17)
            string prefix = "";
            foreach (string word in worriedWords ?? new List<string>())
            {
                if (text.Contains(word))
                {
                    prefix = "Don't worry, I am here to help guide you! ";
                    break;
                }
            }

            // Follow-up Handling (Step 15)
            if ((text.Contains("tell me more") || text.Contains("another tip")) && !string.IsNullOrEmpty(lastTopic))
            {   
                if (keywordResponses.ContainsKey(lastTopic))
                {
                    List<string> tips = keywordResponses[lastTopic];
                    // Provide a multi-line tip set (two lines if available)
                    int i = rng.Next(tips.Count);
                    string first = tips[i];
                    string second = tips.Count > 1 ? tips[(i + 1) % tips.Count] : "";
                    string body = first;
                    if (!string.IsNullOrEmpty(second))
                        body += Environment.NewLine + second;
                    return prefix + $"Here is another detail on {lastTopic}:{Environment.NewLine}{body}";
                }
            }

            // Keyword Matching (Step 13)
            foreach (var key in keywordResponses.Keys)
            {
                if (text.Contains(key))
                {
                    lastTopic = key; // Save topic state for follow-ups
                    List<string> responses = keywordResponses[key];
                    int idx = rng.Next(responses.Count);
                    string first = responses[idx];
                    string second = responses.Count > 1 ? responses[(idx + 1) % responses.Count] : "";
                    string combined = first;
                    if (!string.IsNullOrEmpty(second))
                        combined += Environment.NewLine + second;
                    // Return a multi-line reply (each tip on its own line)
                    return prefix + combined;
                }
            }

            // Central Fallback (Step 18)
            return prefix + "I'm not sure I understand." + Environment.NewLine +
                   "Try asking me about 'passwords', 'scams', or 'privacy'.";
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            // Left empty per Step 8 instructions
        }

        private void rtbConversation_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtUserInput_TextChanged(object sender, EventArgs e)
        {

        }
    }
}