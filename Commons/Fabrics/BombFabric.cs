using Commons.Interfaces;

namespace Commons.Fabrics
{
    //Fabric which create bombs
    public class BombFabric
    {
        public IBomb CreateBomb(BombTypes bombType)
        {
            switch (bombType)
            {
                case BombTypes.Mine:
                    return null;
                default:
                    return null;
            }
        }
    }
}
