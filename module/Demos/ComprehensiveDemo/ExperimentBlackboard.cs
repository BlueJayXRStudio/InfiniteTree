using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
public class ExperimentBlackboard : Blackboard<ExperimentBlackboard>
{
    public Dictionary<(int, int), string> map = new();

    public (int, int) HospitalPos = (0, 2);
    public (int, int) GroceryStorePos = (3, 7);
    public (int, int) ATMPos = (6, 3);

    private Queue<GameObject> EmergencyCalls = new();


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

        queue.Enqueue(start, 0);

        var distances = new DefaultDictionary<(int, int), float>(() => float.PositiveInfinity);
        distances[start] = 0;

        var visited = new HashSet<(int, int)>();

        var parent = new Dictionary<(int, int), (int, int)>();

        
        while (queue.Count > 0) {
            (var u, var priority) = queue.Dequeue();

            if (visited.Contains(u)) {
                continue;
            }
            visited.Add(u);

            if (u == end) break;

            foreach ((var costuv, var v) in GetNeighbors(ExperimentBlackboard.Instance.map, u, end))
            {
                if (!visited.Contains(v)) {
                    var newcost = distances[u] + costuv;
                    if (newcost < distances[v]) {
                        distances[v] = newcost;

                        queue.Enqueue(v, newcost + Mathf.Sqrt(Mathf.Pow(end.Item1 - v.Item1, 2) + Mathf.Pow(end.Item2 - v.Item2, 2)));
                        parent[v] = u;
                    }
                }

                    
            }
        }

        var key = end;
        while (new List<(int, int)>(parent.Keys).Contains(key)) {
            key = parent[key];
            path.Insert(0, key);
        }
        path.Add(end);

        return path;
    }

    public GameObject GetCall {
        get {
            if (EmergencyCalls.Count > 0)
                return EmergencyCalls.Dequeue();
            else
                return null;
        }
    }

    public void SetCall(GameObject go) => EmergencyCalls.Enqueue(go);
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

    public (T, TPriority) Dequeue() {
        if (heap.Count == 0) throw new InvalidOperationException("Queue is empty");
        var item = heap[0];
        heap[0] = heap[^1];
        heap.RemoveAt(heap.Count - 1);
        BubbleDown(0);
        return item;
    }

    public int Count => heap.Count;
}

public class DefaultDictionary<TKey, TValue> : Dictionary<TKey, TValue>
{
    private readonly Func<TValue> _defaultValueFactory;

    public DefaultDictionary(Func<TValue> defaultValueFactory)
    {
        _defaultValueFactory = defaultValueFactory;
    }

    public new TValue this[TKey key]
    {
        get
        {
            if (!TryGetValue(key, out var value))
            {
                value = _defaultValueFactory();
                this[key] = value;
            }
            return value;
        }
        set => base[key] = value;
    }
}