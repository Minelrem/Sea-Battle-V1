using SeaBattle.Controls.ShipUnitControl;
using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace SeaBattle.Controls
{
    /// <summary>
    /// Interaction logic for ShipInstance.xaml
    /// </summary>
    public partial class ShipInstance : UserControl
    {
        
        public List<ShipUnit> shipCels;
        private int _size;
        private bool _horizontal;
        private bool _isDestroyed;

        public bool IsDestroyed { get => _isDestroyed; } //will be used to throw event to set cells around ship

        public ShipInstance(List<Cell> cells)
        {
            InitializeComponent();
            shipCels = new List<ShipUnit>();

            foreach (Cell cell in cells)
            {
                ShipUnit unit = new ShipUnit(cell);
                shipCels.Add(unit);
            }

            SetShipDirection();

            _size = cells.Count;

            SetSkin();

        }

        public void SetShipDirection()
        {
            if (shipCels[shipCels.Count - 1].Y == shipCels[0].Y)
                _horizontal = true;
            else
                _horizontal = false;
        }

        public void SetSkin()
        {
            List<ImageBrush> skins = GetSkin();

            for (int i = 0; i < shipCels.Count; i++)
            {
                shipCels[i].Background = skins[i];
                shipCels[i].State = CellState.Ship;
            }

        }

        public List<ImageBrush> GetSkin()
        {
            
            switch (_size)
            {
                case 1:
                    { 
                        ImageSource source = new BitmapImage(new Uri("Resources/1.png", UriKind.Relative));
                    
                        List<ImageBrush> skin = new List<ImageBrush>(1);
                        skin.Add(new ImageBrush(source));
                        return skin;
                    }
                case 2:
                    {
                        List<ImageBrush> skin = new List<ImageBrush>(2);

                        if (_horizontal)
                        {
                            skin.Add(new ImageBrush(new BitmapImage(new Uri("Resources/2/2.1horizontal.png", UriKind.Relative))));
                            skin.Add(new ImageBrush(new BitmapImage(new Uri("Resources/2/2.2horizontal.png", UriKind.Relative))));
                            return skin;
                        }
                        else
                        {
                            skin.Add((new ImageBrush(new BitmapImage(new Uri("Resources/2/2.1vertical.png", UriKind.Relative)))));
                            skin.Add((new ImageBrush(new BitmapImage(new Uri("Resources/2/2.2vertical.png", UriKind.Relative)))));
                            return skin;
                        }

                    }
                case 3:
                    {
                        List<ImageBrush> skin = new List<ImageBrush>(3);

                        if (_horizontal)
                        {
                            skin.Add(new ImageBrush(new BitmapImage(new Uri("Resources/3/3.1horizontal.png", UriKind.Relative))));
                            skin.Add(new ImageBrush(new BitmapImage(new Uri("Resources/3/3.2horizontal.png", UriKind.Relative))));
                            skin.Add(new ImageBrush(new BitmapImage(new Uri("Resources/3/3.3horizontal.png", UriKind.Relative))));
                            return skin;
                        }
                        else
                        {
                            skin.Add(new ImageBrush(new BitmapImage(new Uri("Resources/3/3.1vertical.png", UriKind.Relative))));
                            skin.Add(new ImageBrush(new BitmapImage(new Uri("Resources/3/3.2vertical.png", UriKind.Relative))));
                            skin.Add(new ImageBrush(new BitmapImage(new Uri("Resources/3/3.3vertical.png", UriKind.Relative))));
                            return skin;
                        }
                    }
                case 4:
                    {
                        List<ImageBrush> skin = new List<ImageBrush>(4);
                        if (_horizontal)
                        {
                            skin.Add(new ImageBrush(new BitmapImage(new Uri("Resources/4/4.1H.png", UriKind.Relative))));
                            skin.Add(new ImageBrush(new BitmapImage(new Uri("Resources/4/4.2H.png", UriKind.Relative))));
                            skin.Add(new ImageBrush(new BitmapImage(new Uri("Resources/4/4.3H.png", UriKind.Relative))));
                            skin.Add(new ImageBrush(new BitmapImage(new Uri("Resources/4/4.4H.png", UriKind.Relative))));
                            return skin;
                        }
                        else
                        {
                            skin.Add(new ImageBrush(new BitmapImage(new Uri("Resources/4/4.1V.png", UriKind.Relative))));
                            skin.Add(new ImageBrush(new BitmapImage(new Uri("Resources/4/4.2V.png", UriKind.Relative))));
                            skin.Add(new ImageBrush(new BitmapImage(new Uri("Resources/4/4.3V.png", UriKind.Relative))));
                            skin.Add(new ImageBrush(new BitmapImage(new Uri("Resources/4/4.4V.png", UriKind.Relative))));

                            return skin;
                        }
                    }
                default: return null;
            }
        }

    }
}
