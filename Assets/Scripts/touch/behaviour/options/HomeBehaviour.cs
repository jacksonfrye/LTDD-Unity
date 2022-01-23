using System;
using System.Collections.Generic;
using UnityEngine;

namespace Cariacity.game
{
    public enum HomeBuilding { Home1, Home2 }
    class HomeBehaviour : BuildingBehaviour
    {
        private float _timer;
        private IList<GameObject> _projects;
        /*
        private HomeBuilding _type;
        private GameObject _currentProject;

        public override void CtrlZ() { }
        public override void OnBegan(GridCell cell) { }
        public override void OnEnded(GridCell cell) { }
        public override void OnCanceled(GridCell cell) { }
        public override void OnStationary(GridCell cell) { }

        public HomeBehaviour(HomeBuilding homeBuilding)
        {
            BuildingData data;
            _type = homeBuilding;

            switch (_type)
            {
                case HomeBuilding.Home1: data = Home1.Data; break;
                case HomeBuilding.Home2: data = Home2.Data; break;
                default: return;
            }

            var pos = new Vector3(0, -10, 0);
            //var influenceArea = CommonModels.InfluenceObj;
            var bound = data.InfluenceBound * 2;

            _currentProject = GameController.InitObj(data.Project, pos);

            //influenceArea.SetActive(true);
            //influenceArea.transform.localScale = new Vector3(bound, 0.01f, bound);
            //influenceArea.transform.position = pos;
        }
        */
        public HomeBehaviour()
        {
            _projects = new List<GameObject>();
        }
        
        public override void OnBegan(GridCell cell)
        {
            _timer = Time.time;
        }

        public override void OnMoved(GridCell cell)
        {
            if (Home.IsBuildable(cell))
            {
                foreach (var item in _projects)
                    if (item.transform.position == cell.center)
                        return;

                _projects.Add(GameController.InitObj(Home.Project, cell.center));
                //_projects.Add(GameController.InitObj(Home1.Data.Project, cell.center));
            };
        }

        public override void OnEnded(GridCell cell)
        {
            if (Time.time - _timer < 0.2)
                for (int i = 0; i < _projects.Count; i++)
                    if (_projects[i].transform.position == cell.center)
                    {
                        UnityEngine.Object.Destroy(_projects[i]);
                        _projects.RemoveAt(i);
                    }
        }

        public override void OnCanceled(GridCell cell) { throw new NotImplementedException(); }
        public override void OnStationary(GridCell cell) { throw new NotImplementedException(); }
       
        public override void Apply()
        {
            foreach (var item in _projects)
            {
                Home.SetOnMap(item.transform.position);
                UnityEngine.Object.Destroy(item);
            }
        }

        public override void Clean()
        {
            foreach (var item in _projects)
                UnityEngine.Object.Destroy(item);
        }
        
        public override void CtrlZ()
        {
            throw new NotImplementedException();
        }
        /*
        public override void OnMoved(GridCell cell)
        {
            _currentProject.transform.position = cell.center;
            CommonModels.InfluenceObj.transform.position = cell.center;

            switch (_type)
            {
                case HomeBuilding.Home1: Building.SetRenderer(_currentProject, Home1.IsBuildable(cell)); break;
                case HomeBuilding.Home2: Building.SetRenderer(_currentProject, Home2.IsBuildable(cell)); break;
            }
        }
        public override void Apply()
        {
            var pos = _currentProject.transform.position;

            switch (_type)
            {
                case HomeBuilding.Home1: Home1.SetOnMap(pos); break;
                case HomeBuilding.Home2: Home2.SetOnMap(pos); break;
            }

            Clean();
        }

        public override void Clean()
        {
            Object.Destroy(_currentProject);
            CommonModels.InfluenceObj.SetActive(false);
        }
        */
    }
}
