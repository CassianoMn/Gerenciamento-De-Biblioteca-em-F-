module Program

open System
open Dominio
open Operacoes

let menu () =
    printfn "\nMenu de Gerenciamento de Biblioteca:"
    printfn "1. Adicionar Livro"
    printfn "2. Listar Todos os Livros"
    printfn "3. Buscar Livro por Título"
    printfn "4. Adicionar Usuário"
    printfn "5. Emprestar Livro"
    printfn "6. Devolver Livro"
    printfn "7. Listar Livros Disponíveis"
    printfn "8. Sair"

let obterInteiro (mensagem: string) =
    try
        printf "%s" mensagem
        let entrada = Console.ReadLine()
        let valor = Int32.Parse(entrada)
        Some valor
    with
    | :? FormatException ->
        printfn "Erro: Entrada inválida. Digite um número inteiro válido."
        None
    | ex ->
        printfn "Erro inesperado: %s" ex.Message
        None

let obterString (mensagem: string) =
    printf "%s" mensagem
    Console.ReadLine()

let mostrarUsuariosDisponiveis () =
    let usuarios = obterUsuarios()
    if List.isEmpty usuarios then
        printfn "Nenhum usuário disponível."
    else
        printfn "\nUsuários Disponíveis:"
        usuarios 
        |> List.iter (fun usuario -> printfn "ID: %d - %s" usuario.Id usuario.Nome)

let mostrarLivrosDisponiveis () =
    let livros = listarLivrosPorStatus Disponivel
    if List.isEmpty livros then
        printfn "Nenhum livro disponível para empréstimo."
    else
        printfn "\nLivros Disponíveis para Empréstimo:"
        livros
        |> List.iter (fun livro -> printfn "ID: %d - %s por %s" livro.Id livro.Titulo livro.Autor)

let rec executarOpcao () =
    menu ()
    printf "\nEscolha uma opção: "
    let opcao = Console.ReadLine()

    match opcao with
    | "1" ->
        let titulo = obterString "Título do Livro: "
        let autor = obterString "Autor do Livro: "
        if Validacao.validarLivro titulo autor then
            let livro = criarLivro titulo autor
            printfn "Livro adicionado com sucesso: %s por %s" livro.Titulo livro.Autor
        else
            printfn "Erro: Dados inválidos! Não foi possível adicionar o livro."
        executarOpcao ()

    | "2" ->
        printfn "\nLista de Todos os Livros:"
        listarTodosLivros ()
        |> List.iter (fun livro -> printfn "ID: %d - %s por %s [%A]" livro.Id livro.Titulo livro.Autor livro.Status)
        executarOpcao ()

    | "3" ->
        let titulo = obterString "Digite o título do livro a ser buscado: "
        match buscarLivroPorTitulo titulo with
        | Some livro -> printfn "Livro encontrado: %s por %s [%A]" livro.Titulo livro.Autor livro.Status
        | None -> printfn "Erro: Livro não encontrado."
        executarOpcao ()

    | "4" ->
        let nome = obterString "Nome do Usuário: "
        if Validacao.validarUsuario nome then
            let usuario = criarUsuario nome
            printfn "Usuário adicionado com sucesso: %s (ID: %d)" usuario.Nome usuario.Id
        else
            printfn "Erro: Nome inválido! Não foi possível adicionar o usuário."
        executarOpcao ()

    | "5" ->
        if listarLivrosPorStatus Disponivel |> List.isEmpty then
            printfn "Nenhum livro disponível para empréstimo."
        elif obterUsuarios() |> List.isEmpty then
            printfn "Nenhum usuário cadastrado."
        else
            mostrarLivrosDisponiveis()
            mostrarUsuariosDisponiveis()
            match obterInteiro "ID do Livro: " with
            | Some livroId ->
                match obterInteiro "ID do Usuário: " with
                | Some usuarioId ->
                    match emprestarLivro livroId usuarioId with
                    | Ok emprestimo -> printfn "Empréstimo realizado com sucesso! Devolução prevista para %A" emprestimo.DataDevolucao
                    | Error msg -> printfn "Erro: %s" msg
                | None -> printfn "Erro: Usuário não encontrado."
            | None -> printfn "Erro: Livro não encontrado."
        executarOpcao ()

    | "6" ->
        if listarLivrosPorStatus Emprestado |> List.isEmpty then
            printfn "Nenhum livro está emprestado no momento."
        else
            printfn "\nLivros Emprestados:"
            listarLivrosPorStatus Emprestado
            |> List.iter (fun livro -> printfn "ID: %d - %s por %s" livro.Id livro.Titulo livro.Autor)
            match obterInteiro "ID do Livro para devolução: " with
            | Some livroId ->
                match devolverLivro livroId with
                | Ok msg -> printfn "%s" msg
                | Error msg -> printfn "Erro: %s" msg
            | None -> printfn "Erro: ID inválido."
        executarOpcao ()

    | "7" ->
        mostrarLivrosDisponiveis ()
        executarOpcao ()

    | "8" ->
        printfn "Saindo do programa..."

    | _ ->
        printfn "Erro: Opção inválida! Tente novamente."
        executarOpcao ()

[<EntryPoint>]
let main argv =
    executarOpcao ()
    0
