using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KnowledgeRepresentation.Interfaces;

namespace KnowledgeRepresentation.Bombs
{
    public class DemolitionExplosiveBomb : IBomb
    {
        public int BeepsLevel { get; private set; }
        public Disarming FirstStageDisarming { get; private set; }
        public Disarming SecondStageDisarming { get; private set; }
        public Disarming ThirdStageDisarming { get; private set; }

        public DemolitionExplosiveBomb()
        {
            BeepsLevel = 90;
            FirstStageDisarming = Disarming.CutGreenWire;
            SecondStageDisarming = Disarming.CutControlWire;
            ThirdStageDisarming = Disarming.CutYellowWire;
        }
    }
}
