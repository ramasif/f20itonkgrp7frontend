using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using f20itonkgrp7frontend.ASPNETCore.MicroService.ClassLib.Models;

namespace f20itonkgrp7frontend.Data
{
    public class f20itonkgrp7frontendContext : DbContext
    {
        public f20itonkgrp7frontendContext (DbContextOptions<f20itonkgrp7frontendContext> options)
            : base(options)
        {
        }

        public DbSet<f20itonkgrp7frontend.ASPNETCore.MicroService.ClassLib.Models.Haandvaerker> Haandvaerker { get; set; }
    }
}
