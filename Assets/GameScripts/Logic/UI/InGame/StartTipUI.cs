using System.Collections;
using UnityEngine;

namespace Assets.GameScripts.Logic.UI.InGame
{
    public class StartTipUI : MonoBehaviour
    {
        void Start()
        {
            StartCoroutine(Timer());
        }

        private IEnumerator Timer()
        {
            yield return new WaitForSeconds(5);
            gameObject.SetActive(false);
        }

    }
}