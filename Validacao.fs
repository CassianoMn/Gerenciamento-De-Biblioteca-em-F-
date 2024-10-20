module Validacao

open Dominio
open System

let validarLivro titulo autor =
    String.IsNullOrWhiteSpace(titulo) |> not && 
    String.IsNullOrWhiteSpace(autor) |> not

let validarUsuario nome =
    String.IsNullOrWhiteSpace(nome) |> not