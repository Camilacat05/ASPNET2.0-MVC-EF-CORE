using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CasaDoCodigo.Models.ViewModels
{
    public class CarrinhoViewModel
    {
        //a view model não é gravadaa no banco de dados como taabeela, cocmo ocorre no model padrão
        public CarrinhoViewModel(IList<ItemPedido> itens)
        {
            Itens = itens;
        }

        public IList<ItemPedido> Itens { get; }

        public decimal Total => Itens.Sum(i => i.Quantidade * i.PrecoUnitario);
    }

}


// Isso mesmo! O ViewModel permite fornecer como modelo à View uma classe
// que contenha todas as
// informações de que ela precisa,
// sem modificarmos as entidades e classes do modelo da aplicação.
