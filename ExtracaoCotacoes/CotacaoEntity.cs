using Microsoft.Azure.Cosmos.Table;

namespace ExtracaoCotacoes
{
    public class CotacaoEntity : TableEntity
    {
        public CotacaoEntity(string codigo, string horario)
        {
            PartitionKey = codigo;
            RowKey = horario;
        }

        public CotacaoEntity() { }

        public string NomeMoeda { get; set; }
        public double ValorReais { get; set; }
        public string Variacao { get; set; }
    }
}