namespace MinhaApiComSQLite.Models
{
    public class Produto
    {
        //percebi que a descricao nao estava sendo utilizada, resolvi apagar. atualizei a migration.
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public decimal Preco { get; set; }
    }
}
