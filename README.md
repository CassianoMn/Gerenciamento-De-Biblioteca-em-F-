Sistema de Biblioteca em F#

Sistema simples para gerenciar uma biblioteca, permitindo controle de livros e empréstimos.

Desenvolvido por:

-Cassiano Menezes
-Luiz Augusto Farias Hora
-Eduardo Vieira

==========================================================================

O que o sistema faz?

-Cadastra livros
-Cadastra usuários
-Controla empréstimos
-Lista livros disponíveis
-Busca livros por título

==========================================================================

Como instalar e usar

1. Preparar o ambiente

Instale o .NET SDK

Windows: Baixe do site oficial da Microsoft: https://dotnet.microsoft.com/download
Linux: sudo apt-get install dotnet-sdk
Mac: brew install dotnet-sdk

2. Verificar instalação
	dotnet --version
Se aparecer um número de versão (ex: 8.0.100), está tudo certo!

3. Rodar o programa

-Entre na pasta do projeto
-Abra o Terminal dentro da pasta do projeto, onde se encontram os arquivos .fs
-rode os seguintes comandos:
	dotnet build
	dotnet run

4. Usar o sistema

Depois que rodar, vai aparecer este menu:

Menu de Gerenciamento de Biblioteca:
1. Adicionar Livro
2. Listar Todos os Livros
3. Buscar Livro por Título
4. Adicionar Usuário
5. Emprestar Livro
6. Devolver Livro
7. Listar Livros Disponíveis
8. Sair

Digite o número da opção e pressione Enter.

=========================================================================

Exemplos de uso:

-Adicionar livro:
> 1
Título do Livro: Harry Potter
Autor do Livro: J.K. Rowling
> Livro adicionado com sucesso!

-Emprestar livro:
> 5
ID do Livro: 1
ID do Usuário: 1
> Empréstimo realizado com sucesso!

=========================================================================

Regras básicas

Cada usuário pode pegar até 3 livros
Prazo de devolução: 14 dias
Livros podem estar: Disponível ou Emprestado

=========================================================================

Estrutura do projeto

-Dominio: Define os tipos básicos (Livro, Usuário, etc)
-Validacao: Checa se dados estão corretos
-Operacoes: Faz as operações principais
-Program: Cuida do menu e interação com usuário

=========================================================================

Problemas comuns

-Erro: dotnet não encontrado

Reinstale o .NET SDK
Adicione ao PATH do sistema


-Erro: não compila

Verifique se está na pasta correta
Tente dotnet restore antes


-Erro: não encontra arquivos

Confira se todos arquivos .fs estão na pasta
Verifique o arquivo .fsproj

=========================================================================


Desenvolvido para estudo de F# e programação funcional, bem com para obtenção de nota para a terceira unidade da matéria de Linguagens de Programação, lecionada pelo professor ANTÔNIO JOSÉ ALVES NETO.
