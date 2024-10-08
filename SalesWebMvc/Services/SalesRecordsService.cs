﻿using Microsoft.EntityFrameworkCore;
using SalesWebMvc.Data;
using SalesWebMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWebMvc.Services
{
	public class SalesRecordsService
	{
		private readonly SalesWebMvcContext _salesContext;

		public SalesRecordsService(SalesWebMvcContext salesContext)
		{
			_salesContext = salesContext;
		}

		public async Task<List<SalesRecord>> FindByDateAsync(DateTime? firstDate, DateTime? lastDate)
		{
			return await _salesContext
								.SalesRecord
								.Include(x => x.Seller)
								.Include(x => x.Seller.Department)
								.Where(sale => sale.Date >= firstDate && sale.Date <= lastDate)
								.OrderByDescending(sale => sale.Date)
								.ToListAsync();
		}

		public async Task<List<IGrouping<Department, SalesRecord>>> FindByDateGroupingAsync(DateTime? firstDate, DateTime? lastDate)
		{
			return await _salesContext
				.SalesRecord
				.Include(x => x.Seller)
				.Include(x => x.Seller.Department)
				.Where(sale => sale.Date >= firstDate && sale.Date <= lastDate)
				.OrderByDescending(sale => sale.Date)
				.GroupBy(x => x.Seller.Department)
				.ToListAsync();

		}
	}
}
