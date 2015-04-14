using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KnowledgeRepresentation.Interfaces;

namespace KnowledgeRepresentation.Bombs
{
    public class ExplosiveBomb : IBomb
    {
        public int BeepsLevel { get; private set; }
        public Disarming FirstStageDisarming { get; private set; }
        public Disarming SecondStageDisarming { get; private set; }
        public Disarming ThirdStageDisarming { get; private set; }

        public ExplosiveBomb()
        {
            BeepsLevel = 4;
            FirstStageDisarming = Disarming.CutRedWire;
            SecondStageDisarming = Disarming.CutGreenWire;
            ThirdStageDisarming = Disarming.CutBlueWire;
        }
    }
}
