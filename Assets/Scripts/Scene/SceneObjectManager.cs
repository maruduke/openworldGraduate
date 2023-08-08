using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using MySystem.SceneControl;

namespace MySystem.SceneControl {
    public class SceneObjectManager
    {

#region Singleton
        private static SceneObjectManager instance = null;

        public static SceneObjectManager Instance {

            get {
                if(instance == null) {
                    instance = new SceneObjectManager();
                }

                return instance;
            }
        }
#endregion

        public List<SceneController> sceneControllers = new List<SceneController>();


        public void update(int x, int z) {
            foreach(SceneController sceneController in sceneControllers) {
                sceneController.update(x , z);
            }
        }

        public void Register(SceneController sceneController) {
            sceneControllers.Add(sceneController);
        }

        public void UnRegister(SceneController sceneController) {
            sceneControllers.Remove(sceneController);
        }

        public SceneController FindSceneController(string sceneName) {
            foreach(SceneController sceneController in sceneControllers)
            {
                if(sceneController.sceneName == sceneName) {
                    return sceneController;
                }

            }

            return null;
        }


    }
}