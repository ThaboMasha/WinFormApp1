using System;
using System.Drawing;
using System.IO;
using System.Media;
using System.Windows.Forms;

namespace WinFormApp1
{
    public partial class Form1 : Form
    {
        private Panel headerPanel;
        private Panel chatBackground;
        private Panel bottomPanel;
        private FlowLayoutPanel chatFlow;

        private SoundPlayer greetingPlayer;
        private ChatEngine engine = new ChatEngine();

        public Form1()
        {
            InitializeComponent();
            BuildWhatsAppInterface();
        }

        // Build the WhatsApp-style chatbot interface
        private void BuildWhatsAppInterface()
        {
            this.Text = "CyberSecureBot";
            this.Size = new Size(1000, 700);
            this.MinimumSize = new Size(700, 500);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.White;

            // Hide original designer controls
            rtbConversation.Visible = false;
            pictureBox1.Visible = false;

            // Header
            headerPanel = new Panel();
            headerPanel.Dock = DockStyle.Top;
            headerPanel.Height = 75;
            headerPanel.BackColor = Color.FromArgb(7, 94, 84);

            Label titleLabel = new Label();
            titleLabel.Text = "CyberSecureBot";
            titleLabel.ForeColor = Color.White;
            titleLabel.Font = new Font(
                "Segoe UI", 17, FontStyle.Bold);
            titleLabel.AutoSize = true;
            titleLabel.Location = new Point(22, 10);

            Label subtitleLabel = new Label();
            subtitleLabel.Text = "Your cybersecurity assistant";
            subtitleLabel.ForeColor =
                Color.FromArgb(220, 240, 235);
            subtitleLabel.Font = new Font("Segoe UI", 9);
            subtitleLabel.AutoSize = true;
            subtitleLabel.Location = new Point(24, 44);

            headerPanel.Controls.Add(titleLabel);
            headerPanel.Controls.Add(subtitleLabel);

            // History button
            Button historyButton = new Button();
            historyButton.Text = "History";
            historyButton.Size = new Size(110, 35);
            historyButton.Font = new Font(
                "Segoe UI", 10, FontStyle.Bold);

            historyButton.BackColor = Color.White;
            historyButton.ForeColor =
                Color.FromArgb(7, 94, 84);

            historyButton.FlatStyle = FlatStyle.Flat;
            historyButton.FlatAppearance.BorderSize = 0;

            historyButton.Location = new Point(
                headerPanel.ClientSize.Width - 130, 20);

            // Keep History button on the right when resizing
            headerPanel.Resize += (sender, e) =>
            {
                historyButton.Location = new Point(
                    Math.Max(
                        10,
                        headerPanel.ClientSize.Width
                        - historyButton.Width - 20
                    ),
                    20
                );
            };

            // Open conversation history
            historyButton.Click += (sender, e) =>
            {
                using (HistoryForm historyForm = new HistoryForm())
                {
                    historyForm.ShowDialog(this);
                }
            };

            headerPanel.Controls.Add(historyButton);

            // Chat background
            chatBackground = new Panel();
            chatBackground.Dock = DockStyle.Fill;
            chatBackground.BackColor =
                Color.FromArgb(235, 229, 221);
            chatBackground.Padding = new Padding(10);

            chatFlow = new FlowLayoutPanel();
            chatFlow.Dock = DockStyle.Fill;
            chatFlow.FlowDirection = FlowDirection.TopDown;
            chatFlow.WrapContents = false;
            chatFlow.AutoScroll = true;
            chatFlow.BackColor =
                Color.FromArgb(235, 229, 221);
            chatFlow.Padding = new Padding(5);

            chatBackground.Controls.Add(chatFlow);

            // Bottom input area
            bottomPanel = new Panel();
            bottomPanel.Dock = DockStyle.Bottom;
            bottomPanel.Height = 75;
            bottomPanel.BackColor =
                Color.FromArgb(245, 245, 245);
            bottomPanel.Padding = new Padding(12);

            // User input textbox
            txtUserInput.Parent = bottomPanel;
            txtUserInput.Visible = true;
            txtUserInput.Multiline = false;
            txtUserInput.Font = new Font("Segoe UI", 11);
            txtUserInput.BackColor = Color.White;
            txtUserInput.ForeColor = Color.Black;
            txtUserInput.BorderStyle =
                BorderStyle.FixedSingle;
            txtUserInput.Anchor =
                AnchorStyles.Left |
                AnchorStyles.Right |
                AnchorStyles.Top;

            // Send button
            btnSend.Parent = bottomPanel;
            btnSend.Visible = true;
            btnSend.Text = "Send";
            btnSend.Font = new Font(
                "Segoe UI", 10, FontStyle.Bold);
            btnSend.BackColor = Color.FromArgb(7, 94, 84);
            btnSend.ForeColor = Color.White;
            btnSend.FlatStyle = FlatStyle.Flat;
            btnSend.FlatAppearance.BorderSize = 0;
            btnSend.Anchor =
                AnchorStyles.Top | AnchorStyles.Right;

            // Resize input controls
            bottomPanel.Resize += (sender, e) =>
            {
                int buttonWidth = 90;
                int gap = 10;
                int left = 12;
                int top = 22;

                int inputWidth = Math.Max(
                    100,
                    bottomPanel.ClientSize.Width
                    - left - buttonWidth - gap - 12
                );

                txtUserInput.Location =
                    new Point(left, top);

                txtUserInput.Size =
                    new Size(inputWidth, 32);

                btnSend.Location = new Point(
                    left + inputWidth + gap,
                    top
                );

                btnSend.Size =
                    new Size(buttonWidth, 32);
            };

            // Add panels to form
            this.Controls.Add(chatBackground);
            this.Controls.Add(bottomPanel);
            this.Controls.Add(headerPanel);

            // Initial chatbot greeting
            AddChatBubble(
                "Hello! Welcome to CyberSecureBot. " +
                "I'm here to help you stay safe online.\n\n" +
                "You can ask me about password safety, phishing, " +
                "safe browsing, suspicious links and cybersecurity.",
                false
            );
        }

        // Play greeting sound
        private void PlayGreeting()
        {
            try
            {
                string soundPath = Path.Combine(
                    Application.StartupPath,
                    "voice",
                    "greeting.wav"
                );

                if (!File.Exists(soundPath))
                {
                    soundPath =
                        @"C:\Users\Student\Downloads\Absa ATM 2.wav";
                }

                if (File.Exists(soundPath))
                {
                    greetingPlayer = new SoundPlayer(soundPath);
                    greetingPlayer.Play();
                }
            }
            catch (Exception)
            {
                // Continue without sound if playback fails.
            }
        }

        // Create chat bubble
        private void AddChatBubble(
            string message,
            bool isUser)
        {
            if (chatFlow == null)
                return;

            int availableWidth = Math.Max(
                300,
                chatFlow.ClientSize.Width - 45
            );

            Panel bubbleRow = new Panel();
            bubbleRow.Width = availableWidth;
            bubbleRow.Height = 10;
            bubbleRow.Margin = new Padding(2, 4, 2, 4);
            bubbleRow.Tag = isUser;

            Label bubble = new Label();
            bubble.AutoSize = true;
            bubble.MaximumSize = new Size(
                Math.Max(180, availableWidth - 45),
                0
            );

            bubble.Text = message;
            bubble.Font = new Font("Segoe UI", 10);
            bubble.Padding = new Padding(10);
            bubble.BorderStyle = BorderStyle.None;

            if (isUser)
            {
                bubble.BackColor =
                    Color.FromArgb(213, 249, 200);

                bubble.ForeColor =
                    Color.FromArgb(25, 35, 25);
            }
            else
            {
                bubble.BackColor = Color.White;
                bubble.ForeColor =
                    Color.FromArgb(35, 35, 35);
            }

            bubbleRow.Height = bubble.PreferredHeight + 4;

            if (isUser)
            {
                bubble.Location = new Point(
                    Math.Max(
                        0,
                        bubbleRow.Width -
                        bubble.PreferredWidth - 5
                    ),
                    0
                );
            }
            else
            {
                bubble.Location = new Point(5, 0);
            }

            bubbleRow.Controls.Add(bubble);

            // Keep chat rows responsive
            bubbleRow.Resize += (sender, e) =>
            {
                int maxBubbleWidth = Math.Max(
                    180,
                    bubbleRow.ClientSize.Width - 45
                );

                bubble.MaximumSize =
                    new Size(maxBubbleWidth, 0);

                if (isUser)
                {
                    bubble.Location = new Point(
                        Math.Max(
                            0,
                            bubbleRow.ClientSize.Width -
                            bubble.PreferredWidth - 5
                        ),
                        0
                    );
                }
                else
                {
                    bubble.Location = new Point(5, 0);
                }

                bubbleRow.Height =
                    bubble.PreferredHeight + 4;
            };

            chatFlow.Controls.Add(bubbleRow);
            chatFlow.ScrollControlIntoView(bubbleRow);
        }

        // Send button click
        private void btnSend_Click(
            object sender,
            EventArgs e)
        {
            SendMessage();
        }

        // Send message and save conversation
        private void SendMessage()
        {
            string userMessage =
                txtUserInput.Text.Trim();

            if (string.IsNullOrWhiteSpace(userMessage))
            {
                MessageBox.Show(
                    "Please type a message before sending.",
                    "Empty Message",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                txtUserInput.Focus();
                return;
            }

            // Display user message
            AddChatBubble(userMessage, true);
            txtUserInput.Clear();

            // Get chatbot response
            string response =
                engine.GetResponse(userMessage);

            // Display chatbot response
            AddChatBubble(response, false);

            // Save conversation to SQL Server
            try
            {
                engine.SaveConversation(
                    userMessage,
                    response
                );
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Could not save conversation " +
                    "to database.\n\n" + ex.Message,
                    "Database Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
            }

            txtUserInput.Focus();
        }

        // Enter key sends message
        private void txtUserInput_KeyDown(
            object sender,
            KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                SendMessage();
            }
        }

        // Form load event
        private void Form1_Load(
            object sender,
            EventArgs e)
        {
            PlayGreeting();
            txtUserInput.Focus();
        }

        // Existing designer event handlers
        private void pictureBox1_Click(
            object sender,
            EventArgs e)
        {
            // Logo area is hidden.
        }

        private void rtbConversation_TextChanged(
            object sender,
            EventArgs e)
        {
            // Original conversation textbox is replaced.
        }

        private void txtUserInput_TextChanged(
            object sender,
            EventArgs e)
        {
            // Reserved for future features.
        }
    }
}