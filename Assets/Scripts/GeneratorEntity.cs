using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorEntity : MonoBehaviour
{

    float SQUARE_SIDE = 0.32f;
    int smallroomsize = 5;
    int corridors = 5;
    int corridorsize = 1;
    int biggestroomhalfsize = 3;
    int smallroomenemy = 3;
    int normalroomenemy = 5;

    public bool GenerateAtStart = true;

    public GameObject[] SmallRooms;
    public GameObject[] NormalRooms;
    public GameObject HorCor;
    public GameObject VerCor;
    public GameObject knight;
    public GameObject StartTile;
    public GameObject FinishTile;
    public GameObject GoblinPrefab;

    public List<GameObject> RoomObjects;
    public List<GameObject> CorridorObjects;
    public Sprite[] sprites;

    MapGenGraph g;


    // Start is called before the first frame update
    void Start()
    {
        if(GenerateAtStart)
        {
            System.Random rnd = new System.Random();
            Random.seed = rnd.Next(0, 20000);
            GenerateMap(4, 30);
        }


    }

    public void GenerateMap(int shortest, int Nodesnum)
    {
        g = new MapGenGraph();
        
        g.InitGraph(shortest, Nodesnum);

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
            FighterController.fighters.Add(f);
            i++;
        }
        g.Start.jnode.fighterCount = 5;
        
        FighterController.goOut();

        g.Print();
    }


    public void CreateMap(MapGenGraph g)
    {

        List<Vertex> visited = new List<Vertex>();
        List<Vertex> waiting = new List<Vertex>();

        waiting.Add(g.Start);

        int iter = 0;

        while(waiting.Count != 0)
        {
            Debug.Log(iter);
            iter++;
            Vertex v = waiting[0];
            visited.Add(v);
            waiting.RemoveAt(0);

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

            if(v == g.Start)
            {
                PutNormalRoom(new Vector3( (v.X - g.Start.X)*10*SQUARE_SIDE, -1*(v.Y - g.Start.Y)*10*SQUARE_SIDE), v, false);
                var roomobject = Instantiate(StartTile, new Vector3( (v.X - g.Start.X)*10*SQUARE_SIDE, -1*(v.Y - g.Start.Y)*10*SQUARE_SIDE), new Quaternion(0.0f, 0.0f, 0.0f, 0.0f));
            }
            else if(v == g.Finish)
            {

                PutNormalRoom(new Vector3( (v.X - g.Start.X)*10*SQUARE_SIDE, -1*(v.Y - g.Start.Y)*10*SQUARE_SIDE), v, false);
                var roomobject = Instantiate(FinishTile, new Vector3( (v.X - g.Start.X)*10*SQUARE_SIDE, -1*(v.Y - g.Start.Y)*10*SQUARE_SIDE), new Quaternion(0.0f, 0.0f, 0.0f, 0.0f));
            }
           else
            {

                int sorn = Random.Range(0, 100);


                if(sorn > 20)
                {
                    //Azért van csere, mert koordináta váltás
                    PutSmallRoom(new Vector3( (v.X - g.Start.X)*10*SQUARE_SIDE, -1*(v.Y - g.Start.Y)*10*SQUARE_SIDE), v);

                }
                else
                {
                    PutNormalRoom(new Vector3( (v.X - g.Start.X)*10*SQUARE_SIDE, -1*(v.Y - g.Start.Y)*10*SQUARE_SIDE), v);
                }

            }

            
        }
    }

    public void PutSmallRoom(Vector3 Pos, Vertex v, bool enemy = true)
    {
        
        int makeupcorridors = biggestroomhalfsize - 2;

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



        int index = System.Convert.ToInt32(s,2);

        var roomobject = Instantiate(SmallRooms[index - 1], Pos, new Quaternion(0.0f, 0.0f, 0.0f, 0.0f));

        RoomAction ra = null;

        Transform roomchild = null;

        foreach (Transform child in roomobject.transform)
        {
            foreach (Transform ch in child)
            {
                
                if(ch.tag == "Room")
                {
                    roomchild = ch; 
                    
                }

            }

            if(child.tag == "Room")
            {
                roomchild = child; 
                
            }
        }

        if(roomchild == null)
        {
            throw new System.ArgumentException("Room has no room tag child");
        }


        
        ra = roomchild.GetComponent<RoomAction>(); 

        
        if(ra == null)
        {
            throw new System.ArgumentException("Room has no room action component");
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

        

        RoomObjects.Add(roomobject);

        /*
        *
        *  Hiányzó folyosók lepakolása
        *
        */

        for(int i = 0; i < 4; i++)
        {
            if(v.CheckNeighbour(i))
            {
                 switch(i)
                {
                    case 0:

                        for(int j = 0; j < makeupcorridors; j++)
                        {
                            CorridorObjects.Add(Instantiate(HorCor, new Vector3(Pos.x - (2.0f + j + 1)*SQUARE_SIDE, Pos.y, 0.0f), new Quaternion(0.0f, 0.0f, 0.0f, 0.0f)));
                        }

                    break;
                    case 1:

                        
                        for(int j = 0; j < makeupcorridors; j++)
                        {
                            CorridorObjects.Add(Instantiate(VerCor, new Vector3(Pos.x, Pos.y - (2.0f + j + 1)*SQUARE_SIDE, 0.0f), new Quaternion(0.0f, 0.0f, 0.0f, 0.0f)));
                        }
                    break;
                    case 2:

                        for(int j = 0; j < makeupcorridors; j++)
                        {
                            CorridorObjects.Add(Instantiate(HorCor, new Vector3(Pos.x + (2.0f + j + 1)*SQUARE_SIDE, Pos.y, 0.0f), new Quaternion(0.0f, 0.0f, 0.0f, 0.0f)));
                        }
                    break;
                    case 3:

                        for(int j = 0; j < makeupcorridors; j++)
                        {
                            CorridorObjects.Add(Instantiate(VerCor, new Vector3(Pos.x, Pos.y + (2.0f + j + 1)*SQUARE_SIDE, 0.0f), new Quaternion(0.0f, 0.0f, 0.0f, 0.0f)));
                        }
                    break;

                    default:
                    break;
                }

            }
        }

        /*
        *
        *   Enemy
        *
        */

        if(enemy)
        {
            int enemynum = Random.Range(0, smallroomenemy);

            List<Vector3> posspos = new List<Vector3>();

            posspos.Add(new Vector3(0.0f, 0.0f, 0.0f));
            posspos.Add(new Vector3(1.0f, 0.0f, 0.0f));
            posspos.Add(new Vector3(-1.0f, 0.0f, 0.0f));
            posspos.Add(new Vector3(0.0f, 1.0f, 0.0f));
            posspos.Add(new Vector3(0.0f, -1.0f, 0.0f));
            posspos.Add(new Vector3(1.0f, 1.0f, 0.0f));
            posspos.Add(new Vector3(-1.0f, -1.0f, 0.0f));

            for(int i = 0; i < enemynum; i++)
            {
                int eind = Random.Range(0, posspos.Count -1);

                Instantiate(GoblinPrefab, Pos +posspos[eind]*SQUARE_SIDE + new Vector3(0.0f, 0.16f, 0.0f), new Quaternion(0.0f, 0.0f, 0.0f, 0.0f));

                posspos.RemoveAt(eind);

            }
        }

    }



    public void PutNormalRoom(Vector3 Pos, Vertex v, bool enemy = true)
    {
        int makeupcorridors = biggestroomhalfsize - 3;

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

        int index = System.Convert.ToInt32(s,2);

        var roomobject = Instantiate(NormalRooms[index - 1], Pos, new Quaternion(0.0f, 0.0f, 0.0f, 0.0f));

        RoomAction ra = null;

        Transform roomchild = null;

        foreach (Transform child in roomobject.transform)
        {
            foreach (Transform ch in child)
            {
                
                if(ch.tag == "Room")
                {
                    roomchild = ch; 
                    
                }

            }

            if(child.tag == "Room")
            {
                roomchild = child; 
                
            }
        }

        if(roomchild == null)
        {
            throw new System.ArgumentException("Room has no room tag child");
        }


        
        ra = roomchild.GetComponent<RoomAction>(); 

        
        if(ra == null)
        {
            throw new System.ArgumentException("Room has no room action component");
        }

        ra.node = v.jnode;

        for(int i = 0; i < 4; i++)
        {
            if(v.CheckNeighbour(i))
            {
                switch(i)
                {
                    case 0:
                        ///Ezek az ajtók, tudom a szobák rácsméretét, ajtókat annyival arrébb toljuk
                        ra.node.neighbours[ v.GetNeighbour(i).jnode ] = new doorData( new Vector2(Pos.x - 3.0f*SQUARE_SIDE, Pos.y), new Vector2(-1.0f, 0.0f));

                    break;
                    case 1:

                        ra.node.neighbours[v.GetNeighbour(i).jnode] = new doorData(new Vector2(Pos.x, Pos.y - 3.0f*SQUARE_SIDE), new Vector2(0.0f, -1.0f));
                    break;
                    case 2:

                        ra.node.neighbours[v.GetNeighbour(i).jnode] = new doorData(new Vector2(Pos.x + 3.0f*SQUARE_SIDE, Pos.y ), new Vector2(1.0f, 0.0f));
                    break;
                    case 3:

                        ra.node.neighbours[v.GetNeighbour(i).jnode] = new doorData(new Vector2(Pos.x, Pos.y + 3.0f*SQUARE_SIDE), new Vector2(0.0f, 1.0f));
                    break;

                    default:
                    break;
                }
            }
        }

        RoomObjects.Add(roomobject);

           /*
        *
        *  Hiányzó folyosók lepakolása
        *
        */

        for(int i = 0; i < 4; i++)
        {
            if(v.CheckNeighbour(i))
            {
                 switch(i)
                {
                    case 0:

                        for(int j = 0; j < makeupcorridors; j++)
                        {
                            CorridorObjects.Add(Instantiate(HorCor, new Vector3(Pos.x - (2.0f + j + 1)*SQUARE_SIDE, Pos.y, 0.0f), new Quaternion(0.0f, 0.0f, 0.0f, 0.0f)));
                        }

                    break;
                    case 1:

                        
                        for(int j = 0; j < makeupcorridors; j++)
                        {
                            CorridorObjects.Add(Instantiate(VerCor, new Vector3(Pos.x, Pos.y - (2.0f + j + 1)*SQUARE_SIDE, 0.0f), new Quaternion(0.0f, 0.0f, 0.0f, 0.0f)));
                        }
                    break;
                    case 2:

                        for(int j = 0; j < makeupcorridors; j++)
                        {
                            CorridorObjects.Add(Instantiate(HorCor, new Vector3(Pos.x + (2.0f + j + 1)*SQUARE_SIDE, Pos.y, 0.0f), new Quaternion(0.0f, 0.0f, 0.0f, 0.0f)));
                        }
                    break;
                    case 3:

                        for(int j = 0; j < makeupcorridors; j++)
                        {
                            CorridorObjects.Add(Instantiate(VerCor, new Vector3(Pos.x, Pos.y + (2.0f + j + 1)*SQUARE_SIDE, 0.0f), new Quaternion(0.0f, 0.0f, 0.0f, 0.0f)));
                        }
                    break;

                    default:
                    break;
                }

            }
        }

        /*
        *
        * Enemys
        *
        */

        if(enemy)
        {
            int enemynum = Random.Range(0, normalroomenemy);

            List<Vector3> posspos = new List<Vector3>();

            posspos.Add(new Vector3(0.0f, 0.0f, 0.0f));
            posspos.Add(new Vector3(1.0f, 0.0f, 0.0f));
            posspos.Add(new Vector3(2.0f, 0.0f, 0.0f));
            posspos.Add(new Vector3(-1.0f, 0.0f, 0.0f));
            posspos.Add(new Vector3(-2.0f, 0.0f, 0.0f));
            posspos.Add(new Vector3(0.0f, 1.0f, 0.0f));
            posspos.Add(new Vector3(0.0f, 2.0f , 0.0f));
            posspos.Add(new Vector3(0.0f, -1.0f, 0.0f));
            posspos.Add(new Vector3(0.0f, -2.0f, 0.0f));
            posspos.Add(new Vector3(1.0f, 1.0f, 0.0f));
            posspos.Add(new Vector3(1.0f, 2.0f, 0.0f));
            posspos.Add(new Vector3(-1.0f, -1.0f, 0.0f));
            posspos.Add(new Vector3(-1.0f, -2.0f, 0.0f));
            posspos.Add(new Vector3(2.0f, 1.0f, 0.0f));
            posspos.Add(new Vector3(-2.0f, -1.0f, 0.0f));
            posspos.Add(new Vector3(-2.0f, 1.0f, 0.0f));
            posspos.Add(new Vector3(2.0f, -1.0f, 0.0f));

            for(int i = 0; i < enemynum; i++)
            {
                int eind = Random.Range(0, posspos.Count-1);

                Instantiate(GoblinPrefab, Pos + posspos[eind]*SQUARE_SIDE + new Vector3(0.0f, 0.16f, 0.0f), new Quaternion(0.0f, 0.0f, 0.0f, 0.0f));

                posspos.RemoveAt(eind);

            }
        }


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

        
        distanceinCoordinates = (int)System.Math.Round(distance/SQUARE_SIDE) - 6;


        switch (d)
        {
            case 0:
            VcoordX -= biggestroomhalfsize*SQUARE_SIDE;
            break;
            case 1:
            VcoordY -= biggestroomhalfsize*SQUARE_SIDE;
            break;
            case 2:
            VcoordX += biggestroomhalfsize*SQUARE_SIDE;
            break;
            case 3:
            VcoordY += biggestroomhalfsize*SQUARE_SIDE;
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
