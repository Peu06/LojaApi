using System.ComponentModel.DataAnnotations;
namespace LojaApi.Models;
public class Produto
{
 public int Id { get; set; }
 [Required(ErrorMessage = "O nome é obrigatório.")]
 [StringLength(100, MinimumLength = 3,
 ErrorMessage = "O nome deve ter entre 3 e 100 caracteres.")]
 public string Nome { get; set; } = string.Empty;
 [Range(0.01, 1_000_000, ErrorMessage = "O preço deve ser maior que zero.")]
 public decimal Preco { get; set; }
}