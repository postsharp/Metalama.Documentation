using System;
using System.Linq;
using System.Collections.Generic;
namespace Doc.NotNull
{
    internal class Invoice
    {
        private List<InvoiceLine> _lines = new();
        private string _invoiceId;
        [NormalizeId]
        public string InvoiceId
        {
            get
            {
                return this._invoiceId;
            }
            set
            {
                value = value?.Trim().ToUpper();
                this._invoiceId = value;
            }
        }
        public InvoiceLine GetLine( [NormalizeId] string id )
        {
            id = id?.Trim()?.ToUpper();
            return _lines.Single( x => x.LineId == id );
        }
    }
    internal class InvoiceLine
    {
        private string _lineId;
        [NormalizeId]
        public string LineId
        {
            get
            {
                return this._lineId;
            }
            set
            {
                value = value?.Trim()?.ToUpper();
                this._lineId = value;
            }
        }
    }
}