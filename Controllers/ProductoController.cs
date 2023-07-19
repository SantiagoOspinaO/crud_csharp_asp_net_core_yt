using Microsoft.AspNetCore.Mvc;
using RepositorioDTO.DTO;
using RepositorioDTO.Models;
using RepositorioDTO.Repository;

namespace RepositorioDTO.Controllers
{
  public class ProductoController : Controller
  {
    private readonly ProductoRepository _repository;

    public ProductoController(ProductoRepository repository)
    {
      _repository = repository;
    }

    public IActionResult Index()
    {
      var productos = _repository.ObtenerProductos();
      var datos = productos.Select(prod => new ProductoDTO
      {
        Id = prod.Id,
        Nombre = prod.Nombre,
        Precio = prod.Precio,
      }).ToList();
      return View(datos);
    }

    public IActionResult Crear()
    {
      return View();
    }

    [HttpPost]
    public IActionResult Crear(ProductoDTO dto)
    {
      if (ModelState.IsValid)
      {
        var producto = new Producto
        {
          Nombre = dto.Nombre,
          Precio = dto.Precio,
        };

        _repository.CrearProducto(producto);
        return RedirectToAction("Index");
      }
      return View(dto);
    }

    public IActionResult Editar(int id)
    {
      var producto = _repository.ObtenerProductos().FirstOrDefault(prod => prod.Id == id);
      if (producto == null)
        return NotFound();

      var productodto = new ProductoDTO
      {
        Id = producto.Id,
        Nombre = producto.Nombre,
        Precio = producto.Precio,
      };

      return View(productodto);
    }

    [HttpPost]
    public IActionResult Editar(ProductoDTO dto)
    {
      if (ModelState.IsValid)
      {
        var producto = new Producto
        {
          Id = dto.Id,
          Nombre = dto.Nombre,
          Precio = dto.Precio,
        };

        _repository.ActualizarProducto(producto);
        return RedirectToAction("Index");
      }
      return View(dto);
    }

    public IActionResult Eliminar(int id)
    {
      var producto = _repository.ObtenerProductos().FirstOrDefault(prod => prod.Id == id);
      if (producto == null)
        return NotFound();

      var productodto = new ProductoDTO
      {
        Id = producto.Id,
        Nombre = producto.Nombre,
        Precio = producto.Precio,
      };

      return View(productodto);
    }

    [HttpPost, ActionName("Eliminar")]
    public IActionResult EliminarConfirmado(int id)
    {
      _repository.EliminarProducto(id);
      return RedirectToAction("Index");
    }
  }
}