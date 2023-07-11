using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevFreela.Coree.Models;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Infrastructure.Persistence
{
    public static class Extensions
    {
        public static async Task<PaginationResult<T>> GetPaged<T>(this IQueryable<T> query, 
            int page, int pageSize) where T : class
        {
             var result = new PaginationResult<T>();

            //definir qual página
            result.Page = page;
            //tamanho de cada pagina
            result.PageSize = pageSize;
            //qual tamanho total da consulta que ta retornando
            result.ItemsCount = await query.CountAsync();

            //obter o tamanho da contagem de paginas que tem e divide pelo tam da page
            var pageCount = (double)result.ItemsCount / pageSize;
            //arredonda o tamanho da página, caso fique quebrado "ceiling = teto" sempre para cima
            result.TotalPages = (int)Math.Ceiling(pageCount);

            //qnts paginas ira pular
            var skip = (page - 1) * pageSize;

            //atribui ao data o valor da consulta, pula x elementos e pega x elementos e faz a consulta paginada!
            result.Data = await query.Skip(skip).Take(pageSize).ToListAsync();

            return result;
        }
    }
}
