using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawnPos : MonoBehaviour
{
    public PolygonCollider2D polygonCollider;
    public int numberRandomPositions = 10;
    public GameObject Fireball;

    void Start()
    {
        if (polygonCollider == null) GetComponent<PolygonCollider2D>();
        if (polygonCollider == null) Debug.Log("Please assign PolygonCollider2D component.");

        int i = 0;
        while (i < numberRandomPositions)
        {
            Vector3 rndPoint3D = RandomPointInBounds(polygonCollider.bounds, 1f);
            Vector2 rndPoint2D = new Vector2(rndPoint3D.x, rndPoint3D.y);
            Vector2 rndPointInside = polygonCollider.ClosestPoint(new Vector2(rndPoint2D.x, rndPoint2D.y));
            if (rndPointInside.x == rndPoint2D.x && rndPointInside.y == rndPoint2D.y)
            {

                //GameObject Test = Instantiate(Fireball, rndPoint2D, Quaternion.identity);
                //Instantiate(Fireball, rndPoint2D, Quaternion.identity);
                //Fireball = GameObject.CreatePrimitive(PrimitiveType.Cube);
                //Test.transform.localScale = new Vector3(1f, 1f, 1f);
                //Test.transform.position = rndPoint2D;
                GameObject Test = GameObject.Instantiate(Fireball, rndPoint3D, Quaternion.identity);

                //GameObject rndCube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                Test.transform.localScale = new Vector3(1f, 1f, 1f);
                Test.transform.position = rndPoint2D;

                Debug.Log("TESt");

                i++;
            }
        }
    }

    private Vector3 RandomPointInBounds(Bounds bounds, float scale)
    {
        return new Vector3(
            Random.Range(bounds.min.x * scale, bounds.max.x * scale),
            Random.Range(bounds.min.y * scale, bounds.max.y * scale),
            Random.Range(bounds.min.z * scale, bounds.max.z * scale)
        );
    }
}
