using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Propostas.Infra.Data.Contexto;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Propostas.Infra.Data.Test
{
    [ExcludeFromCodeCoverage]
    public static class PropostaDbContextTest
    {
        public static PropostaDbContext Context { get; private set; }

       
        public static PropostaDbContext CreateContext()
        {
           

            var options = new DbContextOptionsBuilder<PropostaDbContext>()
                                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                                .EnableSensitiveDataLogging()
                                .Options;

            return  new PropostaDbContext(options);
        }

        
       
    }
}
