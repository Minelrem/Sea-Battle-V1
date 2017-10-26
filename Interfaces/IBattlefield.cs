using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
    public interface IBattlefield
    {
        void SaveToXML(List<Tuple<int, int, int>> tmp, int size);
        Task<List<Tuple<int, int, int>>> LoadFromXML();
        bool CreateShip(int deckNum);
    }
}
