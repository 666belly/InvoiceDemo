using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VT24A6
{
    public class Invoices : MainWindow
    {
        // Define a list to store invoice tuples
        private List<(string Number, DateTime Date, DateTime DueDate)> invoiceList;

        // Constructor
        public Invoices()
        {
            invoiceList = new List<(string Number, DateTime Date, DateTime DueDate)>();
        }

        // Method to add a new invoice tuple to the list
        public void AddInvoice(string number, DateTime date, DateTime dueDate)
        {
            invoiceList.Add((number, date, dueDate));
        }

        // Method to retrieve all invoices
        public List<(string Number, DateTime Date, DateTime DueDate)> GetAllInvoices()
        {
            return invoiceList;
        }
    }
}