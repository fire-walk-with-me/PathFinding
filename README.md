Gustaf Larsson
Malmö Universitet

Testing out pathfinding in a NxN grid with a start and a goal, aswell as nodes that are non-traversable.
The algorithms used are:

**Depth First Search**
Has a time complexity of O(|V|+|E|), since it has to visit every vertex and every edge in a worst case.
Usefull when we know the goal is deep beacuse it reaches depth faster than e.g BFS.
In a cyclical graph it will find a path if there is one, but it might not be the shortest one.
In a directed acyclical graph, like a tree structure, it will find the shortest path

**Breadth First Search**
Has a time complexity of O(|V|+|E|), since it has to visit every vertex and every edge in a worst case.
In both a cyclical graph and a directed acyclical graph it will find the shortest path if there is one.

**Dijkstras**
Has a time complexity of O(V^2).
Dijkstras algorithm will find the shortest path in a weighted graph, however it can not handle negative weights.

**A-Star**
Has a time complexity of O(b^d), where b is branching factor and d is the number of nodes in the resulting path.
A* will find the shortest path in a weighted graph, and because it uses heuristics it useually perfomces better than its worst case scenario.
