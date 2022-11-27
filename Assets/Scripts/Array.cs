using System.Threading.Tasks;
using UnityEngine;

public class Array : MonoBehaviour
{
    [SerializeField] private Node nodePrefab;
    [SerializeField] private float sortSpeed;
    [SerializeField] private int size;
    Node[] array;
    Task currentShift;
    bool canPress = true;

    void OnEnable()
    {
        InitArray();
    }
    
    void InitArray() {
        array = new Node[size];
        for (int i = 0; i < size; i++) {
            array[i] = Instantiate<Node>(nodePrefab, transform);
            array[i].InitNode();
            array[i].transform.position += Vector3.left * (size/2 - i);
        }
        PrintArray();
    }

    public async void InsertionSort() {
        canPress = false;
        Node key;
        int j;
        
        for (int i = 1; i < size; i++) {
            key = array[i];
            key.SetKey(true);
            currentShift = key.Shift(sortSpeed, Vector3.back);
            await currentShift;
            j = i - 1;
            while (j >= 0 && array[j].data > key.data) {
                currentShift = array[j].Shift(sortSpeed, Vector3.right);
                await currentShift;
                array[j+1] = array[j];
                currentShift = key.Shift(sortSpeed, Vector3.left);
                await currentShift;
                j--;
            }
            key.SetKey(false);
            array[j+1] = key;
            currentShift = key.Shift(sortSpeed, Vector3.forward);
            await currentShift;
        }
        PrintArray();
        canPress = true;
    }

    void DestroyNodes() {
        if (array != null) {
            for (int i = 0; i < size; i++) {
                Destroy(array[i]);
            }
        } else {
            print("Error: No array initialized.");
        }
    }

    public void Sort() {
        if (canPress) {
            InsertionSort();
        } else {
            print("Please wait.");
        }
    }

    public void Reset() {
        if (canPress) {
            DestroyNodes();
            InitArray();
        } else {
            print("Please wait.");
        }
    }

    void PrintArray() {
        string res = "";
        foreach (Node node in array) {
            res += node.data;
            if (node != array[size-1]){
                res += ", ";
            }
        }
        print(res);
    }

    void OnDestroy() => DestroyNodes();
}
