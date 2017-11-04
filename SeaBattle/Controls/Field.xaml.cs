using SeaBattle.Controls.ShipUnitControl;
using SeaBattle.Service;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace SeaBattle.Controls
{
    //    TO DO:
    //    Tasks+
    //    Images+;
    //    Cliks on ship+
    //    Refacctor

    /// <summary>
    /// Interaction logic for Field.xaml
    /// </summary>
    public partial class Field : Grid
    {
        public delegate void onKill(string mess);

        public event onKill killed;


        public static readonly DependencyProperty EnemyDependencyProperty = DependencyProperty.Register("IsEnemy", typeof(bool), typeof(Field));

        private List<ShipInstance> Ships;

        private Cell startCell;

        private Cell endCell;

        private List<Cell> choosen;

        public bool isActive;

        private Cell[,] _cells;
        private int _size = 10;


        public Cell[,] Cells => _cells;
        public bool IsEnemy
        {
            get => (bool)GetValue(EnemyDependencyProperty);
            set => SetValue(EnemyDependencyProperty, value);
        }


        public int Size => _size;

        public Cell this[int i, int j] => _cells[i, j];

        public Field()
        {
            InitializeComponent();
            BuildGrid();
            BuildFiled();

            Ships = new List<ShipInstance>();
        }

        private void BuildFiled()
        { 

            _cells = new Cell[_size, _size];

            for (int i = 0; i < _size; i++)
            {
                var hr = new Label
                {
                    Content = (char)('A' + i),
                    Width = 25,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center
                };
                var vr = new Label
                {
                    Content = i + 1,
                    Width = 25,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center
                };

                for (int j = 0; j < _size; j++)
                {

                    var cell = new Cell(this, j + 1, i + 1)
                    {
                        Height = 25,
                        Width = 25,
                        Margin = new Thickness(1)
                    };

                    _cells[i, j] = cell;

                    Grid.SetColumn(cell, j + 1);
                    Grid.SetRow(cell, i + 1);

                    Children.Add(cell);
                }
                Grid.SetRow(hr, 0);
                Grid.SetColumn(hr, i + 1);

                Grid.SetRow(vr, i + 1);
                Grid.SetColumn(vr, 0);

                Children.Add(vr);
                Children.Add(hr);
            }
        }

       

        private void BuildGrid()
        {
            for (int i = 0; i <= _size; i++)
            {
                var rowDef = new RowDefinition
                {
                    Height = GridLength.Auto
                };
                var colDef = new ColumnDefinition
                {
                    Width = GridLength.Auto
                };

                RowDefinitions.Add(rowDef);
                ColumnDefinitions.Add(colDef);
            }
        }

        private void Grid_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (Name == "UserField")
            {
                killed.Invoke("some");

                return;
            }

            foreach (Cell cell in Cells)
            {
                if (cell.State == CellState.Missed && !isActive)
                {
                    choosen = new List<Cell>();
                    cell.State = CellState.Choosen;
                    startCell = cell;
                    isActive = true;
                    return;
                }
                else if (cell.State == CellState.Missed && isActive)
                {

                    endCell = cell;
                    cell.State = CellState.Choosen;
                    isActive = false;

                    SetChoosen();

                    CheckChoosen();

                    CreateShip();

                    return;

                }
            }
        }

        private void SetChoosen()
        {
            foreach (Cell markedCell in Cells)
                if (markedCell.State == CellState.Choosen)
                    choosen.Add(markedCell);
        }

        private void CheckChoosen()
        {

            bool upDown = false, rghtLft = false;

            if ((((startCell.X > endCell.X) || (startCell.X < endCell.X)) && endCell.Y == startCell.Y) || choosen.Count == 1)
                rghtLft = true;
            else if (((endCell.Y > startCell.Y) || (endCell.Y < startCell.Y)) && endCell.X == startCell.X)
                upDown = true;


            if (!upDown && !rghtLft)
            {
                ClearChoosen();

                return;
            }

            foreach (Cell markedCell in choosen.ToArray())
            {
                if (rghtLft == true && markedCell.Y != startCell.Y && markedCell.State == CellState.Choosen
                    )

                    ClearChoosen();
                else if (upDown == true && markedCell.X != startCell.X && markedCell.State == CellState.Choosen)
                    ClearChoosen();

                foreach (ShipInstance ship in Ships)
                    foreach (Cell shipCell in ship.shipCels)
                        if (markedCell.X + 1 == shipCell.X && markedCell.Y == shipCell.Y || markedCell.X - 1 == shipCell.X && markedCell.Y == shipCell.Y || markedCell.Y + 1 == shipCell.Y && markedCell.X == shipCell.X || markedCell.Y - 1 == shipCell.Y && markedCell.X == shipCell.X ||
                        markedCell.X + 1 == shipCell.X && markedCell.Y - 1 == shipCell.Y || markedCell.X - 1 == shipCell.X && markedCell.Y + 1 == shipCell.Y || markedCell.Y + 1 == shipCell.Y && markedCell.X - 1 == shipCell.X || markedCell.Y - 1 == shipCell.Y && markedCell.X + 1 == shipCell.X)
                            ClearChoosen();
            }
        }

        private void ClearChoosen()
        {
            foreach (Cell choosenCell in Cells)
                if (choosenCell.State == CellState.Choosen)
                {
                    choosenCell.State = CellState.None;

                    choosenCell.SetBackground();
                }
            choosen.Clear();
        }


        private void CreateShip()
        {
            int _deckNum = choosen.Count;

            if (UnitOfWork.Instance.BattlefieldService.CreateShip(_deckNum))
            {
                ShipInstance ship = new ShipInstance(choosen);
                Ships.Add(ship);
                RefreshField();

            }
            else
                ClearChoosen();


        }

        private void RefreshField()
        {
            this.Children.Clear();

            _cells = new Cell[_size, _size];

            for (int i = 0; i < _size; i++)
            {
                var hr = new Label
                {
                    Content = (char)('A' + i),
                    Width = 25,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center
                };
                var vr = new Label
                {
                    Content = i + 1,
                    Width = 25,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center
                };

                for (int j = 0; j < _size; j++)
                {

                    var cell = new Cell(this, j + 1, i + 1)
                    {
                        Height = 25,
                        Width = 25,
                        Margin = new Thickness(1)
                    };

                    foreach (ShipInstance Ship in Ships)
                        foreach (ShipUnit one in Ship.shipCels)
                        {
                            if (one.X == cell.X && one.Y == cell.Y)
                            {

                                cell = one;
                                cell.SetBackground();

                            }
                        }

                    _cells[i, j] = cell;

                    Grid.SetColumn(cell, j + 1);
                    Grid.SetRow(cell, i + 1);

                    Children.Add(cell);
                }
                Grid.SetRow(hr, 0);
                Grid.SetColumn(hr, i + 1);

                Grid.SetRow(vr, i + 1);
                Grid.SetColumn(vr, 0);

                Children.Add(vr);
                Children.Add(hr);
            }
        }

        //Marking all cells around destroyed ship
        void SetDestroyedCells(ShipInstance ship)
        {
            foreach (Cell cell in Cells)
            {
                foreach (Cell shipCell in ship.shipCels)
                    if (cell.X == shipCell.X + 1 || cell.X == shipCell.X - 1 || cell.Y == shipCell.Y + 1 || cell.Y == shipCell.Y - 1
                       )
                    {
                        cell.State = CellState.Missed;
                        cell.SetBackground();
                    }
            }
        }
 

    }
}
