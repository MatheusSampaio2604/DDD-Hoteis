﻿using Domain.Interfaces;
using Domain.Models;
using Infra.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infra.Repository
{
    public class AcomodacaoRepository : Repository<Acomodacao>, IAcomodacaoRepository
    {
        public AcomodacaoRepository(DataContext context) : base(context)
        {

        }

        public async Task<IEnumerable<Acomodacao>> FindAcomodacoesWithPhrase(string phrase)
        {
            var obj = await DbSet.Where(a => a.Nome.Contains(phrase) && a.Ativo == true).OrderBy(a => a.Nome).ToListAsync();
            return obj;
        }

    }
}
