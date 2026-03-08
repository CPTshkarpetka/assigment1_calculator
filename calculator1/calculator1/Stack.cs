namespace calculator1;

public class Stack
{
    private string[] _array = new string[10];

    private int _pointer;

    public void Push(string value)
    {
        if (_pointer == _array.Length)
        {
            var extendedArray = new string[_array.Length * 2];
            for (var i = 0; i < _array.Length; i++)
            {
                extendedArray[i] = _array[i];
            }
            _array = extendedArray;
        }

        _array[_pointer] = value;
        _pointer++;
    }
    
    public string Pop()
    {
        if (_pointer == 0)
        {
            return null;
        }
        _pointer--;
        var value = _array[_pointer];
        _array[_pointer] = null;
        return value;
    }

    public string Peek()
    {
        if (_pointer == 0)
        {
            return null;
        }
        return _array[_pointer-1];
    }

    public int Count()
    {
        return _pointer;
    }
}