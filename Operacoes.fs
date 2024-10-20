module Operacoes

open Dominio
open System

let mutable private livros = Map.empty<int, Livro>
let mutable private usuarios = Map.empty<int, Usuario>
let mutable private emprestimos = Map.empty<int, Emprestimo>
let mutable private proximoIdLivro = 1
let mutable private proximoIdUsuario = 1

let private obterDataAtual() = DateTime.Now
let private calcularDataDevolucao() = obterDataAtual().AddDays(14.)

let criarLivro titulo autor =
    let livro = {
        Id = proximoIdLivro
        Titulo = titulo
        Autor = autor
        Status = Disponivel
    }
    livros <- livros.Add(proximoIdLivro, livro)
    proximoIdLivro <- proximoIdLivro + 1
    livro

let buscarLivroPorId id =
    livros.TryFind id

let buscarLivroPorTitulo (titulo: string) =
    livros.Values
    |> Seq.tryFind (fun livro -> 
        livro.Titulo.ToLower().Contains(titulo.ToLower()))

let atualizarStatusLivro livroId novoStatus =
    match buscarLivroPorId livroId with
    | Some livro ->
        let livroAtualizado = { livro with Status = novoStatus }
        livros <- livros.Add(livroId, livroAtualizado)
        Some livroAtualizado
    | None -> None

let criarUsuario nome =
    let usuario = {
        Id = proximoIdUsuario
        Nome = nome
        LivrosEmprestados = 0
    }
    usuarios <- usuarios.Add(proximoIdUsuario, usuario)
    proximoIdUsuario <- proximoIdUsuario + 1
    usuario

let buscarUsuarioPorId id =
    usuarios.TryFind id

let atualizarContadorEmprestimos usuarioId delta =
    match buscarUsuarioPorId usuarioId with
    | Some usuario ->
        let usuarioAtualizado = { usuario with LivrosEmprestados = usuario.LivrosEmprestados + delta }
        usuarios <- usuarios.Add(usuarioId, usuarioAtualizado)
        Some usuarioAtualizado
    | None -> None

let emprestarLivro livroId usuarioId =
    match (buscarLivroPorId livroId, buscarUsuarioPorId usuarioId) with
    | (Some livro, Some usuario) when livro.Status = Disponivel && usuario.LivrosEmprestados < 3 ->
        let emprestimo = {
            LivroId = livroId
            UsuarioId = usuarioId
            DataEmprestimo = obterDataAtual()
            DataDevolucao = calcularDataDevolucao()
        }
        emprestimos <- emprestimos.Add(livroId, emprestimo)
        let _ = atualizarStatusLivro livroId Emprestado
        let _ = atualizarContadorEmprestimos usuarioId 1
        Ok emprestimo
    | (Some _, Some usuario) when usuario.LivrosEmprestados >= 3 ->
        Error "Usuário já possui 3 livros emprestados"
    | (Some livro, _) when livro.Status <> Disponivel ->
        Error "Livro não está disponível"
    | (None, _) ->
        Error "Livro não encontrado"
    | (_, None) ->
        Error "Usuário não encontrado"
    | _ ->
        Error "Erro desconhecido no empréstimo"

let devolverLivro livroId =
    match buscarLivroPorId livroId with
    | Some livro when livro.Status = Emprestado ->
        match emprestimos.TryFind livroId with
        | Some emprestimo ->
            emprestimos <- emprestimos.Remove livroId
            let _ = atualizarStatusLivro livroId Disponivel
            let _ = atualizarContadorEmprestimos emprestimo.UsuarioId -1
            Ok "Livro devolvido com sucesso"
        | None -> Error "Empréstimo não encontrado"
    | Some _ -> Error "Livro não está emprestado"
    | None -> Error "Livro não encontrado"

let listarLivrosPorStatus status =
    livros.Values
    |> Seq.filter (fun livro -> livro.Status = status)
    |> Seq.toList

let listarTodosLivros() =
    livros.Values |> Seq.toList

let obterEmprestimosAtivosPorUsuario usuarioId =
    emprestimos.Values
    |> Seq.filter (fun emprestimo -> emprestimo.UsuarioId = usuarioId)
    |> Seq.toList

let obterUsuarios() =
 usuarios.Values |> Seq.toList

let obterLivros() =
 livros.Values |> Seq.toList