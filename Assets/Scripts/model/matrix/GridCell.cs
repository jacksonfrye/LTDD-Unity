using UnityEngine;

namespace Cariacity.game
{
    //public enum Status { Health, Security, Education, Entertainment}
    public enum Status { Health, Security, Education, Entertainment, Store }

    public class GridCell
    {
        public int i;
        public int j;
        public int type;
        //public float[] status = { 0, 0, 0, 0 };
        public float[] status = { 0, 0, 0, 0, 0 };
        public Vector3 center;
        public GameObject obj;

        public override string ToString()
        {
            var outStr = "";

            // _out += "i: " + i + " j: " + j + '\n';
            // _out += "tag: " + (obj != null ? obj.tag : "null") + '\n';
            outStr += "Health: " + status[(int)Status.Health] + '\n';
            outStr += "Security: " + status[(int)Status.Security] + '\n';
            outStr += "Education: " + status[(int)Status.Education] + '\n';
            outStr += "Entertainment: " + status[(int)Status.Entertainment] + '\n';
            outStr += "Store: " + status[(int)Status.Store];

            return outStr;
        }
    }
}
