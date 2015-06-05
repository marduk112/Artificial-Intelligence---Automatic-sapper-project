using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovementAlgorithms.DepthFirstAlgorithm
{
    public class Vert
    {
        public Vert(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
        public int x { get; set; }
        public int y { get; set; }
        public List<Vert> Verticies
        {
            get
            {
                return VerticiesList;
            }
        }

        public void addChild(Vert v)
        {
            VerticiesList.Add(v);
        }

        List<Vert> VerticiesList = new List<Vert>();

        public override string ToString()
        {
            return (x + " " + y).ToString();
        }

    }
}
