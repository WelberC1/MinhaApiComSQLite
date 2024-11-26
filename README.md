# Minha API com SQLite

Olá, tudo bem? Essa é minha versão da API de produtos. Implementei todas as funcionalidades propostas no desafio.

# Tecnologias utilizadas

ASP.NET Core;
Entity Framework; 
SQLite;
Swagger; 
Postman.

# Funcionalidades da API

A API possui as seguintes funcionalidades:

- **GET** `/api/produtos`: Retorna todos os produtos cadastrados.
- **GET** `/api/produtos/Pesquisa?nome={nome}&tipoOrdenacao={ASC|DES}`: Pesquisa produtos por nome e permite ordenar os resultados por preço (ordem crescente ou decrescente).
- **POST** `/api/produtos`: Cria um novo produto.
- **PUT** `/api/produtos/{id}`: Atualiza um produto existente.
- **DELETE** `/api/produtos/{id}`: Remove um produto pelo ID.

# Endpoints da API:
![image](https://github.com/user-attachments/assets/a03b9352-14f3-4c8c-941f-85d5391f159b)

# Testes

Realizei alguns testes a fim de garantir a plena funcionalidade da API. O objetivo deles era garantir que de fato as validações estavam funcionando. Utilizei o postman para a realização, enviando requisições HTTP de vários tipos. 

# Muito obrigado por sua leitura.


