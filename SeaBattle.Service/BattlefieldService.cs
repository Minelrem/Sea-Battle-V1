using Interfaces;
using SeaBattle.Data.Context;
using SeaBattle.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq; 
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;


namespace SeaBattle.Service
{

    public class BattlefieldService : Base, IBattlefield
    {

        private class ShipsInstance
        {
            int _singleDeck, _doubleDeck, _tripleDeck, _quatroDeck;

            public ShipsInstance(int singleDecknum = 4, int doubleDecknum = 3, int tripleDecknum = 2, int quatroDecknum = 1)
            {
                _singleDeck = singleDecknum; _doubleDeck = doubleDecknum; _tripleDeck = tripleDecknum; _quatroDeck = quatroDecknum;
            }
            
            public bool OnShipCreated(int deckNum)
            {
                switch (deckNum)
                {
                    case 1:
                        {
                            if (_singleDeck > 0)
                            {
                                _singleDeck--;
                                return true;
                            }
                            else
                                return false;
                        }
                    case 2:
                        {
                            if (_doubleDeck > 0)
                            {
                                _doubleDeck--;
                                return true;
                            }
                            else
                                return false;
                        }
                    case 3:
                        {
                            if (_tripleDeck > 0)
                            {
                                _tripleDeck--;
                                return true;
                            }
                            else
                                return false;
                        }
                    case 4:
                        {
                            if (_quatroDeck > 0)
                            {
                                _quatroDeck--;
                                return true;
                            }
                            else
                                return false;
                        }
                    default: return false;
                }
            }
        }

        private ShipsInstance _shipInstance;

        public Battlefield Current { get; private set; }

        public BattlefieldService(SeaBattleContext context) : base(context)
        {
            _shipInstance = _shipInstance ?? new ShipsInstance();
        }

        public async void SaveToXML(List<Tuple<int, int, int>> tmp, int size)
        {
            var doc = new XmlDocument();
            var root = doc.CreateElement("field");
            foreach (Tuple<int, int, int> buff in tmp)
            {
                var node = doc.CreateElement("cell");

                var xAttr = doc.CreateAttribute("x");
                xAttr.InnerText = (buff.Item1).ToString();

                var yAttr = doc.CreateAttribute("y");
                yAttr.InnerText = (buff.Item2).ToString();

                var stateAttr = doc.CreateAttribute("state");
                stateAttr.InnerText = (buff.Item3).ToString();

                node.Attributes.Append(xAttr);
                node.Attributes.Append(yAttr);
                node.Attributes.Append(stateAttr);
                root.AppendChild(node);

            }
            doc.AppendChild(root);


            XElement a = XElement.Load(new XmlNodeReader(doc));
            Battlefield battlefield = new Battlefield() { Placement = a };
            _context.Battlefields.Add(battlefield);
            _context.SaveChanges();
        }

        public async Task<List<Tuple<int, int, int>>> LoadFromXML()
        {

            List<Tuple<int, int, int>> tmp = new List<Tuple<int, int, int>>();

            var field = _context.Battlefields.Where(t => t.Id > 0).ToList();

            if (field.Count == 0)
                return null;

            var doc = new XmlDocument();
            doc.LoadXml(field[3].placement);

            var root = doc.DocumentElement;
            var list = root.GetElementsByTagName("cell");

            foreach (XmlNode node in list)
            {

                int x = Convert.ToInt32(node.Attributes["x"].Value);
                int y = Convert.ToInt32(node.Attributes["y"].Value);
                int state = Convert.ToInt32(node.Attributes["state"].Value);


                tmp.Add(Tuple.Create(x, y, state));

            }

            return tmp;
        }

        public bool CreateShip(int deckNum)
        {
            return _shipInstance.OnShipCreated(deckNum);
        }

    }
}
