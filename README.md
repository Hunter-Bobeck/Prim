# Prim

Created in Unity using SteamVR, Prim is a Vive virtuality application that allows the player to create an undirected graph and visualize [Prim's Algorithm](https://en.wikipedia.org/wiki/Prim%27s_algorithm).

## Features

* *Graph* - The player can create a graph out of two kinds of elements: nodes, and automatic distance weighted undirected edges.
* *[Prim's Algorithm](https://en.wikipedia.org/wiki/Prim%27s_algorithm)* - The player can visualize [Prim's Algorithm](https://en.wikipedia.org/wiki/Prim%27s_algorithm) computing the minimum spanning tree (maximum nodes connected, minimum total weight) for the graph, from a chosen starting node.

### Visuals

* *Environment* - rocky wilderness (to provide a nice natural atmosphere)

* *Nodes* - represented by blue spheres
* *Edges* - represented by green lines by default; yellow when highlighted by Prim's algorithm
* *Weights* - number text label above each corresponding edge (distance is in meters)

### Controls

The following controls are identical for each Vive controller.

Graph adjustments:
* *Touchpad Press* - create node
* *Touchpad Hold* - create edge from initially contacted node to finally contacted node 
* *Trigger Press and Hold* - grab and reposition contacted node
* *Grip Press - Single Controller* - delete contacted node
* *Grip Press - Both Controllers* - delete all elements in the graph

Prim's Algorithm command:
* *Menu Button Press - Either Controller* - toggle the Prim minimum spanning tree visualization; when starting, the starting node will be chosen to be the node nearest to the controller doing the pressing
