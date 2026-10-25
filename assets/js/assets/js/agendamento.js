// assets/js/agendamento.js

console.log('agendamento.js carregado');

// 1. Seleção dos elementos do formulário e avisos
const form = document.querySelector('#form-agendamento');
const nome = document.querySelector('#nome');
const cpf = document.querySelector('#cpf');
const avisoCpf = document.querySelector('#aviso-cpf');

const especialidade = document.querySelector('#especialidade');
const data = document.querySelector('#data');
const avisoData = document.querySelector('#aviso-data');

const horario = document.querySelector('#horario');
const avisoHorario = document.querySelector('#aviso-horario');

const resumo = document.querySelector('#resumo-agendamento');

// 2. Função de validação do CPF (11 dígitos)
function validarCpf(campo, aviso) {
  const digitos = campo.value.replace(/\D/g, ''); // descarta ponto e traço
  const valido = digitos.length === 11;
  aviso.textContent = valido ? '' : 'O CPF precisa ter 11 dígitos.';
  campo.classList.toggle('campo-invalido', !valido);
  return valido;
}

// 3. Função de validação da Data (apenas datas futuras)
function validarData(campo, aviso) {
  const hoje = new Date();
  hoje.setHours(0, 0, 0, 0);
  const escolhida = new Date(`${campo.value}T00:00:00`); // evita fuso UTC
  const valida = escolhida > hoje;
  aviso.textContent = valida ? '' : 'A consulta precisa ser em uma data futura.';
  return valida;
}

// 4. Função de validação do Horário (atendimento das 07h às 19h)
function validarHorario(campo, aviso) {
  const hora = Number(campo.value.split(':')[0]);
  const valida = hora >= 7 && hora < 19;
  aviso.textContent = valida ? '' : 'A Clínica Vida+ atende das 07h às 19h.';
  return valida;
}

// 5. Interceptação do envio do formulário e execução de todas as validações
form.addEventListener('submit', (event) => {
  event.preventDefault(); // Impede o recarregamento da página
  console.log('submit interceptado, a página não recarregou');

  // Executa todas as regras para exibir os erros todos de uma vez
  const resultados = [
    validarCpf(cpf, avisoCpf),
    validarData(data, avisoData),
    validarHorario(horario, avisoHorario),
  ];

  // Se alguma validação falhar (contiver false), interrompe o envio
  if (resultados.includes(false)) return;

  // Exibe a mensagem de confirmação
  resumo.textContent = `${nome.value} agendou ${especialidade.value} em ${data.value} às ${horario.value}.`;
  resumo.classList.remove('oculto');
});
const busca = document.querySelector('#busca-especialidade');
const cards = document.querySelectorAll('.lista-especialidades li');

busca.addEventListener('input', () => {
  const termo = busca.value.trim().toLowerCase();
  cards.forEach((card) => {
    const nome = card.textContent.toLowerCase();
    card.classList.toggle('oculto', !nome.includes(termo));
  });
});