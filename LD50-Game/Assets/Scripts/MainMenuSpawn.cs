using System.Collections;
using UnityEngine;

namespace Nidavellir
{
    public class MainMenuSpawn : MonoBehaviour
    {
        private void Start()
        {
            this.StartCoroutine(this.Shrink());
            this.SetRandomScale();
        }

        private void SetRandomScale()
        {
            var random = Random.Range(1, 15);
            this.transform.localScale = Vector3.one * random;
        }

        private IEnumerator Shrink()
        {
            yield return new WaitForSeconds(5f);
            var t = 0f;
            var startScale = this.transform.localScale;

            while (t < 2f)
            {
                t += Time.deltaTime;
                this.transform.localScale = Vector3.Lerp(startScale, Vector3.zero, t / 2f);
                yield return new WaitForEndOfFrame();
            }

            Destroy(this.gameObject);
        }
    }
}