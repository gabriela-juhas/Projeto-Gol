#language: pt-br

Funcionalidade: Busca de Passagem

Cenario: Realizar a busca de uma Passagem com menor valor
	Dado que eu realize o acesso ao site da Gol
	  E informo o valor de partida como 'SDU'
	  E informo o valor de destino como 'GRU'
	  E informo que serão '2' passageiros
	  E seleciono a data de ida como amanha
	  E seleciono o retorno para daqui '2' meses
	  E realizo a busca das passagens
	Quando eu filtro a passagem de ida pela mais barata
	  E filtro a passagem de volta pela mais barata
	Então devo selecionar as passagens mais baratas
