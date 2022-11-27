using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class Node : MonoBehaviour
{
    public int data;

    public void InitNode() {
        data = Random.Range(1, 50);  
        transform.localScale += new Vector3(0f, data * 0.10f, 0f);
    }

    public async Task Shift(float speed, Vector3 direction) {
        float elapsedTime = 0f;
        Vector3 pos = this.transform.position;

        while (elapsedTime < speed) {
            elapsedTime += Time.deltaTime;
            this.transform.position = Vector3.Lerp(pos, pos + direction, elapsedTime);
            await Task.Yield();
        }
    }

    public void SetKey(bool isKey) {
        Renderer renderer = GetComponentInChildren<Renderer>();
        if (isKey) {
            renderer.material.color = Color.cyan;
        } else {
            renderer.material.color = Color.black;
        }
    }

    void OnDestroy() => Destroy(gameObject);
}
