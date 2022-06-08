namespace AppMinhaBahia.Models
{
    public class Funcionario : Usuario
    {
        public int SetorId { get; set; }
        public Setor Setor { get; set; }
        public bool Disponivel { get; set; }
        public int? IntervencaoId { get; set; }
        public Intervencao Intervencao { get; set; }

        public Funcionario()
        {
            this.Disponivel = true;
        }
    }
}
