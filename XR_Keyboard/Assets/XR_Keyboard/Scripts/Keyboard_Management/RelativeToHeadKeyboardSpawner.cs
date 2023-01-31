using Leap.Unity.Controllers;
using Leap.Unity.Interaction.Keyboard;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Leap.Unity.Interaction.Storage
{
    public class RelativeToHeadKeyboardSpawner : KeyboardSpawner
    {
        public Transform head;
        public Vector3 DistanceFromHead = new Vector3(0, -0.5f, 0.4f);
        public Vector3 Angles;

        private Vector3 targetLocation;
        private Quaternion targetRotation;

        private Coroutine moveToRoutine;

        public override void SpawnKeyboard()
        {
            if (keyboardActive)
            {
                return;
            }
            else
            {
                keyboardActive = true;
            }
            KeyboardPrefabRoot.SetActive(keyboardActive);

        }

        public override void SpawnKeyboard(Transform currentlySelected)
        {
            SpawnKeyboard();
        }

        private void SetPosition()
        {

            Vector3 newPosition = head.position + (head.forward * DistanceFromHead.z);
            newPosition.y = head.position.y + DistanceFromHead.y;

            targetLocation = newPosition;

            if (moveToRoutine != null)
            {
                StopCoroutine(moveToRoutine);
            }
            moveToRoutine = StartCoroutine("MoveToTarget");
        }

        private IEnumerator MoveToTarget()
        {
            while (Vector3.Distance(KeyboardPrefabRoot.transform.position, targetLocation) > 0.005f)
            {
                KeyboardPrefabRoot.transform.position = Vector3.Lerp(KeyboardPrefabRoot.transform.position, targetLocation, Time.deltaTime * 30);
                KeyboardPrefabRoot.transform.rotation = Quaternion.Lerp(KeyboardPrefabRoot.transform.rotation, targetRotation, Time.deltaTime * 30);

                Vector3 pos = KeyboardPrefabRoot.transform.position;
                pos.y = head.position.y;
                Vector3 forward = pos - head.position;
                targetRotation = Quaternion.LookRotation(forward, Vector3.up);

                yield return new WaitForEndOfFrame();
            }
        }
    }
}