using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CameraMovement : MonoBehaviour
{
    [SerializeField]
    private float speed;
    [SerializeField]
    private float axelerationCoeff = 3;

    [SerializeField]
    private float minOrSize = 2;

    [SerializeField]
    private float maxOrSize = 20;

    [SerializeField]
    private int neighborsDistance = 1;

    [SerializeField]
    private Tilemap tilemap;

    private List<Vector2Int> pastNeighbors;

    // Start is called before the first frame update
    void Start()
    {
        pastNeighbors = new List<Vector2Int>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 movement = new Vector3();

        if (Input.GetKey(KeyCode.W))
            movement.y += speed * Time.deltaTime;
        if (Input.GetKey(KeyCode.S))
            movement.y += -speed * Time.deltaTime;

        if (Input.GetKey(KeyCode.A))
            movement.x += -speed * Time.deltaTime;
        if (Input.GetKey(KeyCode.D))
            movement.x += speed * Time.deltaTime;

        if (Input.GetKey(KeyCode.LeftShift))
            movement *= axelerationCoeff;

        transform.Translate(movement);

        GetComponent<Camera>().orthographicSize += Input.mouseScrollDelta.y;

        if (GetComponent<Camera>().orthographicSize < minOrSize)
            GetComponent<Camera>().orthographicSize = minOrSize;

        if (GetComponent<Camera>().orthographicSize > maxOrSize)
            GetComponent<Camera>().orthographicSize = maxOrSize;

        var mc = tilemap.WorldToCell(GetComponent<Camera>().ScreenToWorldPoint(Input.mousePosition));

        //foreach (var pn in pastNeighbors)
        //    if (((WorldTile)(tilemap.GetTile(new Vector3Int(pn.x, pn.y, 0)))) != null)
        //    {
        //        ((WorldTile)(tilemap.GetTile(new Vector3Int(pn.x, pn.y, 0)))).color = Color.white;
        //        tilemap.RefreshTile(new Vector3Int(pn.x, pn.y, 0));
        //    }

        //pastNeighbors = World.Instance.GetNeighborsPositions(mc.x, mc.y, neighborsDistance);

        //if (Input.GetMouseButtonDown(0))
        //{
        //    print(mc);
        //    print(pastNeighbors.Count);
        //}

        //foreach (var pn in pastNeighbors)
        //    if (((WorldTile)(tilemap.GetTile(new Vector3Int(pn.x, pn.y, 0)))) != null)
        //    {
        //        ((WorldTile)(tilemap.GetTile(new Vector3Int(pn.x, pn.y, 0)))).color = Color.red;
        //        tilemap.RefreshTile(new Vector3Int(pn.x, pn.y, 0));
        //    }
    }
}
