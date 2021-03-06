﻿using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using SeaBattle.Controls;
using SeaBattle.Service;
using SeaBattle.Login;

namespace SeaBattle
{
    
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        public MainWindow()
        {
            InitializeComponent();

        }

        private void LoginForm_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!((LoginWindow)sender).IsSucces)
                Close();

            UnitOfWork.Instance.GameService.GameState = false; 
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {

            int size = EnemyField.Size;

            List<Tuple<int, int, int>> tmp = new List<Tuple<int, int, int>>();

            for (int i = 0; i < EnemyField.Size; i++)
            {
                int x, y, state;

                for (int j = 0; j < EnemyField.Size; j++)
                {
                    var cell = EnemyField[i, j];

                    x = (cell.X - 1);

                    y = (cell.Y - 1);

                    state = (cell.State == CellState.Ship ? (int)CellState.Ship : (int)CellState.None);

                    tmp.Add(Tuple.Create(x, y, state));
                }

            }

            EnemyField.ClearField();


            UnitOfWork.Instance.BattlefieldService.SaveToXML(tmp, size);
        }

        private async void button1_Click(object sender, RoutedEventArgs e)
        {
            UnitOfWork.Instance.GameService.GameState = true;

            UserField.LoadField();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoginWindow loginForm = new LoginWindow();

            loginForm.Closing += LoginForm_Closing;

            loginForm.ShowDialog();


        }


        private void RefreshShipArea()
        {
            System.Windows.MessageBox.Show("Created");
        }

        private async void button1_Copy_Click(object sender, RoutedEventArgs e)
        {
            
            UnitOfWork.Instance.GameService.GameState = true;

            EnemyField.ClearField();

            var res = await UnitOfWork.Instance.BattlefieldService.LoadFromXML();

            foreach (Tuple<int, int, int> tmp in res)
            {

                var cell = EnemyField[tmp.Item2, tmp.Item1];
                cell.X = tmp.Item1;
                cell.Y = tmp.Item2;
                cell.State = (CellState)tmp.Item3;
            }

        }
    }
   
}
