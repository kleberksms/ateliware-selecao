using System;
using Ateliware.Shared.Enums;

namespace Ateliware.Server.Entities
{
    public sealed class Repository
    {
        public Repository(int id, string name, string url, Language language)
        {
            Guid = Guid.NewGuid();
            Id = id;
            Name = name;
            Url = url;
            Language = language;
        }

        public Repository()
        {
            

        }

        public Guid Guid { get; private set; }
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Url { get; private set; }
        public Language Language { get; private set; }
    }
}
