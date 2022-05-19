class Carrinho {

    
     clickIncremento(btn) { //INDICANDO QUE O PARAMETRO DA FUNÇÃO É UM BOTÃO

    let data = this.getData(btn);
    data.Quantidade++;              // aumenta a qauantidade do item
    this.postQuantidade(data);
   
  
    clickDecremento(btn) {
        let data = this.getData(btn);
        data.Quantidade--; //diminuí a qauntidade do item
        this.postQuantidade(data);
    }

    updateQuantidade(input) {
        let data = this.getData(input); // metodo input, (inserir) atualizar a quantidade, inserir o numero manualmente e atualizar no banco de dados 
        this.postQuantidade(data);
    }

    getData(elemento) {
        var linhaDoItem = $(elemento).parents('[item-id]'); // pega a tag pai que aparece o primeiro item-id
        var itemId = $(linhaDoItem).attr('item-id'); //função de atribuir,pegar o id da tag item id criada
        var novaQtde = $(linhaDoItem).find('input').val(); //encontra o valor dda qauntidade no input e pega o valor dela com a função .val


        return data = {
            Id: itemId,
            Quantidade: novaQtde
        };
}
    postQuantidade(data) {
        $.ajax({
            url: '/pedido/updatequantidade',
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(data)//tranforma o obj em String
             // passando os dados

        }).done(function (response) {

            let itemPedido = response.itemPedido; // o itempedido de resposta que é atualizado automaticamente na página
            let linhaDoItem = $('[item-id=' + itemPedido.id + ']')
            linhaDoItem.find('input').val(itemPedido.quantidade);
                // passa a qauntidade que foi alterado e após atualiza o sub-total e total do carrinho a partir da qauntidade atualizada
            linhaDoItem.find('[subtotal]').html((itemPedido.subtotal).duasCasas()); //atualizando o HTML com o novo valor com apenas 2 casas decimais após a ,
                debugger;// utilizando JQUERY PARA DÁ FUNÇÃO AO BOTÃO
        });
             $('[numero-itens]').html('Total: ' + carrinhoViewModel.itens.length + ' itens'); //pega o tamanho do array de itens e coloca no total de itens e insere no HTML


             if (itemPedido.quantidade == 0) { //caso a quantidade seja 0 a linha do item é removida
                 linhaDoItem.remove();
             }
       
    }
}

var carrinho = new Carrinho();


Number.prototype.duasCasas = function () {
    return this.toFixed(2).replace('.', ',');
};// foramatando a saída do total e sub-total  para 2 casas decimais após o . ou ,