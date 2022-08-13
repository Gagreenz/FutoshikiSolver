var input = new List<string>();
input.Add("0 0 0>0 0");
input.Add("^        ");
input.Add("0 2>0 4 0");
input.Add("  ^      ");
input.Add("0 0 0>0 0");
input.Add("         ");
input.Add("0 0<0 0 0");
input.Add("        ^");
input.Add("4<0 0 0 0");

int size = input.Count;
List<string> lines = new List<string>();

Solver solver = new Solver(size, input);

Stack<int> track = new Stack<int>();
int index = 0;
int currentNum = 1;
int CellCount = size * size;
while (CellCount > index)
{
    if ((index / size) % 2 != 0)
    {
        index += size;
    }
    if (index % 2 != 0 || solver.Field[index / size][index % size] != '0')
    {
        index++;
        continue;
    }
    bool IsFilled = false;

    for (int i = currentNum; i <= size / 2 + 1; i++)
    {
        if (solver.IsValid(index, i))
        {
            solver.Put(index, i);
            IsFilled = true;
            break;
        }
    }
    if (IsFilled)
    {
        track.Push(index);
        index++;
        currentNum = 1;
    }
    else
    {
        if (track.Count == 0)
        {
            break;
        }
        else
        {
            index = track.Pop();
            currentNum = (solver.Field[index / size][index % size] - '0') + 1;
            solver.Field[index / size][index % size] = '0';
        }
    }

}
solver.DebugPrint();

class Solver
{
    public char[][] Field;
    public int Size;
    public Solver(int _size, List<string> input)
    {
        Size = _size;
        Field = new char[Size][];
        for (int i = 0; i < Size; i++)
        {
            Field[i] = new char[Size];
        }
        for (int i = 0; i < Size; i++)
        {
            for (int j = 0; j < Size; j++)
            {
                Field[i][j] = input[i][j];
            }
        }
    }
    public void DebugPrint()
    {
        for (int i = 0; i < Size; i++)
        {
            for (int j = 0; j < Size; j++)
            {
                Console.Write(Field[i][j]);
            }
            Console.WriteLine();
        }
    }
    public void Put(int index, int value)
    {
        Field[index / Size][index % Size] = Convert.ToChar(value.ToString());
    }
    public bool IsValid(int index, int value)
    {
        var temp = Convert.ToChar(value.ToString());
        if ((index / Size) % 2 != 0) return false;
        for (int i = 0; i < Size; i++)
        {
            if (Field[index / Size][i] == temp) return false;
            if (Field[i][index % Size] == temp) return false;
        }
        //Up
        if (index / Size != 0)
        {
            if (Field[index / Size - 2][index % Size] != '0')
            {
                if (Field[index / Size - 1][index % Size] == 'v')
                    if (temp > Field[index / Size - 2][index % Size]) return false;
                if (Field[index / Size - 1][index % Size] == '^')
                    if (temp < Field[index / Size - 2][index % Size]) return false;
            }
        }
        //Down
        if (index / Size != Size - 1)
        {
            if (Field[index / Size + 2][index % Size] != '0')
            {
                if (Field[index / Size + 1][index % Size] == 'v')
                    if (temp > Field[index / Size + 2][index % Size]) return false;
                if (Field[index / Size + 1][index % Size] == '^')
                    if (temp < Field[index / Size + 2][index % Size]) return false;
            }
        }
        //Left
        if (index > 0 && (index - 1) / Size == index / Size)
        {
            if (Field[index / Size][(index - 2) % Size] != '0')
            {
                if (Field[index / Size][(index - 1) % Size] == '<')
                    if (temp < Field[index / Size][(index - 2) % Size]) return false;
                if (Field[index / Size][(index - 1) % Size] == '>')
                    if (temp > Field[index / Size][(index - 2) % Size]) return false;
            }
        }
        //Right
        if (index < Size * Size && (index + 1) / Size == index / Size)
        {
            if (Field[index / Size][(index + 2) % Size] != '0')
            {
                if (Field[index / Size][(index + 1) % Size] == '<')
                    if (temp > Field[index / Size][(index + 2) % Size]) return false;
                if (Field[index / Size][(index + 1) % Size] == '>')
                    if (temp < Field[index / Size][(index + 2) % Size]) return false;
            }

        }
        return true;
    }
}