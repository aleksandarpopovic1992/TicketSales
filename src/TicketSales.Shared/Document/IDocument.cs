using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketSales.Shared.Projection;

namespace TicketSales.Shared.Document
{
	public interface IDocument<T> where T: IProjection
	{
		void Add(T item);
	}
}
