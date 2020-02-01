// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;


// int MAX_ROOM_SIZE = 10;
// int ONE_ROOM_PIXEL = 32;



// //Ez egy négyzetpapír, legenerálod előre az utat egyenesen, majd elágaztatod, ha egy szintet visszalépsz, és van már rajta node, akkor visszacsatolod. Térkép generáláskor ezt tudod randomizálni, így a síkbarajzolhatóság megmarad, viszont mégis véletlenszerűnek fog tűnni.
// class Graph
// {

// }

// class Node
// {

// }



// public delegate RoomDescript RoomDescriptorGenerator();

// public struct RoomDescript
// {
//     int Width;
//     int Height;

//     ArrayList<String> tiles = new ArrayList();

//     //Ezt lehet generálni a bitkód alapján, holnap meg is csinálhatod.
//     public static RoomDescript generateCross0001()
//     {
//         RoomDescript rd;
//         rd.Width = 5;
//         rd.Height = 5;

//         rd.Tiles.Add("11111");
//         rd.Tiles.Add("10001");
//         rd.Tiles.Add("00001");
//         rd.Tiles.Add("10001");
//         rd.Tiles.Add("11111");    
//     } 
    
//     public static RoomDescript generateCross0010()
//     {
//         RoomDescript rd;
//         rd.Width = 5;
//         rd.Height = 5;

//         rd.Tiles.Add("11111");
//         rd.Tiles.Add("10001");
//         rd.Tiles.Add("10001");
//         rd.Tiles.Add("10001");
//         rd.Tiles.Add("11011");    
//     } 
    
//     public static RoomDescript generateCross0011()
//     {
//         RoomDescript rd;
//         rd.Width = 5;
//         rd.Height = 5;

//         rd.Tiles.Add("11111");
//         rd.Tiles.Add("10001");
//         rd.Tiles.Add("00001");
//         rd.Tiles.Add("10001");
//         rd.Tiles.Add("11011");    
//     } 
    
//     public static RoomDescript generateCross0100()
//     {
//         RoomDescript rd;
//         rd.Width = 5;
//         rd.Height = 5;

//         rd.Tiles.Add("11111");
//         rd.Tiles.Add("10001");
//         rd.Tiles.Add("10000");
//         rd.Tiles.Add("10001");
//         rd.Tiles.Add("11111");    
//     } 
    
//     public static RoomDescript generateCross0101()
//     {
//         RoomDescript rd;
//         rd.Width = 5;
//         rd.Height = 5;

//         rd.Tiles.Add("11111");
//         rd.Tiles.Add("10001");
//         rd.Tiles.Add("00000");
//         rd.Tiles.Add("10001");
//         rd.Tiles.Add("11111");    
//     } 
    
//     public static RoomDescript generateCross0110()
//     {
//         RoomDescript rd;
//         rd.Width = 5;
//         rd.Height = 5;

//         rd.Tiles.Add("11111");
//         rd.Tiles.Add("10001");
//         rd.Tiles.Add("10000");
//         rd.Tiles.Add("10001");
//         rd.Tiles.Add("11011");    
//     } 
    
//     public static RoomDescript generateCross0111()
//     {
//         RoomDescript rd;
//         rd.Width = 5;
//         rd.Height = 5;

//         rd.Tiles.Add("11111");
//         rd.Tiles.Add("10001");
//         rd.Tiles.Add("00000");
//         rd.Tiles.Add("10001");
//         rd.Tiles.Add("11011");    
//     } 
    
//     public static RoomDescript generateCross1000()
//     {
//         RoomDescript rd;
//         rd.Width = 5;
//         rd.Height = 5;

//         rd.Tiles.Add("11011");
//         rd.Tiles.Add("10001");
//         rd.Tiles.Add("10001");
//         rd.Tiles.Add("10001");
//         rd.Tiles.Add("11111");    
//     } 
    
//     public static RoomDescript generateCross1001()
//     {
//         RoomDescript rd;
//         rd.Width = 5;
//         rd.Height = 5;

//         rd.Tiles.Add("11011");
//         rd.Tiles.Add("10001");
//         rd.Tiles.Add("00001");
//         rd.Tiles.Add("10001");
//         rd.Tiles.Add("11111");    
//     } 
    
//     public static RoomDescript generateCross1010()
//     {
//         RoomDescript rd;
//         rd.Width = 5;
//         rd.Height = 5;

//         rd.Tiles.Add("11011");
//         rd.Tiles.Add("10001");
//         rd.Tiles.Add("10001");
//         rd.Tiles.Add("10001");
//         rd.Tiles.Add("11011");    
//     } 
    
//     public static RoomDescript generateCross1011()
//     {
//         RoomDescript rd;
//         rd.Width = 5;
//         rd.Height = 5;

//         rd.Tiles.Add("11011");
//         rd.Tiles.Add("10001");
//         rd.Tiles.Add("00001");
//         rd.Tiles.Add("10001");
//         rd.Tiles.Add("11011");    
//     } 
    
//     public static RoomDescript generateCross1100()
//     {
//         RoomDescript rd;
//         rd.Width = 5;
//         rd.Height = 5;

//         rd.Tiles.Add("11011");
//         rd.Tiles.Add("10001");
//         rd.Tiles.Add("10000");
//         rd.Tiles.Add("10001");
//         rd.Tiles.Add("11111");    
//     } 

//        public static RoomDescript generateCross1101()
//     {
//         RoomDescript rd;
//         rd.Width = 5;
//         rd.Height = 5;

//         rd.Tiles.Add("11011");
//         rd.Tiles.Add("10001");
//         rd.Tiles.Add("00000");
//         rd.Tiles.Add("10001");
//         rd.Tiles.Add("11111");    
//     } 
    
//     public static RoomDescript generateCross1110()
//     {
//         RoomDescript rd;
//         rd.Width = 5;
//         rd.Height = 5;

//         rd.Tiles.Add("11011");
//         rd.Tiles.Add("10001");
//         rd.Tiles.Add("10000");
//         rd.Tiles.Add("10001");
//         rd.Tiles.Add("11011");    
//     } 
    
//     public static RoomDescript generateCross1111()
//     {
//         RoomDescript rd;
//         rd.Width = 5;
//         rd.Height = 5;

//         rd.Tiles.Add("11011");
//         rd.Tiles.Add("10001");
//         rd.Tiles.Add("00000");
//         rd.Tiles.Add("10001");
//         rd.Tiles.Add("11011");    
//     }

//     static RoomDescriptorGenerator[] door1 = new RoomDescriptorGenerator[] {

//         new RoomDescriptorGenerator( RoomDescript.generateCross1000 ),
//         new RoomDescriptorGenerator( RoomDescript.generateCross0100 ),
//         new RoomDescriptorGenerator( RoomDescript.generateCross0010 ),
//         new RoomDescriptorGenerator( RoomDescript.generateCross0001 )
//     };

//     static RoomDescriptorGenerator[] door2 = new RoomDescriptorGenerator[] {

//         new RoomDescriptorGenerator( RoomDescript.generateCross1100 ),
//         new RoomDescriptorGenerator( RoomDescript.generateCross0110 ),
//         new RoomDescriptorGenerator( RoomDescript.generateCross0011 ),
//         new RoomDescriptorGenerator( RoomDescript.generateCross1010 ),
//         new RoomDescriptorGenerator( RoomDescript.generateCross0101 ),
//         new RoomDescriptorGenerator( RoomDescript.generateCross1001 )
//     };

//     static RoomDescriptorGenerator[] door3 = new RoomDescriptorGenerator[] {

//         new RoomDescriptorGenerator( RoomDescript.generateCross0111 ),
//         new RoomDescriptorGenerator( RoomDescript.generateCross1011 ),
//         new RoomDescriptorGenerator( RoomDescript.generateCross1101 ),
//         new RoomDescriptorGenerator( RoomDescript.generateCross1110 )
//     };

//     static RoomDescriptorGenerator[] door4 = new RoomDescriptorGenerator[] {

//         new RoomDescriptorGenerator( RoomDescript.generateCross1111 )
//     };

//     public static RoomDescript GetRandomRoom(int DoorNum)
//     {
        
//         Random rnd = new Random();

//         switch(DoorNum)
//         {
//             case 1:
//             return RoomDescriptorGenerator[rnd.Next(0, door1.Length - 1) ]();
            
//             case 2:
//             return RoomDescriptorGenerator[rnd.Next(0, door2.Length - 1) ]();
            
//             case 3:
//             return RoomDescriptorGenerator[rnd.Next(0, door3.Length - 1) ]();

//             case 4:
//             return RoomDescriptorGenerator[rnd.Next(0, door4.Length - 1) ]();

//             default:
//                  throw new System.ArgumentException("Bruh what, too many doors");
//         }

        
//         throw new System.ArgumentException("Bruh wtf you doing here?");

//         return null;
//     }

// }

// public struct Map
// {

//     ArrayList<ArrayList<int>> myAL = new ArrayList();


// }

// public class MapGenerator
// {

//     public Graph GenerateGraph(int shortestPath, int Nodes)
//     {
//         Graph graph = new Graph();

//         //Legrövidebb út x, x+1 összekapcsolt node kell
//         //Ezek után még Nodes - (x+1) node-ot kell bebaszni
//         //És ezek között a nodeok között kell úgy éleket teremteni, hogy:
//         // 1, Egy node max 4 él
//         // 2, Kezdő és végső node 1 él
//         // 3, Új él után a legrövidebb út a kezdő és a végső node között x maradjon.
//         // Kész, Returnolhat.



//     }

//     public int shortestPathLength(Graph graph, Node from, Node to)
//     {
        
//         //Rohadt egyszerű BFS
//     }

//     public Map GenerateMap(int shortestPath, int Rooms)
//     {

//         Graph graph = GenerateGraph(shortestPath, Rooms);

//         //Leteszem a kezdő szobát, tőle oriántációban kb ugyanúgy mint a gráfban leteszem a következőt, és a kettő között generálok utat. Repeat until complete



//     }





// }
