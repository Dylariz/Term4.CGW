namespace CGW;

public class BinaryTree
{
    /// <summary>
    /// Класс, описывающий узел дерева
    /// </summary>
    private class Node
    {
        public int Value { get; set; }
        public Node? Left { get; set; }
        public Node? Right { get; set; }

        public Node(int value)
        {
            Value = value;
            Left = null;
            Right = null;
        }
    }

    /// <summary>
    /// Корень дерева
    /// </summary>
    private Node? _root;

    /// <summary>
    /// Конструктор, создающий дерево из списка значений
    /// </summary>
    /// <param name="list">Список значений</param>
    public BinaryTree(List<int> list)
    {
        _root = null;
        InsertRange(list);
    }
        
    /// <summary>
    /// Приватная перегрузка метода, вставляющего новый узел в дерево
    /// </summary>
    /// <param name="current">Стартовый узел рекурсии</param>
    /// <param name="value">Значение нового узла</param>
    /// <returns>Новый узел</returns>
    private Node Insert(Node? current, int value)
    {
        if (current == null)
        {
            return new Node(value);
        }

        if (value < current.Value)
        {
            current.Left = Insert(current.Left, value);
        }
        else if (value > current.Value)
        {
            current.Right = Insert(current.Right, value);
        }

        return current;
    }

    /// <summary>
    /// Вставляет новый узел в дерево
    /// </summary>
    /// <param name="value">Значение нового узла</param>
    public void Insert(int value)
    {
        _root = Insert(_root, value);
    }
    
    /// <summary>
    /// Вставляет список значений в дерево
    /// </summary>
    /// <param name="list">Список значений</param>
    public void InsertRange(List<int> list)
    {
        foreach (var t in list)
        {
            Insert(t);
        }
    }
        
    /// <summary>
    /// Удаляет стартовый узел из дерева, таким образом отчищая его.
    /// </summary>
    public void Clear()
    {
        _root = null;
    }
        

    #region ConsolePrint
    private class NodeInfo
    {
        public Node? Node;
        public string? Text;
        public int StartPos;
        public int Size => Text!.Length;

        public int EndPos
        {
            get => StartPos + Size;
            set => StartPos = value - Size;
        }

        public NodeInfo? Parent, Left, Right;
    }
        
    public void Print(int topMargin = 1, int leftMargin = 2)
    {
        Print(_root, topMargin, leftMargin);
    }

    private void Print(Node? current, int topMargin, int leftMargin)
    {
        if (current == null) return;
        int rootTop = Console.CursorTop + topMargin;
        var last = new List<NodeInfo>();
        var next = current;
        for (int level = 0; next != null; level++)
        {
            var item = new NodeInfo { Node = next, Text = next.Value.ToString(" 0 ") };
            if (level < last.Count)
            {
                item.StartPos = last[level].EndPos + 1;
                last[level] = item;
            }
            else
            {
                item.StartPos = leftMargin;
                last.Add(item);
            }

            if (level > 0)
            {
                item.Parent = last[level - 1];
                if (next == item.Parent.Node!.Left)
                {
                    item.Parent.Left = item;
                    item.EndPos = Math.Max(item.EndPos, item.Parent.StartPos);
                }
                else
                {
                    item.Parent.Right = item;
                    item.StartPos = Math.Max(item.StartPos, item.Parent.EndPos);
                }
            }

            next = next.Left ?? next.Right;
            for (; next == null; item = item.Parent)
            {
                Print(item, rootTop + 2 * level);
                if (--level < 0) break;
                if (item == item.Parent!.Left)
                {
                    item.Parent.StartPos = item.EndPos;
                    next = item.Parent.Node!.Right;
                }
                else
                {
                    if (item.Parent.Left == null)
                        item.Parent.EndPos = item.StartPos;
                    else
                        item.Parent.StartPos += (item.StartPos - item.Parent.EndPos) / 2;
                }
            }
        }

        Console.SetCursorPosition(0, rootTop + 2 * last.Count - 1);
    }

    private void Print(NodeInfo item, int top)
    {
        SwapColors();
        Print(item.Text!, top, item.StartPos);
        SwapColors();
        if (item.Left != null)
            PrintLink(top + 1, "┌", "┘", item.Left.StartPos + item.Left.Size / 2, item.StartPos);
        if (item.Right != null)
            PrintLink(top + 1, "└", "┐", item.EndPos - 1, item.Right.StartPos + item.Right.Size / 2);
    }

    private void PrintLink(int top, string start, string end, int startPos, int endPos)
    {
        Print(start, top, startPos);
        Print("─", top, startPos + 1, endPos);
        Print(end, top, endPos);
    }

    private void Print(string s, int top, int left, int right = -1)
    {
        Console.SetCursorPosition(left, top);
        if (right < 0) right = left + s.Length;
        while (Console.CursorLeft < right) Console.Write(s);
    }

    private void SwapColors()
    {
        (Console.ForegroundColor, Console.BackgroundColor) = (Console.BackgroundColor, Console.ForegroundColor);
    }

    #endregion
}