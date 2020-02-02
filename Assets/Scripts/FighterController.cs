using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class FighterController : MonoBehaviour
{
    public static List<Node> route = new List<Node>();
    public static List<Fighter> fighters = new List<Fighter>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void goOut()
    {
        if (route.Any())
        {
            var doorData = fighters[0].currentNode.findDoor(route[0]);
            foreach (var f in fighters)
            {
                f.target = doorData.position;
                f.doorDirection = doorData.direction;
            }
            route.RemoveAt(0);
        }
        else
        {
            var doorData = fighters[0].currentNode.chooseDoor();
            foreach (var f in fighters)
            {
                f.target = doorData.position;
                f.doorDirection = doorData.direction;
            }
        }
    }

    public void killed(Fighter f)
    {
        fighters.Remove(f);
    }
}
