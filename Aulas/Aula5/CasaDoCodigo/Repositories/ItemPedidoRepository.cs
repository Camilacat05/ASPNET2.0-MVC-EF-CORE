using CasaDoCodigo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CasaDoCodigo.Repositories
{
    public interface IItemPedidoRepository
    {
        ItemPedido GetItemPedido(int itemPedidoId);
        void RemoveItemPedido(int itemPedidoId); // método para remover os itens do carrinho pelo o id do item
    }

    public class ItemPedidoRepository : BaseRepository<ItemPedido>, IItemPedidoRepository
    { 
        public ItemPedidoRepository(ApplicationContext contexto) : base(contexto)
        {      
        }
            public ItemPedido GetItemPedido(int itemPedidoId)
        {
            return
            dbSet
            .Where(ip => ip.Id == itemPedidoId)
            .SingleOrDefault();

        }

        public void RemoveItemPedido(int itemPedidoId)
        { 
            dbSet.Remove(GetItemPedido(itemPedidoId));//Remove o tem do carrinho e do bancco de dados 
        }
    }
}