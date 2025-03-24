using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ExperimentBlackboard : Blackboard<ExperimentBlackboard>
{
    public Dictionary<(int, int), string> map = new();
    
    /// <summary>
    /// Get adjacent horizontal and vertical tiles. Ignore diagonals.
    /// </summary>
    /// <param name="map"></param>
    /// <param name="u"></param>
    /// <param name="goal"></param>
    /// <returns></returns>
    public List<(float, (int, int))> GetNeighbors(Dictionary<(int, int), string> map, (int, int) u, (int, int) goal) {
        var neighbors = new List<(float, (int, int))>();
        
        foreach ((int, int) delta in new List<(int, int)>(){(0, 1), (0, -1), (1, 0), (-1, 0)}) {
            var candidate = (u.Item1 + delta.Item1, u.Item2 + delta.Item2);
            if (candidate.Item1 >= 0 && candidate.Item1 < 8 &&
                candidate.Item2 >= 0 && candidate.Item2 < 8 &&
                map[(candidate.Item1, candidate.Item2)] == "ground") {
                neighbors.Add((Mathf.Sqrt(Mathf.Pow(delta.Item1, 2) + Mathf.Pow(delta.Item2, 2)), candidate));
            }
        }
        return neighbors;
    }

    /// <summary>
    /// A* Shortest Path Algorithm. There are so many different ways to implement
    /// and optimize A*. We will just use grid map as hashmap for rapid prototyping. 
    /// Keys will be a 2 dimensional integer tuple.
    /// </summary>
    /// <param name="map"></param>
    /// <param name="start"></param>
    /// <param name="end"></param>
    /// <returns></returns>
    public List<(int, int)> ShortestPath(Dictionary<(int, int), string> map, (int, int) start, (int, int) end) {
        var path = new List<(int, int)>();
        var queue = new PriorityQueue<(int, int), float>();

        // queue = [(0, start)]
        //     heapify(queue)

        //     distances = defaultdict(lambda:float('inf'))
        //     distances[start] = 0

        //     visited = set()
        //     parent = {}

        //     print("planning on the 17th of february, 2025.")
        //     while queue:
        //         (priority, u) = heappop(queue)

        //         if u in visited:
        //             print("GASP it was already expanded!")
        //             continue

        //         visited.add(u)

        //         # run quadtree query during node expansion to prevent redundant queries
        //         found_points = []
        //         x, y = map2world(u[0], u[1])
        //         qtree.query_radius((x+5, y+5), self.robot_radius, found_points)
        //         if len(found_points) > 0:
        //             continue

        //         if u == self.goal:
        //             break

        //         for (costuv, v) in getNeighborsTiered(blackboard.read('cspace'), self.robot_radius, u, self.goal, self.tier):
        //             if v not in visited:
        //                 newcost = distances[u] + costuv
        //                 if newcost < distances[v]:
        //                     distances[v] = newcost
        //                     heappush(queue, (newcost + np.sqrt((self.goal[0]-v[0])**2+(self.goal[1]-v[1])**2),v))
        //                     parent[v] = u

        //     path = []

        //     key = self.goal
        //     while key in parent.keys():
        //         key = parent[key]
        //         path.insert(0, key)

        //     path.append(self.goal)

        return path;
    }
}

class PriorityQueue<T, TPriority> where TPriority : IComparable<TPriority>
{
    private List<(T Item, TPriority Priority)> heap = new();

    private void Swap(int i, int j) {
        var temp = heap[i];
        heap[i] = heap[j];
        heap[j] = temp;
    }

    private void BubbleUp(int index) {
        while (index > 0) {
            int parent = (index - 1) / 2;
            if (heap[index].Priority.CompareTo(heap[parent].Priority) >= 0) break;
            Swap(index, parent);
            index = parent;
        }
    }

    private void BubbleDown(int index) {
        int last = heap.Count - 1;
        while (true) {
            int left = 2 * index + 1, right = 2 * index + 2, smallest = index;
            if (left <= last && heap[left].Priority.CompareTo(heap[smallest].Priority) < 0)
                smallest = left;
            if (right <= last && heap[right].Priority.CompareTo(heap[smallest].Priority) < 0)
                smallest = right;
            if (smallest == index) break;
            Swap(index, smallest);
            index = smallest;
        }
    }

    public void Enqueue(T item, TPriority priority) {
        heap.Add((item, priority));
        BubbleUp(heap.Count - 1);
    }

    public T Dequeue() {
        if (heap.Count == 0) throw new InvalidOperationException("Queue is empty");
        T item = heap[0].Item;
        heap[0] = heap[^1];
        heap.RemoveAt(heap.Count - 1);
        BubbleDown(0);
        return item;
    }

    public int Count => heap.Count;
}