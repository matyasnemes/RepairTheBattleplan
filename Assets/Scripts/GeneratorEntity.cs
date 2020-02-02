using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorEntity : MonoBehaviour
{

    float SQUARE_SIDE = 0.32f;
    int smallroomsize = 5;
    int corridors = 5;
    int corridorsize = 1;

    public GameObject[] SmallRooms;
    public GameObject HorCor;
    public GameObject VerCor;
    public GameObject knight;

    public List<GameObject> RoomObjects;
    public List<GameObject> CorridorObjects;
    public Sprite[] sprites;

    MapGenGraph g;

    // Start is called before the first frame update
    void Start()
    {
        g = new MapGenGraph();
        
        g.InitGraph(4, 30);

        CreateMap(g);

        List<GameObject> knights = new List<GameObject>();
        knights.Add(Instantiate(knight, new Vector3(0.0f, 0.16f, 0.0f), new Quaternion(0.0f, 0.0f, 0.0f, 0.0f)));
        knights.Add(Instantiate(knight, new Vector3(0.0f, -0.16f, 0.0f), new Quaternion(0.0f, 0.0f, 0.0f, 0.0f)));
        knights.Add(Instantiate(knight, new Vector3(0.16f, 0.16f, 0.0f), new Quaternion(0.0f, 0.0f, 0.0f, 0.0f)));
        knights.Add(Instantiate(knight, new Vector3(-0.16f, 0.16f, 0.0f), new Quaternion(0.0f, 0.0f, 0.0f, 0.0f)));
        knights.Add(Instantiate(knight, new Vector3(0.16f, 0.0f, 0.0f), new Quaternion(0.0f, 0.0f, 0.0f, 0.0f)));

        int i = 0;
        Fighter f = null;
        foreach(var k in knights)
        {
            k.GetComponent<SpriteRenderer>().sprite = sprites[i];
            f = k.GetComponent<Fighter>();
            f.currentNode = g.Start.jnode;
            i++;
        }
        g.Start.jnode.fighterCount = 5;
        
        //VALAKI HIVJON EGY FIGHTER INDITOT
        //Fighter.goOut();

        g.Print();

    }

    public void CreateMap(MapGenGraph g)
    {
        List<Vertex> visited = new List<Vertex>();
        List<Vertex> waiting = new List<Vertex>();

        waiting.Add(g.Start);

        while(waiting.Count != 0)
        {
            Vertex v = waiting[0];
            waiting.RemoveAt(0);

            //Azért van csere, mert koordináta váltás
            PutRoom(new Vector3( (v.X - g.Start.X)*10*SQUARE_SIDE, -1*(v.Y - g.Start.Y)*10*SQUARE_SIDE), v);

            for(int i = 0; i < 4; i++)
            {
                if(v.CheckNeighbour(i))
                {
                    
                    Vertex n = v.GetNeighbour(i);
                    if(!visited.Contains(n))
                    {

                        PutCorBtwNeighBours(v, n, i);

                        waiting.Add(n);
                    }
                }
            }

            
            visited.Add(v);
        }
    }

    public void PutRoom(Vector3 Pos, Vertex v)
    {
        string s = "";

        for(int i = 3; i >= 0; i--)
        {
            if(v.CheckNeighbour(i))
            {
                s += '1';
            }
            else
            {
                s += '0';
            }

        }

        if(v.X != g.Start.X)
        {

        }

        if(v.Y != g.Start.Y)
        {

        }


        int index = System.Convert.ToInt32(s,2);

        var roomobject = Instantiate(SmallRooms[index - 1], Pos, new Quaternion(0.0f, 0.0f, 0.0f, 0.0f));

        RoomAction ra = null;

        foreach (Transform child in roomobject.transform)
        {
            foreach (Transform ch in child)
            {
                
                if(ch.tag == "Room")
                {
                    ra = ch.GetComponent<RoomAction>();  
                    
                }

            }

            if(child.tag == "Room")
            {
                ra = child.GetComponent<RoomAction>();  
                
            }
        }

        if(ra == null)
        {
            throw new System.ArgumentException("Room has no room tag child");
        }

        ra.node = v.jnode;

        for(int i = 0; i < 4; i++)
        {
            if(v.CheckNeighbour(i))
            {
                switch(i)
                {
                    case 0:
                    
                        ra.node.neighbours[ v.GetNeighbour(i).jnode ] = new doorData( new Vector2(Pos.x - 2.0f*SQUARE_SIDE, Pos.y), new Vector2(-1.0f, 0.0f));

                    break;
                    case 1:

                        ra.node.neighbours[v.GetNeighbour(i).jnode] = new doorData(new Vector2(Pos.x, Pos.y - 2.0f*SQUARE_SIDE), new Vector2(0.0f, -1.0f));
                    break;
                    case 2:

                        ra.node.neighbours[v.GetNeighbour(i).jnode] = new doorData(new Vector2(Pos.x + 2.0f*SQUARE_SIDE, Pos.y ), new Vector2(1.0f, 0.0f));
                    break;
                    case 3:

                        ra.node.neighbours[v.GetNeighbour(i).jnode] = new doorData(new Vector2(Pos.x, Pos.y + 2.0f*SQUARE_SIDE), new Vector2(0.0f, 1.0f));
                    break;

                    default:
                    break;
                }
            }
        }



        foreach (Transform child in roomobject.transform)
        {
            if(child.tag == "Door")
            {

            }
        }

        

        RoomObjects.Add(roomobject);
    }

    public void PutCorBtwNeighBours(Vertex v, Vertex n, int d)
    {

        float VcoordY = -1*(v.Y - g.Start.Y)*10*SQUARE_SIDE;
        float VcoordX = (v.X - g.Start.X)*10*SQUARE_SIDE;

        float NcoordY = -1*(n.Y - g.Start.Y)*10*SQUARE_SIDE;
        float NcoordX = (n.X - g.Start.X)*10*SQUARE_SIDE;

        int distanceinCoordinates = 0;

        float distance;

        if(d == 0 || d == 2)
        {
            distance = System.Math.Abs(VcoordX - NcoordX);
        }
        else
        {
            distance = System.Math.Abs(VcoordY - NcoordY);
        }

        
        distanceinCoordinates = (int)System.Math.Round(distance/SQUARE_SIDE) - 4;


        switch (d)
        {
            case 0:
            VcoordX -= 2*SQUARE_SIDE;
            break;
            case 1:
            VcoordY -= 2*SQUARE_SIDE;
            break;
            case 2:
            VcoordX += 2*SQUARE_SIDE;
            break;
            case 3:
            VcoordY += 2*SQUARE_SIDE;
            break;

            
            default:
            break;
        }

        if(d == 0 || d == 2)
        {

            //distanceinCoordinates = (5*System.Math.Abs( (v.X - n.X)*2 - 1 ) + 1);
            for(int i = 1; i < distanceinCoordinates; i++)
            {
                if(d == 0)
                {
                    PutCor(new Vector3(-i*SQUARE_SIDE + VcoordX, VcoordY, 0.0f), true);
                }
                else
                {
                    
                    PutCor(new Vector3(i*SQUARE_SIDE + VcoordX, VcoordY, 0.0f), true);
                }
            }
        }

        else if(d == 1 || d == 3)
        {
            
            //distanceinCoordinates = (5*System.Math.Abs( (v.Y - n.Y)*2 - 1 ) + 1);

            
            for(int i = 1; i < distanceinCoordinates; i++)
            {
                if(d == 3)
                {
                    PutCor(new Vector3(VcoordX, i*SQUARE_SIDE + VcoordY, 0.0f), false);
                }
                else
                {
                    
                    PutCor(new Vector3(VcoordX, -i*SQUARE_SIDE + VcoordY, 0.0f), false);
                }
            }
        }

        

        else
        {
            Debug.Log("Cannot put corridors between non neighbours yet");
        }

    }

    public void PutCor(Vector3 Pos, bool hor)
    {

        if(hor)
        {
            var cor = Instantiate(HorCor, Pos, new Quaternion(0.0f, 0.0f, 0.0f, 0.0f));
            CorridorObjects.Add(cor);
        }
        else
        {
            var cor = Instantiate(VerCor, Pos, new Quaternion(0.0f, 0.0f, 0.0f, 0.0f));
            CorridorObjects.Add(cor);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
