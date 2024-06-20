namespace Ofertas.Domain.Entidades
{
    public class Oferta
    {
        public Guid Id { get; set; } = new Guid();
        public string Titulo { get; set; }
        public string? Descricao { get; set; }
        public decimal Preco { get; set; }
        public DateTime DataCriacao { get; set; }
    }
}
