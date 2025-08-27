# DesafioM


A BikeRental API é uma aplicação ASP.NET Core para gerenciamento de aluguel de motocicletas. Ela permite cadastrar, consultar e alugar motos, além de gerenciar entregadores e planos de aluguel. O projeto está estruturado para facilitar testes automatizados e pode ser executado localmente ou via Docker.



Funcionalidades

- MotorcycleController
  - Listar todas as motos cadastradas (`GET /api/motorcycle`)
  - Buscar moto por placa (`GET /api/motorcycle/plate/{plate}`)
  - Buscar moto por identificador (`GET /api/motorcycle/{id}`)
  - Criar nova moto (`POST /api/motorcycle`)
    - Valida se `Id`, `Model` e `Plate` são obrigatórios



Courier e Rental
  - Modelos e contexto prontos para cadastro e consulta de entregadores e aluguéis (implementação futura)



Estrutura dos Códigos

- **Controllers**: Lógica de API REST (exemplo: `MotorcycleController`)
- **Models**: Estruturas de dados (exemplo: `Motorcycle`, `Courier`, `Rental`)
- **Data**: Contexto do Entity Framework (`AppDbContext`)
- **Tests**: Testes unitários com xUnit e Moq (`BikeRental.Api.Tests`)



Testes Automatizados

- Os testes estão em `BikeRental.Api.Tests\Controllers\MotorcycleControllerTest.cs`
- Testam cenários de criação, consulta e validação de motos


Como escrever um teste

1. Crie um método público com `[Fact]`
2. Instancie o controller e o contexto em memória
3. Realize a ação (ex: criar moto)
4. Use asserts para validar o resultado

Exemplo:
```csharp
[Fact]
public void CreateMotorcycle_ShouldSaveMotorcycle()
{
    var controller = new MotorcycleController(context, loggerMock.Object);
    var newMotorcycle = new Motorcycle { Id = "002", Model = "ModelB", Plate = "XYZ789" };
    var result = controller.CreateMotorcycle(newMotorcycle);
    Assert.NotNull(result as OkResult);
}
```


Como rodar os testes

No terminal, execute:
```powershell
dotnet test
```
Como iniciar a aplicação

1. Instale as dependências:
    ```powershell
    dotnet restore
    ```
2. Execute a aplicação:
    ```powershell
    dotnet run --project BikeRental.Api
    ```
3. A API estará disponível em `http://localhost:5000` (ou porta configurada)



Docker Compose

Se houver um arquivo `docker-compose.yml`, basta executar:
```powershell
docker-compose up
```
Isso irá subir a aplicação e os serviços necessários (ex: banco de dados).

Exemplos de chamadas à API (cURL)

- **Listar motos**
    ```bash
    curl -X GET http://localhost:5000/api/motorcycle
    ```

- **Buscar moto por placa**
    ```bash
    curl -X GET http://localhost:5000/api/motorcycle/plate/ABC123
    ```

- **Buscar moto por Id**
    ```bash
    curl -X GET http://localhost:5000/api/motorcycle/001
    ```

- **Criar moto**
    ```bash
    curl -X POST http://localhost:5000/api/motorcycle \
         -H "Content-Type: application/json" \
         -d '{"Id":"005","Model":"ModelX","Plate":"ZZZ999"}'
    ```



Observações

- O projeto está pronto para evoluir com novas funcionalidades (entregadores, aluguéis, planos).
- Os testes garantem que as principais regras de negócio estão sendo respeitadas.
- Devido à falta de experiência e conhecimento geral em c#, aplicações e estruturas de banco de dado, o projeto não foi entregue 100%.
