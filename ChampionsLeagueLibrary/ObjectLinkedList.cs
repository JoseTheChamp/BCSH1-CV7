using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChampionsLeagueLibrary
{
    public class ObjectLinkedList : ICollection, IEnumerable<Player>, IList<Player>
    {
        private Node head;

        internal class Node {
            internal Player player;
            internal Node prev;
            internal Node next;

            public Node(Player player, Node prev, Node next)
            {
                this.player = player;
                this.prev = prev;
                this.next = next;
            }
        }

        public Player this[int index] {
            get {
                if (!(index >= 0 && index < Count))
                {
                    return null;
                }
                return GoToIndex(index).player;
            }
            set {
                if (!(index >= 0 && index < Count))
                {
                    return;
                }
                GoToIndex(index).player = value;
            } 
        }

        public int Count => CountNodes();

        public bool IsReadOnly => false;
        public bool IsSynchronized => false;

        public bool IsFixedSize => false;

        public Object SyncRoot => this;

        //TODO Vlasnotsi kolekci
        
        public void Add(Player item)
        {
            Node node = head;
            if (node == null) {
                head = new Node(item,null,null);
                return;
            }
            while (node.next != null)
            {
                node = node.next;
            }
            node.next = new Node(item,node,null);
        }

        public void Clear()
        {
            if (head != null && head.next != null)
            {
                head.next.prev = null;
            }
            head = null;
        }

        public bool Contains(Player item)
        {
            Node node = head;
            if (node == null)
            {
                return false;
            }
            if (node.player.Equals(item))
            {
                return true;
            }
            while (node.next != null)
            {
                node = node.next;
                if (node.player.Equals(item))
                {
                    return true;
                }
            }
            return false;
        }
        public IEnumerator<Player> GetEnumerator()
        {
            Node node = head;
            while (node != null) {
                yield return node.player;
                node = node.next;
            }
        }

        public int IndexOf(Player item)
        {
            Node node = head;
            if (node == null)
            {
                return -1;
            }
            if (node.player.Equals(item))
            {
                return 0;
            }
            int index = 1;
            while (node.next != null)
            {
                node = node.next;
                if (node.player.Equals(item))
                {
                    return index;
                }
                index++;
            }
            return -1;
        }

        public void Insert(int index, Player item)
        {
            int size = Count;
            if (index < 0 || index > size)
            {
                return;
            }
            if (index == 0)
            {
                if (head == null)
                {
                    head = new Node(item,null,null);
                    return;
                }
                Node headNode = head;
                head = new Node(item, null, headNode);
                headNode.prev = head;
            }
            else {
                if (index == size)
                {
                    Node lastNode = GoToIndex(index-1);
                    Node newNode = new Node(item, lastNode, null);
                    lastNode.next = newNode;
                }
                else { 
                    Node nextNode = GoToIndex(index);
                    Node prevNode = nextNode.prev;
                    Node newNode = new Node(item,prevNode,nextNode);
                    prevNode.next = newNode;
                    nextNode.prev = newNode;
                }
            }
        }

        public bool Remove(Player item)
        {
            Node node = head;
            if (node == null)
            {
                return false;
            }
            if (node.player.Equals(item))
            {
                removeHead(node);
                return true;
            }
            while (node.next != null)
            {
                node = node.next;
                if (node.player.Equals(item))
                {
                    if (node.next == null)
                    {
                        removeTail(node);
                        return true;
                    }
                    removeBody(node);
                    return true;
                }
            }
            return false;
        }

        public void RemoveAt(int index)
        {
            Node node = head;
            if (node == null)
            {
                return;
            }
            for (int i = 0; i < index; i++)
            {
                if (node.next == null)
                {
                    return;
                }
                node = node.next;
            }
            int size = this.Count;

            if (index == 0)
            {
                removeHead(node);
                return;
            }
            if (index < size-1)
            {
                removeBody(node);
                return;
            }
            removeTail(node);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        private int CountNodes() {
            if (head == null)
            {
                return 0;
            }
            int count = 1;
            Node node = head;
            while (node.next != null) { 
                node = node.next;
                count++;
            }
            return count;
        }

        private Node GoToIndex(int index) { 
            Node node = head;
            for (int i = 0; i < index; i++) { 
                node = node.next;
            }
            return node;
        }

        private void removeTail(Node node) {
            node.prev.next = null;
            node.prev = null;
        }
        private void removeBody(Node node)
        {
            node.prev.next = node.next;
            node.next.prev = node.prev;
            node.prev = null;
            node.next = null;
        }
        private void removeHead(Node node)
        {
            if (node.next == null)
            {
                head = null;
                return;
            }
            head = node.next;
            head.prev.next = null;
            head.prev = null;
        }

        public void CopyTo(Player[] array, int arrayIndex)
        {
            Node node = head;
            for (int i = arrayIndex; i < arrayIndex + this.Count; i++)
            {
                array[i] = node.player;
                if (node.next == null)
                {
                    break; 
                }
                node = node.next;
            }
        }

        public void CopyTo(Array array, int index)
        {
            Player[] array2 = (Player[])array.Clone();
            CopyTo(array2,index);
        }
    }
}
