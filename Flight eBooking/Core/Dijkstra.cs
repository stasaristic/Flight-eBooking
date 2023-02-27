namespace Flight_eBooking.Core
{
    public class Dijkstra 
    {
        public class DijkstraGraph
        {
            public string[] nodes;
            public uint[] edges;
        }

        public class DijkstraAlgoData
        {
            public DijkstraGraph graph;
            public uint[] open_list;
            public uint open_list_size;

            public uint[] node_cost;
            public uint[] prev_node;

            public uint starting_node;
            public uint ending_node;
        }

        public const uint INVALID_NODE = 66666666;

        /*
            nodes: List of Node names. -> Cities {"Beograd", "Kragujevac"}
            edges: An array where every 3 elements constructs an edge. {0, 1, 100$}
                First element is starting node index, second is ending node index, third is movement cost (non-negative). 
                -> Departure, Arriva, Flight Price
         */
        public static DijkstraGraph SetupGraph(string[] nodes, uint[] edges) 
        {
            var dg = new DijkstraGraph();
            dg.nodes = nodes;
            dg.edges = edges;
            return dg;
        }

        public static DijkstraAlgoData StartDijkstra(DijkstraGraph graph, uint starting_node, uint ending_node)
        {
            var dad = new DijkstraAlgoData();
            dad.graph = graph;
            dad.starting_node = starting_node;
            dad.ending_node = ending_node;

            dad.node_cost = new uint[graph.nodes.Length];
            dad.prev_node = new uint[graph.nodes.Length];

            // for debugging
            for(int i = 0; i< graph.nodes.Length; i++)
            {
                dad.prev_node[i] = 1000000000;
                dad.node_cost[i] = INVALID_NODE;
            }
            dad.node_cost[starting_node] = 0;

            dad.open_list = new uint[graph.nodes.Length];
            dad.open_list[0] = starting_node;
            dad.open_list_size = 1;

            return dad;
        }

        public static bool Process(DijkstraAlgoData dad) 
        {
            var node = dad.open_list[dad.open_list_size - 1];
            dad.open_list_size = 0;

            for (int i = 0; i < dad.graph.edges.Length; i += 3) 
            {
                var edge_start = dad.graph.edges[i];
                var edge_end = dad.graph.edges[i + 1];
                var cost = dad.graph.edges[i + 2];

                if (edge_start == node) 
                {
                    if (dad.node_cost[edge_end-1] > (dad.node_cost[node] + cost))
                    {
                        dad.node_cost[edge_end-1] = (dad.node_cost[node] + cost);
                        dad.prev_node[edge_end-1] = node;

                        dad.open_list[dad.open_list_size] = edge_end;
                        dad.open_list_size += 1;
                    }
                }
            }

            if (dad.open_list_size == 0)
            {
                return true;
            }
            else 
            {
                return false;
            }
            
        }

        public static uint[] GetPath(DijkstraAlgoData dad) 
        {
            var count_walker = dad.prev_node[dad.ending_node-1];
            int count = 0;
            while (count_walker != INVALID_NODE) 
            {
                count += 1;
                count_walker = dad.prev_node[count_walker];
            }

            if (count == 0)
            {
                return null;
            }
            else
            {
                var ret = new uint[count];
                int i = 1;
                var path_walker = dad.ending_node;

                while (path_walker != dad.starting_node)
                {
                    ret[count - i] = path_walker;
                    i += 1;
                    path_walker = dad.prev_node[path_walker];
                }
                return ret;
            }
        }
    }
}
