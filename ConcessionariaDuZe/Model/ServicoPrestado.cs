﻿namespace ConcessionariaDuZe.Model
{
    public class ServicoPrestado
    {
        public Guid ServicoPrestadoId { get; set; }
        public string servicoNome { get; set; }
        public Guid ServicoId { get; set; }
        public servico? Servico { get; set; }
    }
}
