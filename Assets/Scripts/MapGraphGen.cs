using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class Vertex
{
    public Vertex neighbour0;
    public Vertex neighbour1;
    public Vertex neighbour2;
    public Vertex neighbour3;

    public Node jnode;

    public bool live;

    public int Y;
    public int X;

    public Vertex(int y = -1, int x = -1, bool l = false)
    {
        live = l;
        if(live) jnode = new Node();
        Y = y;
        X = x;
    }

    public Vertex GetNeighbour(int n)
    {
        switch (n)
        {
            case 0:
            return neighbour0;
            case 1:
            return neighbour1;
            case 2:
            return neighbour2;
            case 3:
            return neighbour3;
            
            default:
                throw new System.ArgumentException("Bad Neighbour param");
        }
    }

    public bool CheckNeighbour(int n)
    {
            switch (n)
        {
            case 0:
            return neighbour0 != null;
            case 1:
            return neighbour1 != null;
            case 2:
            return neighbour2 != null; 
            case 3:
            return neighbour3 != null;
            
            default:
                throw new System.ArgumentException("Bad Neighbour param");
        }
    }

    public bool AddNeighbour(Vertex v, int n)
    {
        switch (n)
        {
            case 0:
                
                if(neighbour0 != null || v.neighbour2 != null)
                {
                    throw new System.ArgumentException("Neighbour already filled");
                }

                neighbour0 = v;
                v.neighbour2 = this;

                return true;

            case 1:

                if(neighbour1 != null || v.neighbour3 != null)
                {
                    throw new System.ArgumentException("Neighbour already filled");
                }

                neighbour1 = v;
                v.neighbour3 = this;

                return true;

            case 2:
            
                if(neighbour2 != null || v.neighbour0 != null)
                {
                    throw new System.ArgumentException("Neighbour already filled");
                }

                neighbour2 = v;
                v.neighbour0 = this;

                return true;

            case 3:
            
                if(neighbour3 != null || v.neighbour1 != null)
                {
                    throw new System.ArgumentException("Neighbour already filled");
                }

                neighbour3 = v;
                v.neighbour1 = this;

                return true;

            
            default:
                throw new System.ArgumentException("Bad Neighbour param");
        }
    }

    public int GetDegree()
    {
        int d = 0;

        if(neighbour0 != null) d++;
        if(neighbour1 != null) d++;
        if(neighbour2 != null) d++;
        if(neighbour3 != null) d++;

        return d;
    }

    public void Print()
    {
        Debug.Log("Y: " + Y + " X: " + X);
    }
}

public class MapGenGraph
{

    static int ratio = 2;

    public List<List<Vertex>> vertices = new List<List<Vertex>>();

    public List<Vertex> LiveVertices = new List<Vertex>();

    public Vertex Start;
    public Vertex Finish;

    public void InitGraph(int shortestPath, int Nodes)
    {
        
        System.Random rnd = new System.Random();

        int h = shortestPath + 2;
        int w = shortestPath + 2;
        while(h*w < Nodes*ratio)
        {
            int coin  = rnd.Next(0, 2);

            if(coin == 1)
            {
                h++;
            }
            else
            {
                w++;  
            } 
        }

        for(int i = 0; i < h+1; i++)
        {
            vertices.Add(new List<Vertex>());

            for(int j = 0; j < w+1; j++)
            {
                vertices[i].Add(new Vertex(i, j));
            }
        }

        int StartX = (int)Math.Floor( ((double)w)/2.0 - ((double)shortestPath)/2.0 );
        int StartY = (int)Math.Floor( (double)(h)/2.0);

        for(int i = 0; i < shortestPath + 1; i++)
        {

            vertices[StartY][StartX + i].live = true;
            vertices[StartY][StartX + i].Y = StartY;
            vertices[StartY][StartX + i].X = StartX + i;

            if(i != 0)
            {
                vertices[StartY][StartX + i].AddNeighbour(vertices[StartY][StartX + i - 1], 0);
            }

            if(i == shortestPath)
            {
                Start =  vertices[StartY][StartX];
                Finish = vertices[StartY][StartX + i];
            }
        }


        for(int i = 0; i < Nodes - shortestPath - 1; i++)
        {
            bool generated = false;

            while(!generated)
            {
                for(int j = 0; j < 10; j++)
                {
                    int y = rnd.Next(0, vertices.Count);
                    int x = rnd.Next(0, vertices[y].Count);

                    int tcx = x;
                    int tcy = y;

                    //Ha foglalt nézd meg a szomszédokat
                    if(vertices[y][x].live)
                    {
                        if(y+1 < vertices.Count && vertices[y+1][x].live)
                        {
                            tcy = y+1;
                        }
                        
                        else if(y-1 >= 0 && vertices[y-1][x].live)
                        {
                            tcy = y-1;
                        }

                        
                        else if(x+1 < vertices[y].Count && vertices[y][x+1].live)
                        {
                            tcx = x+1;
                        }

                        
                        else if(x-1 >= 0 && vertices[y][x-1].live)
                        {
                            tcx = x-1;
                        }

                        else
                        {
                            continue;
                        }
                        
                    }
                    
                    //Ha nem foglalt akkor nézd meg hogy van e kapcsolódó szomszéd
                    if(CheckForPossibleNeighbour(tcy, tcx))
                    {
                        vertices[tcy][tcx].live=true;
                        vertices[tcy][tcx].X= tcx;
                        vertices[tcy][tcx].Y= tcy;
                        break;
                    }
                
                }

                generated = true;

            }


        }


        //Build LiveVertices list
        for(int i = 0; i < vertices.Count; i++)
        {
            for(int j = 0; j < vertices[i].Count; j++)
            {
                if(vertices[i][j].live) LiveVertices.Add(vertices[i][j]);
            }
        }



        //Gráfél sorsolás
        //Szabályok: Él csak közvetlen szomszéd közt mehet. Nem mehet kereszt él, ezt csekkolni kell

        for(int i = 0; i < LiveVertices.Count; i++)
        {
            Vertex v = LiveVertices[i];

            //lehetséges szomszédok száma
            int possibleNeigbours = 4;

            
            List<int> nb = new List<int>();
            //Lehetséges szomszéd pozíciók
            nb.Add(0);
            nb.Add(1);
            nb.Add(2);
            nb.Add(3);

            for(int j = 3; j >= 0; j--)
            {
                //Akkor lehet egy irányba szomszédot adni, ha már nincs ott szomszédja, ha lehet arra szomszédja, tehát egy vonalban van még egy csúcs, és ha a szomszéd behúzása nem eredményezne kereszt élt
                if(v.CheckNeighbour(j) || PossibleNeighbourInDir(v, j) == null || CheckIfCrossEdge(v, j))
                {
                    possibleNeigbours--;
                    nb.RemoveAt(j);
                }
            }

            //Patt helyzet, itt nem lehet éle a csúcsnak úgy, hogy a síkba rajzolhatóságot ne sértené
            if(possibleNeigbours == 0 && v.GetDegree() == 0)
            {
                continue;
            }

            int newnbrs = rnd.Next(0, possibleNeigbours + 1);

            //Ha nincs él de lehetne viszont 0 élt sorsoltunk, rakjunk be egyet
            if(v.GetDegree() == 0 && newnbrs == 0) newnbrs++;


            //Kisorsojuk a kellő szomszédot
            for(int j = 0; j < newnbrs; j++)
            {
                //Index a szomszéd pozíciós listában
                int nindex = rnd.Next(0, nb.Count);
                
                //Hozzáadjuk a megfelelő irányban elhelyezkedő szomszédot a megfelelő irányú szomszéd slotban

                v.AddNeighbour( PossibleNeighbourInDir(v, nb[nindex]), nb[nindex]);

                //kivesszük mint lehetséges pozíciót
                nb.RemoveAt(nindex);

            }

        }

        //Jázmin Gráf Felépítése
        for(int i = 0; i < LiveVertices.Count; i++)
        {
            Vertex v = LiveVertices[i];

            for(int j = 0; j < 4; j++)
            {
                if(v.CheckNeighbour(j))
                {
                    Vertex n = v.GetNeighbour(j);

                    v.jnode.neighbours.Add(n.jnode, new doorData(new Vector2(0.0f, 0.0f), new Vector2(0.0f, 0.0f)));
                }
            }
        }


    }

    

    //Gráf generálásnál egyenes éleken keresztül el lehet érni minden szobát
    public bool CheckForPossibleNeighbour(int y, int x)
    {
    
        for(int i = 0; i < y; i++)
        {
            if(vertices[i][x].live)
            {
                return true;
            }
        }
        
        for(int i = 0; i < x; i++)
        {
            if(vertices[y][i].live)
            {
                return true;
            }
        }
        
        for(int i = y + 1; i < vertices.Count; i++)
        {
            if(vertices[i][x].live)
            {
                return true;
            }
        }

        for(int i = x + 1; i < vertices[y].Count; i++)
        {
            if(vertices[y][i].live)
            {
                return true;
            }
        }

        return false;
    }


    public bool CheckIfCrossEdge(Vertex f, int j)
    {
        //Lementjük a csúcsot mely szomszéd lehet
        Vertex n = PossibleNeighbourInDir(f, j);

        int distance = 0;

        //Kiszámoljuk hogy hány négyzet van köztük
        if(j == 0 || j == 2)
        {
            distance = Math.Abs(f.X - n.X) - 1;
        }
        else
        {
            distance = Math.Abs(f.Y - n.Y) - 1;
        }

        //Négyzetenként megnézzük, hogy fut-e él, ha igen akkor van kereszt él tehát tre, ha egyiknél sincs akkor false
        Vertex temp1 = f;
        for(int i = 0; i < distance; i++)
        {
            Vertex temp2 = GetNextInDir(temp1, j);

            if(temp2 == null) 
            {
                Debug.Log("Shouldnt be here");
                break;
            }

            //Két eset, horizontálisan lépkedünk vagy vertikálisan
            //Miért működik? Ha kereszt él van, az a gráf építési szabályok miatt, csak akkor lehet, ha van egy pár az adott rácspont y vagy x koordinátáján, ez a tény el van
            //Tárolva mint két csomópontban amin él lehet. Ezek a csúcsok a legközelebbiek lehetnek csak, mivel csúcsot nem "ugrik át" él. Tehát elég az egyik merőleges irányban
            //Megnézni a legközelebbi csúcspontot, ha nincs akkor nincs is él, és ha a csúcspontnak nincs a beérkezéssel ellenkező irányú szomszédja, szintén nincsen kereszt él
            if(j == 0 || j == 2)
            {
                //Ha horizontálisan
                Vertex possibleCross = PossibleNeighbourInDir(temp2, 1);
                if(possibleCross != null && possibleCross.GetNeighbour(3) != null)
                {
                    return true;
                } 
            }
            else if(j == 1 || j == 3)
            {
                Vertex possibleCross = PossibleNeighbourInDir(temp2, 2);

                if(possibleCross != null && possibleCross.GetNeighbour(0) != null) 
                {
                    return true;
                }

            }
            else
            {

            }

            temp1 = temp2;

        }

        return false;

        
    }

    public Vertex GetNextInDir(Vertex v, int d)
    {
        switch(d)
        {
            case 0:

            if(v.X-1 < 0)
            {
                return null;
            } 
            else
            {
                return vertices[v.Y][v.X-1];
            }

            case 1:
            
            if(v.Y+1 >= vertices.Count)
            {
                return null;
            } 
            else
            {
                return vertices[v.Y+1][v.X];
            }
            

            case 2:
            

            if(v.X+1 >= vertices[v.Y].Count)
            {
                return null;
            } 
            else
            {
                return vertices[v.Y][v.X+1];
            }
            
            case 3:
            
            if(v.Y-1 < 0)
            {
                return null;
            } 
            else
            {
                return vertices[v.Y-1][v.X];
            }

            default:
            
            return null;
        }
    }

    public int PossibleNeighbourNum(Vertex v)
    {
        int r = 0;

        for(int i = 0; i < 4; i++)
        {
            if(PossibleNeighbourInDir(v, i) != null) r++;
        }

        return r;
    }

    public Vertex PossibleNeighbourInDir(Vertex v, int n)
    {
        switch(n)
        {
            case 0:

                for(int i = v.X - 1; i >= 0; i--)
                {
                    if(vertices[v.Y][i].live)
                    {
                        return vertices[v.Y][i];
                    }
                }


            return null;

            
            case 1:
            
                for(int i = v.Y + 1; i < vertices.Count; i++)
                {
                    if(vertices[i][v.X].live)
                    {
                        return vertices[i][v.X];
                    }
                }

            return null;

            
            case 2:

                for(int i = v.X + 1; i < vertices[v.Y].Count; i++)
                {
                    if(vertices[v.Y][i].live)
                    {
                        return vertices[v.Y][i];
                    }
                }

            return null;

            
            case 3:
        
                for(int i = v.Y - 1; i >= 0; i--)
                {
                    if(vertices[i][v.X].live)
                    {
                        return vertices[i][v.X];
                    }
                }

            return null;
            
            default:
            return null;
        }
    }


    public void Print()
    {
        char Space = '.';
        char VEdge = '|';
        char HEdge = '-';
        char Node = '#';
        char CStart = 'S';
        char CFinish = 'F';

        List<List<char>> maprep = new List<List<char>>();



        for(int i = 0; i < vertices.Count; i++)
        {
            maprep.Add(new List<char>());
            for(int j = 0; j < vertices[i].Count; j++)
            {
                if(!vertices[i][j].live)
                {
                    maprep[i].Add(Space);
                }
                else
                {
                    maprep[i].Add(Node);
                }
            }
        }


        //Kiegészíteni helyel az élek rajzolásához.
        for(int i = 1; i < vertices.Count; i++)
        {
            maprep.Insert(i*2-1, new List<char>());

            for(int j = 0; j < maprep[i*2-2].Count; j++)
            {
                maprep[i*2-1].Add(Space);
            }
        }

        for(int i = 0; i < maprep.Count; i++)
        {
            for(int j = 1; j < vertices[  (int)Math.Floor((double)i/2.0)  ].Count; j++)
            {
                maprep[i].Insert(j*2-1, Space);   
            }
        }

        maprep[Start.Y*2][Start.X*2] = CStart;
        maprep[Finish.Y*2][Finish.X*2] = CFinish;



        //Élek Rajzolása

        for(int i = 0; i < vertices.Count; i++)
        {
            for(int j = 0; j < vertices[i].Count; j++)
            {
                Vertex tc = vertices[i][j];
                if(tc.live)
                {   
                    //Balról jobbra haladok, elég a jobb és alsó szomszédokat nézni, mert a másik kettő már meg van nézve
                    if(tc.CheckNeighbour(1))
                    {
                        for(int k = 1; k < tc.GetNeighbour(1).Y*2 - tc.Y*2; k++)
                        {   
                            
                            maprep[tc.Y*2 + k][tc.X*2] = VEdge;

                        }
                    }

                    if(tc.CheckNeighbour(2))
                    {
                        for(int k = 1; k < tc.GetNeighbour(2).X*2 - tc.X*2; k++)
                        {
                            

                            maprep[tc.Y*2][tc.X*2 + k] = HEdge;
                        }
                    }


                }
            }
        }

        
        string s = "";
        for(int i = 0; i < maprep.Count; i++)
        {
            for(int j = 0; j < maprep[i].Count; j++)
            {
                s = s + maprep[i][j];
            }
            s = s + "\n\r";
        }
        
        Debug.Log(s);

    }

}