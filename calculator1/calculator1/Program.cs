using calculator1;

Console.WriteLine("Print your mathematical expression");
string result = Console.ReadLine();
string b = "";
ArrayList tokens = new ArrayList();
for (int i = 0; i < result.Length; i++)
{
    if (char.IsDigit(result[i]))
    {
        b += result[i];
    }
    else if (result[i].ToString() == " " & b != "")
    {
        tokens.Add(b);
        b = "";
    }
    else if ("+-*/^()".Contains(result[i].ToString()))
    {
        if (b != "")
        {
            tokens.Add(b);
            b = "";
        }
        tokens.Add(result[i].ToString());
    }
    else if (char.IsLetter(result[i]))
    {
        while (i < result.Length && char.IsLetter(result[i]))
        {
            b += result[i];
            i++;
        }
        tokens.Add(b);
        b = "";
        i--;
    }
    else if (result[i] == ',')
    {
        if (b != "")
        {
            tokens.Add(b);
            b = "";
        }
        tokens.Add(result[i].ToString());
    }
}

if (b != "")
{
    tokens.Add(b);
}

int bracketBalance = 0;

for (int i = 0; i < tokens.Count(); i++)
{
    if (i > 0 && "+-*/^".Contains(tokens.GetAt(i)) && "+-*/^".Contains(tokens.GetAt(i-1)))
    {
        throw new Exception("You can't put two operators in a row");
    }
    if (tokens.GetAt(i) == "(")
    {
        bracketBalance++; 
    }
    else if (tokens.GetAt(i) == ")")
    {
        bracketBalance--; 
        if (bracketBalance < 0)
        {
            throw new Exception("BracketsError");
        }
    }
}
if (bracketBalance != 0)
{
    throw new Exception("BracketsError");
}

int GetPriority(string op)
{
    switch (op)
    {
        case "sin": return 4;
        case "cos": return 4;
        case "max": return 4;
        case "^": return 3; 
        case "*": return 2;
        case "/": return 2;
        case "+": return 1;
        case "-": return 1;
        default: return 0; 
    }
}

Queue tokenQueue = new Queue();
Stack operatorsStack = new Stack();

for (int i = 0; i < tokens.Count(); i++)
{
    if (double.TryParse(tokens.GetAt(i), out _))
    {
        tokenQueue.Enqueue(tokens.GetAt(i));
    }
    else if (tokens.GetAt(i) == "sin" || tokens.GetAt(i) == "cos" || tokens.GetAt(i) == "max")
    {
        operatorsStack.Push(tokens.GetAt(i));
    }
    else if (tokens.GetAt(i) == "(")
    {
        operatorsStack.Push(tokens.GetAt(i));
    }
    else if (tokens.GetAt(i) == ")")
    {
        while (operatorsStack.Peek() != "(")
        {
            tokenQueue.Enqueue(operatorsStack.Pop());
        }
        operatorsStack.Pop();
        
        if (operatorsStack.Peek() == "sin" ||  operatorsStack.Peek() == "cos" ||   operatorsStack.Peek() == "max")
        {
            tokenQueue.Enqueue(operatorsStack.Pop());
        }
    }
    else if (tokens.GetAt(i) == ",")
    {
        while (operatorsStack.Peek() != "(")
        {
            tokenQueue.Enqueue(tokens.GetAt(i));
        }
    }
    else
    {
        while (operatorsStack.Count() > 0 && GetPriority(operatorsStack.Peek()) >= GetPriority(tokens.GetAt(i)))
        {
            tokenQueue.Enqueue(operatorsStack.Pop());
        }
        operatorsStack.Push(tokens.GetAt(i));
    }
}

while (operatorsStack.Count() > 0)
    {
        tokenQueue.Enqueue(operatorsStack.Pop());
    }

Stack finalStack = new Stack();

while  (tokenQueue.Count() > 0)
{
    string thisToken = tokenQueue.Dequeue();
    if (double.TryParse(thisToken, out _))
    {
        finalStack.Push(thisToken);
    }
    else if (thisToken == "sin")
    {
        double degree = double.Parse(finalStack.Pop());
        finalStack.Push(Math.Sin(degree*Math.PI/180).ToString());
    }
    else if (thisToken == "cos")
    {
        double degree = double.Parse(finalStack.Pop());
        finalStack.Push(Math.Cos(degree*Math.PI/180).ToString());
    }
    else
    {
        double right = double.Parse(finalStack.Pop()); // Правий операнд
        double left = double.Parse(finalStack.Pop()); // Лівий операнд
        double res = 0;

        switch (thisToken)
        {
            case "+": res = left + right; 
                break;
            case "-": res = left - right; 
                break;
            case "*": res = left * right; 
                break;
            case "/": res = left / right; 
                break;
            case "^": res = Math.Pow(left, right);
                break;
            case "max": res = Math.Max(left, right);
                break;
        }
        finalStack.Push(res.ToString());
    }
    
}

Console.WriteLine(finalStack.Peek());




