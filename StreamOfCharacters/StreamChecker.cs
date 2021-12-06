namespace StreamOfCharacters;

public class StreamChecker
{
    private Node? root;
    private int bufferSize;
    private LinkedList<char> buffer;

    public StreamChecker(string[] words)
    {
        root = new Node();
        buffer = new LinkedList<char>();
        foreach (var word in words)
        {
            if (word.Length > bufferSize)
            {
                bufferSize = word.Length;
            }
            var node = root;
            for (var i = word.Length - 1; i > -1; i--)
            {
                var letterCode = word[i] - 'a';
                if(node.children[letterCode] == null)
                { 
                    node.children[letterCode] = new Node();
                }
                node = node.children[letterCode];
            }
            node.isLeaf = true;
        }
    }

    public bool Query(char letter)
    {
        if (buffer.Count == bufferSize)
        {
            buffer.RemoveLast();
        }
        buffer.AddFirst(letter);
        var node = root;
        foreach (var symbol in buffer)
        {
            var code = symbol - 'a';
            node = node.children[code];
            if (node == null)
            {
                break;
            }
            if (node.isLeaf) return true;
        }
        return false;
    }
}