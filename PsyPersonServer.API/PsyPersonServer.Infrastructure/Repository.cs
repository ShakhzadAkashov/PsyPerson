using PsyPersonServer.Domain;
using System;
using System.Collections.Generic;

namespace PsyPersonServer.Infrastructure
{
    public class Repository
    {
        public List<Person> Persons()
        {
            return new List<Person>
            { 
                new Person { Name = "Shakhzad", Age = 21},
                new Person { Name = "Nikita", Age = 20 }
            };
        }
    }
}
