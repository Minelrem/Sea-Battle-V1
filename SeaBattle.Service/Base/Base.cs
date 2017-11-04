using SeaBattle.Data.Context;

namespace SeaBattle.Service
{
    public class Base
    {

        protected readonly SeaBattleContext _context;

        public Base(SeaBattleContext context)
        {
            _context = context;
        }
    }
}
