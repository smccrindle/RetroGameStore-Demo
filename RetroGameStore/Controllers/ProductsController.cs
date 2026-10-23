
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using RetroGameStore.Data;
using RetroGameStore.Models;
using Microsoft.AspNetCore.Authorization;

[Authorize]
public class ProductsController : Controller
{
    private readonly ApplicationDbContext _context;

    public ProductsController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: PRODUCTS
    public async Task<IActionResult> Index()    
    {
        return View(await _context.Products.OrderBy(p => p.Name).ToListAsync());
    }

    // GET: PRODUCTS/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var product = await _context.Products
            .FirstOrDefaultAsync(m => m.Id == id);
        if (product == null)
        {
            return NotFound();
        }

        return View(product);
    }

    // GET: PRODUCTS/Create
    public IActionResult Create()
    {
        // We need the below line to populate the CategoryId dropdown
        ViewData["CategoryId"] = new SelectList(_context.Categories.OrderBy(c => c.Name), "Id", "Name");
        return View();
    }

    // POST: PRODUCTS/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Name,Price,CategoryId,Description")] Product product, IFormFile Photo)
    {
        if (ModelState.IsValid)
        {
            // Check for and process photo upload
            if (Photo.Length > 0)
            {
                // Get temp location of uploaded file
                var tempFile = Path.GetTempFileName();
                // Create unique file name using the Globally Unique Id (GUID) class
                var fileName = Guid.NewGuid() + "." + Photo.FileName;
                // Set dynamic destination path and file name
                var uploadPath = System.IO.Directory.GetCurrentDirectory() + "\\wwwroot\\img\\product-uploads\\" + fileName;
                // Use a stream to create a new file
                using var stream = new FileStream(uploadPath, FileMode.Create);
                await Photo.CopyToAsync(stream);
                // Add unique file name as the photo property of the new product object
                product.Photo = fileName;
            }
            _context.Add(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        // Generate a category list for the dropdown
        ViewData["CategoryId"] = new SelectList(_context.Categories.OrderBy(c => c.Name), "Id", "Name");
        return View(product);
    }

    // GET: PRODUCTS/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var product = await _context.Products.FindAsync(id);
        if (product == null)
        {
            return NotFound();
        }
        // We need the below line to populate the CategoryId dropdown
        ViewData["CategoryId"] = new SelectList(_context.Categories.OrderBy(c => c.Name), "Id", "Name");
        return View(product);
    }

    // POST: PRODUCTS/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, [Bind("Id,Name,Price,CategoryId,Description")] Product product, IFormFile Photo)
    {
        if (id != product.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            // Check for and process photo upload
            if (Photo.Length > 0)
            {
                // Get temp location of uploaded file
                var tempFile = Path.GetTempFileName();
                // Create unique file name using the Globally Unique Id (GUID) class
                var fileName = Guid.NewGuid() + "." + Photo.FileName;
                // Set dynamic destination path and file name
                var uploadPath = System.IO.Directory.GetCurrentDirectory() + "\\wwwroot\\img\\product-uploads\\" + fileName;
                // Use a stream to create a new file
                using var stream = new FileStream(uploadPath, FileMode.Create);
                await Photo.CopyToAsync(stream);
                // Add unique file name as the photo property of the new product object
                product.Photo = fileName;
            }
            try
            {
                _context.Update(product);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(product.Id))
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
        // We need the below line to populate the CategoryId dropdown
        ViewData["CategoryId"] = new SelectList(_context.Categories.OrderBy(c => c.Name), "Id", "Name");
        return View(product);
    }

    // GET: PRODUCTS/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var product = await _context.Products
            .FirstOrDefaultAsync(m => m.Id == id);
        if (product == null)
        {
            return NotFound();
        }

        return View(product);
    }

    // POST: PRODUCTS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product != null)
        {
            _context.Products.Remove(product);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool ProductExists(int? id)
    {
        return _context.Products.Any(e => e.Id == id);
    }
}
