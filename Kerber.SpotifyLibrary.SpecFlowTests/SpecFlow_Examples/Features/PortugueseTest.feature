# language: pt
@Portugues
Funcionalidade: Contar uma história através de uma frase com um nome de um homem e o nome de um touro
	A fim de gerar frases com dois atores fixos
	Como autor espero ter frases que variam os nomes mas não a forma textual
	Eu quero desta forma ter uma função fácil que possa ser reusada em inúmeros casos

Cenário: Quero ver a frase completa com um nome para o homem e um para o touro
	Dado que um homem de nome "João"
	E um touro chamado "Bartô"
	Quando andando pela rua se encontram
	Então resulta na frase "Um homem chamado João se encontra com o touro Bartô"