using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using f20itonkgrp7frontend.ASPNETCore.MicroService.ClassLib.Models;
using f20itonkgrp7frontend.Data;
using System.Net.Http;
using System.Text.Json;
using Newtonsoft.Json;

namespace f20itonkgrp7frontend.Controllers
{
    public class HaandvaerkersController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly HttpClient _httpClient;

        public HaandvaerkersController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
            _httpClient = _httpClientFactory.CreateClient("backend");
        }

        // GET: HaandvaerkersApp
        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync(
                "api/haandvaerkers");
            response.EnsureSuccessStatusCode();
            using var responseStream = await response.Content.ReadAsStreamAsync();

            return View(JsonConvert.DeserializeObject<IEnumerable<Haandvaerker>>(responseStream.ToString()));
        }

        // GET: HaandvaerkersApp/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var response = await _httpClient.GetAsync("api/haandvaerkers/" + id);
            response.EnsureSuccessStatusCode();

            using var responseStream = await response.Content.ReadAsStreamAsync();

            var haandvaerker = JsonConvert.DeserializeObject<Haandvaerker>(responseStream.ToString());

            if (haandvaerker == null)
            {
                return NotFound();
            }

            return View(haandvaerker);
        }

        // GET: HaandvaerkersApp/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: HaandvaerkersApp/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("HaandvaerkerId,HVAnsaettelsedato,HVEfternavn,HVFagomraade,HVFornavn")] Haandvaerker haandvaerker)
        {
            if (ModelState.IsValid)
            {
                var data = new StringContent(JsonConvert.SerializeObject(haandvaerker, Formatting.Indented));
                ////var data = new StringContent(JsonSerializer.Serialize(haandvaerker));
                var response = await _httpClient.PostAsync("api/haandvaerkers", data);
                response.EnsureSuccessStatusCode();
                //_context.Add(haandvaerker);
                //await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(haandvaerker);
        }

        // GET: HaandvaerkersApp/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var haandvaerker = await _context.Haandvaerker.FindAsync(id);
            var response = await _httpClient.GetAsync("api/haandvaerkers/" + id);
            response.EnsureSuccessStatusCode();
            using var responseStream = await response.Content.ReadAsStreamAsync();
            var haandvaerker = JsonConvert.DeserializeObject<Haandvaerker>(responseStream.ToString());
            //var haandvaerker = await JsonSerializer.DeserializeAsync<Haandvaerker>(responseStream);
            if (haandvaerker == null)
            {
                return NotFound();
            }
            return View(haandvaerker);
        }

        // POST: HaandvaerkersApp/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("HaandvaerkerId,HVAnsaettelsedato,HVEfternavn,HVFagomraade,HVFornavn")] Haandvaerker haandvaerker)
        {
            if (id != haandvaerker.HaandvaerkerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var data = new StringContent(JsonConvert.SerializeObject(haandvaerker, Formatting.Indented));
                //var data = new StringContent(JsonSerializer.Serialize(haandvaerker));
                var response = await _httpClient.PutAsync("api/haandvaerkers/" + id, data);
                using var responseStream = await response.Content.ReadAsStreamAsync();
                haandvaerker = JsonConvert.DeserializeObject<Haandvaerker>(responseStream.ToString());
                //haandvaerker = await JsonSerializer.DeserializeAsync<Haandvaerker>(responseStream);
                response.EnsureSuccessStatusCode();
                return RedirectToAction(nameof(Index));
            }
            return View(haandvaerker);
        }

        // GET: HaandvaerkersApp/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var response = await _httpClient.GetAsync(
          "api/haandvaerker/" + id);

            response.EnsureSuccessStatusCode();
            using var responseStream = await response.Content.ReadAsStreamAsync();
            var haandvaerker = JsonConvert.DeserializeObject<Haandvaerker>(responseStream.ToString());
            //var haandvaerker = await JsonSerializer.DeserializeAsync<Haandvaerker>(responseStream);

            //var haandvaerker = await _context.Haandvaerker
            //    .FirstOrDefaultAsync(m => m.HaandvaerkerId == id);

            if (haandvaerker == null)
            {
                return NotFound();
            }

            return View(haandvaerker);
        }

        // POST: HaandvaerkersApp/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            //var haandvaerker = await _context.Haandvaerker.FindAsync(id);
            //_context.Haandvaerker.Remove(haandvaerker);
            //await _context.SaveChangesAsync();
            var response = await _httpClient.DeleteAsync("api/haandvaerkers/" + id);
            response.EnsureSuccessStatusCode();

            return RedirectToAction(nameof(Index));
        }
    }
}
