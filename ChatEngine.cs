using System;
using System.Data;
using Microsoft.Data.SqlClient;

namespace WinFormApp1
{
    public class ChatEngine
    {
        private string userName = "";
        private string favouriteTopic = "";

        private readonly string[] worriedWords =
        {
            "worried", "scared", "afraid",
            "anxious", "stressed", "help"
        };

        private readonly string connectionString =
            @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=New CyberSecureBotDB;Integrated Security=True;TrustServerCertificate=True;";

        public string GetResponse(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return "Please enter a message.";

            string question = input.Trim();
            string lower = question.ToLowerInvariant();

            CaptureUserInfo(question);

            if (lower.StartsWith("my name is "))
            {
                if (userName.Length > 0)
                    return "Nice to meet you, " + userName +
                           "! I'll remember your name during this session.";
            }

            if (lower.Contains("what is my name") ||
                lower.Contains("what's my name") ||
                lower.Contains("who am i"))
            {
                if (userName == "")
                    return "You haven't told me your name yet.";

                return "Your name is " + userName + ".";
            }

            if (lower.StartsWith("my favourite topic is ") ||
                lower.StartsWith("my favorite topic is "))
            {
                return "Great! I'll remember that your favourite topic is " +
                       favouriteTopic + ".";
            }

            if (lower.Contains("what is my favourite topic") ||
                lower.Contains("what is my favorite topic"))
            {
                if (favouriteTopic == "")
                    return "You haven't told me your favourite topic yet.";

                return "Your favourite topic is " + favouriteTopic + ".";
            }

            if (lower == "hi" || lower == "hello" ||
                lower == "hey" || lower.StartsWith("hi ") ||
                lower.StartsWith("hello "))
            {
                if (userName != "")
                    return "Hello " + userName +
                           "! Welcome back to CyberSecureBot. How can I help?";

                return "Hello! Welcome to CyberSecureBot. How can I help you?";
            }

            if (lower.Contains("who are you") ||
                lower.Contains("your name"))
                return "I'm CyberSecureBot, your personal cybersecurity assistant.";

            if (lower.Contains("your purpose") ||
                lower.Contains("what do you do"))
                return "My purpose is to help you understand cybersecurity " +
                       "and protect yourself from online threats.";

            if (lower.Contains("password"))
                return "PASSWORD SAFETY\n\n" +
                       "1. Use long, unique passwords.\n" +
                       "2. Never share your passwords.\n" +
                       "3. Use a trusted password manager.\n" +
                       "4. Enable two-factor authentication.\n" +
                       "5. Change compromised passwords immediately.";

            if (lower.Contains("phishing") ||
                lower.Contains("fake email") ||
                lower.Contains("scam"))
                return "PHISHING AWARENESS\n\n" +
                       "Phishing is an attempt to trick people into revealing private information.\n\n" +
                       "Watch out for suspicious links, fake emails, urgent messages " +
                       "and requests for passwords or banking details.\n\n" +
                       "Always verify the sender before responding.";

            if (lower.Contains("website") ||
                lower.Contains("safe browsing"))
                return "SAFE BROWSING TIPS\n\n" +
                       "1. Keep your browser updated.\n" +
                       "2. Avoid suspicious websites.\n" +
                       "3. Check website addresses carefully.\n" +
                       "4. Avoid unknown downloads.\n" +
                       "5. Never share sensitive information on untrusted sites.";

            if (lower.Contains("suspicious link") ||
                lower.Contains("unknown link"))
                return "Do not click suspicious links. Verify the sender and " +
                       "visit the official website directly instead.";

            if (lower.Contains("two-factor") ||
                lower.Contains("two factor") ||
                lower.Contains("2fa"))
                return "Two-factor authentication (2FA) adds an extra security " +
                       "layer to your account. It helps protect your account " +
                       "even if someone discovers your password.";

            if (lower.Contains("malware") ||
                lower.Contains("virus"))
                return "MALWARE AWARENESS\n\n" +
                       "Malware is harmful software designed to damage devices " +
                       "or steal information.\n\n" +
                       "Avoid unknown downloads, update your software " +
                       "and use reputable security protection.";

            foreach (string word in worriedWords)
            {
                if (lower.Contains(word))
                    return "I understand that this may be concerning. " +
                           "I'm here to help you learn about cybersecurity. " +
                           "What would you like to know?";
            }

            if (lower.Contains("thank"))
                return "You're welcome! Stay alert and stay safe online.";

            if (lower.Contains("bye") || lower.Contains("goodbye"))
                return "Goodbye " + userName +
                       "! Remember to stay safe online.";

            return "I'm not sure how to answer that yet. Try asking me about " +
                   "passwords, phishing, malware, safe browsing or " +
                   "two-factor authentication.";
        }

        // Save conversation
        public void SaveConversation(string userMessage, string botResponse)
        {
            using (SqlConnection connection =
                   new SqlConnection(connectionString))
            {
                connection.Open();

                string query = @"INSERT INTO dbo.[Table]
                    (UserMessage, BotResponse, SentAt)
                    VALUES (@UserMessage, @BotResponse, @SentAt)";

                using (SqlCommand command =
                       new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserMessage", userMessage);
                    command.Parameters.AddWithValue("@BotResponse", botResponse);
                    command.Parameters.AddWithValue("@SentAt", DateTime.Now);

                    command.ExecuteNonQuery();
                }
            }
        }

        // NEW: Retrieve conversation history
        public DataTable GetConversationHistory()
        {
            DataTable history = new DataTable();

            using (SqlConnection connection =
                   new SqlConnection(connectionString))
            {
                connection.Open();

                string query = @"SELECT Id, UserMessage, BotResponse, SentAt
                                 FROM dbo.[Table]
                                 ORDER BY Id DESC";

                using (SqlCommand command =
                       new SqlCommand(query, connection))
                using (SqlDataAdapter adapter =
                       new SqlDataAdapter(command))
                {
                    adapter.Fill(history);
                }
            }

            return history;
        }

        // Capture user information
        private void CaptureUserInfo(string input)
        {
            string question = input.Trim();
            string lower = question.ToLowerInvariant();

            if (lower.StartsWith("my name is "))
                userName = question.Substring(11).Trim();

            else if (lower.StartsWith("my favourite topic is "))
                favouriteTopic = question.Substring(22).Trim();

            else if (lower.StartsWith("my favorite topic is "))
                favouriteTopic = question.Substring(22).Trim();
        }
    }
}