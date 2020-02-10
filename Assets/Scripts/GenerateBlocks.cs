using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GenerateBlocks : MonoBehaviour
{
    [SerializeField]
    GameObject map;
    public int noOfRows = 9;
    private GameObject crate;

    private List<Tuple<int, int>> createCornersList()
    {
        List<Tuple<int, int>> list = new List<Tuple<int, int>>();

        // top left corner
        list.Add(new Tuple<int, int>(-noOfRows, noOfRows - 1));
        list.Add(new Tuple<int, int>(-noOfRows, noOfRows));
        list.Add(new Tuple<int, int>(-noOfRows + 1, noOfRows));

        // top right corner
        list.Add(new Tuple<int, int>(-noOfRows, -noOfRows + 1));
        list.Add(new Tuple<int, int>(-noOfRows, -noOfRows));
        list.Add(new Tuple<int, int>(-noOfRows + 1, -noOfRows));

        // bottom right corner
        list.Add(new Tuple<int, int>(noOfRows - 1, -noOfRows));
        list.Add(new Tuple<int, int>(noOfRows, -noOfRows));
        list.Add(new Tuple<int, int>(noOfRows, -noOfRows + 1));

        // bottom left corner
        list.Add(new Tuple<int, int>(noOfRows, noOfRows - 1));
        list.Add(new Tuple<int, int>(noOfRows, noOfRows));
        list.Add(new Tuple<int, int>(noOfRows - 1, noOfRows));

        return list;
    }

    // Start is called before the first frame update
    void Start()
    {
        crate = Resources.Load<GameObject>("Crate") as GameObject;

        List<Tuple<int, int>> cornersList = createCornersList();

        for (var i = -noOfRows; i <= noOfRows; i++)
        {
            for (var j = -noOfRows; j <= noOfRows; j++)
            {
                Vector3 position = new Vector3(i, 0.5f, j);

                var rate = UnityEngine.Random.Range(0f, 1f);

                if (rate <= 0.7)
                {
                    if (!Physics.CheckSphere(position, 0.4f) && !cornersList.Contains(new Tuple<int, int>(i, j)))
                    {
                        Instantiate(crate, position, Quaternion.identity, map.transform);

                    }
                }
            }
        }
    }
}
