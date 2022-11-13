namespace LSemiRoguelike
{
    using Components;
    using UnityEngine;
    using System;
    using System.Collections.Generic;
    using Random = UnityEngine.Random;

    public class MapMaker
    {
        public Guid guid { get; private set; }
        private List<Node> nodes;

        public int maxLayer { get; set; } = 10;
        public int maxWidth { get; set; } = 5;

        public MapMaker()
        {
            guid = Guid.NewGuid();
            making();
        }

        public void making()
        {
            Random.InitState(guid.GetHashCode());

            nodes = new List<Node>();
            int[] numLayer = new int[maxLayer];
            numLayer[0] = 1;
            numLayer[maxLayer - 1] = 1;

            int i = 1;
            while (numLayer[i - 1] < maxWidth - 2 && i < maxLayer / 2)
            {
                int fL = numLayer[i - 1];
                int nL = Random.Range(fL + 1, (fL + 3) < (maxWidth + 1) ? (fL + 3) : (maxWidth + 1));
                numLayer[i] = nL;
                i++;
            }

            int j = maxLayer - 2;
            while (numLayer[j + 1] < maxWidth - 2 && j > maxLayer / 2)
            {
                int fL = numLayer[j + 1];
                int nL = Random.Range(fL + 1, (fL + 3) < (maxWidth + 1) ? (fL + 3) : (maxWidth + 1));
                numLayer[j] = nL;
                j--;
            }
            j++;

            while (i < j)
            {
                int nL = Random.Range(maxWidth - 2, maxWidth + 1);
                numLayer[i] = nL;
                i++;
            }

            List<List<Node>> layerNodes = new List<List<Node>>();
            for (i = 0; i < maxLayer; i++)
            {
                layerNodes.Add(new List<Node>());
                for (j = 0; j < numLayer[i]; j++)
                {
                    Node node = new Node();
                    layerNodes[i].Add(node);
                    nodes.Add(node);
                }
            }

            for (i = 0; i < maxLayer - 1; i++)
            {
                int fi = 0, ni = 0;
                do
                {
                    Node.Conect(layerNodes[i][fi], layerNodes[i + 1][ni]);
                    NextLine(ref fi, layerNodes[i].Count, ref ni, layerNodes[i + 1].Count);

                } while (!Node.CheckAllConect(layerNodes[i], layerNodes[i + 1]));
            }

            for (i = 0; i < maxLayer; i++)
            {
                for (j = 0; j < numLayer[i]; j++)
                {
                    layerNodes[i][j].Position = new Vector3(j - ((float)layerNodes[i].Count / 2), i);
                }
            }

        }

        public void making(Guid guid)
        {
            this.guid = guid;
            making();
        }

        public void making(string str)
        {
            this.guid = new Guid(str);
            making();
        }

        public void randomPosition()
        {
            foreach (Node node in nodes)
            {
                Vector3 vec = Random.insideUnitCircle;
                node.Position += vec * 0.2f;
            }
        }

        private void NextLine(ref int fi, int maxfi, ref int ni, int maxni)
        {
            int numf = maxfi - fi - 1;
            int numn = maxni - ni - 1;
            int numa = numf + numn;
            if (numa == 0) { return; }
            float pf, pn;
            pf = (float)numf / numa;
            pn = (float)numn / numa;
            bool check = false;
            do
            {
                if (pf > Random.value) { fi++; check = true; }
                if (pn > Random.value) { ni++; check = true; }
            } while (!check);
        }

        public string TextLog()
        {
            string text = "";
            return text;
        }

        public List<Vector3> GetVector3s()
        {
            List<Vector3> vector3s = new List<Vector3>();

            foreach (Node node in nodes)
            {
                vector3s.Add(node.Position);
            }

            return vector3s;
        }

        public List<(Vector3, Vector3)> GetLines()
        {
            List<(Vector3, Vector3)> lines = new List<(Vector3, Vector3)>();

            foreach (Node node in nodes)
            {
                foreach (Node next in node.GetNexts())
                {
                    lines.Add((node.Position, next.Position));
                }
            }

            return lines;
        }

    }


    namespace Components
    {
        public class Node
        {
            private int eventCode;
            private Vector3 position;
            private List<Node> nextNodes;
            private List<Node> prevNodes;

            public Node()
            {
                eventCode = 0;
                nextNodes = new List<Node>();
                prevNodes = new List<Node>();
            }

            public void AddNextNode(Node next)
            {
                nextNodes.Add(next);
            }

            public int NextCount
            {
                get { return nextNodes.Count; }
            }

            public int PrevCount
            {
                get { return prevNodes.Count; }
            }

            public Vector3 Position
            {
                set { this.position = value; }
                get { return position; }
            }

            public int EventCode
            {
                get { return eventCode; }
                set { this.eventCode = value; }
            }

            public List<Node> GetNexts()
            {
                List<Node> nodes = new List<Node>();

                foreach (Node node in nextNodes)
                {
                    nodes.Add(node);
                }

                return nodes;
            }

            static public void Conect(Node aNode, Node bNode)
            {
                if (aNode.nextNodes.Contains(bNode) || bNode.prevNodes.Contains(aNode)) { return; }
                aNode.nextNodes.Add(bNode);
                bNode.prevNodes.Add(aNode);
            }

            static public bool CheckAllConect(List<Node> aNodes, List<Node> bNodes)
            {
                foreach (Node node in aNodes)
                {
                    if (node.NextCount == 0) return false;
                }
                foreach (Node node in bNodes)
                {
                    if (node.PrevCount == 0) return false;
                }
                return true;
            }
        }
    }

    public struct MapStruct
    {

    }
}