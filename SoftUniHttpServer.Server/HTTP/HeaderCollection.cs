using System.Collections;

namespace SoftUniHttpServer.Server.HTTP
{
    public class HeaderCollection : IEnumerable<Header>
    {
        private readonly Dictionary<string, Header> headers = new Dictionary<string, Header>();

        public string this[string name] => this.headers[name].Value;

        public int Count => headers.Count;

        public bool Contains(string name) => this.headers.ContainsKey(name);

        public void Add(string name, string value) => this.headers[name] = new Header(name, value);

        public IEnumerator<Header> GetEnumerator() => this.headers.Values.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
    }
}
