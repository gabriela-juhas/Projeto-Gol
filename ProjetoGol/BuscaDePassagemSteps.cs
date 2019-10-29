using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using TechTalk.SpecFlow;
using OpenQA.Selenium.Support.UI;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ProjetoGol
{
    [Binding]
    public class BuscaDePassagemSteps
    {
        IWebDriver Browser;
        private string uri = "https://www.voegol.com.br/pt";

        [BeforeScenario]
        public void Init()
        {
            this.Browser = new ChromeDriver();
        }

        [AfterScenario]
        public void Close()
        {
            this.Browser.Close();
            this.Browser.Dispose();
        }

        [Given(@"que eu realize o acesso ao site da Gol")]
        public void DadoQueEuRealizeOAcessoAoSiteDaGol()
        {
            this.Browser.Navigate().GoToUrl(uri);
        }
        
        [Given(@"informo o valor de partida como '(.*)'")]
        public void DadoInformoOValoreDePartidaComo(string partida)
        {
            var partida_campo = this.Browser.FindElement(By.ClassName("chosen-placeholder-origin"));
            partida_campo.SendKeys(partida);
            partida_campo.SendKeys(Keys.Enter);
        }
        
        [Given(@"informo o valor de destino como '(.*)'")]
        public void DadoInformoOValorDeChegadaComo(string destino)
        {
            var destino_campo = this.Browser.FindElement(By.ClassName("chosen-placeholder-destiny"));
            destino_campo.SendKeys(destino);
            destino_campo.SendKeys(Keys.Enter);
        }
        
        [Given(@"informo que serão '(.*)' passageiros")]
        public void DadoInformoQueSeraoPassageiros(string qtd_adultos)
        {
            var qtd_adultos_campo = this.Browser.FindElement(By.Id("number-adults"));
            qtd_adultos_campo.SendKeys(qtd_adultos);
        }
        
        [Given(@"seleciono a data de ida como amanha")]
        public void DadoSelecionoADataDeIdaComoAmanha()
        {
            var data_ida_campo = this.Browser.FindElement(By.Id("datepickerGo"));
            var data = DateTime.Today.Day;
            data = data + 1;
            data_ida_campo.SendKeys(data.ToString());
        }
        
        [Given(@"seleciono o retorno para daqui '(.*)' meses")]
        public void DadoSelecionoORetornoParaDaquiMeses(int data_retorno)
        {
            var data = DateTime.Today.Day;
            var data_retorno_campo = this.Browser.FindElement(By.Id("datepickerBack"));
            var proximo_mes_botao = this.Browser.FindElement(By.ClassName("ui-icon ui-icon-circle-triangle-e"));
            var dia_selecionado = this.Browser.FindElement(By.LinkText(data.ToString()));

            data_retorno_campo.Click();
            for (int i = 0; i < data_retorno; i++)
            {
                proximo_mes_botao.Click();
            }
            dia_selecionado.Click();
        }
        
        [Given(@"realizo a busca das passagens")]
        public void DadoRealizoABuscaDasPassagens()
        {
            var comprar_botao = this.Browser.FindElement(By.Id("btn-box-buy"));
            comprar_botao.Click();
        }
        
        [When(@"eu filtro a passagem de ida pela mais barata")]
        public void QuandoEuFiltroAPassagemDeIdaPelaMaisBarata()
        {
            var filtro_ida = this.Browser.FindElement(By.ClassName("flightFiltersClosedButton"));
            var filtro_preco_ida = this.Browser.FindElement(By.Id("selectPrices1"));
            var filtro_preco_ida_select = new SelectElement(filtro_preco_ida);
            var filtro_ida_confirmar = this.Browser.FindElement(By.ClassName("flightFiltersOptionsConfirmButton"));
            var selecionar_voo_ida = this.Browser.FindElement(By.ClassName("taxa taxaPromocional"));

            filtro_ida.Click();
            filtro_preco_ida_select.SelectByIndex(1);
            filtro_ida_confirmar.Click();
            selecionar_voo_ida.Click();
        }
        
        [When(@"filtro a passagem de volta pela mais barata")]
        public void QuandoFiltroAPassagemDeVoltaPelaMaisBarata()
        {
            var filtro_volta = this.Browser.FindElement(By.ClassName("flightFiltersClosedButton"));
            var filtro_preco_volta = this.Browser.FindElement(By.Id("selectPrices2"));
            var filtro_preco_volta_select = new SelectElement(filtro_preco_volta);
            var filtro_volta_confirmar = this.Browser.FindElement(By.ClassName("flightFiltersOptionsConfirmButton"));
            var selecionar_voo_volta = this.Browser.FindElement(By.ClassName("taxa taxaPromocional"));

            filtro_volta.Click();
            filtro_preco_volta_select.SelectByIndex(1);
            filtro_volta_confirmar.Click();
            selecionar_voo_volta.Click();
        }
        
        [Then(@"devo selecionar as passagens mais baratas")]
        public void EntaoDevoSelecionarAsPassagensMaisBaratas()
        {
            try
            {
                var resumo_ida = this.Browser.FindElement(By.Id("idaFly"));
                var resumo_volta = this.Browser.FindElement(By.Id("voltaFly"));
            }
            catch(NoSuchElementException)
            {
                Assert.Fail();
            }
        }
    }
}
