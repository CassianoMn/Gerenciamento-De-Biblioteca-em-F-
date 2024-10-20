module Dominio

type StatusLivro =
    | Disponivel
    | Emprestado
    | EmManutencao

type Livro = {
    Id: int
    Titulo: string
    Autor: string
    Status: StatusLivro
}

type Usuario = {
    Id: int
    Nome: string
    LivrosEmprestados: int
}

type Emprestimo = {
    LivroId: int
    UsuarioId: int
    DataEmprestimo: System.DateTime
    DataDevolucao: System.DateTime
}