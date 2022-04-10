﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChampionsLeague
{
    public partial class ChampionsLeagueForm : Form
    {

        private PlayersRecords playersRecords = new PlayersRecords();
        private PlayersFileSerializerDeserializer serializer;
        private readonly string file = "Players.players";


        public ChampionsLeagueForm()
        {
            InitializeComponent();
            serializer = new PlayersFileSerializerDeserializer(playersRecords,file);
            playersRecords.PlayersCountChanged += UpdatePlayerListBox;
            playersRecords.PlayersCountChanged += UpdateConsoleListBox;
            serializer.Load();
        }

        private void UpdatePlayerListBox(object sender, PlayersCountChangedEventArgs e)
        {
            UpdatePlayersListBox();
        }

        private void UpdateConsoleListBox(object sender, PlayersCountChangedEventArgs e)
        {
            string date = DateTime.Now.ToString("HH:mm:ss");

            listBoxConsole.Items.Add($"{date} | Změna počtu hráču z {e.OldCount} na {e.NewCount}");
            listBoxConsole.Refresh();
        }

        private void UpdatePlayersListBox() {
            listBoxPlayers.Items.Clear();
            Player[] pl = new Player[playersRecords.Count];
            for (int i = 0; i < playersRecords.Count; i++) {
                pl[i] = playersRecords[i];
            }
            listBoxPlayers.Items.AddRange(pl);
            listBoxPlayers.Refresh();
        }

        private void buttonAddPlayer_Click(object sender, EventArgs e)
        {
            PlayerForm playerForm = new PlayerForm();
            if (playerForm.ShowDialog() == DialogResult.OK)
            {
                playersRecords.Add(new Player(playerForm.PlayerName,playerForm.Club,playerForm.Goals));
            }
        }

        private void buttonEditPlayer_Click(object sender, EventArgs e)
        {
            Player? selected = (Player?)listBoxPlayers.SelectedItem;
            int selectedIndex = listBoxPlayers.SelectedIndex;
            if (selected == null)
            {
                MessageBox.Show("Mus9te vybrat hrčee z listu.");
                return;
            }

            PlayerForm playerForm = new PlayerForm(selected.Name,selected.Club,selected.GoalCount);
            if (playerForm.ShowDialog() == DialogResult.OK)
            {
                selected.Name = playerForm.PlayerName;
                selected.Club = playerForm.Club;
                selected.GoalCount = playerForm.Goals;

                playersRecords.players[selectedIndex] = selected;
                UpdatePlayersListBox();
            }

        }

        private void buttonDeletePlayer_Click(object sender, EventArgs e)
        {
            int selected = listBoxPlayers.SelectedIndex;

            if (selected == -1)
            {
                MessageBox.Show("Musíte vybrat hráče ze seznamu.");
                return;
            }

            playersRecords.Delete(selected);
        }

        private void buttonBestClubs_Click(object sender, EventArgs e)
        {
            if (playersRecords.FindBestClubs(out FootballClub[] clubs, out int goalCount))
            {
                BestPlayersForm bestDialog = new BestPlayersForm(clubs, goalCount);
                bestDialog.ShowDialog();
                return;
            }

            MessageBox.Show("Musíte nejdřív přidat hráče do seznamu.");
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            Debug.WriteLine("ButtonSaveClicked");
            serializer.Save();
        }

        private void buttonLoad_Click(object sender, EventArgs e)
        {
            Debug.WriteLine("ButtonLoadClicked");
            serializer.Load();
        }
    }
}
