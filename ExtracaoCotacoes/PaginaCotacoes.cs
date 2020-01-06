using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace ExtracaoCotacoes
{
    public class PaginaCotacoes
    {
        private SeleniumConfigurations _configurations;
        private IWebDriver _driver;

        public PaginaCotacoes(SeleniumConfigurations seleniumConfigurations)
        {
            _configurations = seleniumConfigurations;

            ChromeOptions optionsFF = new ChromeOptions();
            optionsFF.AddArgument("--headless");

            _driver = new ChromeDriver(
                _configurations.CaminhoDriverChrome
                , optionsFF);
        }

        public void CarregarPagina()
        {
            _driver.Manage().Timeouts().PageLoad =
                TimeSpan.FromSeconds(60);
            _driver.Navigate().GoToUrl(
                _configurations.UrlPaginaCotacoes);
        }

        public List<CotacaoEntity> ObterCotacoes()
        {
            List<CotacaoEntity> cotacoes = new List<CotacaoEntity>();

            var rowsCotacoes = _driver
                .FindElement(By.ClassName("container"))
                .FindElement(By.Id("tableCotacoes"))
                .FindElement(By.TagName("tbody"))
                .FindElements(By.TagName("tr"));
            foreach (var rowCotacao in rowsCotacoes)
            {   
                var dadosCotacao = rowCotacao.FindElements(
                    By.TagName("td"));

                CotacaoEntity cotacao = new CotacaoEntity(
                    dadosCotacao[0].Text,
                    DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                cotacao.NomeMoeda = dadosCotacao[1].Text;
                cotacao.Variacao = dadosCotacao[2].Text;
                cotacao.ValorReais = Convert.ToDouble(
                    dadosCotacao[3].Text);

                cotacoes.Add(cotacao);
            }

            return cotacoes;
        }

        public void Fechar()
        {
            _driver.Quit();
            _driver = null;
        }
    }
}