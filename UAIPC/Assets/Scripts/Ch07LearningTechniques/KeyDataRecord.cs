using System.Collections;
using System.Collections.Generic;

public class KeyDataRecord<T>
{
    public Dictionary<T, int> counts;
    public int total;
    
    public KeyDataRecord()
    {
        counts = new Dictionary<T, int>();
    }
}
