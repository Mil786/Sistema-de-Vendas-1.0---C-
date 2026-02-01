using System;

Console.WriteLine("--- SISTEMA DE VENDAS 1.0 ---");

//inicializei as variaveis fora do loop para que elas existam em dodo o código
string Produto = "";
double valorBruto = 0;
bool entradaValida = false;

//ESTRUTURA DE CONTROLE: O while cria um ciclo de repetição
//o loop while garante que o sitema insista até o valor ser numerico
//"!" significa não, ou seja, Enquanto a entrada não for válida...
while (!entradaValida)
{      
    try
    {
        Console.Write("Produto: ");
        Produto = Console.ReadLine()!;

        //validação: se o nome for vazio ou só espaços, lança um erro manual
        if (string.IsNullOrWhiteSpace(Produto))
        {
            throw new Exception("O nome do produto não pode ficar em branco.");
        }

        Console.Write("Valor: R$ ");
        //o double.Parse pode gerar um FormatException se o usuário digitar letras
        valorBruto = double.Parse(Console.ReadLine()!);

        //se chegou aqui sem erro, podemos sair do loop
        entradaValida = true;       
    }

    catch (FormatException)
    {
        //Tratamento específico para erros de digitação numérica
        Console.WriteLine("\n[ERRO]: O valor digitado é inválido. Use apenas números e vírgula.");
        Console.WriteLine("Tente novamente...\n");
    }
    catch (Exception ex)
    {
        // Tratamento genérico para qualquer outro erro (como o nome vazio).
        // O '\n' garante que a mensagem não fique "grudada" no texto anterior.
        Console.WriteLine($"\n[AVISO]: {ex.Message}");
        // Se o erro for algo desconhecido, o 'break' impede um loop infinito por segurança.
        if (!ex.Message.Contains("branco")) break;
    }

}

// CAMADA DE LOGICA: Chamada dos métodos após garantir que os dados estão "limpos".
double valorComDesconto = CalcularDesconto(valorBruto);
double ValorFinal = CalcularImposto(valorComDesconto);

// CAMADA DE APRESENTAÇÃO: Exibe o resultado final formatado (:F2 força duas casas decimais).
Console.WriteLine("\n--- RESUMO DA VENDA ---");
Console.WriteLine($"Produto: {Produto}");
Console.WriteLine($"Preço base: R$ {valorBruto:F2}");
Console.WriteLine($"Preço com desconto: R$ {valorComDesconto:F2}");
Console.WriteLine($"Preço final com imposto: R$ {ValorFinal:F2}");
Console.WriteLine("------------------------------------");


// --- MÉTODOS DE APOIO (BACKEND LOGIC) ---
static double CalcularDesconto (double Valor)
{
    // Regra de negócio: 10% de desconto para valores acima de R$ 100,00.
    if (Valor > 100) return Valor * 0.90;
    return Valor;  
}

static double CalcularImposto (double valor)
{
    // Regra de negócio progressiva para impostos.
    if (valor > 1000) return valor * 1.15;
    if (valor > 50) return valor * 1.05;
    return valor; 
}
