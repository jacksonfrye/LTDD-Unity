using System.Collections.Generic;
using UnityEngine;

namespace Cariacity.game
{
    public class StoreBehaviour : BuildingBehaviour
    {
        /*
        private float _timer;
        private IList<GameObject> _projects;

        public override void CtrlZ() { }
        public override void OnCanceled(GridCell cell) { }
        public override void OnStationary(GridCell cell) { }

        public StoreBehaviour()
        {
            _projects = new List<GameObject>();
        }

        public override void OnBegan(GridCell cell)
        {
            _timer = Time.time;
        }

        public override void OnMoved(GridCell cell)
        {
            if (Store.IsBuildable(cell))
            {
                foreach (var item in _projects)
                    if (item.transform.position == cell.center)
                        return;

                _projects.Add(GameController.InitObj(Store.Project, cell.center));
            };
        }

        public override void OnEnded(GridCell cell)
        {
            if (Time.time - _timer < 0.2)
                for (int i = 0; i < _projects.Count; i++)
                    if (_projects[i].transform.position == cell.center)
                    {
                        Object.Destroy(_projects[i]);
                        _projects.RemoveAt(i);
                    }
        }

        public override void Apply()
        {
            foreach (var item in _projects)
            {
                Store.SetOnMap(item.transform.position);
                Object.Destroy(item);
            }
        }

        public override void Clean()
        {
            foreach (var item in _projects)
                Object.Destroy(item);
        }
        */
        private GameObject _currentProject;

        public override void CtrlZ() { }
        public override void OnBegan(GridCell cell) { }
        public override void OnEnded(GridCell cell) { }
        public override void OnCanceled(GridCell cell) { }
        public override void OnStationary(GridCell cell) { }

        public StoreBehaviour()
        {
            BuildingData data;
            data = Store.Data;

            var pos = new Vector3(0, -10, 0);
            var influenceArea = CommonModels.InfluenceObj;
            var bound = data.InfluenceBound * 2;

            _currentProject = GameController.InitObj(data.Project, pos);

            influenceArea.SetActive(true);
            influenceArea.transform.localScale = new Vector3(bound, 0.01f, bound);
            influenceArea.transform.position = pos;
        }

        public override void OnMoved(GridCell cell)
        {
            _currentProject.transform.position = cell.center;
            CommonModels.InfluenceObj.transform.position = cell.center;

            Building.SetRenderer(_currentProject, Store.IsBuildable(cell));
        }

        public override void Apply()
        {
            var pos = _currentProject.transform.position;

            Store.SetOnMap(pos);

            Clean();
        }

        public override void Clean()
        {
            Object.Destroy(_currentProject);
            CommonModels.InfluenceObj.SetActive(false);
        }
    }
}
