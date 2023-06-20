using Microsoft.Data.Sqlite;
using AtividadeAvaliativa.Database;
using AtividadeAvaliativa.Repositories;
using AtividadeAvaliativa.Models;

var databaseConfig = new DatabaseConfig();
var databaseSetup = new DatabaseSetup(databaseConfig);

var clientesRepository = new ClientesRepository(databaseConfig);
var pedidosRepository = new PedidosRepository(databaseConfig);
var itenspedidosRepository = new ItensPedidosRepository(databaseConfig);
var vendedoresRepository = new VendedoresRepository(databaseConfig);
var produtosRepository = new ProdutosRepository(databaseConfig);

var modelName = args[0];
var modelAction = args[1];


if (modelName == "Cliente")
{
    if (modelAction == "Listar")
    {
        Console.WriteLine("Listar Cliente");
        Console.WriteLine("Código Cliente  Nome Cliente          Endereço                     Cidade                     CEP         UF                          IE");
        foreach (var cliente in clientesRepository.Listar())
        {
            Console.WriteLine($"{cliente.CodCliente,-12} {cliente.Nome,-29} {cliente.Endereco,-26} {cliente.Cidade,-26} {cliente.Cep,-20} {cliente.Uf,-29} {cliente.Ie}");
        }
    }

    if (modelAction == "Inserir")
    {
        Console.WriteLine("Inserir Cliente!");
        Console.Write("Digite o código do cliente    : ");
        int codcliente = Convert.ToInt32(Console.ReadLine());
        Console.Write("Digite o Nome do cliente      : ");
        string? nome = Console.ReadLine();
        Console.Write("Digite o Endereço do cliente  : ");
        string? endereco = Console.ReadLine();
        Console.Write("Digite o Cidade do cliente    : ");
        string? cidade = Console.ReadLine();
        Console.Write("Digite o CEP do cliente       : ");
        string? cep = Console.ReadLine();
        Console.Write("Digite o UF do cliente        : ");
        string? uf = Console.ReadLine();
        Console.Write("Digite o Inscrição Estatual   : ");
        string? ie = Console.ReadLine();

        var cliente = new Clientes(codcliente, nome, endereco, cidade, cep, uf, ie);
        clientesRepository.Inserir(cliente);
    }


    if (modelAction == "Apresentar")
    {
        Console.WriteLine("Apresentar Cliente");
        Console.Write("Digite o código do cliente : ");
        var clienteid = Convert.ToInt32(Console.ReadLine());

        if (clientesRepository.Apresentar(clienteid))
        {
            var cliente = clientesRepository.GetById(clienteid);
            Console.WriteLine($"{cliente.CodCliente}, {cliente.Nome}, {cliente.Endereco}, {cliente.Cidade}, {cliente.Cep}, {cliente.Uf}, {cliente.Ie}");
        }
        else
        {
            Console.WriteLine($"O cliente com o código {clienteid} não existe.");
        }
    }
}



if (modelName == "Pedido")
{
    if (modelAction == "Listar")
    {

        Console.WriteLine("Listar Pedido");
        Console.WriteLine("Código Pedido  Prazo Entrega    Data Pedido        Código Cliente        Código Vendedor");
        foreach (var pedido in pedidosRepository.Listar())
        {
            Console.WriteLine($"{pedido.CodPedido,-12} {pedido.PrazoEntrega,-14} {pedido.DataPedido,-21} {pedido.PedidoCodCliente,-9} {pedido.PedidoCodVendedor,-28}");
        }
    }

    if (modelAction == "Inserir")
    {
        Console.WriteLine("Inserir Pedido");
        Console.Write("Digite o código do pedido:         ");
        int codPedido = Convert.ToInt32(Console.ReadLine());
        Console.Write("Digite o prazo de entrega do pedido:    ");
        DateTime prazoEntrega = Convert.ToDateTime(Console.ReadLine());       
        Console.Write("Digite o código do cliente do pedido");
        int pedidoCodCliente = Convert.ToInt32(Console.ReadLine());
        Console.Write("Digite o código do vendedor do pedido");
        int pedidoCodVendedor = Convert.ToInt32(Console.ReadLine());
        var pedido = new Pedidos(codPedido, prazoEntrega, DateTime.Now, pedidoCodCliente, pedidoCodVendedor);
        pedidosRepository.Inserir(pedido);
    }

    if (modelAction == "Apresentar")
    {
        Console.WriteLine("Apresentar Pedido");
        Console.Write("Digite o id do pedido : ");
        var codPedido = Convert.ToInt32(Console.ReadLine());

        if (pedidosRepository.Apresentar(codPedido))
        {
            var pedido = pedidosRepository.GetById(codPedido);
            Console.WriteLine($"{pedido.CodPedido}, {pedido.PrazoEntrega}, {pedido.DataPedido}, {pedido.PedidoCodCliente}, {pedido.PedidoCodVendedor}");
        }
        else
        {
            Console.WriteLine($"O cliente com o código {codPedido} não existe.");
        }
    }

    if (modelAction == "MostrarPedidosCliente")
    {
        Console.WriteLine("Mostrar Pedidos do Cliente");
        Console.Write("Digite o código do cliente : ");
        var codCliente = Convert.ToInt32(Console.ReadLine());

        if (clientesRepository.Apresentar(codCliente))
        {
            foreach (var pedido in pedidosRepository.Listar())
            {
                if (pedido.PedidoCodCliente == codCliente)
                {
                    Console.WriteLine($"{pedido.CodPedido}, {pedido.PrazoEntrega}, {pedido.DataPedido}, {pedido.PedidoCodCliente}, {pedido.PedidoCodVendedor}");
                }
            }
        }
        else
        {
            Console.WriteLine($"O cliente com Id {codCliente} não existe.");
        }
    }
}

if (modelName == "Produto")
{
    if (modelAction == "Listar")
    {

        Console.WriteLine("Listar Produto");
        Console.WriteLine("Código Produto   Descrição   Valor Unitário");
        foreach (var produto in produtosRepository.Listar())
        {
            Console.WriteLine($"{produto.CodProduto,-12} {produto.Descricao,-14} {produto.ValorUnitario,-21}");
        }
    }

    if (modelAction == "Inserir")
    {
        Console.WriteLine("Inserir Produto!");
        Console.Write("Digite o Código do Produto         : ");
        var codProduto = Convert.ToInt32(Console.ReadLine());
        Console.Write("Digite a Descrição do Produto      : ");
        string? descricao = Console.ReadLine();
        Console.Write("Digite o Valor Únitario do Produto : ");
        var valorUnitario = Convert.ToInt32(Console.ReadLine());
        var produto = new Produtos(codProduto, descricao, valorUnitario);
        produtosRepository.Inserir(produto);
    }

    if (modelAction == "Apresentar")
    {
        Console.WriteLine("Apresentar Produto");
        Console.Write("Digite o código do produto : ");
        var codProduto = Convert.ToInt32(Console.ReadLine());

        if (produtosRepository.ExitsById(codProduto))
        {
            var produto = produtosRepository.GetById(codProduto);

            Console.WriteLine($"{produto.CodProduto} {produto.Descricao} {produto.ValorUnitario}");
        }
        else
        {
            Console.WriteLine($"O produto com código {codProduto} não existe.");
        }
    }
}

if (modelName == "ItensPedido")
{
    if (modelAction == "Listar")
    {

        Console.WriteLine("Listar Itens do Pedido");
        Console.WriteLine("Código Item Pedido   Quantidade   Código Pedido   Codigo Produto");
        foreach (var ip in itenspedidosRepository.Listar())
        {
            Console.WriteLine($"{ip.CodItemPedido,-12} {ip.Quantidade,-15} {ip.ItemPedidoCodPedido,-15} {ip.ItemPedidoCodProduto,-15}");
        }
    }

    if (modelAction == "Inserir")
    {
        Console.WriteLine("Inserir Itens do Pedido");
        Console.Write("Digite o código do item do pedido            : ");
        int codItemPedido = Convert.ToInt32(Console.ReadLine());
        Console.Write("Digite o código do pedido do item do pedido  : ");
        int itempedidocodpedido = Convert.ToInt32(Console.ReadLine());
        Console.Write("Digite o código do produto do item do pedido : ");
        int itempedidocodproduto = Convert.ToInt32(Console.ReadLine());
        Console.Write("Digite a quantidade do item do pedido        : ");
        int quantidade = Convert.ToInt32(Console.ReadLine());

        var itemPedido = new ItensPedidos(codItemPedido, itempedidocodpedido, itempedidocodproduto, quantidade);
        itenspedidosRepository.Inserir(itemPedido);
    }

    if (modelAction == "Apresentar")
    {
        Console.WriteLine("Apresentar Pedido");
        Console.Write("Digite o código do pedido : ");
        var codItemPedido = Convert.ToInt32(Console.ReadLine());

        if (itenspedidosRepository.ExitsById(codItemPedido))
        {
            var itensPedido = itenspedidosRepository.GetById(codItemPedido);
            Console.WriteLine($"{itensPedido.CodItemPedido}, {itensPedido.Quantidade}, {itensPedido.ItemPedidoCodPedido}, {itensPedido.ItemPedidoCodProduto}");
        }
        else
        {
            Console.WriteLine($"O pedido com o código {codItemPedido} não existe.");
        }
    }
}

if (modelName == "Vendedor")
{
    if (modelAction == "Listar")
    {

        Console.WriteLine("Listar Vendedor!");
        Console.WriteLine("Código Vendedor   Nome Vendedor   Salário Fixo        Faixa Comissão");
        foreach (var vendedor in vendedoresRepository.Listar())
        {
            Console.WriteLine($"{vendedor.CodVendedor,-12} {vendedor.Nome,-14} {vendedor.SalarioFixo,-21} {vendedor.FaixaComissao,-17}");
        }
    }

    if (modelAction == "Inserir")
    {
        Console.WriteLine("Inserir Vendedor");
        Console.Write("Digite o Código do vendedor:            ");
        int codVendedor = Console.Read();
        Console.Write("Digite o Nome do vendedor:              ");
        string? nome = Console.ReadLine();
        Console.Write("Digite o Salário do vendedor:           ");
        decimal salario = decimal.Parse(Console.ReadLine());
        Console.Write("Digite a Faixa de Comissão do vendedor: ");
        string? faixaComissao = Console.ReadLine();

        var vendedor = new Vendedores(codVendedor, nome, salario, faixaComissao);
        vendedoresRepository.Inserir(vendedor);
    }

    if (modelAction == "Apresentar")
    {
        Console.WriteLine("Apresentar Vendedor");
        Console.Write("Digite o código do vendedor : ");
        int codVendedor = Console.Read();

        if (vendedoresRepository.ExitsById(codVendedor))
        {
            var vendedor = vendedoresRepository.GetById(codVendedor);
            Console.WriteLine($"{vendedor.CodVendedor}, {vendedor.Nome}, {vendedor.SalarioFixo}, {vendedor.FaixaComissao}");
        }
        else
        {
            Console.WriteLine($"O vendedor com o código {codVendedor} não existe.");
        }
    }
}
