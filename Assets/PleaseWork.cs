using UnityEngine;

public class PleaseWork : MonoBehaviour
{
    private void Start()
    {
        Debug.Log("Pleae Please PLease PLease plelase PLeae");
        Debug.Log("Annuder test");
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            

    }
}
