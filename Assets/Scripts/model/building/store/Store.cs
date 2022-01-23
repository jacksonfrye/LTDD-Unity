using UnityEngine;

namespace Cariacity.game
{
    class Store : Building
    {
        /*
        private static float value = 20;

        public static GameObject Project;
        public static GameObject Model;

        public static int InfluenceBound = 3;

        private static int _isBuildable(GridCell _cell)
        {
            var x0 = _cell.i - 1; //left
            var x1 = _cell.i + 1; //right
            var y0 = _cell.j - 1; //top
            var y1 = _cell.j + 1; //bottom

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

        public static bool IsBuildable(GridCell _cell)
        {
            return _isBuildable(_cell) != 0;
        }

        public static void SetOnMap(Vector3 pos)
        {
            var _cell = Common.GetNearbyCell(pos);
            var mat = Common.Matrix;
            var line = _cell.i;
            var column = _cell.j;
            var rotation = _isBuildable(_cell);

            var x0 = _cell.i - 1; //left
            var x1 = _cell.i + 1; //right
            var y0 = _cell.j - 1; //top
            var y1 = _cell.j + 1; //bottom

            switch (rotation)
            {
                case 1: _cell.obj = GameController.InitObj(Model, pos, Quaternion.Euler(0, 45, 0)); break;
                case 2: _cell.obj = GameController.InitObj(Model, pos, Quaternion.Euler(0, 135, 0)); break;
                case 3: _cell.obj = GameController.InitObj(Model, pos, Quaternion.Euler(0, 225, 0)); break;
                case 4: _cell.obj = GameController.InitObj(Model, pos, Quaternion.Euler(0, 315, 0)); break;
            }

            for (int i = x0; i <= x1; i++)
                for (int j = y0; j <= y1; j++)
                    mat[i, j].obj = _cell.obj;

            var end_line = Mathf.Clamp(line + InfluenceBound, 0, Constants.GridSize - 1);
            var start_line = Mathf.Clamp(line - InfluenceBound, 0, Constants.GridSize - 1);
            var end_column = Mathf.Clamp(column + InfluenceBound, 0, Constants.GridSize - 1);
            var start_column = Mathf.Clamp(column - InfluenceBound, 0, Constants.GridSize - 1);

            for (int i = start_line; i <= end_line; i++)
                for (int j = start_column; j <= end_column; j++)
                    mat[i, j].status[InfuenceType] += (mat[i, j].center - pos).magnitude;

            DebitFromMoney(value);
            Common.CurrentCity.HomeList.Add(_cell);
        }

        public static void RemoveFromMap(Vector3 pos)
        {
            var cell = Common.GetNearbyCell(pos);
            Object.Destroy(cell.obj);
            cell.obj = null;
        }
        */
        public static BuildingData Data = new BuildingData
        {
            Bounds = new Rectangle(0, 1, 0, 0),
            InfuenceType = (int)Status.Store,
            InfluenceBound = 3,
            Value = 100
        };

        public static bool IsBuildable(GridCell cell)
        {
            return IsBuildable(cell, Data.Bounds);
        }

        public static void SetOnMap(Vector3 pos)
        {
            if (IsBuildable(Common.GetNearbyCell(pos)))
            {
                SetOnMap(pos, Data);
                DebitFromMoney(Data.Value);
            }
        }

        public static void RemoveFromMap(Vector3 pos)
        {
            RemoveFromMap(pos, Data);
        }
    }
}
