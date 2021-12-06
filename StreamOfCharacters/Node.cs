namespace StreamOfCharacters;

public class Node
{
    public Node[] children;
    public bool isLeaf;

    public Node()
    {
        children = new Node[26];
        isLeaf = false;
    }
}