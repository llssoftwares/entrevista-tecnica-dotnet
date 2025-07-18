***Desafio implementado usando as tecnologias ASP.NET Core 9, C#, MongoDB e Docker Compose, baseado no exemplo original em Node.js, TypeScript, Express e MongoDB.***

# Desafio - API Menu

Seu objetivo é desenvolver um serviço para gestão de menus de um site corporativo. O menu compõe-se apenas de itens e sub-itens. A ideia é que dentro de cada item do menu, é possível cadastrar infinitos sub-itens.

## Requisitos funcionais

- O menu precisa possuir aninhamento infinito.  
- O Cadastro deve ser por item; cada item é independente. Isto quer dizer que cada item deve ser registrado como um registro único no banco de dados.

## Exemplo

**Menu**

---

* Eletrodomésticos

  * Televisores

    * LCD

      * 110
      * 220
    * Plasma
* Informática

  * Computadores

    * Apple

      * MacBook

        * Cabos
      * iMac

---

**Representação JSON**
```json
[
  {
    "id": "1",
    "name": "Eletrodomésticos",
    "submenus": [
      {
        "id": "2",
        "name": "Televisores",
        "submenus": [
          {
            "id": "3",
            "name": "LCD",
            "submenus": [
              {
                "id": "4",
                "name": "110"
              },
              {
                "id": "5",
                "name": "220"
              }
            ]
          },
          {
            "id": "6",
            "name": "Plasma"
          }
        ]
      }
    ]
  },
  {
    "id": "7",
    "name": "Informática",
    "submenus": [
      {
        "id": "8",
        "name": "Computadores",
        "submenus": [
          {
            "id": "9",
            "name": "Apple",
            "submenus": [
              {
                "id": "10",
                "name": "MacBook",
                "submenus": [
                  {
                    "id": "11",
                    "name": "Cabos"
                  }
                ]
              },
              {
                "id": "12",
                "name": "iMac"
              }
            ]
          }
        ]
      }
    ]
  }
]
````

## A API

A API deverá ser desenvolvida em cima da arquitetura apresentada no *boiler-plate*, seguindo o protocolo HTTP implementado nas tecnologias Nodejs com TypeScript, Express e MongoDB.

## Endpoints

Você desenvolverá 3 endpoints:

### 1. Criar item

**POST** `/api/v1/menu`

* Retorna HTTP status `201`
* Corpo da requisição deve receber dois campos:

  * `name` (String, único, not-null)
  * `relatedId` (Number, opcional. Caso este item seja parte do sub-menu de outro item, deve-se passar aqui o id do item pai; se estiver cadastrando Televisores, deve-se passar o id de Eletrodomésticos ("1"))
* Corpo da resposta deve ter um campo:

  * `id`: (String, Id do item criado)

### 2. Excluir item

**DELETE** `/api/v1/menu/{id}`

* Retorna HTTP status `200`

### 3. Consultar menu

**GET** `/api/v1/menu`

* Retorna HTTP status `200`
* Deve retornar o menu completo cadastrado no banco, seguindo o formato
  JSON do exemplo acima.
