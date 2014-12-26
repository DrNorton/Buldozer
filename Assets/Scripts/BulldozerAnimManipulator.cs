using Assets.Scripts;
using Assets.Scripts.Grid;
using UnityEngine;
using System.Collections;

    public class BulldozerAnimManipulator : MonoBehaviour
    {
        public Vector2 direction;
        void Update()
        {
            this.transform.Translate(direction * Time.deltaTime,Space.Self);
        }

        void OnBecameInvisible()
        {
            transform.Rotate(new Vector3(90, 0, 0));
            Debug.LogWarning("гемморой");
        }
    }

