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


if(modelName == "Cliente")
{
    if(modelAction == "Listar")
    {
        Console.WriteLine("Listar Cliente");
        Console.WriteLine("Código Cliente  Nome Cliente          Endereço                     Cidade                     CEP         UF                          IE");
        foreach (var cliente in clientesRepository.Listar()) 
        {
             Console.WriteLine($"{cliente.CodCliente, -12} {cliente.Nome, -29} {cliente.Endereco, -26} {cliente.Cidade, -26} {cliente.Cep, -20} {cliente.Uf, -29} {cliente.Ie}");
        }
    }

    if(modelAction == "Inserir")
    {
        Console.WriteLine("Inserir Cliente!");
        Console.WriteLine("Digite o código do cliente    : ");
        var codcliente = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("Digite o Nome do cliente      : ");   
        string nome  = Console.ReadLine();
        Console.WriteLine("Digite o Endereço do cliente  : ");
        string endereco  =  Console.ReadLine();
        Console.WriteLine("Digite o Cidade do cliente    : ");
        string cidade  = Console.ReadLine();
        Console.WriteLine("Digite o CEP do cliente       : ");
        string cep = Console.ReadLine();
        Console.WriteLine("Digite o UF do cliente        : ");
        string uf  = Console.ReadLine();
        Console.WriteLine("Digite o Inscrição Estatual   : ");
        string ie  = Console.ReadLine();
       
        var cliente = new Clientes(codcliente, nome, endereco, cidade, cep, uf, ie);
        clientesRepository.Inserir(cliente);
    }


    if(modelAction == "Apresentar")
    {
        Console.WriteLine("Apresentar Cliente");
	    Console.Write("Digite o código do cliente : ");
        var clienteid = Convert.ToInt32(Console.ReadLine());
       
        if(clientesRepository.Apresentar(clienteid))
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



if(modelName == "Pedido")
{
    if(modelAction == "Listar")
    {

        Console.WriteLine("Pedido Listado!");
         Console.WriteLine("Nro Pedido   Id Empregado   Data do Pedido        Peso        Codigo da Transportadora   Id do Cliente");
        foreach (var pedido in pedidosRepository.GetAll())
        {
             Console.WriteLine($"{pedido.CodPedido, -12} {pedido.EmpregadoID, -14} {pedido.DataPedido, -21} {pedido.Peso, -9} {pedido.CodTransportadora, -28} {pedido.PedidoClienteID}");
        }
    }

    if(modelAction == "Inserir")
    {
        Console.WriteLine("Inserir Pedido");
        Console.WriteLine("Digite o código do Pedido");
        var codPedido = Convert.ToInt32(args[2]);
        var empregadoID = Convert.ToInt32(args[3]);
        string dataPedido = args[4];
        string peso = args[5];
        var codTransportadora = Convert.ToInt32(args[6]);
        var pedidoClienteID = Convert.ToInt32(args[7]);
        var pedido = new Pedido(pedidoId, empregadoID, dataPedido, peso, codTransportadora, pedidoClienteID);
        pedidoRepository.Save(pedido);
    }

    if(modelAction == "Apresentar")
    {
        Console.WriteLine("Apresentar Pedido");
	    Console.Write("Digite o id do pedido : ");
        var pedidoid = Convert.ToInt32(Console.ReadLine());
       
        if(pedidoRepository.ExitsById(pedidoid))
        {
            var pedido = pedidoRepository.GetById(pedidoid);
            Console.WriteLine($"{pedido.PedidoID}, {pedido.EmpregadoID}, {pedido.DataPedido}, {pedido.Peso}, {pedido.CodTransportadora}, {pedido.PedidoClienteID}");
        } 
        else 
        {
            Console.WriteLine($"O cliente com Id {pedidoid} não existe.");
        }
    }
 
   if(modelAction == "MostrarPedidosCliente")
    {
        Console.WriteLine("Mostrar Pedidos do Cliente");
	    Console.Write("Digite o id do cliente : ");
        var clienteid = Convert.ToInt32(Console.ReadLine());
       
        if(clienteRepository.ExitsById(clienteid))
        {
          foreach (var pedido in pedidoRepository.GetAll()){
            if(pedido.PedidoClienteID == clienteid){
            Console.WriteLine($"{pedido.PedidoID}, {pedido.EmpregadoID}, {pedido.DataPedido}, {pedido.Peso}, {pedido.CodTransportadora}, {pedido.PedidoClienteID}");
            }
          }
        } 
        else 
        {
            Console.WriteLine($"O cliente com Id {clienteid} não existe.");
        }
    }
}

if(modelName == "Produto")
{
    if(modelAction == "Listar")
    {

        Console.WriteLine("Listar Produto");
        Console.WriteLine("Código Produto   Descrição   Valor Unitário");
        foreach (var pedido in produtoRepository.GetAll())
        {
             Console.WriteLine($"{produto.CodProduto, -12} {produto.Descricao, -14} {produto.ValorUnitario, -21}");
        }
    }

    if(modelAction == "Inserir")
    {
        Console.WriteLine("Inserir Produto!");
        Console.WriteLine("Digite o Código do Produto         : ");
        var codProduto = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("Digite a Descrição do Produto      : ");
        string descricao = Console.ReadLine();
        Console.WriteLine("Digite o Valor Únitario do Produto : ");
        var valorUnitario = Convert.ToInt32(Console.ReadLine());
        var produto = new Produto(codProduto, descricao, valorUnitario);
        produtoRepository.Save(produto);
    }

    if(modelAction == "Apresentar")
    {
        Console.WriteLine("Apresentar Pedido");
	    Console.Write("Digite o id do pedido : ");
        var pedidoid = Convert.ToInt32(Console.ReadLine());
       
        if(pedidoRepository.ExitsById(pedidoid))
        {
            var pedido = pedidoRepository.GetById(pedidoid);
            Console.WriteLine($"{pedido.PedidoID}, {pedido.EmpregadoID}, {pedido.DataPedido}, {pedido.Peso}, {pedido.CodTransportadora}, {pedido.PedidoClienteID}");
        } 
        else 
        {
            Console.WriteLine($"O cliente com Id {pedidoid} não existe.");
        }
    }
} 

if(modelName == "ItensPedido")
{
    if(modelAction == "Listar")
    {

        Console.WriteLine("Pedido Listado!");
         Console.WriteLine("Nro Pedido   Id Empregado   Data do Pedido        Peso        Codigo da Transportadora   Id do Cliente");
        foreach (var pedido in pedidoRepository.GetAll())
        {
             Console.WriteLine($"{pedido.PedidoID, -12} {pedido.EmpregadoID, -14} {pedido.DataPedido, -21} {pedido.Peso, -9} {pedido.CodTransportadora, -28} {pedido.PedidoClienteID}");
        }
    }

    if(modelAction == "Inserir")
    {
        Console.WriteLine("Pedido Inserido!");
        var pedidoId = Convert.ToInt32(args[2]);
        var empregadoID = Convert.ToInt32(args[3]);
        string dataPedido = args[4];
        string peso = args[5];
        var codTransportadora = Convert.ToInt32(args[6]);
        var pedidoClienteID = Convert.ToInt32(args[7]);
        var pedido = new Pedido(pedidoId, empregadoID, dataPedido, peso, codTransportadora, pedidoClienteID);
        pedidoRepository.Save(pedido);
    }

    if(modelAction == "Apresentar")
    {
        Console.WriteLine("Apresentar Pedido");
	    Console.Write("Digite o id do pedido : ");
        var pedidoid = Convert.ToInt32(Console.ReadLine());
       
        if(pedidoRepository.ExitsById(pedidoid))
        {
            var pedido = pedidoRepository.GetById(pedidoid);
            Console.WriteLine($"{pedido.PedidoID}, {pedido.EmpregadoID}, {pedido.DataPedido}, {pedido.Peso}, {pedido.CodTransportadora}, {pedido.PedidoClienteID}");
        } 
        else 
        {
            Console.WriteLine($"O cliente com Id {pedidoid} não existe.");
        }
    }
} 

if(modelName == "Vendedor")
{
    if(modelAction == "Listar")
    {

        Console.WriteLine("Pedido Listado!");
         Console.WriteLine("Nro Pedido   Id Empregado   Data do Pedido        Peso        Codigo da Transportadora   Id do Cliente");
        foreach (var pedido in pedidoRepository.GetAll())
        {
             Console.WriteLine($"{pedido.PedidoID, -12} {pedido.EmpregadoID, -14} {pedido.DataPedido, -21} {pedido.Peso, -9} {pedido.CodTransportadora, -28} {pedido.PedidoClienteID}");
        }
    }

    if(modelAction == "Inserir")
    {
        Console.WriteLine("Pedido Inserido!");
        var pedidoId = Convert.ToInt32(args[2]);
        var empregadoID = Convert.ToInt32(args[3]);
        string dataPedido = args[4];
        string peso = args[5];
        var codTransportadora = Convert.ToInt32(args[6]);
        var pedidoClienteID = Convert.ToInt32(args[7]);
        var pedido = new Pedido(pedidoId, empregadoID, dataPedido, peso, codTransportadora, pedidoClienteID);
        pedidoRepository.Save(pedido);
    }

    if(modelAction == "Apresentar")
    {
        Console.WriteLine("Apresentar Pedido");
	    Console.Write("Digite o id do pedido : ");
        var pedidoid = Convert.ToInt32(Console.ReadLine());
       
        if(pedidoRepository.ExitsById(pedidoid))
        {
            var pedido = pedidoRepository.GetById(pedidoid);
            Console.WriteLine($"{pedido.PedidoID}, {pedido.EmpregadoID}, {pedido.DataPedido}, {pedido.Peso}, {pedido.CodTransportadora}, {pedido.PedidoClienteID}");
        } 
        else 
        {
            Console.WriteLine($"O cliente com Id {pedidoid} não existe.");
        }
    }
} 
