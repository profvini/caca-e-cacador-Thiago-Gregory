## Introdução
Esse projeto foi desenvolvido como trabalho final da disciplina de Inteligência Artificial (IA), com o principal objetivo de exercitar sobre a modelagem e implementação de agentes inteligentes e competitivos utilizando máquinas de estado. O resultado é um protótipo de uma simulação de um jogo 2D com a temática Caça e Caçador.


## Funcionamento Base e Exibição
O jogo possui um ambiente em formato de grade de células de 30 x 30. Cada célula inicia vazia, e em um primeiro momento, o Caçador e de 5 a 10 Caças são posicionadas aleatoriamente dentro desse ambiente. Cada célula poderá estar vazia, conter o Caçador ou conter uma única Caça.
A tela de exibição possui os seguintes elementos:
  - **Marcador de Caças mortas no topo da tela:** No topo da tela serão exibidas todas as caças contidas no ambiente, conforme determinado anteriormente, com uma representação de uma Caça viva. Conforme as Caças do ambiente forem sendo morta, o ícone de cada marcador mudará para uma representação da Caça morta. Isso indica quantas caças estão no ambiente e quantas estão vivas ou mortas.
  - **Ambiente da Simulação:** Logo abaixo, e ocupando a maior parte da tela, é mostrado o ambiente da simulação, bem como o Caçador e as Caças já posicionadas. Aqui é onde o resultado da simulação será representado em tempo real. Após a simulação ser finalizada, e todas as Caças serem mortas, uma tela será exibida em cima do ambiente  informando que a simulação foi finalizada e quantos movimentos foram necessários para o Caçador matar todas as Caças.
  - **Contador de Movimentos e Botões**: Na parte inferior da tela, à esquerda, há um texto contabilizando os movimentos do Caçador alterado após cada movimento realizado, à direita, dois botões têm como função executar a simulação. Um botão ativa execução em ciclo, e outro realiza apenas um movimento.


## Como a Simulação Funciona
Após o Caçador e as Caças serem devidamente posicionados, e um dos botões serem pressionados, a simulação será iniciada.
Cada movimento da simulação é dividido em três etapas realizadas na seguinte ordem: Checagem se alguma caça está no campo de visão do Caçador, Detecção de uma Caça adjacente ao Caçador e movimentação do Caçador e das Caças.
**1ª Etapa:** essa etapa varre uma área de tamanho 5 para cada direção a partir do Caçador (seu campo de visão). Se alguma Caça for identificada nessa área, ela passará a ser o alvo principal do Caçador, que irá persegui-la até matá-la ou a caça sair do campo de visão do Caçador. A caça, por sua vez, estará com estado de 'fugindo' ativado, procurando se distancias do Caçador ao máximo.
Essa etapa é executada antes da movimentação do Caçador e das Caças, pois ambos já foram posicionados anteriormente.
**2ª Etapa**: nessa etapa, será analisado se o Caçador está próximo o suficiente de uma Caça para matá-la. Uma área de 3x3 ao redor do Caçador é analisada para tentar encontrar alguma Caça. Se uma Caça for encontrada nessa área, ela será morta pelo Caçador. A Caça morta será removida da simulação e o contador de Caças mortas será incrementado em 1.
**3ª Etapa**: Nessa etapa, a movimentação de todos os Agentes será executada.
Se o Caçador não estiver com o estado de 'caçando' ativo, ele se moverá para uma direção aleatória, com uma pequena possibilidade de se mover para uma direção mais próxima do centro do ambiente. Se o estado 'caçando' estiver ativo, o Caçador irá preferir se mover para a direção que deixará ele mais perto da Caça que ele estiver caçando.
Se a Caça não estiver com estado de 'fugindo' ativo, ela se moverá para uma direção aleatória, uma possibilidade relativa de preferir uma direção que a deixe mais longe do Caçador conforme quão próximo ela estiver do Caçador:
  - Se a Caça estiver no campo de visão do Caçador, ela sempre escolherá uma posição mais distante do Caçador;
  - Se a Caça estiver próxima do Caçador, mas não em seu campo de visão, ela terá uma chance de 10% de preferir uma posição mais distante do Caçador;
  - Se a Caça estiver mais distante, porém não muito longe, do Caçador, essa chance é de 2.5%
  - Caso nenhum critério anterior for atendido, a direção escolhida será aleatória.
Tanto para o Caçador, quanto para cada Caça, essa etapa ficará sendo escuta enquanto a próxima posição prevista para qualquer agente esteja fora do ambiente*
