using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Lab.Entities;

namespace Lab
{
    public class MyLookup : IEnumerable<IGrouping<string, Employee>>
    {
        private Dictionary<string, List<Employee>> _myLookup = new Dictionary<string, List<Employee>>();

        public IEnumerator<IGrouping<string, Employee>> GetEnumerator()
        {
            return _myLookup.Select(x => new MyGrouping(x.Key, x.Value)).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void AddElement(string key, Employee value)
        {
            if (_myLookup.ContainsKey(key))
            {
                _myLookup[key].Add(value);
            }
            else
            {
                _myLookup.Add(key, new List<Employee> { value });
            }
        }
    }
}