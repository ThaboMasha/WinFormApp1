using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace WinFormApp1
{
    public class HistoryForm : Form
    {
        private DataGridView historyGrid;
        private TextBox searchBox;
        private DataTable historyTable;

        private ChatEngine engine = new ChatEngine();

        public HistoryForm()
        {
            BuildHistoryInterface();
            LoadHistory();
        }

        private void BuildHistoryInterface()
        {
            Text = "CyberSecureBot - Conversation History";
            Size = new Size(1100, 650);
            MinimumSize = new Size(750, 450);
            StartPosition = FormStartPosition.CenterParent;
            BackColor = Color.White;

            // Header
            Panel header = new Panel();
            header.Dock = DockStyle.Top;
            header.Height = 70;
            header.BackColor = Color.FromArgb(7, 94, 84);

            Label title = new Label();
            title.Text = "Conversation History";
            title.Dock = DockStyle.Fill;
            title.TextAlign = ContentAlignment.MiddleCenter;
            title.Font = new Font(
                "Segoe UI", 19, FontStyle.Bold);
            title.ForeColor = Color.White;

            header.Controls.Add(title);

            // Search and refresh area
            Panel topPanel = new Panel();
            topPanel.Dock = DockStyle.Top;
            topPanel.Height = 65;
            topPanel.Padding = new Padding(15);
            topPanel.BackColor = Color.FromArgb(245, 245, 245);

            searchBox = new TextBox();
            searchBox.Font = new Font("Segoe UI", 11);
            searchBox.Location = new Point(15, 17);
            searchBox.Size = new Size(350, 30);

            searchBox.PlaceholderText = "Search conversations...";

            searchBox.TextChanged += (sender, e) =>
            {
                ApplySearch();
            };

            Button refreshButton = new Button();
            refreshButton.Text = "Refresh";
            refreshButton.Size = new Size(100, 32);
            refreshButton.Location = new Point(380, 15);
            refreshButton.Font = new Font(
                "Segoe UI", 10, FontStyle.Bold);
            refreshButton.BackColor =
                Color.FromArgb(7, 94, 84);
            refreshButton.ForeColor = Color.White;
            refreshButton.FlatStyle = FlatStyle.Flat;
            refreshButton.FlatAppearance.BorderSize = 0;

            refreshButton.Click += (sender, e) =>
            {
                LoadHistory();
            };

            topPanel.Controls.Add(searchBox);
            topPanel.Controls.Add(refreshButton);

            // Conversation table
            historyGrid = new DataGridView();
            historyGrid.Dock = DockStyle.Fill;
            historyGrid.ReadOnly = true;
            historyGrid.AllowUserToAddRows = false;
            historyGrid.AllowUserToDeleteRows = false;
            historyGrid.AutoGenerateColumns = true;
            historyGrid.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;
            historyGrid.AutoSizeRowsMode =
                DataGridViewAutoSizeRowsMode.AllCells;
            historyGrid.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;
            historyGrid.MultiSelect = false;
            historyGrid.RowHeadersVisible = false;
            historyGrid.BackgroundColor = Color.White;
            historyGrid.BorderStyle = BorderStyle.None;

            historyGrid.ColumnHeadersDefaultCellStyle.BackColor =
                Color.FromArgb(7, 94, 84);
            historyGrid.ColumnHeadersDefaultCellStyle.ForeColor =
                Color.White;
            historyGrid.ColumnHeadersDefaultCellStyle.Font =
                new Font("Segoe UI", 10, FontStyle.Bold);

            historyGrid.EnableHeadersVisualStyles = false;

            historyGrid.DefaultCellStyle.Font =
                new Font("Segoe UI", 10);
            historyGrid.DefaultCellStyle.WrapMode =
                DataGridViewTriState.True;

            // Add controls
            Controls.Add(historyGrid);
            Controls.Add(topPanel);
            Controls.Add(header);
        }

        // Load conversations from SQL Server
        private void LoadHistory()
        {
            try
            {
                historyTable = engine.GetConversationHistory();

                historyGrid.DataSource = historyTable;

                if (historyGrid.Columns.Contains("Id"))
                {
                    historyGrid.Columns["Id"].HeaderText =
                        "ID";
                    historyGrid.Columns["Id"].FillWeight = 10;
                }

                if (historyGrid.Columns.Contains("UserMessage"))
                {
                    historyGrid.Columns["UserMessage"].HeaderText =
                        "User Message";
                    historyGrid.Columns["UserMessage"].FillWeight = 35;
                }

                if (historyGrid.Columns.Contains("BotResponse"))
                {
                    historyGrid.Columns["BotResponse"].HeaderText =
                        "Bot Response";
                    historyGrid.Columns["BotResponse"].FillWeight = 40;
                }

                if (historyGrid.Columns.Contains("SentAt"))
                {
                    historyGrid.Columns["SentAt"].HeaderText =
                        "Date and Time";
                    historyGrid.Columns["SentAt"].FillWeight = 20;
                    historyGrid.Columns["SentAt"].DefaultCellStyle.Format =
                        "dd/MM/yyyy HH:mm:ss";
                }

                ApplySearch();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Could not load conversation history.\n\n" +
                    ex.Message,
                    "Database Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        // Search conversations
        private void ApplySearch()
        {
            if (historyTable == null)
                return;

            string searchText = searchBox.Text.Trim();

            if (string.IsNullOrWhiteSpace(searchText))
            {
                historyTable.DefaultView.RowFilter = "";
                return;
            }

            string safeText = searchText.Replace("'", "''");

            historyTable.DefaultView.RowFilter =
                "Convert(UserMessage, 'System.String') LIKE '%" +
                safeText +
                "%' OR Convert(BotResponse, 'System.String') LIKE '%" +
                safeText + "%'";
        }
    }
}