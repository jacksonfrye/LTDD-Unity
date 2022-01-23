using UnityEngine;
using System;

namespace Cariacity.game
{
    class Home : Building
    {
        private static float value = 20;

        public static GameObject Project;
        public static GameObject[] Model = new GameObject[6];

        /*
        public static BuildingData Data = new BuildingData
        {
            Bounds = new Rectangle(1, 1, 0, 0),
            InfuenceType = (int)Status.Home,
            InfluenceBound = 5,
            Value = 20
        };
        */
        //-----------------test-----------------------------------
        private static int _isBuildable(GridCell _cell)
        {
            var x0 = _cell.i - 0; //left
            var x1 = _cell.i + 0; //right
            var y0 = _cell.j - 0; //top 
            var y1 = _cell.j + 0; //bottom

            if (x0 < 0 || x1 > (Constants.GridSize - 1) || y0 < 0 || y1 > (Constants.GridSize - 1))
                return 0;

            for (int i = x0; i <= x1; i++)
                for (int j = y0; j <= y1; j++)
                    if (Common.Matrix[i, j].obj != null)
                        return 0;

            GameObject _obj;

            if (_cell.obj != null)
                return 0;

            for (int i = x0 - 1; i < x1 + 2; i++)
            {
                if (y0 >= 0)
                {
                    _obj = Common.Matrix[_cell.i, y0 - 1].obj;
                    if (_obj != null && _obj.tag == Tags.Street) return 3;
                }

                if (y1 < Constants.GridSize)
                {
                    _obj = Common.Matrix[_cell.i, y1 + 1].obj;
                    if (_obj != null && _obj.tag == Tags.Street) return 1;
                }
            }

            for (int j = y0 - 1; j < y1 + 2; j++)
            {
                if (x0 >= 0)
                {
                    _obj = Common.Matrix[x0 - 1, _cell.j].obj;
                    if (_obj != null && _obj.tag == Tags.Street) { return 4; }
                }

                if (x1 < Constants.GridSize)
                {
                    _obj = Common.Matrix[x1 + 1, _cell.j].obj;
                    if (_obj != null && _obj.tag == Tags.Street) { return 2; }
                }
            }

            return 0;
        }
        //--------------------test--------------------------------
        public static bool IsBuildable(GridCell _cell)
        {
            //return _isBuildable(_cell) != 0;
            return _isBuildable(_cell) != 0;
        }
        //----------------------test------------------------------
        public static void SetOnMap(Vector3 pos)
        {
            var cell = Common.GetNearbyCell(pos);

            var rotation = _isBuildable(cell);
            //var idx = Random.Range(0, 6);
            var idx = 0;

            switch (rotation)
            {
                case 1: cell.obj = GameController.InitObj(Model[idx], pos, Quaternion.Euler(0, 45, 0)); break; //135
                case 2: cell.obj = GameController.InitObj(Model[idx], pos, Quaternion.Euler(0, 135, 0)); break; //225
                case 3: cell.obj = GameController.InitObj(Model[idx], pos, Quaternion.Euler(0, 225, 0)); break; //315
                case 4: cell.obj = GameController.InitObj(Model[idx], pos, Quaternion.Euler(0, 315, 0)); break; //45
            }
            
            Common.CurrentCity.HomeList.Add(cell);
            DebitFromMoney(value);
            /*
            if (IsBuildable(Common.GetNearbyCell(pos)))
            {
                SetOnMap(pos, Data);
                DebitFromMoney(Data.Value);
            }
            */
        }

        public static void RemoveFromMap(Vector3 pos)
        {
            var cell = Common.GetNearbyCell(pos);
            UnityEngine.Object.Destroy(cell.obj);
            cell.obj = null;
            //RemoveFromMap(pos, Data);
        }
    }
}
