using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ScorePointer1.Models;
using ScorePointer1.Services;
using System.IO;

namespace ScorePointer1
{
    public partial class Form3 : Form 
    {
        GameService _gameService;
        Games _games = new Games();
       
        public Form3(GameService gameService)
        {
            _gameService = gameService;
            InitializeComponent();
        }

        private string GetSelsectedName()
        {
            return lbPlayersInGame.SelectedItem.ToString().Split("---").ToArray()[0];
            
        }
        private double GetSelectedPoints()
        {
            return Convert.ToDouble(txtSlectedPoints.Text);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            AddPoints();
        }

        private void lbPlayersInGame_SelectedIndexChanged(object sender, EventArgs e)
        {
            EnableMinusPlus();
        }
        private void EnableMinusPlus()
        {
            if (lbPlayersInGame.SelectedIndex >= 0 || lbPlayersInGame.SelectedIndex <= _gameService.GetGame().Players.Count - 1)
            {
                button1.Enabled = true;
                btMinus.Enabled = true;
            }
            else
            {
                button1.Enabled = false;
                btMinus.Enabled = false;
            }
        }
        private void AddPlayersToLB()
        {
            foreach (var item in _gameService.GetGame().Players)
            {
                lbPlayersInGame.Items.Add(item.Name + "---" + item.StartingPoints.ToString());
            }

        }
        private void AddPoints()
        {
            foreach (Player item in _gameService.GetGame().Players)
            {
                if (item.Name == GetSelsectedName())
                {
                    item.StartingPoints = item.StartingPoints + GetSelectedPoints();
                }
            }
            lbPlayersInGame.Items.Clear();
            AddPlayersToLB();
        }
        private void MinusPoints()
        {
            foreach (Player item in _gameService.GetGame().Players)
            {
                if (item.Name == GetSelsectedName())
                {
                    item.StartingPoints = item.StartingPoints - GetSelectedPoints();
                }
            }
            lbPlayersInGame.Items.Clear();
            AddPlayersToLB();
        }

        private void lbGameNameStart_Click(object sender, EventArgs e)
        {
            lbGameNameStart.Text = _gameService.GetGame().Name.ToString();  
            AddPlayersToLB();
            Timer timer1 = new Timer();
            timer1.Interval = 1000;
            timer1.Start();
        }

        private void btMinus_Click(object sender, EventArgs e)
        {
            MinusPoints();
        }

        private void btEnd_Click(object sender, EventArgs e)
        {
            _gameService.GetGame().FinishedOn = DateTime.Now;
            //
            _games.SaveGame(_gameService.GetGame());
            //
            timer1.Stop();
            Message();
            PrintGame();
        }
        private void PrintGame()
        {
            string filepath = @"D:\Programing\Proizvodstwena praktika\ScorePointer1\Games";
            string line = "# " + _gameService.GetGame().AddedOn.ToString() + " The game --- " + _gameService.GetGame().Name + " --- has finished on " + _gameService.GetGame().FinishedOn + ". The winner is " + _gameService.GetGame().GetWinner().Name + " with " + _gameService.GetGame().GetWinner().StartingPoints + ".";
            StreamWriter sw = new StreamWriter(filepath, true, Encoding.Unicode);
            sw.WriteLine(line);
            sw.Close();
        }
        private void Message()
        {
            string gameName = _gameService.GetGame().Name.ToString();
            string winnerName = _gameService.GetGame().GetWinner().Name.ToString();
            string points = _gameService.GetGame().GetWinner().StartingPoints.ToString();
            string timer = timer1.Interval.ToString();
            string message = "The winner of the " + gameName + " game is " + winnerName + " with score of " + points + ". Game duration: " + timer + ". Would you like to start a new game?";
            string title = "The game has finished"; 
            if (MessageBox.Show(message, title, MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Form1 f1 = new Form1();
                f1.Show();
                Visible = false;
            }
            else
            {
                Application.Exit();
            }
        }
        
        private void Form3_Load(object sender, EventArgs e)
        {
            
        }
    }
}
