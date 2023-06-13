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

            Console.WriteLine("Listar Pedido");
            Console.WriteLine("Código Pedido  Prazo Entrega    Data Pedido        Código Cliente        Código Vendedor");
            foreach (var pedido in pedidosRepository.Listar())
            {
                Console.WriteLine($"{pedido.CodPedido, -12} {pedido.PrazoEntrega, -14} {pedido.DataPedido, -21} {pedido.PedidoCodCliente, -9} {pedido.PedidoCodVendedor, -28}");
            }
        }

        if(modelAction == "Inserir")
        {
            Console.WriteLine("Inserir Pedido");
            Console.WriteLine("Digite o código do pedido");
            var codPedido = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Digite o prazo de entrega do pedido");
            var prazoEntrega = Convert.ToDateTime(Console.ReadLine());
            Console.WriteLine("Digite a data de entrega do pedido");
            var dataPedido = Convert.ToDateTime(Console.ReadLine());
            Console.WriteLine("Digite o código do cliente do pedido");
            var pedidoCodCliente = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Digite o código do vendedor do pedido");
            var pedidoCodVendedor = Convert.ToInt32(Console.ReadLine());
            var pedido = new Pedidos(codPedido, prazoEntrega, dataPedido, pedidoCodCliente, pedidoCodVendedor);
            pedidosRepository.Inserir(pedido);
        }

        if(modelAction == "Apresentar")
        {
            Console.WriteLine("Apresentar Pedido");
            Console.Write("Digite o id do pedido : ");
            var codPedido = Convert.ToInt32(Console.ReadLine());
        
            if(pedidosRepository.Apresentar(codPedido))
            {
                var pedido = pedidosRepository.GetById(codPedido);
                Console.WriteLine($"{pedido.CodPedido}, {pedido.PrazoEntrega}, {pedido.DataPedido}, {pedido.PedidoCodCliente}, {pedido.PedidoCodVendedor}");
            } 
            else 
            {
                Console.WriteLine($"O cliente com o código {codPedido} não existe.");
            }
        }
    
    if(modelAction == "MostrarPedidosCliente")
        {
            Console.WriteLine("Mostrar Pedidos do Cliente");
            Console.Write("Digite o código do cliente : ");
            var codCliente = Convert.ToInt32(Console.ReadLine());
        
            if(clientesRepository.Apresentar(codCliente))
            {
            foreach (var pedido in pedidosRepository.Listar()){
                if(pedido.PedidoCodCliente == codCliente){
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

    if(modelName == "Produto")
    {
        if(modelAction == "Listar")
        {

            Console.WriteLine("Listar Produto");
            Console.WriteLine("Código Produto   Descrição   Valor Unitário");
            foreach (var produto in produtosRepository.Listar())
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
            var produto = new Produtos(codProduto, descricao, valorUnitario);
            produtosRepository.Inserir(produto);
        }

        if(modelAction == "Apresentar")
        {
            Console.WriteLine("Apresentar Produto");
            Console.Write("Digite o código do produto : ");
            var codProduto = Convert.ToInt32(Console.ReadLine());
        
            if(produtosRepository.Apresentar(codProduto))
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
