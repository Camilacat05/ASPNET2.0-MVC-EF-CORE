using CasaDoCodigo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CasaDoCodigo.Repositories
{
    public interface IItemPedidoRepository
    {
        void UpdateQuant(ItemPedido itemPedido);
    }

    public class ItemPedidoRepository : BaseRepository<ItemPedido>, IItemPedidoRepository
    {
        public ItemPedidoRepository(ApplicationContext contexto) : base(contexto)
        {


        }

        public void UpdateQuant(ItemPedido itemPedido)
        {
            // este metodo altera a quantidade do produto no carrinho  e grava no banco de dados. 
            var itemPedidoDB =
                 dbSet
                .Where(ip => ip.Id == itemPedido.Id)
                .SingleOrDefault();

            if (itemPedidoDB != null)
            {
                itemPedidoDB.AtualizaQuantidade(itemPedido.Quantidade);
                contexto.SaveChanges();// Salva as alterações no Banco de Dados
            }

        }
    }
}