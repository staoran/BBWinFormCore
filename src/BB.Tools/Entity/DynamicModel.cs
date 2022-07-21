using System.Collections;
using System.Dynamic;

namespace BB.Tools.Entity;

public class DynamicModel : DynamicObject, IEnumerable<KeyValuePair<string, object>>
{
    private readonly Dictionary<string, object> _storage = new();
    public override bool TryGetMember(GetMemberBinder binder, out object result)
    {
        if (_storage.ContainsKey(binder.Name))
        {
            result = _storage[binder.Name];
            return true;
        }
        result = null;
        return false;
    }
    public override bool TrySetMember(SetMemberBinder binder, object value)
    {
        string key = binder.Name;
        if (_storage.ContainsKey(key))
            _storage[key] = value;
        else
            _storage.Add(key, value);
        return true;
    }
    public override string ToString()
    {
        var message = new StringWriter();
        foreach (KeyValuePair<string, object> item in _storage)
            message.WriteLine("\"{0}\":\"{1}\"", item.Key, item.Value);
        return message.ToString();
    }
    public int Count => _storage.Count;

    public void Add(string key, object value)
    {
        _storage.Add(key, value);
    }

    public bool ContainsKey(string key)
    {
        return _storage.ContainsKey(key);
    }

    public bool Remove(string key)
    {
        return _storage.Remove(key);
    }

    public bool TryGetValue(string key, out object value)
    {
        return _storage.TryGetValue(key, out value);
    }

    public void Clear()
    {
        _storage.Clear();
    }

    public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
    {
        return ((IEnumerable<KeyValuePair<string, object>>)_storage).GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return ((IEnumerable<KeyValuePair<string, object>>)_storage).GetEnumerator();
    }
}