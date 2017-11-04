using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace SeaBattle.Controls
{
    /// <summary>
    /// Interaction logic for Cell.xaml
    /// </summary>
    public partial class Cell : UserControl
    {
        private CellState _state;
        private int _x;
        private int _y;
        public Field _field;

        public int X { get => _x; set => _x = value; }
        public int Y { get => _y; set => _y = value; }
        public CellState State { get => _state; set => _state = value; }

        public Cell(Field field, int X, int Y)
        {
            InitializeComponent();
            _field = field;
            _x = X;
            _y = Y;
        }

        private void Grid_MouseEnter(object sender, MouseEventArgs e)
        {
            //if (!_field.IsEnemy)   return; 

            MyGrid.Background = Brushes.Aqua;

         
        }

        private void MyGrid_OnMouseLeave(object sender, MouseEventArgs e)
        {
           // if (!_field.IsEnemy) return;

            
            if (_field.isActive && State != CellState.Ship)
                State = CellState.Choosen;

            MyGrid.Background = GetColor();
        }

        private void MyGrid_MouseDown(object sender, MouseButtonEventArgs e)
        {

            // if (!_field.IsEnemy) return; will be turned of when game mod on

            if (_state == CellState.Ship)
                _state = CellState.Damage;
            else if (_state != CellState.Damage)
                _state = CellState.Missed;

            _field.killed += _field_killed;

            MyGrid.Background = GetColor();
        }

        private void _field_killed(string mess)
        {
            MessageBox.Show("HH");
        }

        public void SetBackground()
        {
            MyGrid.Background = GetColor();
        }

        private ImageBrush GetColor()
        {
            switch (_state)
            {
                case CellState.Damage:
                    {
                        ImageSource source =  new BitmapImage(new Uri("Resources/X.png", UriKind.Relative));
                        return new ImageBrush(source);
                    }
                case CellState.Missed:
                    {
                        ImageSource source = new BitmapImage(new Uri("Resources/Missed.png", UriKind.Relative));
                        return new ImageBrush(source);
                    }
                case CellState.None:
                    {
                        {
                            ImageSource source = new BitmapImage(new Uri("Resources/None.png", UriKind.Relative));
                            return new ImageBrush(source);
                        }
                    }
                //case CellState.Ship:
                //    return Background;
                case CellState.Choosen:
                    {
                        ImageSource source = new BitmapImage(new Uri("Resources/Choosen.png", UriKind.Relative));
                        return new ImageBrush(source);
                    }
                default:return null;
            }
        }

    }

    public enum CellState
    {
        None = 0,
        Ship = 1,
        Damage = 2,
        Missed = 3,
        Choosen = 4
    }
}
