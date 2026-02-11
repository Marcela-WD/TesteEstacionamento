using System.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Estacionamento.Data;
using Estacionamento.Models;
using System.ComponentModel.Design;
using Microsoft.VisualBasic;
using NuGet.Common;

namespace Estacionamento.Controllers
{
    public class VeiculosController : Controller
    {
        private readonly AppDbContext _context;

        public VeiculosController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Veiculos
        public async Task<IActionResult> Index()
        {
            return View(await _context.VEICULO.ToListAsync());
        }

        // GET: Veiculos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var veiculo = await _context.VEICULO
                .FirstOrDefaultAsync(m => m.ID == id);
            if (veiculo == null)
            {
                return NotFound();
            }

            return View(veiculo);
        }

        // GET: Veiculos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Veiculos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,PLACA,HORAENT,HORASAI")] Veiculo veiculo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(veiculo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(veiculo);
        }

        // GET: Veiculos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var veiculo = await _context.VEICULO.FindAsync(id);
            if (veiculo == null)
            {
                return NotFound();
            }
            return View(veiculo);
        }

        // POST: Veiculos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,PLACA,HORAENT,HORASAI")] Veiculo veiculo)
        {
            if (id != veiculo.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(veiculo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VeiculoExists(veiculo.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(veiculo);
        }

        // GET: Veiculos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var veiculo = await _context.VEICULO
                .FirstOrDefaultAsync(m => m.ID == id);
            if (veiculo == null)
            {
                return NotFound();
            }

            return View(veiculo);
        }

        // POST: Veiculos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var veiculo = await _context.VEICULO.FindAsync(id);
            if (veiculo != null)
            {
                _context.VEICULO.Remove(veiculo);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VeiculoExists(int id)
        {
            return _context.VEICULO.Any(e => e.ID == id);
        }

        //GET: Tela de registor
        public IActionResult Saida(int id)
        {
            var veiculo = _context.VEICULO.Find(id);

            if (veiculo == null)
              return NotFound();

            return View(veiculo);  
        }

        //POST: Regista saída e calcula tarifa
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Saida(int id, Veiculo veiculoModel)
        {
            var veiculo = _context.VEICULO.Find(id);
            if (veiculo == null)
              return NotFound();

            veiculo.HORASAI = DateTime.Now;

            var tarifaAtual = _context.TARIFA
                .Where(t => t.DATAINIVIG <= DateTime.Today && t.DATAFIMVIG >= DateTime.Today)
                .OrderByDescending(t => t.DATAINIVIG)  
                .FirstOrDefault();

            if (tarifaAtual != null)
            {
                var horas = (veiculo.HORASAI.Value - veiculo.HORAENT).TotalHours;
                horas = Math.Ceiling(horas);
                decimal valor = 0;

                if (horas <= 0.5)
                  valor = tarifaAtual.VALHORAINI / 2;  
                else
                if (horas <= 1)
                  valor = tarifaAtual.VALHORAINI;
                else
                  valor = tarifaAtual.VALHORAINI + (decimal)(horas - 1) * tarifaAtual.VALHORAADI;  

                ViewBag.ValorTotal = valor;
                  
            }   
            else
            {
                ViewBag.ValoTotal = 0;
            } 

            _context.SaveChanges();
            return View("Recibo", veiculo);

        }
    }
}
