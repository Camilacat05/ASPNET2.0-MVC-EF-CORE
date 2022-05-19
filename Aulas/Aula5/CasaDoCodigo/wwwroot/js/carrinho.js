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

        });  // passando os dados 
        debugger;// utilizando JQUERY PARA DÁ FUNÇÃO AO BOTÃO
    }
}

var carrinho = new Carrinho();
