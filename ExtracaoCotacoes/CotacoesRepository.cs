using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Azure.Cosmos.Table;
using Microsoft.Extensions.Configuration;

namespace ExtracaoCotacoes
{
    public class CotacoesRepository
    {
        private CloudTable _cotacoesTable;

        public CotacoesRepository(IConfiguration configuration)
        {
            var storageAccount = CloudStorageAccount
                .Parse(configuration["BaseCotacoes"]);
            _cotacoesTable = storageAccount
                .CreateCloudTableClient().GetTableReference("Cotacoes");
            if (_cotacoesTable.CreateIfNotExistsAsync().Result)
                Console.WriteLine("Criada a Tabela de Cotações...");
        }

        public void IncluirCotacoes(
            IEnumerable<CotacaoEntity> cotacoes)
        {
            foreach (var cotacao in cotacoes)
            {
                var insertOperation = TableOperation.Insert(cotacao);
                var resultInsert = _cotacoesTable.ExecuteAsync(insertOperation).Result;

                Thread.Sleep(2000);
            }
        }
    }
}