using SeaBattle.Controls.ShipUnitControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SeaBattle.Controls
{
    /// <summary>
    /// Interaction logic for ShipInstance.xaml
    /// </summary>
    public partial class ShipInstance : UserControl
    {
        List<ShipUnit> shipCels;
        int _size;

        public ShipInstance(List<Cell> cells)
        {
            InitializeComponent();

            foreach (Cell cell in cells)
            {
                shipCels = new List<ShipUnit>();
                ShipUnit unit = new ShipUnit(cell);
                unit.Margin = cell.Margin;
                shipCels.Add(unit);


            }


            _size = cells.Count;

            Width = cells[0].Width * _size;

            Height = cells[0].Height * _size;
        }
    
    }
}
