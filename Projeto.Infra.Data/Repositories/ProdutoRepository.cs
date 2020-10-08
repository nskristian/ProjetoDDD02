using Microsoft.EntityFrameworkCore;
using Projeto.Domain.Contracts.Repositories;
using Projeto.Domain.Entities;
using Projeto.Infra.Data.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Projeto.Infra.Data.Repositories
{
    public class ProdutoRepository : BaseRepository<Produto>, IProdutoRepository
    {
        private readonly DataContext dataContext;

        public ProdutoRepository(DataContext dataContext)
            : base(dataContext)
        {
            this.dataContext = dataContext;
        }

        public List<Produto> GetByNome(string nome)
        {
            return dataContext.Produto.Where(p => p.Nome.Contains(nome)).ToList();
        }

        public override List<Produto> GetAll()
        {
            return dataContext.Produto
                .Include(p => p.Fornecedor)
                .ToList();
        }

        public override Produto GetById(int id)
        {
            return dataContext.Produto
                .Include(p => p.Fornecedor) //JOIN
                .FirstOrDefault(p => p.IdProduto == id);
        }
    }
}
