using CasaDoCodigo.Models;
using CasaDoCodigo.Models.ViewModels;
using CasaDoCodigo.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CasaDoCodigo.Controllers
{
    public class PedidoController : Controller
    {
        private readonly IProdutoRepository produtoRepository;
        private readonly IPedidoRepository pedidoRepository;
        private readonly IItemPedidoRepository itemPedidoRepository; //injetando o repositório no Controller


        public PedidoController(IProdutoRepository produtoRepository,
            IPedidoRepository pedidoRepository, IItemPedidoRepository itemPedidoRepository) // passa a interfacee e o repositório no constructor do Controller
        {
            this.produtoRepository = produtoRepository;
            this.pedidoRepository = pedidoRepository;
            this.itemPedidoRepository = itemPedidoRepository; 

        }

        public IActionResult Carrossel()
        {
            return View(produtoRepository.GetProdutos());
        }

        public IActionResult Carrinho(string codigo)
        {
            if (!string.IsNullOrEmpty(codigo))
            {
                pedidoRepository.AddItem(codigo);
            }


            List<ItemPedido> itens = pedidoRepository.GetPedido().Itens;
            CarrinhoViewModel carrinhoViewModel = new CarrinhoViewModel(itens);
           return base.View(carrinhoViewModel);
        }

        public IActionResult Cadastro()
        {
            var pedido = pedidoRepository.GetPedido(); // vai pegar o pedido realizado com os itens

            if (pedido == null) //caso nao tenha nenhum pedido
            {
                return RedirectToAction("Carrosel");// o cliente vai ser redirecionado para a página inicial que nesse caso é a página de carrosel
            }

            return View(pedido.Cadastro);
        }
        [HttpPost] // define  que o metodo e post e que nao e possivel acessar pelo o endereco do Brownser
        //uma forma de proteger a Action do Controller contra ataques CRSF(Cross-site Request Forgery)
        [ValidateAntiForgeryToken] //regra de seguranca, faz com que a aplicacao forneca o token na requisicao, para que seja concluida e autorizada o compartillhamento de informacao. Valida o tolken no momento em que recebemos uma requisição.
        public IActionResult Resumo(Cadastro cadastro)//o resumo vai receber os dados do cadastro pelo o metodo post 
        {
            if (ModelState.IsValid) //verifica o estado do modelo cadastro que se estiver valido, vai para o resumo com os dados validado, se n, e redirecionado pro cadastro novamente para refazer 
            {
                return View(pedidoRepository.UpdateCadastro(cadastro));//Isso mesmo! Caso o modelo seja válido, os dados serão gravados. Caso contrário, a chamada é redirecionada para a action "Cadastro".
            }
            return RedirectToAction("Cadastro");
        }
 
        
        [HttpPost] //para definir que a requisição é post
        
        public UpdateQuantidadeResponse UpdateQuantidade([FromBody] ItemPedido itemPedido) //os métodos do projeto
        {
            //FromBody indica que os dadoss irão vim no corpo da requisição, co caso o item pedidos também vem o data dentro
            // com a quantidade e id do pedido 
             return  pedidoRepository.UpdateQuant(itemPedido);


        }

    }

}

